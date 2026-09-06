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
    public class BankPaymentsService : IBankPaymentsService
    {
        private readonly BilzFinDbContext _context;
        private readonly ILogger<BankPaymentsService> _logger;
        private readonly ILedgerRecalculationService _ledgerRecalculationService;

        public BankPaymentsService(BilzFinDbContext context, ILogger<BankPaymentsService> logger, ILedgerRecalculationService ledgerRecalculationService)
        {
            _context = context;
            _logger = logger;
            _ledgerRecalculationService = ledgerRecalculationService;
        }

        public async Task<IEnumerable<ExistingBankPaymentDto>> GetAllBankPaymentsAsync(string accPeriod)
        {
            try
            {
                var q = from bp in _context.CfnBankpayments.AsNoTracking()
                        join d in _context.CfnBnkpdetails.AsNoTracking()
                            on bp.CtrlOnholdno equals d.CtrlOnholdno
                        join acc in _context.CfnAccounts.AsNoTracking()
                            on bp.Bankaccount equals acc.Accountcode
                        join v in _context.CfnVBnkVendors.AsNoTracking()
                            on bp.CtrlOnholdno equals v.CtrlOnholdno into vendorGroup
                        from v in vendorGroup.DefaultIfEmpty()
                        where bp.CtrlAccperiod == accPeriod
                           && d.Accountcode == bp.Bankaccount
                        group new { bp, d, acc, v } by new
                        {
                            bp.CtrlOnholdno,
                            bp.VchrNumber,
                            bp.VchrDate,
                            bp.Bankcode,
                            AccDescription = acc.Description,
                            bp.VchrNarration,
                            bp.CtrlCreatedon,
                            VendorName = v.Vendorname
                        } into g
                        orderby g.Key.CtrlCreatedon descending
                        select new ExistingBankPaymentDto
                        {
                            CtrlOnHoldNo = g.Key.CtrlOnholdno,
                            VchrNumber = g.Key.VchrNumber ?? string.Empty,
                            VchrDate = g.Key.VchrDate,
                            TotalAmount = g.Sum(x => x.d.Dbcramount),
                            BankCode = g.Key.Bankcode ?? string.Empty,
                            Description = g.Key.AccDescription ?? string.Empty,
                            VchrNarration = g.Key.VchrNarration ?? string.Empty,
                            VendorName = g.Key.VendorName ?? string.Empty
                        };

                return await q.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Bank Payments.");
                throw new ApplicationException("Error retrieving Bank Payments: " + ex.Message, ex);
            }
        }

        public async Task<BankPaymentsDto?> GetBankPaymentByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return null;
            try
            {
                return await _context.CfnBankpayments.AsNoTracking()
                    .Where(x => x.CtrlOnholdno == onHoldNo)
                    .Select(x => new BankPaymentsDto
                    {
                        CtrlOnHoldNo = x.CtrlOnholdno,
                        VoucherNumber = x.VchrNumber ?? string.Empty,
                        VoucherDate = x.VchrDate,
                        BankCode = x.Bankcode,
                        BankAccount = x.Bankaccount,
                        BankRate = x.BankRate,
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
                _logger.LogError(ex, "Error retrieving Bank Payment header for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Bank Payment header for {onHoldNo}", ex);
            }
        }

        public async Task<List<BankpDetailsDto>> GetBankpDetailsByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return new List<BankpDetailsDto>();
            try
            {
                var details = await _context.CfnBnkpdetails.AsNoTracking()
                    .Where(d => d.CtrlOnholdno == onHoldNo)
                    .OrderBy(d => d.CtrlSequenceno)
                    .Select(d => new BankpDetailsDto
                    {
                        CtrlOnHoldNo = d.CtrlOnholdno,
                        CtrlSequenceNo = d.CtrlSequenceno,
                        DbCrFlag = d.Dbcrflag,
                        AccountCode = d.Accountcode,
                        SubAccountCode = d.Subaccountcode ?? string.Empty,
                        DrCrAmount = d.Dbcramount,
                        Instrument = d.Instrument ?? string.Empty,
                        InstrumentNo = d.Instrumentno ?? string.Empty,
                        InstrumentDate = d.Instrumentdate,
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
                _logger.LogError(ex, "Error retrieving Bank Payment details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Bank Payment details for {onHoldNo}", ex);
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

        public async Task<BankPaymentWithDetailsDto> GetBankPaymentWithDetailsAsync(string onHoldNo)
        {
            var result = new BankPaymentWithDetailsDto();
            if (string.IsNullOrWhiteSpace(onHoldNo)) return result;
            try
            {
                var header = await GetBankPaymentByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);
                var details = await GetBankpDetailsByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);

                result.Header = header ?? new BankPaymentsDto();
                result.Details = details ?? new List<BankpDetailsDto>();

                var totalDebit = result.Details.Where(d => string.Equals(d.DbCrFlag, "D", StringComparison.OrdinalIgnoreCase)).Sum(d => d.DrCrAmount ?? 0m);

                result.Header.Balance ??= totalDebit;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Bank Payment with details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Bank Payment with details for {onHoldNo}", ex);
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
                        VALUES ('BNKP', @voucherType, @accPeriod, 0, 0, 0, 0, 0, 0, LEFT(@voucherType, 3), '', '');";

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

        public async Task<string> OnHoldBankPaymentAsync(BankPaymentsRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request?.VoucherData);
            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var detailsDto = request.Details ?? new List<BankpDetailsDto>();

                if (string.IsNullOrWhiteSpace(request.VoucherData.CtrlOnHoldNo))
                {
                    request.VoucherData.CtrlOnHoldNo = await GenerateVoucherNoInTxAsync(
                        request.AccountingPeriod ?? throw new ApplicationException("Accounting Period is required."),
                        request.VoucherData.VoucherType ?? throw new ApplicationException("Voucher Type is required."),
                        request.VoucherData.VoucherDate ?? now,
                        isOnHold: true);
                }

                var onHoldNo = request.VoucherData.CtrlOnHoldNo.Trim();
                var header = await _context.CfnBankpayments.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo) ?? new CfnBankpayment { CtrlOnholdno = onHoldNo, CtrlCreatedon = now };

                header.VchrDate = request.VoucherData.VoucherDate ?? now;
                header.VchrNarration = request.VoucherData.VoucherNarration;
                header.VchrType = request.VoucherData.VoucherType ?? string.Empty;
                header.VchrSyscategory = request.VoucherData.VoucherSysCategory;
                header.Bankcode = request.VoucherData.BankCode ?? string.Empty;
                header.Bankaccount = request.VoucherData.BankAccount;
                header.BankRate = request.VoucherData.BankRate;
                header.Currencycode = request.VoucherData.CurrencyCode;
                header.CtrlAccperiod = request.AccountingPeriod;
                header.CtrlStatus = "Hold";
                header.CtrlUsername = request.Username;
                header.CtrlLocationcode = request.LocationCode;
                header.CtrlLastupdate = now;

                if (_context.Entry(header).State == EntityState.Detached)
                    _context.CfnBankpayments.Add(header);

                var existingStatus = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo).Select(g => g.CtrlStatus).FirstOrDefaultAsync();
                if (existingStatus == "Post")
                    throw new ApplicationException($"Voucher {onHoldNo} is already posted. Posted vouchers cannot be modified.");

                if (existingStatus == "Hold")
                    await _ledgerRecalculationService.ReverseOnHoldAmountsAsync(onHoldNo);

                await _context.CfnBnkpdetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
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

                var bnkpDetails = detailsDto.Select(d =>
                {
                    bool isVendorLine = !string.IsNullOrWhiteSpace(d.SubAccountCode);
                    bool hasCostCentres = d.CostCenterDetails != null && d.CostCenterDetails.Any();
                    return new CfnBnkpdetail
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

                if (bnkpDetails.Any())
                    await _context.CfnBnkpdetails.AddRangeAsync(bnkpDetails);

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

                header.VchrTotalamount = bnkpDetails.Where(x => string.Equals(x.Dbcrflag, "D", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Dbcramount);

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
                _logger.LogError(ex, "Error saving Bank Payment (OnHold)");
                throw new ApplicationException("Error saving Bank Payment (OnHold): " + ex.Message, ex);
            }
        }

        public async Task<string> PostBankPaymentAsync(BankPaymentsRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request?.VoucherData);
            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var detailsDto = request.Details ?? new List<BankpDetailsDto>();
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
                var header = await _context.CfnBankpayments.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo) ?? new CfnBankpayment { CtrlOnholdno = onHoldNo, CtrlCreatedon = now };

                header.VchrNumber = string.IsNullOrWhiteSpace(voucherNo) ? null : voucherNo;
                header.VchrDate = request.VoucherData.VoucherDate ?? now;
                header.VchrNarration = request.VoucherData.VoucherNarration;
                header.VchrType = request.VoucherData.VoucherType ?? string.Empty;
                header.VchrSyscategory = request.VoucherData.VoucherSysCategory;
                header.Bankcode = request.VoucherData.BankCode ?? string.Empty;
                header.Bankaccount = request.VoucherData.BankAccount;
                header.BankRate = request.VoucherData.BankRate;
                header.Currencycode = request.VoucherData.CurrencyCode;
                header.CtrlStatus = "Post";
                header.CtrlAccperiod = request.AccountingPeriod;
                header.CtrlUsername = request.Username;
                header.CtrlLocationcode = request.LocationCode;
                header.CtrlLastupdate = now;

                if (_context.Entry(header).State == EntityState.Detached)
                    _context.CfnBankpayments.Add(header);

                var existingStatus = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo).Select(g => g.CtrlStatus).FirstOrDefaultAsync();
                if (existingStatus == "Post")
                    throw new ApplicationException($"Voucher {onHoldNo} is already posted. Posted vouchers cannot be re-posted.");

                if (existingStatus == "Hold")
                    await _ledgerRecalculationService.ReverseOnHoldAmountsAsync(onHoldNo);

                await _context.CfnBnkpdetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
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

                var bnkpDetails = detailsDto.Select(d => new CfnBnkpdetail
                {
                    CtrlOnholdno = onHoldNo,
                    CtrlSequenceno = d.CtrlSequenceNo!.Value,
                    Dbcrflag = string.IsNullOrWhiteSpace(d.DbCrFlag) ? "C" : d.DbCrFlag,
                    Accountcode = d.AccountCode ?? string.Empty,
                    Subaccountcode = d.SubAccountCode,
                    Instrument = d.Instrument,
                    Instrumentno = d.InstrumentNo,
                    Instrumentdate = d.InstrumentDate,
                    Dbcramount = d.DrCrAmount ?? 0m,
                    Lineparticulars = d.LineParticulars,
                    Automated = string.IsNullOrWhiteSpace(d.Automated) ? "N" : d.Automated
                }).ToList();

                if (bnkpDetails.Any())
                    await _context.CfnBnkpdetails.AddRangeAsync(bnkpDetails);

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

                header.VchrTotalamount = bnkpDetails.Where(x => string.Equals(x.Dbcrflag, "D", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Dbcramount);

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
                _logger.LogError(ex, "Error saving Bank Payment (Post)");
                throw new ApplicationException("Error saving Bank Payment (Post): " + ex.Message, ex);
            }
        }

        private void DetachVoucherEntries(string onHoldNo)
        {
            var stale = _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Added && e.Entity switch
                {
                    CfnBnkpdetail x => x.CtrlOnholdno == onHoldNo,
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