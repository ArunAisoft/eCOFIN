using eCOFIN.Application.DTOs.Vouchers;
using eCOFIN.Application.Interfaces.Vouchers;
using eCOFIN.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Data;

namespace eCOFIN.Infrastructure.Services.Vouchers
{
    public class DebitNotesService : IDebitNotesService
    {
        private readonly BilzFinDbContext _context;
        private readonly ILogger<DebitNotesService> _logger;
        private readonly ILedgerRecalculationService _ledgerRecalculationService;

        public DebitNotesService(BilzFinDbContext context, ILogger<DebitNotesService> logger, ILedgerRecalculationService ledgerRecalculationService)
        {
            _context = context;
            _logger = logger;
            _ledgerRecalculationService = ledgerRecalculationService;
        }

        public async Task<IEnumerable<ExistingDebitNoteDto>> GetAllDebitNotesAsync(string accPeriod)
        {
            try
            {
                var q = from cn in _context.CfnDebitnotes.AsNoTracking()
                        join d in _context.CfnDebndetails.AsNoTracking()
                            on cn.CtrlOnholdno equals d.CtrlOnholdno
                        join sub in _context.CfnVSubcodeslinks.AsNoTracking()
                            on new { Account = cn.Accountcode, Sub = cn.Subaccountcode }
                            equals new { Account = sub.Accountcode, Sub = sub.Subcode }
                        where d.Dbcrflag == "D"
                           && cn.CtrlAccperiod == accPeriod
                        group new { cn, d, sub } by new
                        {
                            cn.CtrlOnholdno,
                            cn.VchrNumber,
                            cn.VchrDate,
                            SubAccountCode = cn.Subaccountcode,
                            SubDescription = sub.Subcodedescription,
                            cn.VchrNarration
                        } into g
                        orderby g.Key.VchrNumber descending, g.Key.VchrDate descending
                        select new ExistingDebitNoteDto
                        {
                            CtrlOnHoldNo = g.Key.CtrlOnholdno,
                            VchrNumber = g.Key.VchrNumber ?? string.Empty,
                            VchrDate = g.Key.VchrDate,
                            TotalAmount = g.Sum(x => x.d.Dbcramount),
                            BankCode = g.Key.SubAccountCode ?? string.Empty,
                            Description = g.Key.SubDescription ?? string.Empty,
                            VchrNarration = g.Key.VchrNarration ?? string.Empty
                        };

                return await q.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Debit Notes.");
                throw new ApplicationException("Error retrieving Debit Notes: " + ex.Message, ex);
            }
        }

        public async Task<DebitNotesDto?> GetDebitNoteByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return null;
            try
            {
                return await _context.CfnDebitnotes.AsNoTracking()
                    .Where(x => x.CtrlOnholdno == onHoldNo)
                    .Select(x => new DebitNotesDto
                    {
                        CtrlOnHoldNo = x.CtrlOnholdno,
                        VoucherNumber = x.VchrNumber ?? string.Empty,
                        VoucherDate = x.VchrDate,
                        BankCode = x.Accountcode,
                        BankAccount = x.Subaccountcode,
                        CurrencyCode = x.Currencycode ?? null,
                        VoucherType = x.VchrType,
                        VoucherSysCategory = x.VchrSyscategory ?? null,
                        VoucherNarration = x.VchrNarration ?? string.Empty,
                        CreatedOn = x.CtrlCreatedon,
                        UpdatedOn = x.CtrlLastupdate,
                    })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Debit Note header for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Debit Note header for {onHoldNo}", ex);
            }
        }

