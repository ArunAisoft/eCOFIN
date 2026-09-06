using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class SalesReportService : ISalesReportService
    {
        private readonly BilzFinDbContext _context;

        public SalesReportService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesAccountDto>> GetSalesAccountsAsync()
        {
            try
            {
                return await (
                    from d in _context.CfnSalvdetails.AsNoTracking()
                    join a in _context.CfnAccounts.AsNoTracking()
                        on d.Accountcode equals a.Accountcode
                    where d.Dbcrflag == "C"
                    select new SalesAccountDto
                    {
                        AccountCode = d.Accountcode,
                        Description = a.Description ?? string.Empty
                    })
                    .Distinct()
                    .OrderBy(x => x.AccountCode)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving sales accounts: " + ex.Message);
            }
        }

        public async Task<IEnumerable<SalesReportRowDto>> GetSalesReportAsync(SalesReportFilterModel filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter.AccountCode))
                    return Enumerable.Empty<SalesReportRowDto>();

                if (!DateTime.TryParse(filter.FromDate, out var fromDate) ||
                    !DateTime.TryParse(filter.ToDate, out var toDate))
                    return Enumerable.Empty<SalesReportRowDto>();

                var accountCodes = filter.AccountCode
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .ToList();

                if (!accountCodes.Any())
                    return Enumerable.Empty<SalesReportRowDto>();

                var paramNames = accountCodes.Select((_, i) => $"@p{i}").ToList();
                var inClause = string.Join(", ", paramNames);

                var parameters = new List<object>();
                for (int i = 0; i < accountCodes.Count; i++)
                    parameters.Add(new SqlParameter(paramNames[i], accountCodes[i]));

                parameters.Add(new SqlParameter("@fromDate", fromDate));
                parameters.Add(new SqlParameter("@toDate", toDate));

                var sql = $@"
            SELECT
                sv.CTRL_ACCPERIOD                             AS CtrlAccPeriod,
                sv.CTRL_STATUS                                AS CtrlStatus,
                sv.CTRL_ONHOLDNO                              AS CtrlOnholdno,
                sv.VCHR_NUMBER                                AS VchrNumber,
                CONVERT(varchar(10), sv.VCHR_DATE, 120)       AS VchrDate,
                sv.ACCOUNTCODE                                AS HdrAccountCode,
                accA.DESCRIPTION                              AS HdrAccountDesc,
                sv.SUBACCOUNTCODE                             AS HdrSubAccountCode,
                subA.SUBCODEDESCRIPTION                       AS HdrSubAccountDesc,
                sv.VCHR_NARRATION                             AS VchrNarration,
                CONVERT(varchar(10), sv.VCHR_REFDATE, 120)    AS VchrRefDate,
                sv.VCHR_REFNUMBER                             AS VchrRefNumber,
                b.BILLNO                                      AS BillNo,
                CONVERT(varchar(10), b.BILLDATE, 120)         AS BillDate,
                b.BILLAMOUNT                                  AS BillAmount,
                ISNULL(b.TDSCODE, '')                         AS TdsCode,
                CAST(ISNULL(t.TDSPERC, 0) AS VARCHAR)         AS TdsDescription,
                CAST(0 AS DECIMAL(18,2))                      AS TdsAmount,
                sd.ACCOUNTCODE                                AS DetailAccountCode,
                accB.DESCRIPTION                              AS DetailAccountDesc,
                sd.SUBACCOUNTCODE                             AS DetailSubAccountCode,
                subB.SUBCODEDESCRIPTION                       AS DetailSubAccountDesc,
                sd.DBCRFLAG                                   AS DbCrFlag,
                sd.DBCRAMOUNT                                 AS Amount,
                ISNULL(c.COMPANYNAME, '')                     AS CompanyName
            FROM CFN_SALEVOUCHER sv
            INNER JOIN CFN_SALVDETAIL sd
                ON sv.CTRL_ONHOLDNO = sd.CTRL_ONHOLDNO
            INNER JOIN CFN_ACCOUNT accA
                ON sv.ACCOUNTCODE = accA.ACCOUNTCODE
            INNER JOIN CFN_ACCOUNT accB
                ON sd.ACCOUNTCODE = accB.ACCOUNTCODE
            LEFT JOIN CFN_BILL b
                ON b.CTRL_ONHOLDNO = sd.CTRL_ONHOLDNO
            LEFT JOIN CFN_TDS t
                ON t.TDSCODE = b.TDSCODE
            LEFT JOIN (
                SELECT c.customercode AS subcode, c.customername AS subcodedescription, ac.accountcode
                FROM cfn_customer c JOIN cfn_acccustomer ac ON c.customercode = ac.customercode
                UNION ALL
                SELECT v.vendorcode, v.vendorname, av.accountcode
                FROM cfn_vendor v JOIN cfn_accvendor av ON v.vendorcode = av.vendorcode
                UNION ALL
                SELECT e.employeecode, e.employeename, ae.accountcode
                FROM cfn_employee e JOIN cfn_accemployee ae ON e.employeecode = ae.employeecode
            ) subA ON sv.SUBACCOUNTCODE = subA.subcode AND sv.ACCOUNTCODE = subA.accountcode
            LEFT JOIN (
                SELECT c.customercode AS subcode, c.customername AS subcodedescription, ac.accountcode
                FROM cfn_customer c JOIN cfn_acccustomer ac ON c.customercode = ac.customercode
                UNION ALL
                SELECT v.vendorcode, v.vendorname, av.accountcode
                FROM cfn_vendor v JOIN cfn_accvendor av ON v.vendorcode = av.vendorcode
                UNION ALL
                SELECT e.employeecode, e.employeename, ae.accountcode
                FROM cfn_employee e JOIN cfn_accemployee ae ON e.employeecode = ae.employeecode
            ) subB ON sd.SUBACCOUNTCODE = subB.subcode AND sd.ACCOUNTCODE = subB.accountcode
            LEFT JOIN CFN_COMPANY c ON 1 = 1
            WHERE
                sv.CTRL_STATUS    = 'Post'
                AND sv.VCHR_DATE  BETWEEN @fromDate AND @toDate
                AND sv.ACCOUNTCODE IN ({inClause})
                AND (sv.ACCOUNTCODE + ISNULL(sv.SUBACCOUNTCODE, ''))
                    <> (sd.ACCOUNTCODE + ISNULL(sd.SUBACCOUNTCODE, ''))
            ORDER BY sv.ACCOUNTCODE, sv.VCHR_NUMBER";

                var rows = await _context.Database
                    .SqlQueryRaw<SalesReportRawRow>(sql, parameters.ToArray())
                    .ToListAsync();

                return rows.Select(r => new SalesReportRowDto
                {
                    CtrlAccPeriod = r.CtrlAccPeriod,
                    CtrlStatus = r.CtrlStatus,
                    CtrlOnholdno = r.CtrlOnholdno,
                    VchrNumber = r.VchrNumber,
                    VchrDate = r.VchrDate,
                    HdrAccountCode = r.HdrAccountCode,
                    HdrAccountDesc = r.HdrAccountDesc,
                    HdrSubAccountCode = r.HdrSubAccountCode,
                    HdrSubAccountDesc = r.HdrSubAccountDesc,
                    VchrNarration = r.VchrNarration,
                    VchrRefDate = r.VchrRefDate,
                    VchrRefNumber = r.VchrRefNumber,
                    BillNo = r.BillNo,
                    BillDate = r.BillDate,
                    BillAmount = r.BillAmount,
                    TdsCode = r.TdsCode,
                    TdsDescription = r.TdsDescription,
                    TdsAmount = r.TdsAmount,
                    DetailAccountCode = r.DetailAccountCode,
                    DetailAccountDesc = r.DetailAccountDesc,
                    DetailSubAccountCode = r.DetailSubAccountCode,
                    DetailSubAccountDesc = r.DetailSubAccountDesc,
                    DbCrFlag = r.DbCrFlag,
                    Amount = r.Amount,
                    CompanyName = r.CompanyName
                });
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error executing Sales Report: " + ex.Message);
            }
        }
    }

    internal class SalesReportRawRow
    {
        public string? CtrlAccPeriod { get; set; }
        public string? CtrlStatus { get; set; }
        public string? CtrlOnholdno { get; set; }
        public string? VchrNumber { get; set; }
        public string? VchrDate { get; set; }
        public string? HdrAccountCode { get; set; }
        public string? HdrAccountDesc { get; set; }
        public string? HdrSubAccountCode { get; set; }
        public string? HdrSubAccountDesc { get; set; }
        public string? VchrNarration { get; set; }
        public string? VchrRefDate { get; set; }
        public string? VchrRefNumber { get; set; }
        public string? BillNo { get; set; }
        public string? BillDate { get; set; }
        public decimal? BillAmount { get; set; }
        // ✅ FIX: Changed `internal set` → `set` on both properties
        public string? TdsCode { get; set; }
        // ✅ FIX: Added TdsDescription property (was missing from RawRow entirely)
        public string? TdsDescription { get; set; }
        public decimal? TdsAmount { get; set; }
        public string? DetailAccountCode { get; set; }
        public string? DetailAccountDesc { get; set; }
        public string? DetailSubAccountCode { get; set; }
        public string? DetailSubAccountDesc { get; set; }
        public string? DbCrFlag { get; set; }
        public decimal? Amount { get; set; }
        public string? CompanyName { get; set; }
    }
}