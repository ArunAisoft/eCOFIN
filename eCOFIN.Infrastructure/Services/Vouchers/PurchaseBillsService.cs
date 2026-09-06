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
    public partial class PurchaseBillsService : IPurchaseBillsService
    {
        private readonly BilzFinDbContext _context;
        private readonly ILogger<PurchaseBillsService> _logger;
        private readonly ILedgerRecalculationService _ledgerRecalculationService;

        public PurchaseBillsService(BilzFinDbContext context, ILogger<PurchaseBillsService> logger, ILedgerRecalculationService ledgerRecalculationService)
        {
            _context = context;
            _logger = logger;
            _ledgerRecalculationService = ledgerRecalculationService;
        }

        public async Task<IEnumerable<ExistingPurchaseBillDto>> GetAllPurchaseBillsAsync(string accPeriod)
        {
            try
            {
                var q = from bill in _context.CfnBills.AsNoTracking()
                        join pur in _context.CfnPurchasejnls.AsNoTracking()
                            on bill.CtrlOnholdno equals pur.CtrlOnholdno
                        join vend in _context.CfnVendors.AsNoTracking()
                            on pur.Subaccountcode equals vend.Vendorcode
                        where pur.CtrlAccperiod == accPeriod
                        orderby pur.CtrlCreatedon descending
                        select new ExistingPurchaseBillDto
                        {
                            CtrlOnHoldNo = pur.CtrlOnholdno,
                            VchrNumber = pur.VchrNumber ?? string.Empty,
                            BillNumber = bill.Billno,
                            BillDate = bill.Billdate,
                            BillAmount = bill.Billamount ?? 0m,
                            BankCode = vend.Vendorcode ?? string.Empty,
                            Description = vend.Vendorname ?? string.Empty,
                            VchrNarration = bill.VchrNarration ?? string.Empty
                        };

                return await q.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Purchase Bills.");
                throw new ApplicationException("Error retrieving Purchase Bills: " + ex.Message, ex);
            }
        }

        public async Task<PurchaseBillsDto?> GetPurchaseBillByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return null;
            try
            {
                return await _context.CfnPurchasejnls.AsNoTracking()
                    .Where(x => x.CtrlOnholdno == onHoldNo)
                    .Select(x => new PurchaseBillsDto
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
                _logger.LogError(ex, "Error retrieving Purchase Bill header for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Purchase Bill header for {onHoldNo}", ex);
            }
        }

        public async Task<List<PurjDetailsDto>> GetPurjDetailsByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return new List<PurjDetailsDto>();
            try
            {
                var details = await _context.CfnPurjdetails.AsNoTracking()
                    .Where(d => d.CtrlOnholdno == onHoldNo)
                    .OrderBy(d => d.CtrlSequenceno)
                    .Select(d => new PurjDetailsDto
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
                _logger.LogError(ex, "Error retrieving Purchase Bill details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Purchase Bill details for {onHoldNo}", ex);
            }
        }

        public async Task<PurchaseBillDetails> GetPurchaseBillDetailsByOnHoldNoAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo)) return new PurchaseBillDetails();
            try
            {
                var result = await _context.CfnBills.AsNoTracking()
                    .Where(d => d.CtrlOnholdno == onHoldNo)
                    .OrderBy(d => d.CtrlSequenceno)
                    .Select(d => new PurchaseBillDetails
                    {
                        BillNo = d.Billno,
                        BillDate = d.Billdate,
                        BillDueDate = d.Billduedate,
                        PORefNo = d.Ponumber,
                        PODate = d.Podate,
                        TDSCode = d.Tdscode,
                        BillAmount = d.Billamount,
                        DeduAmount = 0,
                        TDSAmount = d.Tdsamount
                    })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                return result ?? new PurchaseBillDetails();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Purchase Bill details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Purchase Bill details for {onHoldNo}", ex);
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

        public async Task<PurchaseBillWithDetailsDto> GetPurchaseBillWithDetailsAsync(string onHoldNo)
        {
            var result = new PurchaseBillWithDetailsDto();
            if (string.IsNullOrWhiteSpace(onHoldNo)) return result;
            try
            {
                var header = await GetPurchaseBillByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);
                var details = await GetPurjDetailsByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);
                var billDetails = await GetPurchaseBillDetailsByOnHoldNoAsync(onHoldNo).ConfigureAwait(false);

                result.Header = header ?? new PurchaseBillsDto();
                result.Details = details ?? new List<PurjDetailsDto>();
                result.BillDetails = billDetails ?? new PurchaseBillDetails();

                var totalDebit = result.Details.Where(d => string.Equals(d.DbCrFlag, "D", StringComparison.OrdinalIgnoreCase)).Sum(d => d.DrCrAmount ?? 0m);

                result.Header.Balance ??= totalDebit;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Purchase Bill with details for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error retrieving Purchase Bill with details for {onHoldNo}", ex);
            }
        }

        //public async Task<IEnumerable<GINImportModel>> GetGINImportDataAsync(DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        var q = from gm in _context.GinMasters.AsNoTracking()
        //                join sd in _context.SupplierDetails.AsNoTracking() on gm.SuppCode equals sd.SuppCode1
        //                join pd in _context.GinPriceDomestics.AsNoTracking() on gm.GinNo equals pd.GinNo
        //                join gd in _context.GinDetails.AsNoTracking() on gm.GinNo equals gd.GinNo
        //                join cb in _context.CfnBills.AsNoTracking() on gm.GinNo equals cb.VchrRefnumber into cfnBills
        //                from cb in cfnBills.DefaultIfEmpty()
        //                where gm.AppDate >= fromDate
        //                   && gm.AppDate <= toDate
        //                   && gm.Check == "Y"
        //                   && cb.VchrRefnumber == null
        //                group new { gm, sd, gd } by new
        //                {
        //                    gm.GinNo,
        //                    gm.GinDate,
        //                    gm.InvChaNo,
        //                    gm.InvChaDate,
        //                    gm.PmtDDate,
        //                    gm.PoNo,
        //                    sd.SuppCode1,
        //                    sd.SuppName,
        //                    gm.AppDate
        //                } into g
        //                select new GINImportModel
        //                {
        //                    GINNo = g.Key.GinNo,
        //                    GINDate = g.Key.GinDate,
        //                    InvoiceNo = g.Key.InvChaNo,
        //                    InvoiceDate = g.Key.InvChaDate,
        //                    PmtDDate = g.Key.PmtDDate,
        //                    PONo = g.Key.PoNo,
        //                    SuppCode = g.Key.SuppCode1,
        //                    VendorName = g.Key.SuppName,
        //                    AppDate = g.Key.AppDate,
        //                    AccQtyPSLPrice = (decimal)g.Sum(x => (x.gd.AccQty ?? 0) * (x.gd.PslPrice ?? 0))
        //                };
        //        return await q.ToListAsync().ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error retrieving GIN import data");
        //        throw new ApplicationException("Error retrieving GIN import data: " + ex.Message, ex);
        //    }
        //}

        //public async Task<IEnumerable<JINImportModel>> GetJINImportDataAsync(DateTime fromDate, DateTime toDate)
        //{
        //    try
        //    {
        //        var q = from d in _context.StoAnnexDetails.AsNoTracking()
        //                join v in _context.Vendcodes.AsNoTracking() on d.VendCode equals v.Code
        //                join av in _context.CfnAccvendors.AsNoTracking() on d.VendCode equals av.Vendorcode
        //                join cb in _context.CfnBills.AsNoTracking() on d.JinNo equals cb.VchrRefnumber into cfnBills
        //                from cb in cfnBills.DefaultIfEmpty()
        //                join cp in _context.CfnPayments.AsNoTracking() on d.JinNo equals cp.VchrRefnumber into cfnPayments
        //                from cp in cfnPayments.DefaultIfEmpty()
        //                where d.JinDate >= fromDate
        //                   && d.JinDate <= toDate
        //                   && d.JinApprove == "Y"
        //                   && cb.VchrRefnumber == null
        //                   && cp.VchrRefnumber == null
        //                group new { d, v, av } by new
        //                {
        //                    d.JinNo,
        //                    d.JinDate,
        //                    d.TdsAccCode,
        //                    av.Accountcode,
        //                    d.VendCode,
        //                    d.AccRemarks,
        //                    d.TdsCode,
        //                    v.Name
        //                } into g
        //                select new
        //                {
        //                    Key = g.Key,
        //                    ProdValue = g.Sum(x => (x.d.AccValue ?? 0d)),
        //                    RejValue = g.Sum(x => (x.d.RejValue ?? 0d)),
        //                    TdsSum = g.Sum(x => (x.d.TdsValue ?? 0d))
        //                };

        //        var raw = await q.ToListAsync().ConfigureAwait(false);

        //        return raw
        //            .Where(r => r.ProdValue > 0 || r.RejValue > 0)
        //            .Select(r => new JINImportModel
        //            {
        //                JINNo = r.Key.JinNo,
        //                JINDate = r.Key.JinDate,
        //                BankCode = r.Key.TdsAccCode,
        //                AccountCode = r.Key.Accountcode,
        //                VendorCode = r.Key.VendCode,
        //                VendorName = r.Key.Name,
        //                AccRemarks = r.Key.AccRemarks,
        //                TDSCode = r.Key.TdsCode,
        //                ProductValue = (decimal)r.ProdValue,
        //                RejValue = (decimal)r.RejValue,
        //                TDSValue = (decimal)r.TdsSum
        //            }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error retrieving JIN import data");
        //        throw new ApplicationException("Error retrieving JIN import data: " + ex.Message, ex);
        //    }
        //}

        private static string ValidationMsg(params (bool ok, string err)[] checks) => string.Join(" · ", checks.Where(c => !c.ok).Select(c => c.err));

        public async Task<IEnumerable<GINImportModel>> GetGINImportDataAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var result = await (
                    from gm in _context.GinMasters.AsNoTracking()
                    join sd in _context.SupplierDetails.AsNoTracking() on gm.SuppCode equals sd.SuppCode1 into sj
                    from sd in sj.DefaultIfEmpty()
                    join gd in _context.GinDetails.AsNoTracking() on gm.GinNo equals gd.GinNo into gdj
                    from gd in gdj.DefaultIfEmpty()
                    join cb in _context.CfnBills.AsNoTracking() on gm.GinNo equals cb.VchrRefnumber into bj
                    from cb in bj.DefaultIfEmpty()
                    where gm.AppDate >= fromDate && gm.AppDate <= toDate && gm.MainType == "DOMESTIC" && gm.Check == "Y" && cb.VchrRefnumber == null
                    group new { gm, sd, gd } by new
                    {
                        gm.GinNo,
                        gm.GinDate,
                        gm.InvChaNo,
                        gm.InvChaDate,
                        gm.PmtDDate,
                        gm.PoNo,
                        gm.AppDate,
                        SuppCode = sd != null ? sd.SuppCode1 : gm.SuppCode,
                        SuppName = sd != null ? sd.SuppName : null,
                        VendorMasterExists = _context.CfnVendors.Any(v => v.Vendorcode == gm.SuppCode),
                        VendorAccountExists = _context.CfnAccvendors.Any(v => v.Vendorcode == gm.SuppCode && (v.Vendorstatus == "ACTVE" || v.Vendorstatus == "ACTIVE"))
                    }
                    into g
                    select new
                    {
                        g.Key,
                        AccQtyPSLPrice = (decimal)g.Sum(x => x.gd != null ? (x.gd.AccQty ?? 0) * (x.gd.PslPrice ?? 0) : 0)
                    }
                ).ToListAsync();

                if (!result.Any())
                    return Enumerable.Empty<GINImportModel>();

                var ginNos = result.Select(x => x.Key.GinNo).Distinct().ToList();
                var articleMappings = await (
                    from gd in _context.GinDetails.AsNoTracking()
                    join ct in _context.CodeTypes.AsNoTracking() on gd.ArticleNo equals ct.ArticleNo into ctj
                    from ct in ctj.DefaultIfEmpty()
                    group new { gd, ct } by gd.GinNo into g
                    select new { GinNo = g.Key, ArticleMappingExists = !g.Any(x => x.ct == null || x.ct.AccCode == null || x.ct.AccCode == "") }
                ).ToListAsync();

                var articleLookup = articleMappings.ToDictionary(x => x.GinNo, x => x.ArticleMappingExists);
                return result.Select(r =>
                {
                    bool articleMappingExists = articleLookup.TryGetValue(r.Key.GinNo, out bool exists) ? exists : false;
                    return new GINImportModel
                    {
                        GINNo = r.Key.GinNo,
                        GINDate = r.Key.GinDate,
                        InvoiceNo = r.Key.InvChaNo,
                        InvoiceDate = r.Key.InvChaDate,
                        PmtDDate = r.Key.PmtDDate,
                        PONo = r.Key.PoNo,
                        SuppCode = r.Key.SuppCode,
                        VendorName = r.Key.SuppName,
                        AppDate = r.Key.AppDate,
                        AccQtyPSLPrice = r.AccQtyPSLPrice,
                        VendorMasterExists = r.Key.VendorMasterExists,
                        VendorAccountExists = r.Key.VendorAccountExists,
                        ArticleMappingExists = articleMappingExists,
                        ValidationMessage = ValidationMsg((r.Key.VendorMasterExists, "Vendor not in CfnVendors"),
                            (r.Key.VendorAccountExists, "No account mapping in CfnAccvendors"),
                            (articleMappingExists, "One or more articles missing AccCode in CodeTypes"))
                    };
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving GIN import data");
                throw new ApplicationException("Error retrieving GIN import data: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<JINImportModel>> GetJINImportDataAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var raw = await (
                    from d in _context.StoAnnexDetails.AsNoTracking()
                    join v in _context.Vendcodes.AsNoTracking() on d.VendCode equals v.Code
                    join av in _context.CfnAccvendors.AsNoTracking() on d.VendCode equals av.Vendorcode into avj
                    from av in avj.DefaultIfEmpty()
                    join cb in _context.CfnBills.AsNoTracking() on d.JinNo equals cb.VchrRefnumber into bj
                    from cb in bj.DefaultIfEmpty()
                    join cp in _context.CfnPayments.AsNoTracking() on d.JinNo equals cp.VchrRefnumber into pj
                    from cp in pj.DefaultIfEmpty()
                    where d.JinDate >= fromDate && d.JinDate <= toDate
                       && d.JinApprove == "Y"
                       && cb.VchrRefnumber == null
                       && cp.VchrRefnumber == null
                    group new { d, v, av } by new
                    {
                        d.JinNo,
                        d.JinDate,
                        d.TdsAccCode,
                        d.VendCode,
                        d.AccRemarks,
                        d.TdsCode,
                        v.Name,
                        AccountCode = av != null ? av.Accountcode : null,
                        VendorMasterExists = _context.CfnVendors.Any(x => x.Vendorcode == d.VendCode),
                        VendorAccountExists = _context.CfnAccvendors.Any(x => x.Vendorcode == d.VendCode && (x.Vendorstatus == "ACTVE" || x.Vendorstatus == "ACTIVE")),
                        BankCodeExists = d.TdsAccCode != null && _context.CfnAccounts.Any(x => x.Accountcode == d.TdsAccCode && x.Accountstatus == "ACTVE"),
                    } into g
                    select new
                    {
                        g.Key,
                        ProdValue = g.Sum(x => (decimal?)(x.d.AccValue) ?? 0m),
                        RejValue = g.Sum(x => (decimal?)(x.d.RejValue) ?? 0m),
                        TdsSum = g.Sum(x => (decimal?)(x.d.TdsValue) ?? 0m)
                    }
                ).ToListAsync().ConfigureAwait(false);

                return raw
                    .Where(r => r.ProdValue > 0 || r.RejValue > 0)
                    .Select(r => new JINImportModel
                    {
                        JINNo = r.Key.JinNo,
                        JINDate = r.Key.JinDate,
                        BankCode = r.Key.TdsAccCode,
                        AccountCode = r.Key.AccountCode,
                        VendorCode = r.Key.VendCode,
                        VendorName = r.Key.Name,
                        AccRemarks = r.Key.AccRemarks,
                        TDSCode = r.Key.TdsCode,
                        ProductValue = r.ProdValue,
                        RejValue = r.RejValue,
                        TDSValue = r.TdsSum,
                        VendorMasterExists = r.Key.VendorMasterExists,
                        VendorAccountExists = r.Key.VendorAccountExists,
                        BankCodeExists = r.Key.BankCodeExists,
                        ValidationMessage = ValidationMsg((r.Key.VendorMasterExists, "Vendor not in CfnVendors"), (r.Key.VendorAccountExists, "No account mapping in CfnAccvendors"), (r.Key.BankCodeExists, $"TDS code '{r.Key.TdsAccCode}' not in CfnAccounts")),
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving JIN import data");
                throw new ApplicationException("Error retrieving JIN import data: " + ex.Message, ex);
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
                        VALUES ('JRNL', @voucherType, @accPeriod, 0, 0, 0, 0, 0, 0, LEFT(@voucherType, 3), '', '');";

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

        public async Task<string> OnHoldPurchaseBillAsync(PurchaseBillsRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request?.VoucherData);
            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var detailsDto = request.Details ?? new List<PurjDetailsDto>();
                var netBill = await NormalizePurchaseBillAsync(request);

                if (string.IsNullOrWhiteSpace(request.VoucherData.CtrlOnHoldNo))
                {
                    request.VoucherData.CtrlOnHoldNo = await GenerateVoucherNoInTxAsync(
                        request.AccountingPeriod ?? throw new ApplicationException("Accounting Period is required."),
                        request.VoucherData.VoucherType ?? throw new ApplicationException("Voucher Type is required."),
                        request.VoucherData.VoucherDate ?? now,
                        isOnHold: true);
                }

                var onHoldNo = request.VoucherData.CtrlOnHoldNo.Trim();
                var header = await _context.CfnPurchasejnls.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo) ?? new CfnPurchasejnl { CtrlOnholdno = onHoldNo, CtrlCreatedon = now };

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
                    _context.CfnPurchasejnls.Add(header);

                var existingStatus = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo).Select(g => g.CtrlStatus).FirstOrDefaultAsync();
                if (existingStatus == "Post")
                    throw new ApplicationException($"Voucher {onHoldNo} is already posted. Posted vouchers cannot be modified.");

                if (existingStatus == "Hold")
                    await _ledgerRecalculationService.ReverseOnHoldAmountsAsync(onHoldNo);

                await _context.CfnPurjdetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
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

                var purjDetails = detailsDto.Select(d =>
                {
                    bool isVendorLine = !string.IsNullOrWhiteSpace(d.SubAccountCode);
                    bool hasCostCentres = d.CostCenterDetails != null && d.CostCenterDetails.Any();
                    return new CfnPurjdetail
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

                if (purjDetails.Any())
                    await _context.CfnPurjdetails.AddRangeAsync(purjDetails);

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
                        Billduedate = bill.BillDueDate,
                        Ponumber = bill.PORefNo,
                        Podate = bill.PODate,
                        Billamount = bill.BillAmount,
                        Billamountadjusted = 0m,
                        Billbalance = netBill,
                        Dbcrflag = "C",
                        Tdsdedamount = bill.DeduAmount,
                        Tdsamount = bill.TDSAmount,
                        Tdscode = bill.TDSCode,
                        Releaseamt = netBill,
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

                header.VchrTotalamount = purjDetails.Where(x => string.Equals(x.Dbcrflag, "D", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Dbcramount);

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
                _logger.LogError(ex, "Error saving Purchase Bill (OnHold)");
                throw new ApplicationException("Error saving Purchase Bill (OnHold): " + ex.Message, ex);
            }
        }

        public async Task<string> PostPurchaseBillAsync(PurchaseBillsRequestDto request)
        {
            ArgumentNullException.ThrowIfNull(request?.VoucherData);
            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var detailsDto = request.Details ?? new List<PurjDetailsDto>();
                var netBill = await NormalizePurchaseBillAsync(request);
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
                var header = await _context.CfnPurchasejnls.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo) ?? new CfnPurchasejnl { CtrlOnholdno = onHoldNo, CtrlCreatedon = now };

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
                    _context.CfnPurchasejnls.Add(header);

                var existingStatus = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo).Select(g => g.CtrlStatus).FirstOrDefaultAsync();
                if (existingStatus == "Post")
                    throw new ApplicationException($"Voucher {onHoldNo} is already posted. Posted vouchers cannot be re-posted.");

                if (existingStatus == "Hold")
                    await _ledgerRecalculationService.ReverseOnHoldAmountsAsync(onHoldNo);

                await _context.CfnPurjdetails.Where(x => x.CtrlOnholdno == onHoldNo).ExecuteDeleteAsync();
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

                var purjDetails = detailsDto.Select(d =>
                {
                    bool isVendorLine = !string.IsNullOrWhiteSpace(d.SubAccountCode);
                    bool hasCostCentres = d.CostCenterDetails != null && d.CostCenterDetails.Any();
                    return new CfnPurjdetail
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

                if (purjDetails.Any())
                    await _context.CfnPurjdetails.AddRangeAsync(purjDetails);

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
                        Billduedate = bill.BillDueDate,
                        Ponumber = bill.PORefNo,
                        Podate = bill.PODate,
                        Billamount = bill.BillAmount,
                        Billamountadjusted = 0m,
                        Billbalance = netBill,
                        Dbcrflag = "C",
                        Tdsdedamount = bill.DeduAmount,
                        Tdsamount = bill.TDSAmount,
                        Tdscode = bill.TDSCode,
                        Releaseamt = netBill,
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

                header.VchrTotalamount = purjDetails.Where(x => string.Equals(x.Dbcrflag, "D", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Dbcramount);

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
                _logger.LogError(ex, "Error saving Purchase Bill (Post)");
                throw new ApplicationException("Error saving Purchase Bill (Post): " + ex.Message, ex);
            }
        }

        public async Task<List<string>> OnHoldMultipleGINAsync(List<string> ginNumbers, string voucherType, string accountingPeriod, string username, string locationCode, DateTime voucherDate)
        {
            if (ginNumbers == null || !ginNumbers.Any())
                throw new ApplicationException("No GIN selected.");

            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var generatedOnHoldNos = new List<string>();

                foreach (var ginNo in ginNumbers.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    var onHoldNo = await GenerateVoucherNoInTxAsync(accountingPeriod, voucherType, voucherDate, isOnHold: true);
                    generatedOnHoldNos.Add(onHoldNo);

                    var ginMaster = await (
                        from gm in _context.GinMasters
                        join av in _context.CfnAccvendors
                            on gm.SuppCode equals av.Vendorcode
                        where gm.GinNo == ginNo
                           && (av.Vendorstatus == "ACTIVE" || av.Vendorstatus == "ACTVE")
                        select new
                        {
                            gm.GinNo,
                            gm.InvChaNo,
                            gm.InvChaDate,
                            gm.PmtDDate,
                            gm.PoNo,
                            gm.InvBasicAmount,
                            gm.AppDate,
                            av.Accountcode,
                            av.Vendorcode
                        }).FirstOrDefaultAsync();

                    if (ginMaster == null)
                        throw new ApplicationException($"GIN {ginNo} not found.");

                    var ginDetails = await (
                        from gd in _context.GinDetails
                        join ct in _context.CodeTypes on gd.ArticleNo equals ct.ArticleNo
                        where gd.GinNo == ginNo
                        group new { gd, ct } by new { gd.GinNo, ct.AccCode } into g
                        select new
                        {
                            AccCode = g.Key.AccCode,
                            BaseAmount = g.Sum(x => (decimal?)((decimal)x.gd.AccQty * (decimal)x.gd.PslPrice)) ?? 0m,
                            SGST = g.Sum(x => (decimal?)((decimal)x.gd.AccQty * (decimal)x.gd.PslPrice * (decimal)(x.gd.SgstPer ?? 0) / 100m)) ?? 0m,
                            CGST = g.Sum(x => (decimal?)((decimal)x.gd.AccQty * (decimal)x.gd.PslPrice * (decimal)(x.gd.CgstPer ?? 0) / 100m)) ?? 0m,
                            IGST = g.Sum(x => (decimal?)((decimal)x.gd.AccQty * (decimal)x.gd.PslPrice * (decimal)(x.gd.IgstPer ?? 0) / 100m)) ?? 0m
                        }).ToListAsync();

                    if (!ginDetails.Any())
                        throw new ApplicationException($"No details found for GIN {ginNo}");

                    decimal basicAmount = ginDetails.Sum(x => x.BaseAmount);
                    decimal sgst = ginDetails.Sum(x => x.SGST);
                    decimal cgst = ginDetails.Sum(x => x.CGST);
                    decimal igst = ginDetails.Sum(x => x.IGST);
                    decimal totalAmount = basicAmount + sgst + cgst + igst;
                    DateTime vDate = ginMaster.AppDate ?? voucherDate;

                    await _context.CfnPurchasejnls.AddAsync(new CfnPurchasejnl
                    {
                        CtrlOnholdno = onHoldNo,
                        VchrDate = vDate,
                        VchrType = voucherType,
                        VchrSyscategory = "REGUL",
                        Accountcode = ginMaster.Accountcode,
                        Subaccountcode = ginMaster.Vendorcode,
                        VchrNarration = $"Inv.Ch.No. {ginMaster.InvChaNo} Dt.{vDate:dd/MM/yyyy} PO.No. {ginMaster.PoNo}",
                        VchrTotalamount = totalAmount,
                        CtrlStatus = "Hold",
                        CtrlAccperiod = accountingPeriod,
                        CtrlUsername = username,
                        CtrlLocationcode = locationCode,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });

                    var details = new List<CfnPurjdetail>();
                    int seq = 1;

                    foreach (var detail in ginDetails)
                    {
                        details.Add(new CfnPurjdetail
                        {
                            CtrlOnholdno = onHoldNo,
                            CtrlSequenceno = seq++,
                            Dbcrflag = "D",
                            Accountcode = detail.AccCode,
                            Dbcramount = detail.BaseAmount,
                            Lineparticulars = $"GIN No. {ginNo}"
                        });
                    }

                    foreach (var (accCode, amt) in new[]
                    {
                        ("A070211", cgst),
                        ("A070212", sgst),
                        ("A070213", igst)
                    }.Where(t => t.Item2 > 0))
                    {
                        details.Add(new CfnPurjdetail
                        {
                            CtrlOnholdno = onHoldNo,
                            CtrlSequenceno = seq++,
                            Dbcrflag = "D",
                            Accountcode = accCode,
                            Dbcramount = amt,
                            Lineparticulars = $"GIN No. {ginNo}"
                        });
                    }

                    details.Add(new CfnPurjdetail
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = seq++,
                        Dbcrflag = "C",
                        Accountcode = ginMaster.Accountcode,
                        Subaccountcode = ginMaster.Vendorcode,
                        Dbcramount = totalAmount,
                        Lineparticulars = $"GIN No. {ginNo}",
                        Automated = "Y"
                    });

                    await _context.CfnPurjdetails.AddRangeAsync(details);

                    await _context.CfnBills.AddAsync(new CfnBill
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = 1,
                        Accountcode = ginMaster.Accountcode,
                        Subaccountcode = ginMaster.Vendorcode,
                        Billno = ginMaster.InvChaNo?.Length > 20 ? ginMaster.InvChaNo[..20] : ginMaster.InvChaNo,
                        Billdate = ginMaster.InvChaDate ?? vDate,
                        Billduedate = ginMaster.PmtDDate ?? null,
                        Billamount = totalAmount,
                        Billbalance = totalAmount,
                        Billamountadjusted = 0m,
                        VchrType = voucherType,
                        VchrSyscategory = "REGUL",
                        VchrRefnumber = ginMaster.GinNo,
                        VchrNarration = $"GIN.No. {ginMaster.GinNo} Dt.{vDate:dd/MM/yyyy}",
                        Ponumber = ginMaster.PoNo,
                        Podate = ginMaster.PmtDDate,
                        Dbcrflag = "C",
                        CtrlStatus = "Hold",
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
                        VchrDate = vDate,
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
                _logger.LogError(ex, "Error saving GIN Purchase (OnHold): {Msg}", ex.Message);
                throw new ApplicationException($"Error saving GIN Purchase (OnHold): {ex.Message}", ex);
            }
        }

        public async Task<List<string>> OnHoldMultipleJINAsync(List<string> jinNumbers, string voucherType, string accountingPeriod, string username, string locationCode, DateTime voucherDate)
        {
            if (jinNumbers == null || !jinNumbers.Any())
                throw new ApplicationException("No JIN selected.");

            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var now = DateTime.UtcNow;
                var generatedOnHoldNos = new List<string>();

                foreach (var jinNo in jinNumbers.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    var jin = await (
                        from d in _context.StoAnnexDetails
                        join v in _context.Vendcodes on d.VendCode equals v.Code
                        join av in _context.CfnAccvendors on d.VendCode equals av.Vendorcode
                        where d.JinNo == jinNo && d.JinApprove == "Y"
                        group new { d, v, av } by new
                        {
                            d.JinNo,
                            d.JinDate,
                            d.TdsAccCode,
                            av.Accountcode,
                            d.VendCode,
                            d.AccRemarks,
                            d.TdsCode,
                            v.Name
                        } into g
                        select new
                        {
                            JinNo = g.Key.JinNo,
                            JinDate = g.Key.JinDate,
                            BankCode = g.Key.TdsAccCode,
                            AccountCode = g.Key.Accountcode,
                            VendorCode = g.Key.VendCode,
                            VendorName = g.Key.Name,
                            TdsCode = g.Key.TdsCode,
                            ProductValue = g.Sum(x => (decimal?)(x.d.AccValue) ?? 0m),
                            RejValue = g.Sum(x => (decimal?)(x.d.RejValue) ?? 0m),
                            TdsValue = g.Sum(x => (decimal?)(x.d.TdsValue) ?? 0m)
                        }).FirstOrDefaultAsync();

                    if (jin == null)
                        throw new ApplicationException($"JIN {jinNo} not found.");

                    DateTime vDate = jin.JinDate ?? voucherDate;
                    decimal rejValue = jin.RejValue;

                    if (rejValue > 0)
                    {
                        decimal rejAmount = Math.Abs(rejValue);
                        var dnOnHoldNo = await GenerateVoucherNoInTxAsync(accountingPeriod, "DEB", vDate, isOnHold: true);

                        generatedOnHoldNos.Add(dnOnHoldNo);

                        await _context.CfnDebitnotes.AddAsync(new CfnDebitnote
                        {
                            CtrlOnholdno = dnOnHoldNo,
                            VchrDate = vDate,
                            VchrType = "DEB",
                            VchrSyscategory = "REGUL",
                            Accountcode = jin.AccountCode,
                            Subaccountcode = jin.VendorCode,
                            VchrNarration = $"Rej. JIN.No. {jin.JinNo} Dt.{vDate:dd/MM/yyyy}",
                            VchrTotalamount = rejAmount,
                            CtrlStatus = "Hold",
                            CtrlAccperiod = accountingPeriod,
                            CtrlUsername = username,
                            CtrlLocationcode = locationCode,
                            CtrlCreatedon = now,
                            CtrlLastupdate = now
                        });

                        var dnDetails = new List<CfnDebndetail>
                        {
                            new CfnDebndetail
                            {
                                CtrlOnholdno = dnOnHoldNo,
                                CtrlSequenceno = 1,
                                Dbcrflag = "D",
                                Accountcode = jin.AccountCode,
                                Subaccountcode = jin.VendorCode,
                                Dbcramount = rejAmount,
                                Lineparticulars = $"Rej. JIN.No. {jin.JinNo}",
                                Automated = "Y"
                            },
                            new CfnDebndetail
                            {
                                CtrlOnholdno = dnOnHoldNo,
                                CtrlSequenceno = 2,
                                Dbcrflag = "C",
                                Accountcode = "E020400",
                                Dbcramount = rejAmount,
                                Lineparticulars = $"Rej. JIN.No. {jin.JinNo}",
                                Automated = "N"
                            },
                        };

                        await _context.CfnDebndetails.AddRangeAsync(dnDetails);

                        int dnGlSeq = 1;
                        await _context.CfnGldetails.AddRangeAsync(
                            dnDetails.Select(d => new CfnGldetail
                            {
                                Accperiod = accountingPeriod,
                                Accountcode = d.Accountcode,
                                Subaccountcode = d.Subaccountcode,
                                CtrlOnholdno = dnOnHoldNo,
                                CtrlSequenceno = dnGlSeq++,
                                VchrDate = vDate,
                                VchrType = "DEB",
                                Linedetails = d.Lineparticulars,
                                Dbcrflag = d.Dbcrflag,
                                Voucheramount = d.Dbcramount,
                                CtrlStatus = "Hold",
                                CtrlUsername = username,
                                CtrlCreatedon = now,
                                CtrlLastupdate = now
                            }));

                        await _context.SaveChangesAsync();

                        await _ledgerRecalculationService.RecalculateLedgersAsync(
                            accountingPeriod,
                            dnDetails.Select(d => new VoucherLineDto
                            {
                                AccountCode = d.Accountcode,
                                SubAccountCode = d.Subaccountcode,
                                DbCrFlag = d.Dbcrflag,
                                DrCrAmount = d.Dbcramount
                            }).ToList(), "ONHOLD");

                        await _context.SaveChangesAsync();
                        _context.ChangeTracker.Clear();
                    }

                    var onHoldNo = await GenerateVoucherNoInTxAsync(accountingPeriod, voucherType, voucherDate, isOnHold: true);
                    generatedOnHoldNos.Add(onHoldNo);

                    decimal productValue = jin.ProductValue;
                    decimal tdsValue = jin.TdsValue;
                    decimal vendorNet = productValue - tdsValue;

                    await _context.CfnPurchasejnls.AddAsync(new CfnPurchasejnl
                    {
                        CtrlOnholdno = onHoldNo,
                        VchrDate = vDate,
                        VchrType = voucherType,
                        VchrSyscategory = "REGUL",
                        Accountcode = jin.AccountCode,
                        Subaccountcode = jin.VendorCode,
                        VchrNarration = $"JIN.No. {jin.JinNo} Dt.{vDate:dd/MM/yyyy}",
                        VchrTotalamount = productValue,
                        CtrlStatus = "Hold",
                        CtrlAccperiod = accountingPeriod,
                        CtrlUsername = username,
                        CtrlLocationcode = locationCode,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now
                    });

                    var details = new List<CfnPurjdetail>();
                    int seq = 1;

                    details.Add(new CfnPurjdetail
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = seq++,
                        Dbcrflag = "D",
                        Accountcode = jin.BankCode,
                        Dbcramount = productValue,
                        Lineparticulars = jin.JinNo,
                    });

                    if (tdsValue > 0)
                    {
                        details.Add(new CfnPurjdetail
                        {
                            CtrlOnholdno = onHoldNo,
                            CtrlSequenceno = seq++,
                            Dbcrflag = "C",
                            Accountcode = "L060603",
                            Dbcramount = tdsValue,
                            Lineparticulars = jin.JinNo,
                            Automated = "Y"
                        });
                    }

                    details.Add(new CfnPurjdetail
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = seq++,
                        Dbcrflag = "C",
                        Accountcode = jin.AccountCode,
                        Subaccountcode = jin.VendorCode,
                        Dbcramount = vendorNet,
                        Lineparticulars = jin.JinNo,
                        Automated = "Y"
                    });

                    await _context.CfnPurjdetails.AddRangeAsync(details);

                    await _context.CfnBills.AddAsync(new CfnBill
                    {
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = 1,
                        Accountcode = jin.AccountCode,
                        Subaccountcode = jin.VendorCode,
                        Billno = jin.JinNo?.Length > 20 ? jin.JinNo[..20] : jin.JinNo,
                        Billdate = vDate,
                        Billamount = productValue,
                        Billbalance = vendorNet,
                        Billamountadjusted = 0m,
                        VchrType = voucherType,
                        VchrSyscategory = "REGUL",
                        VchrRefnumber = jin.JinNo,
                        VchrNarration = $"JIN.No. {jin.JinNo} Dt.{vDate:dd/MM/yyyy}",
                        Dbcrflag = "C",
                        CtrlStatus = "Hold",
                        CtrlAccperiod = accountingPeriod,
                        CtrlUsername = username,
                        CtrlCreatedon = now,
                        CtrlLastupdate = now,
                        Tdsamount = tdsValue,
                        Tdscode = jin.TdsCode
                    });

                    int glSeq = 1;
                    await _context.CfnGldetails.AddRangeAsync(details.Select(d => new CfnGldetail
                    {
                        Accperiod = accountingPeriod,
                        Accountcode = d.Accountcode,
                        Subaccountcode = d.Subaccountcode,
                        CtrlOnholdno = onHoldNo,
                        CtrlSequenceno = glSeq++,
                        VchrDate = vDate,
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
                _logger.LogError(ex, "Error saving JIN Purchase (OnHold): {Msg}", ex.Message);
                throw new ApplicationException($"Error saving JIN Purchase (OnHold): {ex.Message}", ex);
            }
        }

        public async Task<PostMultipleResult> PostMultiplePurchaseBillsAsync(List<string> onHoldNumbers, string accountingPeriod, string username, string locationCode)
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
                    var header = await _context.CfnPurchasejnls.FirstOrDefaultAsync(x => x.CtrlOnholdno == onHoldNo);
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
                    var voucherDate = header.VchrDate ?? now;
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
                    CfnPurjdetail x => x.CtrlOnholdno == onHoldNo,
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
        /// <summary>
        /// Corrects and validates the payload in place, and returns the NET bill
        /// amount for Billbalance / Releaseamt.
        ///
        /// Everything here is derived from the database, not from the browser.
        /// </summary>
        private async Task<decimal> NormalizePurchaseBillAsync(PurchaseBillsRequestDto request)
        {
            var details = request.Details ?? new List<PurjDetailsDto>();
            var bill = request.BillDetails;

            var groupAccount = request.VoucherData?.BankCode?.Trim();      // e.g. L061300
            var vendorCode = request.VoucherData?.BankAccount?.Trim();     // e.g. DEB034

            /* ---- 1. Undo the vendor / account inversion --------------------
               The component sends bankBlock.bankAccount (the vendor) as
               accountCode with a null subAccountCode. Detect that and put each
               value in its proper column. */
            if (!string.IsNullOrWhiteSpace(groupAccount) && !string.IsNullOrWhiteSpace(vendorCode))
            {
                foreach (var line in details)
                {
                    var isCredit = string.Equals(line.DbCrFlag, "C", StringComparison.OrdinalIgnoreCase);
                    var accIsVendor = string.Equals(line.AccountCode?.Trim(), vendorCode,
                                                    StringComparison.OrdinalIgnoreCase);

                    if (isCredit && accIsVendor && string.IsNullOrWhiteSpace(line.SubAccountCode))
                    {
                        line.AccountCode = groupAccount;
                        line.SubAccountCode = vendorCode;

                        _logger.LogWarning(
                            "Purchase bill {OnHold}: vendor credit line corrected - accountCode was "
                          + "'{Wrong}' (a vendor code); rewritten as {Account}/{Vendor}.",
                            request.VoucherData?.CtrlOnHoldNo, vendorCode, groupAccount, vendorCode);
                    }
                }
            }

            if (bill == null) return 0m;

            /* ---- 2. Derive TDS from cfn_tds, never from the browser --------
               Coreman reads the rate:
                   SELECT isnull(CFN_TDS.TDSPERC, 0) FROM CFN_TDS WHERE TDSCODE = ?
               The UI lets TDS Amount be typed directly, which is how 2,000 was
               saved with no taxable base behind it. */
            var grossBill = bill.BillAmount ?? 0m;
            var tdsCode = bill.TDSCode?.Trim();
            var tdsAmount = bill.TDSAmount ?? 0m;

            if (!string.IsNullOrWhiteSpace(tdsCode))
            {
                var tdsRow = await _context.CfnTds.AsNoTracking()
                    .Where(t => t.Tdscode == tdsCode)
                    .Select(t => new { t.Tdsperc, t.Accountcode })
                    .FirstOrDefaultAsync();

                if (tdsRow == null)
                    throw new ApplicationException($"TDS code '{tdsCode}' does not exist.");

                var pct = tdsRow.Tdsperc ?? 0m;

                // Taxable base. When the form left it blank, fall back to the
                // sum of the non-tax debit lines - which is what the base is.
                var baseAmount = bill.DeduAmount ?? 0m;
                if (baseAmount <= 0m)
                {
                    baseAmount = details
                        .Where(d => string.Equals(d.DbCrFlag, "D", StringComparison.OrdinalIgnoreCase)
                                 && !IsTaxAccount(d.AccountCode))
                        .Sum(d => d.DrCrAmount ?? 0m);

                    bill.DeduAmount = baseAmount;   // persisted to cfn_bill.tdsdedamount
                }

                var derived = Math.Round(baseAmount * pct / 100m, 2, MidpointRounding.AwayFromZero);

                if (tdsAmount != derived)
                {
                    _logger.LogWarning(
                        "Purchase bill {OnHold}: TDS recalculated. Posted {Posted}, derived {Derived} "
                      + "({Base} x {Pct}% for code {Code}).",
                        request.VoucherData?.CtrlOnHoldNo, tdsAmount, derived, baseAmount, pct, tdsCode);
                }

                tdsAmount = derived;
                bill.TDSAmount = derived;
            }
            else
            {
                tdsAmount = 0m;
                bill.TDSAmount = 0m;
            }

            /* ---- 3. The voucher must balance ------------------------------- */
            var debit = details
                .Where(d => string.Equals(d.DbCrFlag, "D", StringComparison.OrdinalIgnoreCase))
                .Sum(d => d.DrCrAmount ?? 0m);

            var credit = details
                .Where(d => string.Equals(d.DbCrFlag, "C", StringComparison.OrdinalIgnoreCase))
                .Sum(d => d.DrCrAmount ?? 0m);

            if (Math.Round(debit, 2) != Math.Round(credit, 2))
                throw new ApplicationException(
                    $"Voucher does not balance: debit {debit:N2} against credit {credit:N2}.");

            if (grossBill > 0m && Math.Round(grossBill, 2) != Math.Round(debit, 2))
                throw new ApplicationException(
                    $"Bill amount {grossBill:N2} does not equal total debit {debit:N2}.");

            /* ---- 4. Net payable, for Billbalance and Releaseamt ------------- */
            return grossBill - tdsAmount;
        }

        /// <summary>
        /// GST / tax receivable accounts, excluded from the TDS taxable base.
        /// A070211 CGST, A070212 SGST, A070213 IGST - the same accounts used by
        /// OnHoldMultipleGINAsync when it splits GST out.
        /// </summary>
        private static bool IsTaxAccount(string? accountCode)
        {
            var code = accountCode?.Trim().ToUpperInvariant();
            return code is "A070211" or "A070212" or "A070213";
        }
    }
}