        public async Task<List<DebnDetailsDto>> GetDebnDetailsByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return new List<DebnDetailsDto>();
            try
            {
                var details = await _context.CfnDebndetails.AsNoTracking()
                    .Where(d => d.CtrlOnholdno == onHoldNo)
                    .OrderBy(d => d.CtrlSequenceno)
                    .Select(d => new DebnDetailsDto
                    {
                        CtrlOnHoldNo = d.CtrlOnholdno,
                        CtrlSequenceNo = d.CtrlSequenceno,
                        DbCrFlag = d.Dbcrflag,
                        AccountCode = d.Accountcode,
                        SubAccountCode = d.Subaccountcode ?? string.Empty,
                        DrCrAmount = d.Dbcramount,
                        Instrument = string.Empty,
                        InstrumentNo = d.Referencenumber ?? string.Empty,
                        InstrumentDate = d.Referencedate,
                        LineParticulars = d.Lineparticulars ?? string.Empty,
                        Automated = d.Automated
                    })
                    .ToListAsync()
                    .ConfigureAwait(false);

                if (!details.Any())
                    return details;

                var costMap = await LoadCostCentresAsync(onHoldNo);
                foreach (var d in details.Where(x => x.CtrlSequenceNo.HasValue))
                {
                    if (costMap.TryGetValue(d.CtrlSequenceNo!.Value, out var centres))
                    {
                        d.CostCenterDetails = centres;
                    }
                }
                return details;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Debit Note details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Debit Note details for {onHoldNo}", ex);
            }
        }

        private async Task<Dictionary<decimal, List<VoucherCostCenterDetailDto>>> LoadCostCentresAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo))
                return new Dictionary<decimal, List<VoucherCostCenterDetailDto>>();

            try
            {
                var rows = await _context.CfnCostdetails.AsNoTracking().Where(c => c.CtrlOnholdno == onHoldNo).OrderBy(c => c.CtrlSequenceno).ThenBy(c => c.CostSequenceno).ToListAsync().ConfigureAwait(false);

                return rows
                    .Where(c => c.CtrlSequenceno > 0)
                    .GroupBy(c => c.CtrlSequenceno)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(c => new VoucherCostCenterDetailDto
                        {
                            CostCenter = c.Costcentrecode ?? string.Empty,
                            Amount = c.Voucheramount ?? 0m,
                            GroupAccount = c.Accountcode ?? string.Empty
                        }).ToList()
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Cost Center details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Cost Center details for {onHoldNo}", ex);
            }
        }

        public async Task<DebitNoteWithDetailsDto> GetDebitNoteWithDetailsAsync(string onHoldNo)
        {
            var result = new DebitNoteWithDetailsDto();
            if (string.IsNullOrWhiteSpace(onHoldNo)) return result;
            try
            {
                var header = await GetDebitNoteByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);
                var details = await GetDebnDetailsByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);

                result.Header = header ?? new DebitNotesDto();
                result.Details = details ?? new List<DebnDetailsDto>();

                var totalDebit = result.Details.Where(d => string.Equals(d.DbCrFlag, "D", StringComparison.OrdinalIgnoreCase)).Sum(d => d.DrCrAmount ?? 0m);

                result.Header.Balance ??= totalDebit;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Debit Note with details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Debit Note with details for {onHoldNo}", ex);
            }
        }

        private async Task<string> GenerateVoucherNoInTxAsync(string accPeriod, string voucherType, DateTime voucherDate, bool isOnHold)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    throw new ArgumentException("AccPeriod is required.", nameof(accPeriod));
                if (string.IsNullOrWhiteSpace(voucherType))
                    throw new ArgumentException("VoucherType is required.", nameof(voucherType));

                accPeriod = accPeriod.Trim();
                string yyMM = voucherDate.ToString("yyMM");
                string targetColumn = isOnHold ? "CURRENTONHOLDNO" : "CURRENTPOSTEDNO";

                var conn = _context.Database.GetDbConnection();
                var dbTx = _context.Database.CurrentTransaction?.GetDbTransaction();

                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                int? currentNo = null;
                string? prefixType = null;

                string updateSql = $@"UPDATE CFN_VCHRCONTROL WITH (ROWLOCK) SET {targetColumn} = ISNULL({targetColumn}, 0) + 1 OUTPUT inserted.{targetColumn}, inserted.PREFIXTYPE WHERE  VOUCHERTYPE = @voucherType AND ACCPERIOD  = @accPeriod;";

                await using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = updateSql;
                    cmd.Transaction = dbTx;
                    cmd.Parameters.Add(new SqlParameter("@voucherType", voucherType));
                    cmd.Parameters.Add(new SqlParameter("@accPeriod", accPeriod));

                    await using var reader = await cmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        currentNo = Convert.ToInt32(reader.GetValue(0));
                        prefixType = reader.IsDBNull(1) ? null : reader.GetString(1);
                    }
                }

                if (currentNo == null)
                {
                    const string insertSql = @"INSERT INTO CFN_VCHRCONTROL (VOUCHERGROUP, VOUCHERTYPE, ACCPERIOD, STVOUCHERPOSTED, ENVOUCHERPOSTED, STVOUCHERONHOLDSLNUM, ENVOUCHERONHOLDSLNUM, CURRENTONHOLDNO, CURRENTPOSTEDNO, PREFIXTYPE, POSTCOLUMNNAME, CTRL_TRGLOCATIONCODE) 
                        VALUES ('DEBT', @voucherType, @accPeriod, 0, 0, 0, 0, 0, 0, LEFT(@voucherType, 3), '', '');";

                    await using (var insertCmd = conn.CreateCommand())
                    {
                        insertCmd.CommandText = insertSql;
                        insertCmd.Transaction = dbTx;
                        insertCmd.Parameters.Add(new SqlParameter("@voucherType", voucherType));
                        insertCmd.Parameters.Add(new SqlParameter("@accPeriod", accPeriod));
                        await insertCmd.ExecuteNonQueryAsync();
                    }

                    await using (var retryCmd = conn.CreateCommand())
                    {
                        retryCmd.CommandText = updateSql;
                        retryCmd.Transaction = dbTx;
                        retryCmd.Parameters.Add(new SqlParameter("@voucherType", voucherType));
                        retryCmd.Parameters.Add(new SqlParameter("@accPeriod", accPeriod));

                        await using var retryReader = await retryCmd.ExecuteReaderAsync();
                        if (await retryReader.ReadAsync())
                        {
                            currentNo = Convert.ToInt32(retryReader.GetValue(0));
                            prefixType = retryReader.IsDBNull(1) ? null : retryReader.GetString(1);
                        }
                    }

                    if (currentNo == null)
                        throw new ApplicationException($"Voucher Control not configured for type '{voucherType}' / period '{accPeriod}'.");
                }

                string prefix = string.IsNullOrWhiteSpace(prefixType) ? voucherType : prefixType;
                return $"{yyMM}{prefix}{currentNo:D4}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating {Mode} number for {Period}", isOnHold ? "OnHold" : "Posted", accPeriod);
                throw new ApplicationException($"Error generating {(isOnHold ? "OnHold" : "Posted")} number for {accPeriod}", ex);
            }
        }

        public async Task<string> OnHoldDebitNoteAsync(DebitNotesRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request?.VoucherData);
            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var detailsDto = request.Details ?? new List<DebnDetailsDto>();

                if (string.IsNullOrWhiteSpace(request.VoucherData.CtrlOnHoldNo))
                {
                    request.VoucherData.CtrlOnHoldNo = await GenerateVoucherNoInTxAsync(
                        request.AccountingPeriod ?? throw new ApplicationException("Accounting Period is required."),
                        request.VoucherData.VoucherType ?? throw new ApplicationException("Voucher Type is required."),
                        request.VoucherData.VoucherDate ?? now,
                        isOnHold: true);
                }

                var onHoldNo = request.VoucherData.CtrlOnHoldNo.Trim();
                var header = await _context.CfnDebitnotes.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo) ?? new CfnDebitnote { CtrlOnholdno = onHoldNo, CtrlCreatedon = now };

                header.VchrDate = request.VoucherData.VoucherDate ?? now;
                header.VchrNarration = request.VoucherData.VoucherNarration;
                header.VchrType = request.VoucherData.VoucherType ?? string.Empty;
                header.VchrSyscategory = request.VoucherData.VoucherSysCategory;
                header.Accountcode = request.VoucherData.BankCode ?? string.Empty;
                header.Subaccountcode = request.VoucherData.BankAccount ?? string.Empty;
                header.CtrlStatus = "Hold";
                header.CtrlLocationcode = request.LocationCode;
                header.CtrlAccperiod = request.AccountingPeriod;
                header.CtrlUsername = request.Username;
                header.CtrlLastupdate = now;
                header.Currencycode = request.VoucherData.CurrencyCode;

                if (_context.Entry(header).State == EntityState.Detached)
                    _context.CfnDebitnotes.Add(header);

                var existingStatus = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo).Select(g => g.CtrlStatus).FirstOrDefaultAsync();
                if (existingStatus == "Post")
                    throw new ApplicationException($"Voucher {onHoldNo} is already posted. Posted vouchers cannot be modified.");

                if (existingStatus == "Hold")
                    await _ledgerRecalculationService.ReverseOnHoldAmountsAsync(onHoldNo);

                await _context.CfnDebndetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
                await _context.CfnBills.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
                await _context.CfnPayments.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
                await _context.CfnCostdetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
                await _context.CfnGldetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
                DetachVoucherEntries(onHoldNo);

                int seq = 1;
                detailsDto.ForEach(d =>
                {
                    d.CtrlOnHoldNo = onHoldNo;
                    if (!d.CtrlSequenceNo.HasValue || d.CtrlSequenceNo <= 0)
                        d.CtrlSequenceNo = seq++;
                });

                var debnDetails = detailsDto.Select(d =>
                {
                    bool isVendorLine = !string.IsNullOrWhiteSpace(d.SubAccountCode);
                    bool hasCostCentres = d.CostCenterDetails != null && d.CostCenterDetails.Any();
                    return new CfnDebndetail
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = (decimal)d.CtrlSequenceNo!.Value,
                        Dbcrflag = string.IsNullOrWhiteSpace(d.DbCrFlag) ? "C" : d.DbCrFlag,
                        Accountcode = d.AccountCode ?? string.Empty,
                        Referencenumber = string.IsNullOrWhiteSpace(d.InstrumentNo) ? null : d.InstrumentNo,
                        Referencedate = d.InstrumentDate,
                        Dbcramount = d.DrCrAmount ?? 0m,
                        Lineparticulars = string.IsNullOrWhiteSpace(d.LineParticulars) ? null : d.LineParticulars,
                        Automated = string.IsNullOrWhiteSpace(d.Automated) ? "N" : d.Automated,
                        Subaccountcode = isVendorLine ? d.SubAccountCode : "",
                        Costcentrecode = isVendorLine ? null : (hasCostCentres ? "N" : ""),
                        Costtype = isVendorLine ? null : "",
                        Productcode = isVendorLine ? null : "",
                        Expensetype = isVendorLine ? null : "",
                        Employeecode = isVendorLine ? null : "",
                        Segcode2 = null,
                        CtrlTrglocationcode = null
                    };
                }).ToList();

                if (debnDetails.Any())
                    await _context.CfnDebndetails.AddRangeAsync(debnDetails);

                var costCentreDetails = detailsDto
                    .Where(d => d.CostCenterDetails != null && d.CostCenterDetails.Any())
                    .SelectMany(d => d.CostCenterDetails.Select((c, idx) => new CfnCostdetail
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = d.CtrlSequenceNo!.Value,
                        Costcentrecode = c.CostCenter,
                        Voucheramount = c.Amount,
                        Accountcode = c.GroupAccount,
                        CostSequenceno = idx + 1,
                        GlSequenceno = d.CtrlSequenceNo!.Value,
                        CtrlStatus = "Hold"
                    })).ToList();

                if (costCentreDetails.Any())
                    await _context.CfnCostdetails.AddRangeAsync(costCentreDetails);

                seq = 1;
                foreach (var d in detailsDto.Where(x => !string.IsNullOrWhiteSpace(x.SubAccountCode)))
                {
                    var amt = d.DrCrAmount ?? 0m;
                    await _context.CfnPayments.AddAsync(new CfnPayment
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = d.CtrlSequenceNo > 0 ? d.CtrlSequenceNo.Value : seq++,
                        Accountcode = d.AccountCode,
                        Subaccountcode = d.SubAccountCode,
                        VchrDate = request.VoucherData.VoucherDate ?? now,
                        VchrType = request.VoucherData.VoucherType,
                        Paymentamount = amt,
                        Paymentamountbalance = amt,
                        Instrumentno = d.InstrumentNo,
                        Instrumentdate = d.InstrumentDate,
                        Dbcrflag = string.IsNullOrEmpty(d.DbCrFlag) ? "C" : d.DbCrFlag,
                        CtrlStatus = "Hold",
                        CtrlAccperiod = request.AccountingPeriod,
                        CtrlUsername = request.Username,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });
                }

                header.VchrTotalamount = debnDetails.Where(x => string.Equals(x.Dbcrflag, "C", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Dbcramount);

                int glSeq = 1;
                await _context.CfnGldetails.AddRangeAsync(
                    detailsDto.Select(d =>
                    {
                        bool isVendorLine = !string.IsNullOrWhiteSpace(d.SubAccountCode);
                        bool hasCostCentres = d.CostCenterDetails != null && d.CostCenterDetails.Any();

                        return new CfnGldetail
                        {
                            Accperiod = request.AccountingPeriod,
                            Accountcode = d.AccountCode,
                            CtrlOnholdno = onHoldNo,
                            CtrlSequenceno = glSeq++,
                            VchrDate = request.VoucherData.VoucherDate ?? now,
                            VchrNarration = request.VoucherData.VoucherNarration,
                            VchrType = request.VoucherData.VoucherType,
                            VchrSyscategory = request.VoucherData.VoucherSysCategory,
                            Instrumentno = d.InstrumentNo,
                            Instrumentdate = d.InstrumentDate,
                            Linedetails = d.LineParticulars,
                            VchrTotalamount = 0m,
                            Dbcrflag = string.IsNullOrEmpty(d.DbCrFlag) ? "D" : d.DbCrFlag[0].ToString(),
                            Voucheramount = d.DrCrAmount ?? 0m,
                            CtrlStatus = "Hold",
                            CtrlUsername = request.Username,
                            CtrlCreatedon = now,
                            CtrlLastupdate = now,
                            Subaccountcode = isVendorLine ? d.SubAccountCode : "",
                            Costcentrecode = isVendorLine ? null : (hasCostCentres ? "N" : ""),
                            Costtype = isVendorLine ? null : "",
                            Productcode = isVendorLine ? null : "",
                            Expensetype = isVendorLine ? null : "",
                            Employeecode = isVendorLine ? null : ""
                        };
                    }));
                await _context.SaveChangesAsync();

                var lines = detailsDto.Select(d => new VoucherLineDto
                {
                    AccountCode = d.AccountCode,
                    SubAccountCode = d.SubAccountCode,
                    DbCrFlag = d.DbCrFlag,
                    DrCrAmount = d.DrCrAmount
                }).ToList();

                await _ledgerRecalculationService.RecalculateLedgersAsync(request.AccountingPeriod, lines, "ONHOLD");
                await _context.SaveChangesAsync();

                await tx.CommitAsync();
                return onHoldNo;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error saving Debit Note (OnHold)");
                throw new ApplicationException("Error saving Debit Note (OnHold): " + ex.Message, ex);
            }
        }

        public async Task<string> PostDebitNoteAsync(DebitNotesRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request?.VoucherData);
            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var detailsDto = request.Details ?? new List<DebnDetailsDto>();
                if (string.IsNullOrWhiteSpace(request.VoucherData.CtrlOnHoldNo))
                {
                    request.VoucherData.CtrlOnHoldNo = await GenerateVoucherNoInTxAsync(
                        request.AccountingPeriod ?? throw new ApplicationException("Accounting Period is required."),
                        request.VoucherData.VoucherType ?? throw new ApplicationException("Voucher Type is required."),
                        request.VoucherData.VoucherDate ?? now,
                        isOnHold: true);
                }

                if (string.IsNullOrWhiteSpace(request.VoucherData.VoucherNumber))
                {
                    request.VoucherData.VoucherNumber = await GenerateVoucherNoInTxAsync(
                        request.AccountingPeriod ?? throw new ApplicationException("Accounting Period is required."),
                        request.VoucherData.VoucherType ?? throw new ApplicationException("Voucher Type is required."),
                        request.VoucherData.VoucherDate ?? now,
                        isOnHold: false);
                }

                var onHoldNo = request.VoucherData.CtrlOnHoldNo.Trim();
                var voucherNo = request.VoucherData.VoucherNumber.Trim();
                var header = await _context.CfnDebitnotes.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo) ?? new CfnDebitnote { CtrlOnholdno = onHoldNo, CtrlCreatedon = now };

                header.VchrNumber = string.IsNullOrWhiteSpace(voucherNo) ? null : voucherNo;
                header.VchrDate = request.VoucherData.VoucherDate ?? now;
                header.VchrNarration = request.VoucherData.VoucherNarration;
                header.VchrType = request.VoucherData.VoucherType ?? string.Empty;
                header.VchrSyscategory = request.VoucherData.VoucherSysCategory;
                header.Accountcode = request.VoucherData.BankCode ?? string.Empty;
                header.Subaccountcode = request.VoucherData.BankAccount ?? string.Empty;
                header.CtrlStatus = "Post";
                header.CtrlLocationcode = request.LocationCode;
                header.CtrlAccperiod = request.AccountingPeriod;
                header.CtrlUsername = request.Username;
                header.CtrlLastupdate = now;
                header.Currencycode = request.VoucherData.CurrencyCode;

                if (_context.Entry(header).State == EntityState.Detached)
                    _context.CfnDebitnotes.Add(header);

                var existingStatus = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo).Select(g => g.CtrlStatus).FirstOrDefaultAsync();
                if (existingStatus == "Post")
                    throw new ApplicationException($"Voucher {onHoldNo} is already posted. Posted vouchers cannot be re-posted.");

                if (existingStatus == "Hold")
                    await _ledgerRecalculationService.ReverseOnHoldAmountsAsync(onHoldNo);

                await _context.CfnDebndetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
                await _context.CfnBills.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
                await _context.CfnPayments.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
                await _context.CfnCostdetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
                await _context.CfnGldetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
                DetachVoucherEntries(onHoldNo);

                int seq = 1;
                detailsDto.ForEach(d =>
                {
                    d.CtrlOnHoldNo = onHoldNo;
                    if (!d.CtrlSequenceNo.HasValue || d.CtrlSequenceNo <= 0)
                        d.CtrlSequenceNo = seq++;
                });

                var debnDetails = detailsDto.Select(d =>
                {
                    bool isVendorLine = !string.IsNullOrWhiteSpace(d.SubAccountCode);
                    bool hasCostCentres = d.CostCenterDetails != null && d.CostCenterDetails.Any();
                    return new CfnDebndetail
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = (decimal)d.CtrlSequenceNo!.Value,
                        Dbcrflag = string.IsNullOrWhiteSpace(d.DbCrFlag) ? "C" : d.DbCrFlag,
                        Accountcode = d.AccountCode ?? string.Empty,
                        Referencenumber = string.IsNullOrWhiteSpace(d.InstrumentNo) ? null : d.InstrumentNo,
                        Referencedate = d.InstrumentDate,
                        Dbcramount = d.DrCrAmount ?? 0m,
                        Lineparticulars = string.IsNullOrWhiteSpace(d.LineParticulars) ? null : d.LineParticulars,
                        Automated = string.IsNullOrWhiteSpace(d.Automated) ? "N" : d.Automated,
                        Subaccountcode = isVendorLine ? d.SubAccountCode : "",
                        Costcentrecode = isVendorLine ? null : (hasCostCentres ? "N" : ""),
                        Costtype = isVendorLine ? null : "",
                        Productcode = isVendorLine ? null : "",
                        Expensetype = isVendorLine ? null : "",
                        Employeecode = isVendorLine ? null : "",
                        Segcode2 = null,
                        CtrlTrglocationcode = null
                    };
                }).ToList();

                if (debnDetails.Any())
                    await _context.CfnDebndetails.AddRangeAsync(debnDetails);

                var costCentreDetails = detailsDto
                    .Where(d => d.CostCenterDetails != null && d.CostCenterDetails.Any())
                    .SelectMany(d => d.CostCenterDetails.Select((c, idx) => new CfnCostdetail
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = d.CtrlSequenceNo!.Value,
                        Costcentrecode = c.CostCenter,
                        Voucheramount = c.Amount,
                        Accountcode = c.GroupAccount,
                        CostSequenceno = idx + 1,
                        GlSequenceno = d.CtrlSequenceNo!.Value,
                        CtrlStatus = "Post"
                    })).ToList();

                if (costCentreDetails.Any())
                    await _context.CfnCostdetails.AddRangeAsync(costCentreDetails);

                seq = 1;
                foreach (var d in detailsDto.Where(x => !string.IsNullOrWhiteSpace(x.SubAccountCode)))
                {
                    var amt = d.DrCrAmount ?? 0m;
                    await _context.CfnPayments.AddAsync(new CfnPayment
                    {
                        CtrlOnholdno = onHoldNo,
                        VchrNumber = voucherNo,
                        CtrlSequenceno = d.CtrlSequenceNo > 0 ? d.CtrlSequenceNo.Value : seq++,
                        Accountcode = d.AccountCode,
                        Subaccountcode = d.SubAccountCode,
                        VchrDate = request.VoucherData.VoucherDate ?? now,
                        VchrType = request.VoucherData.VoucherType,
                        Paymentamount = amt,
                        Paymentamountbalance = amt,
                        Instrumentno = d.InstrumentNo,
                        Instrumentdate = d.InstrumentDate,
                        Dbcrflag = string.IsNullOrEmpty(d.DbCrFlag) ? "C" : d.DbCrFlag,
                        CtrlStatus = "Post",
                        CtrlAccperiod = request.AccountingPeriod,
                        CtrlUsername = request.Username,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });
                }

                header.VchrTotalamount = debnDetails.Where(x => string.Equals(x.Dbcrflag, "C", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Dbcramount);

                int glSeq = 1;
                await _context.CfnGldetails.AddRangeAsync(
                    detailsDto.Select(d =>
                    {
                        bool isVendorLine = !string.IsNullOrWhiteSpace(d.SubAccountCode);
                        bool hasCostCentres = d.CostCenterDetails != null && d.CostCenterDetails.Any();
                        return new CfnGldetail
                        {
                            Accperiod = request.AccountingPeriod,
                            Accountcode = d.AccountCode,
                            CtrlOnholdno = onHoldNo,
                            VchrNumber = voucherNo,
                            CtrlSequenceno = glSeq++,
                            VchrDate = request.VoucherData.VoucherDate ?? now,
                            VchrNarration = request.VoucherData.VoucherNarration,
                            VchrType = request.VoucherData.VoucherType,
                            VchrSyscategory = request.VoucherData.VoucherSysCategory,
                            Instrumentno = d.InstrumentNo,
                            Instrumentdate = d.InstrumentDate,
                            Linedetails = d.LineParticulars,
                            VchrTotalamount = 0m,
                            Dbcrflag = string.IsNullOrEmpty(d.DbCrFlag) ? "D" : d.DbCrFlag[0].ToString(),
                            Voucheramount = d.DrCrAmount ?? 0m,
                            CtrlStatus = "Post",
                            CtrlUsername = request.Username,
                            CtrlCreatedon = now,
                            CtrlLastupdate = now,
                            Subaccountcode = isVendorLine ? d.SubAccountCode : "",
                            Costcentrecode = isVendorLine ? null : (hasCostCentres ? "N" : ""),
                            Costtype = isVendorLine ? null : "",
                            Productcode = isVendorLine ? null : "",
                            Expensetype = isVendorLine ? null : "",
                            Employeecode = isVendorLine ? null : ""
                        };
                    }));
                await _context.SaveChangesAsync();

                var lines = detailsDto.Select(d => new VoucherLineDto
                {
                    AccountCode = d.AccountCode,
                    SubAccountCode = d.SubAccountCode,
                    DbCrFlag = d.DbCrFlag,
                    DrCrAmount = d.DrCrAmount
                }).ToList();

                if (existingStatus == "Hold" || existingStatus == null)
                {
                    await _ledgerRecalculationService.RecalculateLedgersAsync(request.AccountingPeriod, lines, "ONHOLD");
                    await _context.SaveChangesAsync();
                }

                await _ledgerRecalculationService.RecalculateLedgersAsync(request.AccountingPeriod, lines, "POST");
                await _context.SaveChangesAsync();

                await tx.CommitAsync();
                return voucherNo;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error saving Debit Note (Post)");
                throw new ApplicationException("Error saving Debit Note (Post): " + ex.Message, ex);
            }
        }

        public async Task<PostMultipleResult> PostMultipleDebitNotesAsync(List<string> onHoldNumbers, string accountingPeriod, string username, string locationCode)
        {
            if (onHoldNumbers == null || !onHoldNumbers.Any())
                throw new ApplicationException("No OnHold numbers provided.");

            var result = new PostMultipleResult();

            foreach (var onHoldNo in onHoldNumbers.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
                try
                {
                    var now = DateTime.UtcNow;
                    var header = await _context.CfnDebitnotes.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo);

                    if (header == null)
                    {
                        result.Failed.Add(new PostMultipleFailure { OnHoldNo = onHoldNo, Reason = "OnHold header not found in DB." });
                        await tx.RollbackAsync();
                        continue;
                    }

                    if (header.CtrlStatus == "Post")
                    {
                        _logger.LogWarning("OnHold {OnHoldNo} already posted — skipped.", onHoldNo);
                        result.Failed.Add(new PostMultipleFailure { OnHoldNo = onHoldNo, Reason = "Already posted." });
                        await tx.RollbackAsync();
                        continue;
                    }

                    var voucherType = header.VchrType;
                    var voucherDate = header.VchrDate;
                    var voucherAccPeriod = header.CtrlAccperiod ?? accountingPeriod;

                    if (string.IsNullOrWhiteSpace(voucherType))
                    {
                        result.Failed.Add(new PostMultipleFailure { OnHoldNo = onHoldNo, Reason = "VoucherType missing on OnHold header." });
                        await tx.RollbackAsync();
                        continue;
                    }

                    var voucherNo = await GenerateVoucherNoInTxAsync(accountingPeriod, voucherType, voucherDate, isOnHold: false);

                    header.VchrNumber = voucherNo;
                    header.CtrlStatus = "Post";
                    header.CtrlAccperiod = accountingPeriod;
                    header.CtrlUsername = username;
                    header.CtrlLocationcode = locationCode;
                    header.CtrlLastupdate = now;

                    var details = await _context.CfnDebndetails.Where(x => x.CtrlOnholdno == onHoldNo).ToListAsync();
                    var payments = await _context.CfnPayments.Where(x => x.CtrlOnholdno == onHoldNo).ToListAsync();
                    var costDetails = await _context.CfnCostdetails.Where(x => x.CtrlOnholdno == onHoldNo).ToListAsync();
                    var glDetails = await _context.CfnGldetails.Where(x => x.CtrlOnholdno == onHoldNo).ToListAsync();

                    foreach (var payment in payments)
                    {
                        payment.VchrNumber = voucherNo;
                        payment.CtrlStatus = "Post";
                        payment.CtrlLastupdate = now;
                    }

                    foreach (var cost in costDetails)
                        cost.CtrlStatus = "Post";

                    foreach (var gl in glDetails)
                    {
                        gl.VchrNumber = voucherNo;
                        gl.CtrlStatus = "Post";
                        gl.CtrlLastupdate = now;
                    }

                    await _context.SaveChangesAsync();

                    var lines = details.Select(d => new VoucherLineDto
                    {
                        AccountCode = d.Accountcode,
                        SubAccountCode = d.Subaccountcode,
                        DbCrFlag = d.Dbcrflag,
                        DrCrAmount = d.Dbcramount
                    }).ToList();

                    await _ledgerRecalculationService.RecalculateLedgersAsync(voucherAccPeriod, lines, "ONHOLD");
                    await _context.SaveChangesAsync();

                    await _ledgerRecalculationService.RecalculateLedgersAsync(voucherAccPeriod, lines, "POST");
                    await _context.SaveChangesAsync();

                    await tx.CommitAsync();

                    result.Posted.Add(voucherNo);
                    _logger.LogInformation("OnHold {OnHoldNo} posted successfully as {VoucherNo}.", onHoldNo, voucherNo);
                }
                catch (Exception ex)
                {
                    await tx.RollbackAsync();
                    _logger.LogError(ex, "Error posting OnHold {OnHoldNo} — skipped, continuing.", onHoldNo);
                    result.Failed.Add(new PostMultipleFailure { OnHoldNo = onHoldNo, Reason = ex.Message });
                }
                finally
                {
                    _context.ChangeTracker.Clear();
                }
            }

            return result;
        }

        private void DetachVoucherEntries(string onHoldNo)
        {
            var stale = _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Added && e.Entity switch
                {
                    CfnDebndetail x => x.CtrlOnholdno == onHoldNo,
                    CfnBill x => x.CtrlOnholdno == onHoldNo,
                    CfnPayment x => x.CtrlOnholdno == onHoldNo,
                    CfnCostdetail x => x.CtrlOnholdno == onHoldNo,
                    CfnGldetail x => x.CtrlOnholdno == onHoldNo,
                    _ => false
                })
                .ToList();

            foreach (var entry in stale)
                entry.State = EntityState.Detached;
        }
    }
}