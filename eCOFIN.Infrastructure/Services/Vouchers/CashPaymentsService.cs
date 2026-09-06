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
    public class CashPaymentsService : ICashPaymentsService
    {
        private readonly BilzFinDbContext _context;
        private readonly ILogger<CashPaymentsService> _logger;
        private readonly ILedgerRecalculationService _ledgerRecalculationService;

        public CashPaymentsService(BilzFinDbContext context, ILogger<CashPaymentsService> logger, ILedgerRecalculationService ledgerRecalculationService)
        {
            _context = context;
            _logger = logger;
            _ledgerRecalculationService = ledgerRecalculationService;
        }

        public async Task<IEnumerable<ExistingCashPaymentDto>> GetAllCashPaymentsAsync(string accPeriod)
        {
            try
            {
                var q = from cp in _context.CfnCashpayments.AsNoTracking()
                        join d in _context.CfnCshpdetails.AsNoTracking()
                            on cp.CtrlOnholdno equals d.CtrlOnholdno
                        join acc in _context.CfnAccounts.AsNoTracking()
                            on cp.Cashaccount equals acc.Accountcode
                        where d.Dbcrflag == "D"
                           && cp.CtrlAccperiod == accPeriod
                        group new { cp, d, acc } by new
                        {
                            cp.CtrlOnholdno,
                            cp.VchrNumber,
                            cp.VchrDate,
                            cp.Cashaccount,
                            AccDescription = acc.Description,
                            cp.VchrNarration
                        } into g
                        orderby g.Key.VchrNumber descending, g.Key.VchrDate descending
                        select new ExistingCashPaymentDto
                        {
                            CtrlOnHoldNo = g.Key.CtrlOnholdno,
                            VchrNumber = g.Key.VchrNumber ?? string.Empty,
                            VchrDate = g.Key.VchrDate,
                            TotalAmount = g.Sum(x => x.d.Dbcramount),
                            BankAccount = g.Key.Cashaccount ?? string.Empty,
                            Description = g.Key.AccDescription ?? string.Empty,
                            VchrNarration = g.Key.VchrNarration ?? string.Empty
                        };

                return await q.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Cash Payments.");
                throw new ApplicationException("Error retrieving Cash Payments: " + ex.Message, ex);
            }
        }

        public async Task<CashPaymentsDto?> GetCashPaymentByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return null;
            try
            {
                return await _context.CfnCashpayments.AsNoTracking()
                    .Where(x => x.CtrlOnholdno == onHoldNo)
                    .Select(x => new CashPaymentsDto
                    {
                        CtrlOnHoldNo = x.CtrlOnholdno,
                        VoucherNumber = x.VchrNumber ?? string.Empty,
                        VoucherDate = x.VchrDate,
                        BankAccount = x.Cashaccount,
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
                _logger.LogError(ex, "Error retrieving Cash Payment header for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Cash Payment header for {onHoldNo}", ex);
            }
        }

        public async Task<List<CashpDetailsDto>> GetCashpDetailsByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return new List<CashpDetailsDto>();
            try
            {
                var details = await _context.CfnCshpdetails.AsNoTracking()
                    .Where(d => d.CtrlOnholdno == onHoldNo)
                    .OrderBy(d => d.CtrlSequenceno)
                    .Select(d => new CashpDetailsDto
                    {
                        CtrlOnHoldNo = d.CtrlOnholdno,
                        CtrlSequenceNo = d.CtrlSequenceno,
                        DbCrFlag = d.Dbcrflag,
                        AccountCode = d.Accountcode,
                        SubAccountCode = d.Subaccountcode ?? string.Empty,
                        DrCrAmount = d.Dbcramount,
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
                _logger.LogError(ex, "Error retrieving Cash Payment details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Cash Payment details for {onHoldNo}", ex);
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

        public async Task<CashPaymentWithDetailsDto> GetCashPaymentWithDetailsAsync(string onHoldNo)
        {
            var result = new CashPaymentWithDetailsDto();
            if (string.IsNullOrWhiteSpace(onHoldNo)) return result;
            try
            {
                var header = await GetCashPaymentByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);
                var details = await GetCashpDetailsByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);

                result.Header = header ?? new CashPaymentsDto();
                result.Details = details ?? new List<CashpDetailsDto>();

                var totalDebit = result.Details
                    .Where(d => string.Equals(d.DbCrFlag, "D", StringComparison.OrdinalIgnoreCase))
                    .Sum(d => d.DrCrAmount ?? 0m);

                result.Header.Balance ??= totalDebit;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Cash Payment with details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Cash Payment with details for {onHoldNo}", ex);
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

                string updateSql = $@"UPDATE CFN_VCHRCONTROL WITH (ROWLOCK) SET {targetColumn} = ISNULL({targetColumn}, 0) + 1 OUTPUT inserted.{targetColumn}, inserted.PREFIXTYPE WHERE VOUCHERTYPE = @voucherType AND ACCPERIOD = @accPeriod;";

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
                        VALUES ('CASP', @voucherType, @accPeriod, 0, 0, 0, 0, 0, 0, LEFT(@voucherType, 3), '', '');";

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

        public async Task<string> OnHoldCashPaymentAsync(CashPaymentsRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request?.VoucherData);
            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var detailsDto = request.Details ?? new List<CashpDetailsDto>();

                if (string.IsNullOrWhiteSpace(request.VoucherData.CtrlOnHoldNo))
                {
                    request.VoucherData.CtrlOnHoldNo = await GenerateVoucherNoInTxAsync(
                        request.AccountingPeriod ?? throw new ApplicationException("Accounting Period is required."),
                        request.VoucherData.VoucherType ?? throw new ApplicationException("Voucher Type is required."),
                        request.VoucherData.VoucherDate ?? now,
                        isOnHold: true);
                }

                var onHoldNo = request.VoucherData.CtrlOnHoldNo.Trim();
                var header = await _context.CfnCashpayments.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo) ?? new CfnCashpayment { CtrlOnholdno = onHoldNo, CtrlCreatedon = now };

                header.VchrDate = request.VoucherData.VoucherDate ?? now;
                header.VchrNarration = request.VoucherData.VoucherNarration;
                header.VchrType = request.VoucherData.VoucherType ?? string.Empty;
                header.VchrSyscategory = request.VoucherData.VoucherSysCategory;
                header.Cashaccount = request.VoucherData.BankAccount;
                header.CtrlAccperiod = request.AccountingPeriod;
                header.CtrlStatus = "Hold";
                header.CtrlUsername = request.Username;
                header.CtrlLocationcode = request.LocationCode;
                header.CtrlLastupdate = now;

                if (_context.Entry(header).State == EntityState.Detached)
                    _context.CfnCashpayments.Add(header);

                var existingStatus = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo).Select(g => g.CtrlStatus).FirstOrDefaultAsync();
                if (existingStatus == "Post")
                    throw new ApplicationException($"Voucher {onHoldNo} is already posted. Posted vouchers cannot be modified.");

                if (existingStatus == "Hold")
                    await _ledgerRecalculationService.ReverseOnHoldAmountsAsync(onHoldNo);

                await _context.CfnCshpdetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
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

                var cshpDetails = detailsDto.Select(d =>
                {
                    bool isVendorLine = !string.IsNullOrWhiteSpace(d.SubAccountCode);
                    bool hasCostCentres = d.CostCenterDetails != null && d.CostCenterDetails.Any();
                    return new CfnCshpdetail
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

                if (cshpDetails.Any())
                    await _context.CfnCshpdetails.AddRangeAsync(cshpDetails);

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

                foreach (var d in detailsDto)
                {
                    if (d.InvoiceDetails == null || !d.InvoiceDetails.Any()) continue;
                    foreach (var inv in d.InvoiceDetails.Where(x => x.AcceptedAmount > 0))
                    {
                        var bill = await _context.CfnBills.FirstOrDefaultAsync(b => b.Billno == inv.BillNo && b.Accountcode == inv.GroupAccount && b.Subaccountcode == inv.SubAccount && b.CtrlOnholdno == inv.OnHoldNo && b.CtrlSequenceno == inv.SequenceNo);
                        if (bill == null) continue;

                        bill.Billamountadjusted += inv.AcceptedAmount;
                        bill.Billbalance -= inv.AcceptedAmount;
                        bill.CtrlLastupdate = now;
                        _context.CfnBills.Update(bill);
                    }
                }

                seq = 1;
                foreach (var d in detailsDto.Where(x => !string.IsNullOrWhiteSpace(x.SubAccountCode)))
                {
                    var amt = d.DrCrAmount ?? 0m;
                    await _context.CfnBills.AddAsync(new CfnBill
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = d.CtrlSequenceNo > 0 ? d.CtrlSequenceNo.Value : seq++,
                        Accountcode = d.AccountCode,
                        Subaccountcode = d.SubAccountCode,
                        VchrDate = request.VoucherData.VoucherDate ?? now,
                        VchrType = request.VoucherData.VoucherType,
                        Billamount = amt,
                        Billbalance = amt,
                        VchrRefnumber = d.InstrumentNo,
                        VchrRefdate = d.InstrumentDate,
                        Dbcrflag = string.IsNullOrEmpty(d.DbCrFlag) ? "C" : d.DbCrFlag,
                        CtrlStatus = "Hold",
                        CtrlAccperiod = request.AccountingPeriod,
                        CtrlUsername = request.Username,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });
                }

                header.VchrTotalamount = cshpDetails.Where(x => string.Equals(x.Dbcrflag, "D", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Dbcramount);

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
                _logger.LogError(ex, "Error saving Cash Payment (OnHold)");
                throw new ApplicationException("Error saving Cash Payment (OnHold): " + ex.Message, ex);
            }
        }

        public async Task<string> PostCashPaymentAsync(CashPaymentsRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request?.VoucherData);
            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var detailsDto = request.Details ?? new List<CashpDetailsDto>();
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
                var header = await _context.CfnCashpayments.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo) ?? new CfnCashpayment { CtrlOnholdno = onHoldNo, CtrlCreatedon = now };

                header.VchrNumber = string.IsNullOrWhiteSpace(voucherNo) ? null : voucherNo;
                header.VchrDate = request.VoucherData.VoucherDate ?? now;
                header.VchrNarration = request.VoucherData.VoucherNarration;
                header.VchrType = request.VoucherData.VoucherType ?? string.Empty;
                header.VchrSyscategory = request.VoucherData.VoucherSysCategory;
                header.Cashaccount = request.VoucherData.BankAccount;
                header.CtrlAccperiod = request.AccountingPeriod;
                header.CtrlStatus = "Post";
                header.CtrlUsername = request.Username;
                header.CtrlLocationcode = request.LocationCode;
                header.CtrlLastupdate = now;

                if (_context.Entry(header).State == EntityState.Detached)
                    _context.CfnCashpayments.Add(header);

                var existingStatus = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo).Select(g => g.CtrlStatus).FirstOrDefaultAsync();
                if (existingStatus == "Post")
                    throw new ApplicationException($"Voucher {onHoldNo} is already posted. Posted vouchers cannot be modified.");

                if (existingStatus == "Hold")
                    await _ledgerRecalculationService.ReverseOnHoldAmountsAsync(onHoldNo);

                await _context.CfnCshpdetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
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

                var cshpDetails = detailsDto.Select(d =>
                {
                    bool isVendorLine = !string.IsNullOrWhiteSpace(d.SubAccountCode);
                    bool hasCostCentres = d.CostCenterDetails != null && d.CostCenterDetails.Any();
                    return new CfnCshpdetail
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

                if (cshpDetails.Any())
                    await _context.CfnCshpdetails.AddRangeAsync(cshpDetails);

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
                    await _context.CfnBills.AddAsync(new CfnBill
                    {
                        CtrlOnholdno = onHoldNo,
                        VchrNumber = voucherNo,
                        CtrlSequenceno = d.CtrlSequenceNo > 0 ? d.CtrlSequenceNo.Value : seq++,
                        Accountcode = d.AccountCode,
                        Subaccountcode = d.SubAccountCode,
                        VchrDate = request.VoucherData.VoucherDate ?? now,
                        VchrType = request.VoucherData.VoucherType,
                        Billamount = amt,
                        Billbalance = amt,
                        VchrRefnumber = d.InstrumentNo,
                        VchrRefdate = d.InstrumentDate,
                        Dbcrflag = string.IsNullOrEmpty(d.DbCrFlag) ? "C" : d.DbCrFlag,
                        CtrlStatus = "Post",
                        CtrlAccperiod = request.AccountingPeriod,
                        CtrlUsername = request.Username,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });
                }

                header.VchrTotalamount = cshpDetails.Where(x => string.Equals(x.Dbcrflag, "D", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Dbcramount);

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
                _logger.LogError(ex, "Error saving Cash Payment (Post)");
                throw new ApplicationException("Error saving Cash Payment (Post): " + ex.Message, ex);
            }
        }

        private void DetachVoucherEntries(string onHoldNo)
        {
            var stale = _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Added && e.Entity switch
                {
                    CfnCshpdetail x => x.CtrlOnholdno == onHoldNo,
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