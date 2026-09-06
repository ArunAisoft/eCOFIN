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
    public class SalesService : ISalesService
    {
        private readonly BilzFinDbContext _context;
        private readonly ILogger<SalesService> _logger;
        private readonly ILedgerRecalculationService _ledgerRecalculationService;

        public SalesService(BilzFinDbContext context, ILogger<SalesService> logger, ILedgerRecalculationService ledgerRecalculationService)
        {
            _context = context;
            _logger = logger;
            _ledgerRecalculationService = ledgerRecalculationService;
        }

        public async Task<IEnumerable<ExistingSaleDto>> GetAllSalesAsync(string accPeriod)
        {
            try
            {
                var q = from bill in _context.CfnBills.AsNoTracking()
                        join sale in _context.CfnSalevouchers.AsNoTracking()
                            on bill.CtrlOnholdno equals sale.CtrlOnholdno
                        join cust in _context.CfnCustomers.AsNoTracking()
                            on bill.Subaccountcode equals cust.Customercode
                        where sale.CtrlAccperiod == accPeriod
                        orderby sale.CtrlCreatedon descending
                        select new ExistingSaleDto
                        {
                            CtrlOnHoldNo = sale.CtrlOnholdno,
                            VchrNumber = sale.VchrNumber ?? string.Empty,
                            BillNumber = bill.Billno,
                            BillDate = bill.Billdate,
                            BillAmount = bill.Billamount ?? 0m,
                            BankCode = sale.Subaccountcode ?? string.Empty,
                            Description = cust.Customername ?? string.Empty,
                            VchrNarration = bill.VchrNarration ?? string.Empty
                        };

                return await q.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Sales.");
                throw new ApplicationException("Error retrieving Sales: " + ex.Message, ex);
            }
        }

        public async Task<SalesDto?> GetSaleByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return null;
            try
            {
                return await _context.CfnSalevouchers.AsNoTracking()
                    .Where(x => x.CtrlOnholdno == onHoldNo)
                    .Select(x => new SalesDto
                    {
                        CtrlOnHoldNo = x.CtrlOnholdno,
                        VoucherNumber = x.VchrNumber ?? string.Empty,
                        VoucherDate = x.VchrDate,
                        BankCode = x.Accountcode,
                        BankAccount = x.Subaccountcode,
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
                _logger.LogError(ex, "Error retrieving Sale header for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Sale header for {onHoldNo}", ex);
            }
        }

        public async Task<List<SalvDetailsDto>> GetSalvDetailsByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return new List<SalvDetailsDto>();
            try
            {
                var details = await _context.CfnSalvdetails.AsNoTracking()
                    .Where(d => d.CtrlOnholdno == onHoldNo)
                    .OrderBy(d => d.CtrlSequenceno)
                    .Select(d => new SalvDetailsDto
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
                _logger.LogError(ex, "Error retrieving Sale details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Sale details for {onHoldNo}", ex);
            }
        }

        public async Task<SaleBillDetails> GeSaleBillDetailsByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return new SaleBillDetails();
            try
            {
                var result = await _context.CfnBills.AsNoTracking()
                    .Where(d => d.CtrlOnholdno == onHoldNo)
                    .OrderBy(d => d.CtrlSequenceno)
                    .Select(d => new SaleBillDetails
                    {
                        BillNo = d.Billno,
                        BillDate = d.Billdate,
                        BillAmount = d.Billamount,
                    })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                return result ?? new SaleBillDetails();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Bill details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Bill details for {onHoldNo}", ex);
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

        public async Task<SaleWithDetailsDto> GetSaleWithDetailsAsync(string onHoldNo)
        {
            var result = new SaleWithDetailsDto();
            if (string.IsNullOrWhiteSpace(onHoldNo)) return result;
            try
            {
                var header = await GetSaleByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);
                var details = await GetSalvDetailsByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);
                var billDetails = await GeSaleBillDetailsByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);

                result.Header = header ?? new SalesDto();
                result.Details = details ?? new List<SalvDetailsDto>();
                result.BillDetails = billDetails ?? new SaleBillDetails();

                var totalDebit = result.Details.Where(d => string.Equals(d.DbCrFlag, "D", StringComparison.OrdinalIgnoreCase)).Sum(d => d.DrCrAmount ?? 0m);
                result.Header.Balance ??= totalDebit;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Sale with details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Sale with details for {onHoldNo}", ex);
            }
        }

        //public async Task<IEnumerable<SaleExportModel>> GetSaleDomesticDataAsync(DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        var q = from inv in _context.InvMains.AsNoTracking()
        //                join cust in _context.Customers.AsNoTracking()
        //                    on inv.CustCode equals cust.Custcode
        //                join cb in _context.CfnBills.AsNoTracking()
        //                    on inv.Slno equals cb.Billno into cfnBills
        //                from cb in cfnBills.DefaultIfEmpty()
        //                where inv.StartDate >= fromDate
        //                   && inv.StartDate <= toDate
        //                   && (inv.Type == "Domestic" || inv.Type == "Scrap")
        //                   && (inv.Mode == null || inv.Mode != "Cancelled")
        //                   && cb.Billno == null
        //                select new SaleExportModel
        //                {
        //                    InvoiceNo = inv.Slno,
        //                    InvoiceDate = inv.StartDate,
        //                    CustomerCode = inv.CustCode,
        //                    CustomerName = cust.Coname ?? string.Empty
        //                };

        //        return await q.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error retrieving Domestic invoice data");
        //        throw new ApplicationException("Error retrieving Domestic invoice data: " + ex.Message, ex);
        //    }
        //}

        //public async Task<IEnumerable<SaleExportModel>> GetSaleExportDataAsync(DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        var q = from inv in _context.EinvMains.AsNoTracking()
        //                join cust in _context.Customers.AsNoTracking()
        //                    on inv.CustCode equals cust.Custcode
        //                join cb in _context.CfnBills.AsNoTracking()
        //                    on inv.Slno equals cb.Billno into cfnBills
        //                from cb in cfnBills.DefaultIfEmpty()
        //                where inv.StartDate >= fromDate
        //                   && inv.StartDate <= toDate
        //                   && inv.Type == "Export"
        //                   && inv.Ftype == "Export"
        //                   && inv.Mode != "Cancelled"
        //                   && cb.Billno == null
        //                select new SaleExportModel
        //                {
        //                    InvoiceNo = inv.Slno,
        //                    InvoiceDate = inv.StartDate,
        //                    CustomerCode = inv.CustCode,
        //                    CustomerName = cust.Coname ?? string.Empty
        //                };

        //        return await q.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error retrieving Export invoice data");
        //        throw new ApplicationException("Error retrieving Export invoice data: " + ex.Message, ex);
        //    }
        //}

        private static string ValidationMsg(params (bool ok, string err)[] checks) => string.Join(" · ", checks.Where(c => !c.ok).Select(c => c.err));
        public async Task<IEnumerable<SaleExportModel>> GetSaleDomesticDataAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return (await (
                    from im in _context.InvMains.AsNoTracking()
                    join cust in _context.Customers.AsNoTracking() on im.CustCode equals cust.Custcode into cj
                    from cust in cj.DefaultIfEmpty()
                    join cb in _context.CfnBills.AsNoTracking() on im.Slno equals cb.Billno into bj
                    from cb in bj.DefaultIfEmpty()
                    where im.StartDate >= fromDate && im.StartDate <= toDate
                       && (im.Type == "Domestic" || im.Type == "Scrap")
                       && (im.Mode == null || im.Mode != "Cancelled")
                       && cb.Billno == null
                    select new
                    {
                        im.Slno,
                        im.StartDate,
                        im.CustCode,
                        CustomerName = cust != null ? cust.Coname : null,
                        CustomerExists = _context.CfnCustomers.Any(c => c.Customercode == im.CustCode),
                        AccountMappingExists = _context.CfnAcccustomers.Any(c => c.Customercode == im.CustCode && c.Customerstatus == "ACTVE"),
                        LinkedAccountExists = _context.CfnAccountottolinks.Any(c => c.ProdPrefix == im.CustCode && c.VchrType == "I" && c.ProdPrefixtype == "D" && c.ProdLevytype == "P"),
                    }
                ).ToListAsync())
                .GroupBy(r => r.Slno).Select(g =>
                {
                    var r = g.First(); return new SaleExportModel
                    {
                        InvoiceNo = r.Slno,
                        InvoiceDate = r.StartDate,
                        CustomerCode = r.CustCode,
                        CustomerName = r.CustomerName ?? string.Empty,
                        CustomerExists = r.CustomerExists,
                        AccountMappingExists = r.AccountMappingExists,
                        LinkedAccountExists = r.LinkedAccountExists,
                        ValidationMessage = ValidationMsg((r.CustomerExists, "Customer not in CfnCustomers"), (r.AccountMappingExists, "No mapping in CfnAcccustomers"), (r.LinkedAccountExists, "No link in CfnAccountottolinks (Domestic)")),
                    };
                }).OrderByDescending(r => r.InvoiceDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Domestic invoice data");
                throw new ApplicationException("Error retrieving Domestic invoice data: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<SaleExportModel>> GetSaleExportDataAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return (await (
                    from im in _context.EinvMains.AsNoTracking()
                    join cust in _context.Customers.AsNoTracking() on im.CustCode equals cust.Custcode into cj
                    from cust in cj.DefaultIfEmpty()
                    join cb in _context.CfnBills.AsNoTracking() on im.Slno equals cb.Billno into bj
                    from cb in bj.DefaultIfEmpty()
                    where im.StartDate >= fromDate && im.StartDate <= toDate
                       && im.Type == "Export" && im.Ftype == "Export" && im.Mode != "Cancelled"
                       && cb.Billno == null
                    select new
                    {
                        im.Slno,
                        im.StartDate,
                        im.CustCode,
                        CustomerName = cust != null ? cust.Coname : null,
                        CustomerExists = _context.CfnCustomers.Any(c => c.Customercode == im.CustCode),
                        AccountMappingExists = _context.CfnAcccustomers.Any(c => c.Customercode == im.CustCode && c.Customerstatus == "ACTVE"),
                        LinkedAccountExists = _context.CfnAccountottolinks.Any(c => c.ProdPrefix == im.CustCode && c.VchrType == "I" && c.ProdPrefixtype == "E" && c.ProdLevytype == "P"),
                    }
                ).ToListAsync())
                .GroupBy(r => r.Slno).Select(g =>
                {
                    var r = g.First(); return new SaleExportModel
                    {
                        InvoiceNo = r.Slno,
                        InvoiceDate = r.StartDate,
                        CustomerCode = r.CustCode,
                        CustomerName = r.CustomerName ?? string.Empty,
                        CustomerExists = r.CustomerExists,
                        AccountMappingExists = r.AccountMappingExists,
                        LinkedAccountExists = r.LinkedAccountExists,
                        ValidationMessage = ValidationMsg((r.CustomerExists, "Customer not in CfnCustomers"), (r.AccountMappingExists, "No mapping in CfnAcccustomers"), (r.LinkedAccountExists, "No link in CfnAccountottolinks (Export)")),
                    };
                }).OrderByDescending(r => r.InvoiceDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Export invoice data");
                throw new ApplicationException("Error retrieving Export invoice data: " + ex.Message, ex);
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
                        VALUES ('SALV', @voucherType, @accPeriod, 0, 0, 0, 0, 0, 0, LEFT(@voucherType, 3), '', '');";

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

        public async Task<string> OnHoldSaleAsync(SalesRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request?.VoucherData);
            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var detailsDto = request.Details ?? new List<SalvDetailsDto>();

                if (string.IsNullOrWhiteSpace(request.VoucherData.CtrlOnHoldNo))
                {
                    request.VoucherData.CtrlOnHoldNo = await GenerateVoucherNoInTxAsync(
                        request.AccountingPeriod ?? throw new ApplicationException("Accounting Period is required."),
                        request.VoucherData.VoucherType ?? throw new ApplicationException("Voucher Type is required."),
                        request.VoucherData.VoucherDate ?? now,
                        isOnHold: true);
                }

                var onHoldNo = request.VoucherData.CtrlOnHoldNo.Trim();
                var header = await _context.CfnSalevouchers.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo) ?? new CfnSalevoucher { CtrlOnholdno = onHoldNo, CtrlCreatedon = now };

                header.VchrDate = request.VoucherData.VoucherDate ?? now;
                header.VchrNarration = request.VoucherData.VoucherNarration;
                header.VchrType = request.VoucherData.VoucherType ?? string.Empty;
                header.VchrSyscategory = request.VoucherData.VoucherSysCategory;
                header.Accountcode = request.VoucherData.BankCode ?? string.Empty;
                header.Subaccountcode = request.VoucherData.BankAccount;
                header.CtrlAccperiod = request.AccountingPeriod;
                header.CtrlStatus = "Hold";
                header.CtrlUsername = request.Username;
                header.CtrlLocationcode = request.LocationCode;
                header.CtrlLastupdate = now;

                if (_context.Entry(header).State == EntityState.Detached)
                    _context.CfnSalevouchers.Add(header);

                var existingStatus = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo).Select(g => g.CtrlStatus).FirstOrDefaultAsync();
                if (existingStatus == "Post")
                    throw new ApplicationException($"Voucher {onHoldNo} is already posted. Posted vouchers cannot be modified.");

                if (existingStatus == "Hold")
                    await _ledgerRecalculationService.ReverseOnHoldAmountsAsync(onHoldNo);

                await _context.CfnSalvdetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
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

                var salvDetails = detailsDto.Select(d =>
                {
                    bool isVendorLine = !string.IsNullOrWhiteSpace(d.SubAccountCode);
                    bool hasCostCentres = d.CostCenterDetails != null && d.CostCenterDetails.Any();
                    return new CfnSalvdetail
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

                if (salvDetails.Any())
                    await _context.CfnSalvdetails.AddRangeAsync(salvDetails);

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

                if (request.BillDetails != null)
                {
                    var bill = request.BillDetails;
                    await _context.CfnBills.AddAsync(new CfnBill
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = 1,
                        Accountcode = request.VoucherData.BankCode,
                        Subaccountcode = request.VoucherData.BankAccount,
                        Billno = bill.BillNo,
                        Billdate = bill.BillDate,
                        Billamount = bill.BillAmount,
                        Billamountadjusted = 0m,
                        Billbalance = bill.BillAmount,
                        Dbcrflag = "D",
                        Tdsdedamount = 0m,
                        Tdsamount = 0m,
                        Tdscode = string.Empty,
                        VchrDate = request.VoucherData.VoucherDate,
                        VchrType = request.VoucherData.VoucherType,
                        VchrSyscategory = request.VoucherData.VoucherSysCategory,
                        VchrNarration = request.VoucherData.VoucherNarration,
                        CtrlStatus = "Hold",
                        CtrlLocationcode = request.LocationCode,
                        CtrlAccperiod = request.AccountingPeriod,
                        CtrlUsername = request.Username,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });
                }

                header.VchrTotalamount = salvDetails.Where(x => string.Equals(x.Dbcrflag, "C", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Dbcramount);

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
                _logger.LogError(ex, "Error saving Sale (OnHold)");
                throw new ApplicationException("Error saving Sale (OnHold): " + ex.Message, ex);
            }
        }

        public async Task<string> PostSaleAsync(SalesRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request?.VoucherData);
            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var detailsDto = request.Details ?? new List<SalvDetailsDto>();
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
                var header = await _context.CfnSalevouchers.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo) ?? new CfnSalevoucher { CtrlOnholdno = onHoldNo, CtrlCreatedon = now };

                header.VchrNumber = string.IsNullOrWhiteSpace(voucherNo) ? null : voucherNo;
                header.VchrDate = request.VoucherData.VoucherDate ?? now;
                header.VchrNarration = request.VoucherData.VoucherNarration;
                header.VchrType = request.VoucherData.VoucherType ?? string.Empty;
                header.VchrSyscategory = request.VoucherData.VoucherSysCategory;
                header.Accountcode = request.VoucherData.BankCode ?? string.Empty;
                header.Subaccountcode = request.VoucherData.BankAccount;
                header.CtrlAccperiod = request.AccountingPeriod;
                header.CtrlStatus = "Post";
                header.CtrlUsername = request.Username;
                header.CtrlLocationcode = request.LocationCode;
                header.CtrlLastupdate = now;

                if (_context.Entry(header).State == EntityState.Detached)
                    _context.CfnSalevouchers.Add(header);

                var existingStatus = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo).Select(g => g.CtrlStatus).FirstOrDefaultAsync();
                if (existingStatus == "Post")
                    throw new ApplicationException($"Voucher {onHoldNo} is already posted. Posted vouchers cannot be re-posted.");

                if (existingStatus == "Hold")
                    await _ledgerRecalculationService.ReverseOnHoldAmountsAsync(onHoldNo);

                await _context.CfnSalvdetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
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

                var salvDetails = detailsDto.Select(d =>
                {
                    bool isVendorLine = !string.IsNullOrWhiteSpace(d.SubAccountCode);
                    bool hasCostCentres = d.CostCenterDetails != null && d.CostCenterDetails.Any();
                    return new CfnSalvdetail
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

                if (salvDetails.Any())
                    await _context.CfnSalvdetails.AddRangeAsync(salvDetails);

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

                if (request.BillDetails != null)
                {
                    var bill = request.BillDetails;
                    await _context.CfnBills.AddAsync(new CfnBill
                    {
                        CtrlOnholdno = onHoldNo,
                        VchrNumber = voucherNo,
                        CtrlSequenceno = 1,
                        Accountcode = request.VoucherData.BankCode,
                        Subaccountcode = request.VoucherData.BankAccount,
                        Billno = bill.BillNo,
                        Billdate = bill.BillDate,
                        Billamount = bill.BillAmount,
                        Billamountadjusted = 0m,
                        Billbalance = bill.BillAmount,
                        Dbcrflag = "D",
                        Tdsdedamount = 0m,
                        Tdsamount = 0m,
                        Tdscode = string.Empty,
                        VchrDate = request.VoucherData.VoucherDate,
                        VchrType = request.VoucherData.VoucherType,
                        VchrSyscategory = request.VoucherData.VoucherSysCategory,
                        VchrNarration = request.VoucherData.VoucherNarration,
                        CtrlStatus = "Post",
                        CtrlLocationcode = request.LocationCode,
                        CtrlAccperiod = request.AccountingPeriod,
                        CtrlUsername = request.Username,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });
                }

                header.VchrTotalamount = salvDetails.Where(x => string.Equals(x.Dbcrflag, "C", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Dbcramount);

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
                _logger.LogError(ex, "Error saving Sale (Post)");
                throw new ApplicationException("Error saving Sale (Post): " + ex.Message, ex);
            }
        }

        public async Task<List<string>> OnHoldMultipleDomesticSalesAsync(List<string> invoiceNos, string voucherType, string accountingPeriod, string username, string locationCode, DateTime voucherDate)
        {
            if (invoiceNos == null || !invoiceNos.Any())
                throw new ApplicationException("No Invoices selected.");

            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var generatedOnHoldNos = new List<string>();

                foreach (var invoiceNo in invoiceNos.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    var onHoldNo = await GenerateVoucherNoInTxAsync(accountingPeriod, voucherType, voucherDate, isOnHold: true);
                    generatedOnHoldNos.Add(onHoldNo);

                    var invoiceData = await (
                        from im in _context.InvMains
                        join id in _context.InvDetails on im.Slno equals id.Slno
                        join cac in _context.CfnAcccustomers on im.CustCode equals cac.Customercode
                        join col in _context.CfnAccountottolinks on im.CustCode equals col.ProdPrefix
                        where im.Slno == invoiceNo
                           && col.VchrType == "I"
                           && col.ProdPrefixtype == "D"
                           && col.ProdLevytype == "P"
                        group new { im, id, cac, col } by new
                        {
                            im.Slno,
                            id.InvDate,
                            im.CustCode,
                            CustomerAccountCode = cac.Accountcode,
                            LinkedAccountCode = col.Accountcode
                        } into g
                        select new
                        {
                            InvoiceNo = g.Key.Slno,
                            InvoiceDate = g.Key.InvDate,
                            CustomerCode = g.Key.CustCode,
                            AccountCode = g.Key.CustomerAccountCode,
                            LinkedAccountCode = g.Key.LinkedAccountCode,
                            BaseAmount = g.Sum(x => (Convert.ToDecimal(x.id.Qty) * Convert.ToDecimal(x.id.Price)) - (Convert.ToDecimal(x.id.Ded ?? 0) > 0 ? Convert.ToDecimal(x.id.Ded ?? 0) : 0)),
                            SGST = g.Sum(x => ((Convert.ToDecimal(x.id.Qty) * Convert.ToDecimal(x.id.Price)) - (Convert.ToDecimal(x.id.Ded ?? 0) > 0 ? Convert.ToDecimal(x.id.Ded ?? 0) : 0)) * Convert.ToDecimal(x.im.SgstPerM ?? 0) / 100),
                            CGST = g.Sum(x => ((Convert.ToDecimal(x.id.Qty) * Convert.ToDecimal(x.id.Price)) - (Convert.ToDecimal(x.id.Ded ?? 0) > 0 ? Convert.ToDecimal(x.id.Ded ?? 0) : 0)) * Convert.ToDecimal(x.im.CgstPerM ?? 0) / 100),
                            IGST = g.Sum(x => ((Convert.ToDecimal(x.id.Qty) * Convert.ToDecimal(x.id.Price)) - (Convert.ToDecimal(x.id.Ded ?? 0) > 0 ? Convert.ToDecimal(x.id.Ded ?? 0) : 0)) * Convert.ToDecimal(x.im.IgstPerM ?? 0) / 100),
                            TCSAmount = g.Sum(x => (((Convert.ToDecimal(x.id.Qty) * Convert.ToDecimal(x.id.Price)) - (Convert.ToDecimal(x.id.Ded ?? 0))) * (1 + (Convert.ToDecimal(x.id.GstPer ?? 0) / 100))) * (Convert.ToDecimal(x.id.TcsPer) / 100)),
                        }).FirstOrDefaultAsync();

                    if (invoiceData == null)
                        throw new ApplicationException($"Invoice {invoiceNo} not found.");

                    decimal totalWithGST = invoiceData.BaseAmount + invoiceData.SGST + invoiceData.CGST + invoiceData.IGST;

                    await _context.CfnSalevouchers.AddAsync(new CfnSalevoucher
                    {
                        CtrlOnholdno = onHoldNo,
                        VchrDate = invoiceData.InvoiceDate ?? new DateTime(1900, 1, 1),
                        VchrType = voucherType,
                        VchrSyscategory = "REGUL",
                        Accountcode = invoiceData.AccountCode,
                        Subaccountcode = invoiceData.CustomerCode,
                        VchrNarration = $"Inv no. {invoiceData.InvoiceNo}",
                        VchrTotalamount = totalWithGST,
                        CtrlStatus = "Hold",
                        CtrlAccperiod = accountingPeriod,
                        CtrlUsername = username,
                        CtrlLocationcode = locationCode,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });

                    var details = new List<CfnSalvdetail>();
                    int seq = 1;

                    details.Add(new CfnSalvdetail
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = seq++,
                        Dbcrflag = "C",
                        Accountcode = invoiceData.LinkedAccountCode,
                        Dbcramount = invoiceData.BaseAmount,
                        Lineparticulars = $"Inv no. {invoiceData.InvoiceNo}",
                        Automated = "N"
                    });

                    foreach (var (accCode, amt) in new[]
                    {
                        ("L060411", invoiceData.SGST),
                        ("L060410", invoiceData.CGST),
                        ("L060412", invoiceData.IGST),
                        ("L060610", invoiceData.TCSAmount)
                    }.Where(t => t.Item2 > 0))
                    {
                        details.Add(new CfnSalvdetail
                        {
                            CtrlOnholdno = onHoldNo,
                            CtrlSequenceno = seq++,
                            Dbcrflag = "C",
                            Accountcode = accCode,
                            Dbcramount = amt,
                            Lineparticulars = $"Inv no. {invoiceData.InvoiceNo}",
                            Automated = "N"
                        });
                    }

                    details.Add(new CfnSalvdetail
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = seq++,
                        Dbcrflag = "D",
                        Accountcode = invoiceData.AccountCode,
                        Subaccountcode = invoiceData.CustomerCode,
                        Dbcramount = totalWithGST,
                        Lineparticulars = $"Inv no. {invoiceData.InvoiceNo}",
                        Automated = "Y"
                    });

                    await _context.CfnSalvdetails.AddRangeAsync(details);

                    await _context.CfnBills.AddAsync(new CfnBill
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = 1,
                        Accountcode = invoiceData.AccountCode,
                        Subaccountcode = invoiceData.CustomerCode,
                        Billno = invoiceData.InvoiceNo,
                        Billdate = invoiceData.InvoiceDate,
                        Billamount = totalWithGST,
                        Billamountadjusted = 0m,
                        Billbalance = totalWithGST,
                        Dbcrflag = "D",
                        CtrlStatus = "Hold",
                        CtrlLocationcode = locationCode,
                        CtrlAccperiod = accountingPeriod,
                        CtrlUsername = username,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });

                    int glSeq = 1;
                    await _context.CfnGldetails.AddRangeAsync(details.Select(d => new CfnGldetail
                    {
                        Accperiod = accountingPeriod,
                        Accountcode = d.Accountcode,
                        Subaccountcode = d.Subaccountcode,
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = glSeq++,
                        VchrDate = voucherDate,
                        VchrNarration = $"Inv no. {invoiceData.InvoiceNo}",
                        VchrType = voucherType,
                        Linedetails = d.Lineparticulars,
                        Dbcrflag = d.Dbcrflag,
                        Voucheramount = d.Dbcramount,
                        CtrlStatus = "Hold",
                        CtrlUsername = username,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    }));
                    await _context.SaveChangesAsync();

                    await _ledgerRecalculationService.RecalculateLedgersAsync(accountingPeriod, details.Select(d => new VoucherLineDto { AccountCode = d.Accountcode, SubAccountCode = d.Subaccountcode, DbCrFlag = d.Dbcrflag, DrCrAmount = d.Dbcramount }).ToList(), "ONHOLD");
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                await tx.CommitAsync();
                return generatedOnHoldNos;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error saving Domestic Sales (OnHold)");
                throw new ApplicationException("Error saving Domestic Sales (OnHold): " + ex.Message, ex);
            }
        }

        public async Task<List<string>> OnHoldMultipleExportSalesAsync(List<string> invoiceNos, string voucherType, string accountingPeriod, string username, string locationCode, DateTime voucherDate)
        {
            if (invoiceNos == null || !invoiceNos.Any())
                throw new ApplicationException("No Export Invoices selected.");

            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var generatedOnHoldNos = new List<string>();

                foreach (var invoiceNo in invoiceNos.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    var onHoldNo = await GenerateVoucherNoInTxAsync(accountingPeriod, voucherType, voucherDate, isOnHold: true);
                    generatedOnHoldNos.Add(onHoldNo);

                    var invoiceData = await (
                        from im in _context.EinvMains
                        join id in _context.EinvDetails on im.Slno equals id.Slno
                        join cac in _context.CfnAcccustomers on im.CustCode equals cac.Customercode
                        join col in _context.CfnAccountottolinks on im.CustCode equals col.ProdPrefix
                        where im.Slno == invoiceNo
                           && col.VchrType == "I"
                           && col.ProdPrefixtype == "E"
                           && col.ProdLevytype == "P"
                        group new { im, id, cac, col } by new
                        {
                            im.Slno,
                            id.InvDate,
                            im.CustCode,
                            CustomerAccountCode = cac.Accountcode,
                            LinkedAccountCode = col.Accountcode
                        } into g
                        select new
                        {
                            InvoiceNo = g.Key.Slno,
                            InvoiceDate = g.Key.InvDate ?? voucherDate,
                            CustomerCode = g.Key.CustCode,
                            AccountCode = g.Key.CustomerAccountCode,
                            LinkedAccountCode = g.Key.LinkedAccountCode,
                            //BaseAmount = g.Sum(x => ((((decimal?)x.id.Qty ?? 0m) * ((decimal?)x.id.Price ?? 0m) * ((decimal?)x.im.CurrencyRate ?? 0m))) - (((decimal?)x.id.Ded ?? 0m) > 0 ? ((decimal?)x.id.Ded ?? 0m) : 0m))
                            BaseAmount = g.Sum(x => ((((decimal?)x.id.Qty ?? 0m) * ((decimal?)x.id.Price ?? 0m))) - (((decimal?)x.id.Ded ?? 0m) > 0 ? ((decimal?)x.id.Ded ?? 0m) : 0m))
                        }).FirstOrDefaultAsync();

                    if (invoiceData == null)
                        throw new ApplicationException($"Export invoice {invoiceNo} not found.");

                    decimal totalAmount = invoiceData.BaseAmount;

                    await _context.CfnSalevouchers.AddAsync(new CfnSalevoucher
                    {
                        CtrlOnholdno = onHoldNo,
                        VchrDate = invoiceData.InvoiceDate,
                        VchrType = voucherType,
                        VchrSyscategory = "REGUL",
                        Accountcode = invoiceData.AccountCode,
                        Subaccountcode = invoiceData.CustomerCode,
                        VchrNarration = $"Inv no. {invoiceData.InvoiceNo}",
                        VchrTotalamount = totalAmount,
                        CtrlStatus = "Hold",
                        CtrlAccperiod = accountingPeriod,
                        CtrlUsername = username,
                        CtrlLocationcode = locationCode,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });

                    var details = new List<CfnSalvdetail>();
                    int seq = 1;

                    details.Add(new CfnSalvdetail
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = seq++,
                        Dbcrflag = "C",
                        Accountcode = invoiceData.LinkedAccountCode,
                        Dbcramount = totalAmount,
                        Lineparticulars = $"Inv no. {invoiceData.InvoiceNo}",
                        Automated = "N"
                    });

                    details.Add(new CfnSalvdetail
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = seq++,
                        Dbcrflag = "D",
                        Accountcode = invoiceData.AccountCode,
                        Subaccountcode = invoiceData.CustomerCode,
                        Dbcramount = totalAmount,
                        Lineparticulars = $"Inv no. {invoiceData.InvoiceNo}",
                        Automated = "Y"
                    });

                    await _context.CfnSalvdetails.AddRangeAsync(details);

                    await _context.CfnBills.AddAsync(new CfnBill
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = 1,
                        Accountcode = invoiceData.AccountCode,
                        Subaccountcode = invoiceData.CustomerCode,
                        Billno = invoiceData.InvoiceNo,
                        Billdate = invoiceData.InvoiceDate,
                        Billamount = totalAmount,
                        Billamountadjusted = totalAmount,
                        Billbalance = 0m,
                        Dbcrflag = "D",
                        CtrlStatus = "Hold",
                        CtrlLocationcode = locationCode,
                        CtrlAccperiod = accountingPeriod,
                        CtrlUsername = username,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });

                    int glSeq = 1;
                    await _context.CfnGldetails.AddRangeAsync(details.Select(d => new CfnGldetail
                    {
                        Accperiod = accountingPeriod,
                        Accountcode = d.Accountcode,
                        Subaccountcode = d.Subaccountcode,
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = glSeq++,
                        VchrDate = voucherDate,
                        VchrNarration = $"Inv no. {invoiceData.InvoiceNo}",
                        VchrType = voucherType,
                        Linedetails = d.Lineparticulars,
                        Dbcrflag = d.Dbcrflag,
                        Voucheramount = d.Dbcramount,
                        CtrlStatus = "Hold",
                        CtrlUsername = username,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    }));
                    await _context.SaveChangesAsync();

                    await _ledgerRecalculationService.RecalculateLedgersAsync(accountingPeriod, details.Select(d => new VoucherLineDto { AccountCode = d.Accountcode, SubAccountCode = d.Subaccountcode, DbCrFlag = d.Dbcrflag, DrCrAmount = d.Dbcramount }).ToList(), "ONHOLD");
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                await tx.CommitAsync();
                return generatedOnHoldNos;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error saving Export Sales (OnHold)");
                throw new ApplicationException("Error saving Export Sales (OnHold): " + ex.Message, ex);
            }
        }

        public async Task<PostMultipleResult> PostMultipleSalesAsync(List<string> onHoldNumbers, string accountingPeriod, string username, string locationCode)
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
                    var header = await _context.CfnSalevouchers.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo);
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
                    var voucherDate = header.VchrDate == default ? now : header.VchrDate;
                    var voucherAccPeriod = header.CtrlAccperiod ?? accountingPeriod;

                    if (string.IsNullOrWhiteSpace(voucherType))
                    {
                        result.Failed.Add(new PostMultipleFailure { OnHoldNo = onHoldNo, Reason = "VoucherType missing on OnHold header." });
                        await tx.RollbackAsync();
                        continue;
                    }

                    await _ledgerRecalculationService.ReverseOnHoldAmountsAsync(onHoldNo);

                    var voucherNo = await GenerateVoucherNoInTxAsync(accountingPeriod, voucherType, voucherDate, isOnHold: false);

                    header.VchrNumber = voucherNo;
                    header.CtrlStatus = "Post";
                    header.CtrlAccperiod = accountingPeriod;
                    header.CtrlUsername = username;
                    header.CtrlLocationcode = locationCode;
                    header.CtrlLastupdate = now;

                    var bills = await _context.CfnBills.Where(b => b.CtrlOnholdno == onHoldNo).ToListAsync();
                    foreach (var bill in bills)
                    {
                        bill.VchrNumber = voucherNo;
                        bill.CtrlStatus = "Post";
                        bill.CtrlLastupdate = now;
                    }

                    var glDetails = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo).ToListAsync();
                    foreach (var gl in glDetails)
                    {
                        gl.VchrNumber = voucherNo;
                        gl.CtrlStatus = "Post";
                        gl.CtrlLastupdate = now;
                    }

                    var costDetails = await _context.CfnCostdetails.Where(c => c.CtrlOnholdno == onHoldNo).ToListAsync();

                    foreach (var cost in costDetails)
                        cost.CtrlStatus = "Post";

                    await _context.SaveChangesAsync();

                    var lines = glDetails.Select(g => new VoucherLineDto { AccountCode = g.Accountcode, SubAccountCode = g.Subaccountcode, DbCrFlag = g.Dbcrflag, DrCrAmount = g.Voucheramount, }).ToList();
                    await _ledgerRecalculationService.RecalculateLedgersAsync(voucherAccPeriod, lines, "ONHOLD");
                    await _context.SaveChangesAsync();

                    await _ledgerRecalculationService.RecalculateLedgersAsync(voucherAccPeriod, lines, "POST");
                    await _context.SaveChangesAsync();
                    await tx.CommitAsync();

                    result.Posted.Add(voucherNo);
                    _logger.LogInformation("Sale OnHold {OnHoldNo} posted successfully as {VoucherNo}.", onHoldNo, voucherNo);
                }
                catch (Exception ex)
                {
                    await tx.RollbackAsync();
                    _logger.LogError(ex, "Error posting Sale OnHold {OnHoldNo} — skipped, continuing.", onHoldNo);
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
                    CfnSalvdetail x => x.CtrlOnholdno == onHoldNo,
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