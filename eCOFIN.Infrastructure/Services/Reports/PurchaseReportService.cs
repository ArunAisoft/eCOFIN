using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class PurchaseReportService : IPurchaseReportService
    {
        private readonly BilzFinDbContext _context;

        public PurchaseReportService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurchaseAccountDto>> GetPurchaseAccountsAsync()
        {
            try
            {
                return await (
                    from d in _context.CfnPurjdetails.AsNoTracking()
                    join a in _context.CfnAccounts.AsNoTracking()
                        on d.Accountcode equals a.Accountcode
                    where d.Dbcrflag == "D"
                    select new PurchaseAccountDto
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
                throw new ApplicationException("Error retrieving purchase accounts: " + ex.Message);
            }
        }

        public async Task<IEnumerable<PurchaseReportRowDto>> GetPurchaseReportAsync(PurchaseReportFilterModel filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter.AccountCode))
                    return Enumerable.Empty<PurchaseReportRowDto>();

                if (!DateTime.TryParse(filter.FromDate, out var fromDate) ||
                    !DateTime.TryParse(filter.ToDate, out var toDate))
                    return Enumerable.Empty<PurchaseReportRowDto>();

                var accountCodes = filter.AccountCode
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .ToList();

                if (!accountCodes.Any())
                    return Enumerable.Empty<PurchaseReportRowDto>();

                var paramNames = accountCodes.Select((_, i) => $"@p{i}").ToList();
                var inClause = string.Join(", ", paramNames);

                var parameters = new List<object>();
                for (int i = 0; i < accountCodes.Count; i++)
                    parameters.Add(new SqlParameter(paramNames[i], accountCodes[i]));

                parameters.Add(new SqlParameter("@fromDate", fromDate));
                parameters.Add(new SqlParameter("@toDate", toDate));

                var sql = $@"
            SELECT
                j.CTRL_ACCPERIOD                              AS CtrlAccPeriod,
                j.CTRL_STATUS                                 AS CtrlStatus,
                j.CTRL_ONHOLDNO                               AS CtrlOnholdno,
                j.VCHR_NUMBER                                 AS VchrNumber,
                CONVERT(varchar(10), j.VCHR_DATE, 120)        AS VchrDate,
                j.ACCOUNTCODE                                 AS HdrAccountCode,
                a_hdr.DESCRIPTION                             AS HdrAccountDesc,
                j.SUBACCOUNTCODE                              AS HdrSubAccountCode,
                subA.SUBCODEDESCRIPTION                       AS HdrSubAccountDesc,
                j.VCHR_NARRATION                              AS VchrNarration,
                CONVERT(varchar(10), j.VCHR_REFDATE, 120)     AS VchrRefDate,
                j.VCHR_REFNUMBER                              AS VchrRefNumber,
                b.BILLNO                                      AS BillNo,
                CONVERT(varchar(10), b.BILLDATE, 120)         AS BillDate,
                b.BILLAMOUNT                                  AS BillAmount,
                ISNULL(b.TDSCODE, '')                         AS TdsCode,
                CAST(ISNULL(t.TDSPERC, 0) AS VARCHAR)         AS TdsDescription,
                CAST(0 AS DECIMAL(18,2))                      AS TdsAmount,
                d.ACCOUNTCODE                                 AS DetailAccountCode,
                a_det.DESCRIPTION                             AS DetailAccountDesc,
                d.SUBACCOUNTCODE                              AS DetailSubAccountCode,
                subB.SUBCODEDESCRIPTION                       AS DetailSubAccountDesc,
                d.DBCRFLAG                                    AS DbCrFlag,
                d.DBCRAMOUNT                                  AS Amount
            FROM CFN_PURCHASEJNL j
            INNER JOIN CFN_ACCOUNT a_hdr
                ON a_hdr.ACCOUNTCODE = j.ACCOUNTCODE
            INNER JOIN CFN_PURJDETAIL d
                ON d.CTRL_ONHOLDNO = j.CTRL_ONHOLDNO
            INNER JOIN CFN_ACCOUNT a_det
                ON a_det.ACCOUNTCODE = d.ACCOUNTCODE
            LEFT JOIN CFN_BILL b
                ON b.CTRL_ONHOLDNO = d.CTRL_ONHOLDNO
            LEFT JOIN CFN_TDS t
                ON t.TDSCODE = b.TDSCODE
            LEFT JOIN (
                SELECT customercode AS subcode, customername AS subcodedescription FROM cfn_customer
                UNION ALL
                SELECT vendorcode, vendorname FROM cfn_vendor
                UNION ALL
                SELECT employeecode, employeename FROM cfn_employee
            ) subA ON j.SUBACCOUNTCODE = subA.subcode
            LEFT JOIN (
                SELECT customercode AS subcode, customername AS subcodedescription FROM cfn_customer
                UNION ALL
                SELECT vendorcode, vendorname FROM cfn_vendor
                UNION ALL
                SELECT employeecode, employeename FROM cfn_employee
            ) subB ON d.SUBACCOUNTCODE = subB.subcode
            WHERE
                j.CTRL_STATUS  = 'Post'
                AND j.VCHR_DATE BETWEEN @fromDate AND @toDate
                AND j.ACCOUNTCODE IN ({inClause})
                AND d.DBCRFLAG  = 'D'
                AND (j.ACCOUNTCODE + ISNULL(j.SUBACCOUNTCODE, ''))
                    <> (d.ACCOUNTCODE + ISNULL(d.SUBACCOUNTCODE, ''))
            ORDER BY j.ACCOUNTCODE, j.VCHR_NUMBER";

                var rows = await _context.Database
                    .SqlQueryRaw<PurchaseReportRawRow>(sql, parameters.ToArray())
                    .ToListAsync();

                return rows.Select(r => new PurchaseReportRowDto
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
                    Amount = r.Amount
                });
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error executing Purchase Report: " + ex.Message);
            }
        }
    }
    internal class PurchaseReportRawRow
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
        // ✅ FIX: Added TdsCode and TdsDescription to RawRow
        public string? TdsCode { get; set; }
        public string? TdsDescription { get; set; }
        public decimal? TdsAmount { get; set; }
        public string? DetailAccountCode { get; set; }
        public string? DetailAccountDesc { get; set; }
        public string? DetailSubAccountCode { get; set; }
        public string? DetailSubAccountDesc { get; set; }
        public string? DbCrFlag { get; set; }
        public decimal? Amount { get; set; }
    }
}