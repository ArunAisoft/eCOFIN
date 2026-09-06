using eCOFIN.Application.DTOs.Dashboard;
using eCOFIN.Application.Interfaces.Dashboard;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCOFIN.Infrastructure.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly BilzFinDbContext _context;
        private readonly ILogger<DashboardService> _logger;

        /// <summary>
        /// Maps cfn_vchrtype.VOUCHERGROUP onto an account-type bucket.
        ///
        /// The previous version keyed this on VCHR_SYSCATEGORY and mixed group
        /// codes (BNKP, CASP) with type codes (DEB, OTH, BCCON). Verified
        /// against the database, VOUCHERGROUP is the correct level: ADJV, BNKP,
        /// BNKR, CASP, CASR, CHQR, CONT, CRDT, DEBT, JRNL, MEMO, PAYBR, PAYCR,
        /// RETM, REVC, RJV, SALV, TRVL.
        /// </summary>
        private static readonly Dictionary<string, string> GroupAccountType =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["BNKP"] = "BNKC",
                ["BNKR"] = "BNKC",
                ["CASP"] = "BNKC",
                ["CASR"] = "BNKC",
                ["CONT"] = "BNKC",
                ["CHQR"] = "BNKC",
                ["PAYBR"] = "BNKC",
                ["PAYCR"] = "BNKC",
                ["SALV"] = "DEBT",
                ["DEBT"] = "DEBT",
                ["CRDT"] = "CRDT",
                ["JRNL"] = "CRDT",
                ["ADJV"] = "BNKC",
                ["MEMO"] = "BNKC",
                ["RETM"] = "BNKC",
                ["REVC"] = "BNKC",
                ["RJV"] = "BNKC",
                ["TRVL"] = "BNKC",
            };

        /// <summary>
        /// Status values verified against cfn_gldetail:
        ///   'Post' 2,772,123 rows, 'Hold' 1,104 rows. 'ONHOLD' does not exist.
        /// The previous code filtered on "ONHOLD", so every on-hold figure was
        /// permanently zero.
        /// </summary>
        private const string StatusPost = "Post";
        private const string StatusHold = "Hold";

        /// <summary>Rolling window for the trend chart. The calendar holds 269 periods.</summary>
        private const int TrendPeriods = 24;

        public DashboardService(BilzFinDbContext context, ILogger<DashboardService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ── Shared helpers ────────────────────────────────────────────────────

        private static decimal ComputeBalance(
            decimal? closing,
            decimal? onholdDr, decimal? postedDr,
            decimal? onholdCr, decimal? postedCr)
            => (closing ?? 0m)
             + ((onholdDr ?? 0m) - (postedDr ?? 0m))
             - ((onholdCr ?? 0m) - (postedCr ?? 0m));

        /// <summary>
        /// Returns the latest active accounting period (highest Sequence among CLOSD/OPNPR).
        /// Used as the default when no accPeriod is supplied by the caller.
        /// </summary>
        private async Task<string?> GetLatestPeriodAsync(CancellationToken ct) =>
            await _context.CfnAccncalenders.AsNoTracking()
                .Where(c => c.Periodstate == "CLOSD" || c.Periodstate == "OPNPR")
                .OrderByDescending(c => c.Sequence)
                .Select(c => c.Accperiod)
                .FirstOrDefaultAsync(ct);

        /// <summary>
        /// Converts an Indian financial year label ("2026-27") into the
        /// inclusive cfn_accncalender.SEQUENCE range it covers.
        ///
        /// SEQUENCE is yyyyMM, so FY 2026-27 runs 202604 (April 2026) through
        /// 202703 (March 2027). Returns null when the label cannot be parsed,
        /// in which case no financial-year filter is applied.
        /// </summary>
        private static (decimal Start, decimal End)? FinYearRange(string? finYear)
        {
            if (string.IsNullOrWhiteSpace(finYear)) return null;

            var text = finYear.Trim();
            var dash = text.IndexOfAny(new[] { '-', '/' });
            var startText = dash > 0 ? text[..dash] : text;

            if (!int.TryParse(startText.Trim(), out var startYear)) return null;
            if (startYear < 1900 || startYear > 2200) return null;

            return (startYear * 100 + 4, (startYear + 1) * 100 + 3);
        }

        /// <summary>
        /// Lists the financial years that actually have active periods, newest
        /// first, so the UI never offers an empty year.
        /// </summary>
        private async Task<List<string>> GetFinancialYearsAsync(CancellationToken ct)
        {
            var sequences = await _context.CfnAccncalenders.AsNoTracking()
                .Where(c => c.Periodstate == "CLOSD" || c.Periodstate == "OPNPR")
                .Select(c => c.Sequence)
                .ToListAsync(ct);

            var years = new SortedSet<int>();
            foreach (var seq in sequences)
            {
                var n = (int)seq;
                var year = n / 100;
                var month = n % 100;
                if (year < 1900 || month < 1 || month > 12) continue;

                // Jan-Mar belong to the financial year that started the prior April.
                years.Add(month >= 4 ? year : year - 1);
            }

            return years.Reverse().Select(y => $"{y}-{(y + 1) % 100:D2}").ToList();
        }

        /// <summary>
        /// Builds a period scope filtered to active periods.
        ///
        /// accPeriod is the most specific filter and wins when supplied.
        /// Otherwise finYear narrows to that financial year, and with neither
        /// the scope is all active (CLOSD + OPNPR) periods.
        /// </summary>
        private IQueryable<CfnAccncalender> PeriodScope(string? accPeriod = null, string? finYear = null)
        {
            var q = _context.CfnAccncalenders.AsNoTracking()
                .Where(c => c.Periodstate == "CLOSD" || c.Periodstate == "OPNPR");

            if (!string.IsNullOrWhiteSpace(accPeriod))
                return q.Where(c => c.Accperiod == accPeriod);

            var range = FinYearRange(finYear);
            if (range.HasValue)
                q = q.Where(c => c.Sequence >= range.Value.Start && c.Sequence <= range.Value.End);

            return q;
        }

        // ── GetDashboardSummaryAsync ──────────────────────────────────────────

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(
            string userName,
            string? bankCode = null,
            string? accPeriod = null,
            string? finYear = null,
            CancellationToken ct = default)
        {
            try
            {
                // Auto-select the latest period only when the caller gave
                // neither a period nor a financial year. Defaulting on top of a
                // financial year would collapse it back to a single month.
                if (string.IsNullOrWhiteSpace(accPeriod) && string.IsNullOrWhiteSpace(finYear))
                    accPeriod = await GetLatestPeriodAsync(ct);



                var periodScope = PeriodScope(accPeriod, finYear);

                // Exclude GL lines with no voucher number. GroupBy(VchrNumber)
                // collapses them into a single phantom "voucher": in AUG-2026
                // that was 2 orphan lines (one Dr, one Cr of 321,475.60) which
                // inflated TOTAL VOUCHERS from 199 to 200 and produced the
                // entire "ON HOLD - 1 voucher / 3,21,475.6" figure.
                var glQuery = from g in _context.CfnGldetails.AsNoTracking()
                              join c in periodScope on g.Accperiod equals c.Accperiod
                              where g.VchrNumber != null && g.VchrNumber != ""
                              select new
                              {
                                  g.VchrNumber,
                                  g.CtrlOnholdno,
                                  g.VchrType,
                                  g.VchrSyscategory,
                                  g.Voucheramount,
                                  g.Dbcrflag,
                                  g.CtrlStatus,
                                  g.VchrDate,
                                  g.Accountcode,
                                  g.Subaccountcode,
                                  g.Linedetails,
                                  g.CtrlUsername,
                                  g.Accperiod
                              };

                if (!string.IsNullOrWhiteSpace(bankCode))
                {
                    var bankAccountScope = from a in _context.CfnAccounts.AsNoTracking()
                                           join b in _context.CfnBanks.AsNoTracking()
                                               on a.Bankcode equals b.Bankcode
                                           where b.Bankcode == bankCode
                                              && a.Accountstatus == "ACTVE"
                                           select a.Accountcode;

                    glQuery = from g in glQuery
                              join a in bankAccountScope on g.Accountcode equals a
                              select g;
                }

                var glRows = await glQuery.ToListAsync(ct);

                var vtRows = await (
                    from vt in _context.CfnVchrtypes.AsNoTracking()
                    join uv in _context.CfnUservchrs.AsNoTracking()
                        on vt.Vouchertype equals uv.Vouchertype
                    where uv.Username.ToUpper() == userName.ToUpper() && vt.Activestatus == "Y"
                    select new { vt.Vouchertype, vt.Vouchertypedescription, vt.Vouchergroup }
                ).ToListAsync(ct);

                var vtMap = vtRows.DistinctBy(x => x.Vouchertype).ToDictionary(x => x.Vouchertype!, x => x);
                var distinctVouchers = glRows.GroupBy(g => g.VchrNumber).ToList();

                var kpi = new DashboardKpiDto
                {
                    TotalVouchers = distinctVouchers.Count,
                    TotalPostedAmount = (decimal)glRows.Where(g => g.CtrlStatus == StatusPost && g.Dbcrflag == "D").Sum(g => g.Voucheramount),
                    TotalOnHoldAmount = (decimal)glRows.Where(g => g.CtrlStatus == StatusHold && g.Dbcrflag == "D").Sum(g => g.Voucheramount),
                    PostedCount = distinctVouchers.Count(v => v.Any(g => g.CtrlStatus == StatusPost)),
                    OnHoldCount = distinctVouchers.Count(v => v.Any(g => g.CtrlStatus == StatusHold)),
                    DraftCount = distinctVouchers.Count(v => v.Any(g => g.CtrlStatus == "Draft")),
                };

                var bankData = await (
                    from b in _context.CfnBanks.AsNoTracking()
                    join a in _context.CfnAccounts.AsNoTracking() on b.Bankcode equals a.Bankcode
                    where a.Accountstatus == "ACTVE"
                    select new { b.Bankcode, a.Accountcode }
                ).ToListAsync(ct);

                kpi.TotalBanks = bankData.Select(x => x.Bankcode).Distinct().Count();
                kpi.TotalAccounts = bankData.Select(x => x.Accountcode).Distinct().Count();
                kpi.TotalVendors = await _context.CfnAccvendors.AsNoTracking()
                    .Where(av => av.Vendorstatus == "ACTVE").Select(av => av.Vendorcode).Distinct().CountAsync(ct);
                kpi.TotalCustomers = await _context.CfnAcccustomers.AsNoTracking()
                    .Where(ac => ac.Customerstatus == "ACTVE").Select(ac => ac.Customercode).Distinct().CountAsync(ct);

                // Build per-type metrics.
                //
                // Grouped on VCHR_TYPE, not VCHR_SYSCATEGORY. Verified against
                // the database: VCHR_TYPE joins cfn_vchrtype for 100.00% of
                // 2,773,227 GL lines; VCHR_SYSCATEGORY joins for 0.16%.
                // VCHR_SYSCATEGORY holds REGUL / PUBIL / NULL, which are not
                // voucher types at all - YBI appears under both REGUL and
                // PUBIL - and neither exists in cfn_vchrtype. Grouping on it
                // dropped every unmatched voucher, which is why 118 of the 199
                // vouchers in AUG-2026 never reached the breakdown cards.
                var voucherMetrics = new List<VoucherTypeMetricDto>();
                foreach (var typeGrp in glRows
                             .Where(g => !string.IsNullOrWhiteSpace(g.VchrType))
                             .GroupBy(g => g.VchrType!.Trim()))
                {
                    var vchrType = typeGrp.Key;
                    var info = vtMap.GetValueOrDefault(vchrType);
                    var group = info?.Vouchergroup?.Trim() ?? string.Empty;

                    var vouchers = typeGrp.GroupBy(g => g.VchrNumber).ToList();
                    var drRows = typeGrp.Where(g => g.Dbcrflag == "D").ToList();

                    var total = vouchers.Count;

                    // A voucher counts as posted only when ALL its lines are
                    // posted. The previous version used Any() for both posted
                    // and on-hold, so a mixed-status voucher was counted twice
                    // and DraftCount went negative before Math.Max clamped it
                    // to zero - hiding the double count instead of fixing it.
                    var posted = vouchers.Count(v => v.All(g => g.CtrlStatus == StatusPost));
                    var onhold = vouchers.Count(v => v.Any(g => g.CtrlStatus == StatusHold));
                    var draft = Math.Max(0, total - posted - onhold);

                    voucherMetrics.Add(new VoucherTypeMetricDto
                    {
                        VoucherSysCategory = vchrType,
                        VoucherType = vchrType,
                        Description = info?.Vouchertypedescription?.Trim() ?? vchrType,
                        VoucherGroup = group,
                        AccountType = GroupAccountType.GetValueOrDefault(group, "BNKC"),
                        TotalCount = total,
                        PostedCount = posted,
                        OnHoldCount = onhold,
                        DraftCount = draft,
                        PostedAmount = (decimal)drRows.Where(g => g.CtrlStatus == StatusPost).Sum(g => g.Voucheramount),
                        OnHoldAmount = (decimal)drRows.Where(g => g.CtrlStatus == StatusHold).Sum(g => g.Voucheramount),
                        PostedPercent = total > 0 ? Math.Round((decimal)posted / total * 100, 1) : 0,
                        OnHoldPercent = total > 0 ? Math.Round((decimal)onhold / total * 100, 1) : 0,
                        MonthlyVolume = typeGrp.GroupBy(g => g.Accperiod)
                            .Select(pg => new MonthlyVolumeDto
                            {
                                AccPeriod = pg.Key!,
                                Count = pg.Select(x => x.VchrNumber).Distinct().Count(),
                                Amount = (decimal)pg.Where(x => x.Dbcrflag == "D").Sum(x => x.Voucheramount)
                            })
                            .OrderBy(x => x.AccPeriod).ToList()
                    });
                }

                await EnrichLinkedAccountsAsync(voucherMetrics, userName, ct);

                var bankSummary = await GetBankSummaryAsync(userName, bankCode, null, ct);
                var trend = await GetMonthlyTrendAsync(null, null, bankCode, finYear, ct);
                var recent = await GetRecentVouchersAsync(null, bankCode, null, 20, null, ct);

                return new DashboardSummaryDto
                {
                    FinancialYears = await GetFinancialYearsAsync(ct),
                    SelectedFinYear = finYear,
                    Kpi = kpi,
                    VoucherMetrics = voucherMetrics.OrderByDescending(x => x.TotalCount).ToList(),
                    BankSummary = bankSummary,
                    MonthlyTrend = trend,
                    RecentVouchers = recent
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error building dashboard summary.");
                throw new ApplicationException("Error building dashboard summary.", ex);
            }
        }

        // ── GetVoucherTypeDrillAsync ──────────────────────────────────────────

        public async Task<VoucherTypeDrillDto> GetVoucherTypeDrillAsync(
            string voucherSysCategory,
            string userName,
            string? bankCode = null,
            string? accPeriod = null,
            CancellationToken ct = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    accPeriod = await GetLatestPeriodAsync(ct);

                var periodScope = PeriodScope(accPeriod);

                var glQuery = from g in _context.CfnGldetails.AsNoTracking()
                              join c in periodScope on g.Accperiod equals c.Accperiod
                              where g.VchrType == voucherSysCategory
                                 && g.VchrNumber != null && g.VchrNumber != ""
                              select new
                              {
                                  g.VchrNumber,
                                  g.CtrlOnholdno,
                                  g.VchrType,
                                  g.VchrSyscategory,
                                  g.Voucheramount,
                                  g.Dbcrflag,
                                  g.CtrlStatus,
                                  g.VchrDate,
                                  g.Accountcode,
                                  g.Subaccountcode,
                                  g.Linedetails,
                                  g.CtrlUsername,
                                  g.Accperiod
                              };

                if (!string.IsNullOrWhiteSpace(bankCode))
                {
                    var bankAccScope = from a in _context.CfnAccounts.AsNoTracking()
                                       join b in _context.CfnBanks.AsNoTracking() on a.Bankcode equals b.Bankcode
                                       where b.Bankcode == bankCode && a.Accountstatus == "ACTVE"
                                       select a.Accountcode;
                    glQuery = from g in glQuery join a in bankAccScope on g.Accountcode equals a select g;
                }

                var glRows = await glQuery.ToListAsync(ct);
                var vtInfo = await _context.CfnVchrtypes.AsNoTracking()
                    .Where(vt => vt.Vouchertype == voucherSysCategory)
                    .Select(vt => new { vt.Vouchertypedescription, vt.Vouchergroup })
                    .FirstOrDefaultAsync(ct);

                var vouchers = glRows.GroupBy(g => g.VchrNumber).ToList();
                var drRows = glRows.Where(g => g.Dbcrflag == "D").ToList();
                var total = vouchers.Count;
                var posted = vouchers.Count(v => v.Any(g => g.CtrlStatus == StatusPost));
                var onhold = vouchers.Count(v => v.Any(g => g.CtrlStatus == StatusHold));
                var draft = Math.Max(0, total - posted - onhold);
                var postedAmt = drRows.Where(g => g.CtrlStatus == StatusPost).Sum(g => g.Voucheramount);
                var holdAmt = drRows.Where(g => g.CtrlStatus == StatusHold).Sum(g => g.Voucheramount);

                var linkedAccounts = await (
                    from vsd in _context.CfnVouchersysdata.AsNoTracking()
                    join a in _context.CfnAccounts.AsNoTracking() on vsd.Code equals a.Accountcode
                    join b in _context.CfnBanks.AsNoTracking() on a.Bankcode equals b.Bankcode
                    join gl in _context.CfnGeneralledgers.AsNoTracking() on a.Accountcode equals gl.Accountcode
                    join c in periodScope on gl.Accperiod equals c.Accperiod
                    where vsd.Vouchertype == voucherSysCategory && a.Accountstatus == "ACTVE"
                    group new { gl, b, a } by new { b.Bankcode, b.Name, a.Accountcode, a.Description, a.Accounttype } into grp
                    select new LinkedBankAccountDto
                    {
                        BankCode = grp.Key.Bankcode ?? string.Empty,
                        BankName = grp.Key.Name ?? string.Empty,
                        AccountCode = grp.Key.Accountcode ?? string.Empty,
                        AccountName = grp.Key.Description ?? string.Empty,
                        AccountType = grp.Key.Accounttype ?? string.Empty,
                        Balance = grp.OrderByDescending(x => x.gl.Accperiod)
                                         .Select(x => ComputeBalance(
                                             x.gl.Postedclosingbalance,
                                             x.gl.Onholddebitamount, x.gl.Posteddebitamount,
                                             x.gl.Onholdcreditamount, x.gl.Postedcreditamount))
                                         .FirstOrDefault()
                    }
                ).ToListAsync(ct);

                var monthlyVolume = glRows
                    .GroupBy(g => g.Accperiod)
                    .Select(pg => new MonthlyVolumeDto
                    {
                        AccPeriod = pg.Key!,
                        Count = pg.Select(x => x.VchrNumber).Distinct().Count(),
                        Amount = (decimal)pg.Where(x => x.Dbcrflag == "D").Sum(x => x.Voucheramount)
                    })
                    .OrderBy(x => x.AccPeriod).ToList();

                var recent = glRows
                    .GroupBy(g => g.VchrNumber)
                    .OrderByDescending(g => g.Max(x => x.VchrDate))
                    .Take(10)
                    .Select(g =>
                    {
                        var first = g.OrderBy(x => x.Voucheramount).Last();
                        return new RecentVoucherDto
                        {
                            VoucherNo = g.Key ?? string.Empty,
                            OnHoldNo = first.CtrlOnholdno ?? string.Empty,
                            VoucherSysCat = voucherSysCategory,
                            VoucherGroup = vtInfo?.Vouchergroup ?? string.Empty,
                            Description = first.Linedetails ?? string.Empty,
                            AccountCode = first.Accountcode ?? string.Empty,
                            SubAccountCode = first.Subaccountcode ?? string.Empty,
                            AccPeriod = first.Accperiod ?? string.Empty,
                            VoucherDate = first.VchrDate,
                            Amount = (decimal)g.Where(x => x.Dbcrflag == "D").Sum(x => x.Voucheramount),
                            DbCrFlag = first.Dbcrflag ?? string.Empty,
                            CtrlStatus = first.CtrlStatus ?? string.Empty,
                            CreatedBy = first.CtrlUsername ?? string.Empty,
                        };
                    }).ToList();

                return new VoucherTypeDrillDto
                {
                    VoucherSysCategory = voucherSysCategory,
                    Description = vtInfo?.Vouchertypedescription ?? voucherSysCategory,
                    VoucherGroup = vtInfo?.Vouchergroup ?? string.Empty,
                    Kpi = new DashboardKpiDto
                    {
                        TotalVouchers = total,
                        TotalPostedAmount = (decimal)postedAmt,
                        TotalOnHoldAmount = (decimal)holdAmt,
                        PostedCount = posted,
                        OnHoldCount = onhold,
                        DraftCount = draft
                    },
                    StatusBreakdown = new StatusBreakdownDto
                    {
                        PostedCount = posted,
                        OnHoldCount = onhold,
                        DraftCount = draft,
                        PostedAmount = (decimal)postedAmt,
                        OnHoldAmount = (decimal)holdAmt,
                        PostedPct = total > 0 ? Math.Round((decimal)posted / total * 100, 1) : 0,
                        OnHoldPct = total > 0 ? Math.Round((decimal)onhold / total * 100, 1) : 0,
                        DraftPct = total > 0 ? Math.Round((decimal)draft / total * 100, 1) : 0,
                    },
                    LinkedAccounts = linkedAccounts,
                    MonthlyVolume = monthlyVolume,
                    RecentVouchers = recent
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving drill-down for {SysCat}.", voucherSysCategory);
                throw new ApplicationException($"Error retrieving drill-down for {voucherSysCategory}.", ex);
            }
        }

        // ── GetMonthlyTrendAsync ──────────────────────────────────────────────

        /// <summary>
        /// Monthly trend, aggregated in SQL over a rolling window.
        ///
        /// Three problems fixed here:
        ///
        /// 1. The join to cfn_vchrtype was an INNER join on VCHR_SYSCATEGORY.
        ///    REGUL and PUBIL do not exist in cfn_vchrtype, so it matched 0.16%
        ///    of rows and returned nothing - the chart rendered a blank panel
        ///    because buildLineChart() returns early on an empty series. It is
        ///    now a LEFT join on VCHR_TYPE, with unmatched rows bucketed as
        ///    UNCLASSIFIED rather than silently dropped.
        ///
        /// 2. The caller passes null for accPeriod meaning "all active periods",
        ///    but this method immediately overwrote null with the latest period,
        ///    so a "monthly trend" could only ever plot one point.
        ///
        /// 3. It pulled every matching GL row with ToListAsync and grouped in
        ///    memory. The calendar holds 269 active periods over a 2.77 million
        ///    row ledger, so removing the period default without also moving the
        ///    aggregation into SQL would have loaded most of the GL into memory
        ///    on a 1 GB SQL Express instance. It now aggregates server-side and
        ///    returns at most TrendPeriods rows per voucher group.
        /// </summary>
        public async Task<List<MonthlyTrendDto>> GetMonthlyTrendAsync(
            string? voucherGroup = null,
            string? accPeriod = null,
            string? bankCode = null,
            string? finYear = null,
            CancellationToken ct = default)
        {
            try
            {
                var sql = $@"
                    SELECT  w.ACCPERIOD,
                            w.SEQUENCE,
                            w.PERIODSTATE,
                            ISNULL(NULLIF(LTRIM(RTRIM(vt.VOUCHERGROUP)), ''), 'UNCLASSIFIED') AS VOUCHERGROUP,
                            COUNT(DISTINCT g.VCHR_NUMBER) AS VOUCHERS,
                            SUM(CASE WHEN g.CTRL_STATUS = @post THEN ISNULL(g.VOUCHERAMOUNT, 0) ELSE 0 END) AS POSTEDAMOUNT,
                            SUM(CASE WHEN g.CTRL_STATUS = @hold THEN ISNULL(g.VOUCHERAMOUNT, 0) ELSE 0 END) AS ONHOLDAMOUNT
                    FROM (
                            SELECT TOP ({TrendPeriods}) c.ACCPERIOD, c.SEQUENCE, c.PERIODSTATE
                            FROM   dbo.cfn_accncalender c
                            WHERE  c.PERIODSTATE IN ('CLOSD', 'OPNPR')
                              AND  (@period IS NULL OR c.ACCPERIOD = @period)
                              AND  (@fyStart IS NULL OR c.SEQUENCE >= @fyStart)
                              AND  (@fyEnd   IS NULL OR c.SEQUENCE <= @fyEnd)
                            ORDER BY c.SEQUENCE DESC
                         ) w
                    JOIN      dbo.cfn_gldetail g  ON g.ACCPERIOD   = w.ACCPERIOD
                    LEFT JOIN dbo.cfn_vchrtype vt ON vt.VOUCHERTYPE = g.VCHR_TYPE
                    WHERE     g.DBCRFLAG = 'D'
                      AND     g.VCHR_NUMBER IS NOT NULL
                      AND     LTRIM(RTRIM(g.VCHR_NUMBER)) <> ''
                      AND     (@grp IS NULL OR vt.VOUCHERGROUP = @grp)
                      AND     (@bank IS NULL OR EXISTS (
                                  SELECT 1
                                  FROM   dbo.cfn_account a
                                  WHERE  a.ACCOUNTCODE = g.ACCOUNTCODE
                                    AND  a.BANKCODE    = @bank
                                    AND  a.ACCOUNTSTATUS = 'ACTVE'))
                    GROUP BY  w.ACCPERIOD, w.SEQUENCE, w.PERIODSTATE,
                              ISNULL(NULLIF(LTRIM(RTRIM(vt.VOUCHERGROUP)), ''), 'UNCLASSIFIED')
                    ORDER BY  w.SEQUENCE, VOUCHERGROUP;";

                var rows = new List<(string Period, int Sequence, string State,
                                     string Group, int Count, decimal Posted, decimal Hold)>();

                var conn = _context.Database.GetDbConnection();
                var opened = false;
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        await _context.Database.OpenConnectionAsync(ct);
                        opened = true;
                    }

                    await using var cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 120;

                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter(
                        "@post", System.Data.SqlDbType.VarChar, 5)
                    { Value = StatusPost });
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter(
                        "@hold", System.Data.SqlDbType.VarChar, 5)
                    { Value = StatusHold });
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter(
                        "@period", System.Data.SqlDbType.VarChar, 10)
                    { Value = string.IsNullOrWhiteSpace(accPeriod) ? DBNull.Value : accPeriod.Trim() });
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter(
                        "@grp", System.Data.SqlDbType.VarChar, 5)
                    { Value = string.IsNullOrWhiteSpace(voucherGroup) ? DBNull.Value : voucherGroup.Trim() });
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter(
                        "@bank", System.Data.SqlDbType.VarChar, 5)
                    { Value = string.IsNullOrWhiteSpace(bankCode) ? DBNull.Value : bankCode.Trim() });

                    var fy = FinYearRange(finYear);
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter(
                        "@fyStart", System.Data.SqlDbType.Decimal)
                    { Value = fy.HasValue ? fy.Value.Start : (object)DBNull.Value });
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter(
                        "@fyEnd", System.Data.SqlDbType.Decimal)
                    { Value = fy.HasValue ? fy.Value.End : (object)DBNull.Value });

                    await using var rdr = await cmd.ExecuteReaderAsync(ct);
                    while (await rdr.ReadAsync(ct))
                    {
                        rows.Add((
                            rdr.IsDBNull(0) ? string.Empty : rdr.GetString(0).TrimEnd(),
                            rdr.IsDBNull(1) ? 0 : Convert.ToInt32(rdr.GetValue(1)),
                            rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2).TrimEnd(),
                            rdr.IsDBNull(3) ? "UNCLASSIFIED" : rdr.GetString(3).TrimEnd(),
                            rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4),
                            rdr.IsDBNull(5) ? 0m : Convert.ToDecimal(rdr.GetValue(5)),
                            rdr.IsDBNull(6) ? 0m : Convert.ToDecimal(rdr.GetValue(6))));
                    }
                }
                finally
                {
                    if (opened) await _context.Database.CloseConnectionAsync();
                }

                return rows
                    .GroupBy(r => new { r.Period, r.Sequence, r.State })
                    .OrderBy(p => p.Key.Sequence)
                    .Select(p => new MonthlyTrendDto
                    {
                        AccPeriod = p.Key.Period,
                        Sequence = p.Key.Sequence,
                        PeriodState = p.Key.State,
                        Groups = p.Select(g => new GroupTrendDto
                        {
                            VoucherGroup = g.Group,
                            Count = g.Count,
                            PostedAmount = g.Posted,
                            OnHoldAmount = g.Hold
                        })
                        .OrderBy(g => g.VoucherGroup).ToList()
                    }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving monthly trend.");
                throw new ApplicationException("Error retrieving monthly trend.", ex);
            }
        }

        // ── GetBankSummaryAsync ───────────────────────────────────────────────

        public async Task<List<BankSummaryDto>> GetBankSummaryAsync(
            string userName,
            string? bankCode = null,
            string? accPeriod = null,
            CancellationToken ct = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    accPeriod = await GetLatestPeriodAsync(ct);

                var periodScope = PeriodScope(accPeriod);

                var accScope = from a in _context.CfnAccounts.AsNoTracking()
                               join b in _context.CfnBanks.AsNoTracking() on a.Bankcode equals b.Bankcode
                               where a.Accountstatus == "ACTVE"
                               select new { a, b };

                if (!string.IsNullOrWhiteSpace(bankCode))
                    accScope = accScope.Where(x => x.b.Bankcode == bankCode);

                var accRows = await accScope
                    .Select(x => new { x.a.Accountcode, x.a.Description, x.a.Accounttype, x.a.Bankcode, BankName = x.b.Name, x.b.Objectstatus })
                    .ToListAsync(ct);

                if (!accRows.Any()) return [];

                var activeAccScope = from aa in _context.CfnAccounts.AsNoTracking()
                                     join bb in _context.CfnBanks.AsNoTracking() on aa.Bankcode equals bb.Bankcode
                                     where aa.Accountstatus == "ACTVE"
                                     select aa;

                var glRows = await (
                    from g in _context.CfnGeneralledgers.AsNoTracking()
                    join a in activeAccScope on g.Accountcode equals a.Accountcode
                    join c in periodScope on g.Accperiod equals c.Accperiod
                    select new
                    {
                        g.Accountcode,
                        c.Sequence,
                        g.Postedclosingbalance,
                        g.Postedopeningbalance,
                        g.Onholddebitamount,
                        g.Onholdcreditamount,
                        g.Posteddebitamount,
                        g.Postedcreditamount
                    }
                ).ToListAsync(ct);

                var balanceMap = glRows.GroupBy(g => g.Accountcode!).ToDictionary(g => g.Key, g =>
                {
                    var latest = g.OrderByDescending(x => x.Sequence).First();
                    return new
                    {
                        Balance = ComputeBalance(latest.Postedclosingbalance, latest.Onholddebitamount, latest.Posteddebitamount, latest.Onholdcreditamount, latest.Postedcreditamount),
                        PostedDr = g.Sum(x => x.Posteddebitamount ?? 0m),
                        PostedCr = g.Sum(x => x.Postedcreditamount ?? 0m),
                        OnHoldDr = g.Sum(x => x.Onholddebitamount ?? 0m),
                        OnHoldCr = g.Sum(x => x.Onholdcreditamount ?? 0m),
                    };
                });

                var vtByAcc = await (
                    from uv in _context.CfnUservchrs.AsNoTracking()
                    join vsd in _context.CfnVouchersysdata.AsNoTracking() on uv.Vouchertype equals vsd.Vouchertype
                    join vt in _context.CfnVchrtypes.AsNoTracking() on vsd.Vouchertype equals vt.Vouchertype
                    join a in activeAccScope on vsd.Code equals a.Accountcode
                    where uv.Username.ToUpper() == userName.ToUpper() && vt.Activestatus == "Y"
                    select new { a.Accountcode, vt.Vouchertype }
                ).ToListAsync(ct);

                var vtLookup = vtByAcc.GroupBy(x => x.Accountcode!).ToDictionary(g => g.Key, g => g.Select(x => x.Vouchertype!).Distinct().ToList());

                return accRows.GroupBy(a => a.Bankcode!).Select(bankGrp =>
                {
                    var accounts = bankGrp.Select(a =>
                    {
                        var gl = balanceMap.GetValueOrDefault(a.Accountcode!);
                        return new BankAccountSummaryDto
                        {
                            AccountCode = a.Accountcode ?? string.Empty,
                            AccountName = a.Description ?? string.Empty,
                            AccountType = a.Accounttype ?? string.Empty,
                            Balance = gl?.Balance ?? 0m,
                            PostedDebit = gl?.PostedDr ?? 0m,
                            PostedCredit = gl?.PostedCr ?? 0m,
                            OnHoldDebit = gl?.OnHoldDr ?? 0m,
                            OnHoldCredit = gl?.OnHoldCr ?? 0m,
                            VoucherTypes = vtLookup.GetValueOrDefault(a.Accountcode ?? string.Empty) ?? []
                        };
                    }).ToList();

                    var first = bankGrp.First();
                    return new BankSummaryDto
                    {
                        BankCode = bankGrp.Key,
                        BankName = first.BankName ?? string.Empty,
                        ObjectStatus = first.Objectstatus ?? string.Empty,
                        AccountCount = accounts.Count,
                        TotalBalance = accounts.Sum(a => a.Balance),
                        PostedDebit = accounts.Sum(a => a.PostedDebit),
                        PostedCredit = accounts.Sum(a => a.PostedCredit),
                        OnHoldDebit = accounts.Sum(a => a.OnHoldDebit),
                        OnHoldCredit = accounts.Sum(a => a.OnHoldCredit),
                        Accounts = accounts
                    };
                }).OrderBy(b => b.BankCode).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bank summary.");
                throw new ApplicationException("Error retrieving bank summary.", ex);
            }
        }

        // ── GetRecentVouchersAsync ────────────────────────────────────────────

        public async Task<List<RecentVoucherDto>> GetRecentVouchersAsync(
            string? voucherSysCategory = null,
            string? bankCode = null,
            string? ctrlStatus = null,
            int top = 20,
            string? accPeriod = null,
            CancellationToken ct = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    accPeriod = await GetLatestPeriodAsync(ct);

                var periodScope = PeriodScope(accPeriod);

                var query = from g in _context.CfnGldetails.AsNoTracking()
                            join c in periodScope on g.Accperiod equals c.Accperiod
                            join vt in _context.CfnVchrtypes.AsNoTracking()
                                on g.VchrType equals vt.Vouchertype into vtj
                            from vt in vtj.DefaultIfEmpty()
                            join a in _context.CfnAccounts.AsNoTracking()
                                on g.Accountcode equals a.Accountcode into aj
                            from a in aj.DefaultIfEmpty()
                            join b in _context.CfnBanks.AsNoTracking()
                                on a.Bankcode equals b.Bankcode into bj
                            from b in bj.DefaultIfEmpty()
                            where g.Dbcrflag == "D"
                            select new
                            {
                                g.VchrNumber,
                                g.CtrlOnholdno,
                                g.VchrType,
                                g.VchrSyscategory,
                                VoucherGroup = vt.Vouchergroup,
                                g.Voucheramount,
                                g.Dbcrflag,
                                g.CtrlStatus,
                                g.VchrDate,
                                g.Accountcode,
                                g.Subaccountcode,
                                g.Linedetails,
                                g.CtrlUsername,
                                g.Accperiod,
                                BankCode = b.Bankcode
                            };

                if (!string.IsNullOrWhiteSpace(voucherSysCategory)) query = query.Where(g => g.VchrType == voucherSysCategory);
                if (!string.IsNullOrWhiteSpace(ctrlStatus)) query = query.Where(g => g.CtrlStatus == ctrlStatus);
                if (!string.IsNullOrWhiteSpace(bankCode)) query = query.Where(g => g.BankCode == bankCode);

                var rows = await query.OrderByDescending(g => g.VchrDate).Take(top * 3).ToListAsync(ct);

                return rows.GroupBy(g => g.VchrNumber).Take(top).Select(g =>
                {
                    var first = g.First();
                    return new RecentVoucherDto
                    {
                        VoucherNo = g.Key ?? string.Empty,
                        OnHoldNo = first.CtrlOnholdno ?? string.Empty,
                        VoucherType = first.VoucherGroup ?? string.Empty,
                        VoucherSysCat = first.VchrType?.Trim() ?? string.Empty,
                        VoucherGroup = first.VoucherGroup ?? string.Empty,
                        Description = first.Linedetails ?? string.Empty,
                        AccountCode = first.Accountcode ?? string.Empty,
                        SubAccountCode = first.Subaccountcode ?? string.Empty,
                        BankCode = first.BankCode ?? string.Empty,
                        AccPeriod = first.Accperiod ?? string.Empty,
                        VoucherDate = first.VchrDate,
                        Amount = (decimal)g.Sum(x => x.Voucheramount),
                        DbCrFlag = first.Dbcrflag ?? string.Empty,
                        CtrlStatus = first.CtrlStatus ?? string.Empty,
                        CreatedBy = first.CtrlUsername ?? string.Empty,
                    };
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving recent vouchers.");
                throw new ApplicationException("Error retrieving recent vouchers.", ex);
            }
        }

        // ── EnrichLinkedAccountsAsync ─────────────────────────────────────────

        private async Task EnrichLinkedAccountsAsync(
            List<VoucherTypeMetricDto> metrics,
            string userName,
            CancellationToken ct)
        {
            if (!metrics.Any()) return;

            var periodScope = PeriodScope();   // all active periods for balance enrichment

            var linkedRows = await (
                from uv in _context.CfnUservchrs.AsNoTracking()
                join vsd in _context.CfnVouchersysdata.AsNoTracking() on uv.Vouchertype equals vsd.Vouchertype
                join vt in _context.CfnVchrtypes.AsNoTracking() on vsd.Vouchertype equals vt.Vouchertype
                join a in _context.CfnAccounts.AsNoTracking() on vsd.Code equals a.Accountcode
                join b in _context.CfnBanks.AsNoTracking() on a.Bankcode equals b.Bankcode
                join gl in _context.CfnGeneralledgers.AsNoTracking() on a.Accountcode equals gl.Accountcode
                join c in periodScope on gl.Accperiod equals c.Accperiod
                where uv.Username == userName && a.Accountstatus == "ACTVE" && vt.Activestatus == "Y"
                group new { gl, b, a, vsd } by new
                {
                    vsd.Vouchertype,
                    b.Bankcode,
                    BankName = b.Name,
                    a.Accountcode,
                    AccName = a.Description,
                    a.Accounttype
                } into grp
                orderby grp.Key.Bankcode
                select new
                {
                    SysCategory = grp.Key.Vouchertype,
                    BankCode = grp.Key.Bankcode,
                    BankName = grp.Key.BankName,
                    AccountCode = grp.Key.Accountcode,
                    AccountName = grp.Key.AccName,
                    AccountType = grp.Key.Accounttype,
                    Balance = grp.OrderByDescending(x => x.gl.Accperiod)
                                     .Select(x => ComputeBalance(
                                         x.gl.Postedclosingbalance,
                                         x.gl.Onholddebitamount, x.gl.Posteddebitamount,
                                         x.gl.Onholdcreditamount, x.gl.Postedcreditamount))
                                     .FirstOrDefault()
                }
            ).ToListAsync(ct);

            var linkedByType = linkedRows.GroupBy(x => x.SysCategory!)
                .ToDictionary(g => g.Key, g => g.Select(x => new LinkedBankAccountDto
                {
                    BankCode = x.BankCode ?? string.Empty,
                    BankName = x.BankName ?? string.Empty,
                    AccountCode = x.AccountCode ?? string.Empty,
                    AccountName = x.AccountName ?? string.Empty,
                    AccountType = x.AccountType ?? string.Empty,
                    Balance = x.Balance
                }).ToList());

            foreach (var m in metrics)
                m.LinkedAccounts = linkedByType.GetValueOrDefault(m.VoucherSysCategory) ?? [];
        }
    }
}