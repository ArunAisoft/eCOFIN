using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class TdsReportService : ITdsReportService
    {
        private readonly BilzFinDbContext _context;

        public TdsReportService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TdsAccountDto>> GetTdsAccountsAsync()
        {
            try
            {
                return await _context.CfnAccounts
                    .AsNoTracking()
                    .Where(x => x.Accounttype == "TDS")
                    .OrderBy(x => x.Accountcode)
                    .Select(x => new TdsAccountDto
                    {
                        AccountCode = x.Accountcode,
                        Description = x.Description
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving TDS accounts: " + ex.Message);
            }
        }

        public async Task<IEnumerable<TdsReportRowDto>> GetTdsReportAsync(TdsReportFilterModel filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter.AccountCode))
                    return Enumerable.Empty<TdsReportRowDto>();

                if (!DateTime.TryParse(filter.FromDate, out var fromDate))
                    return Enumerable.Empty<TdsReportRowDto>();

                if (!DateTime.TryParse(filter.ToDate, out var toDate))
                    return Enumerable.Empty<TdsReportRowDto>();

                var accountCodes = filter.AccountCode
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim())
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .Distinct()
                    .ToList();

                if (accountCodes.Count == 0)
                    return Enumerable.Empty<TdsReportRowDto>();

                // ── Parameterised IN clause ───────────────────────────────────────────
                var paramNames = accountCodes.Select((_, i) => $"@p{i}").ToList();
                var inClause = string.Join(", ", paramNames);

                var parameters = new List<object>();
                for (int i = 0; i < accountCodes.Count; i++)
                    parameters.Add(new SqlParameter($"@p{i}", accountCodes[i]));

                parameters.Add(new SqlParameter("@fromDate", fromDate));
                parameters.Add(new SqlParameter("@toDate", toDate));

                // ── SQL: subquery unchanged from working original ─────────────────────
                // Only change: TdsAccount = ginner.accountcode (individual, not combined label)
                var sql = $@"
            SELECT DISTINCT
                ginner.accountcode                           AS TdsAccount,
                g.vchr_number                                AS VchrNumber,
                CONVERT(varchar(10), g.vchr_date, 120)       AS VchrDate,
                g.subaccountcode                             AS SubAccountCode,
                v.subcodedescription                         AS SubAccountCodeDesc,
                g.dbcrflag                                   AS DbCrFlag,
                b.tdsdedamount                               AS TdsDedAmount,
                b.tdsamount                                  AS TdsAmount,
                b.billamount                                 AS BillAmount,
                b.tdscode                                    AS TdsCode,
                (CASE g.dbcrflag
                    WHEN 'D' THEN -g.voucheramount
                    WHEN 'C' THEN  g.voucheramount
                 END)                                        AS Amount,
                ''                                           AS TdsDescription
            FROM cfn_gldetail g
            LEFT OUTER JOIN cfn_bill b
                ON g.ctrl_onholdno = b.ctrl_onholdno
            INNER JOIN cfn_v_subcodlnk v
                ON g.subaccountcode = v.subcode
            -- join back to get which account code this voucher belongs to
            INNER JOIN cfn_gldetail ginner
                ON  g.ctrl_onholdno  = ginner.ctrl_onholdno
                AND ginner.accountcode IN ({inClause})
                AND ginner.vchr_date BETWEEN @fromDate AND @toDate
            WHERE
                g.subaccountcode IS NOT NULL
                AND LEN(RTRIM(LTRIM(g.subaccountcode))) > 0
            ORDER BY ginner.accountcode, g.vchr_number";

                var rows = await _context.Database
                    .SqlQueryRaw<TdsReportRawRow>(sql, parameters.ToArray())
                    .ToListAsync();

                return rows.Select(r => new TdsReportRowDto
                {
                    TdsAccount = r.TdsAccount,
                    VchrNumber = r.VchrNumber,
                    VchrDate = r.VchrDate,
                    SubAccountCode = r.SubAccountCode,
                    SubAccountCodeDesc = r.SubAccountCodeDesc,
                    DbCrFlag = r.DbCrFlag,
                    TdsDedAmount = r.TdsDedAmount,
                    TdsAmount = r.TdsAmount,
                    BillAmount = r.BillAmount,
                    TdsCode = r.TdsCode,
                    Amount = r.Amount,
                    TdsDescription = r.TdsDescription
                });
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error executing TDS report: " + ex.Message);
            }
        }

        public async Task<IEnumerable<VoucherDetailDto>> GetVoucherDetailsAsync(string vchrNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(vchrNumber))
                    return Enumerable.Empty<VoucherDetailDto>();

                var rows = await (
                    from g in _context.CfnGldetails.AsNoTracking()
                    join a in _context.CfnAccounts.AsNoTracking()
                        on g.Accountcode equals a.Accountcode into aJoin
                    from a in aJoin.DefaultIfEmpty()
                    join v in _context.CfnVSubcodlnks.AsNoTracking()
                        on g.Subaccountcode equals v.Subcode into vJoin
                    from v in vJoin.DefaultIfEmpty()
                    where g.VchrNumber == vchrNumber
                    orderby g.CtrlSequenceno
                    select new VoucherDetailDto
                    {
                        VchrNumber = g.VchrNumber,
                        VchrDate = g.VchrDate != null
                                             ? g.VchrDate.ToString("yyyy-MM-dd")
                                             : null,
                        AccountCode = g.Accountcode,
                        AccountDesc = a != null ? a.Description : null,
                        SubAccountCode = g.Subaccountcode,
                        SubAccountDesc = v != null ? v.Subcodedescription : null,
                        DbCrFlag = g.Dbcrflag,
                        VoucherAmount = g.Voucheramount,
                        Particulars = g.Linedetails,
                        OnHoldNo = g.CtrlOnholdno
                    }
                ).ToListAsync();

                return rows;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving voucher details: " + ex.Message);
            }
        }
    }

    internal class TdsReportRawRow
    {
        public string? TdsAccount { get; set; }
        public string? VchrNumber { get; set; }
        public string? VchrDate { get; set; }
        public string? SubAccountCode { get; set; }
        public string? SubAccountCodeDesc { get; set; }
        public string? DbCrFlag { get; set; }
        public decimal? TdsDedAmount { get; set; }
        public decimal? TdsAmount { get; set; }
        public decimal? BillAmount { get; set; }
        public string? TdsCode { get; set; }
        public decimal? Amount { get; set; }
        public string? TdsDescription { get; set; }
    }
}