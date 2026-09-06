using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class TravelReportService : ITravelReportService
    {
        private readonly BilzFinDbContext _context;

        public TravelReportService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TravelAccountDto>> GetTravelAccountsAsync()
        {
            try
            {
                var sql = @"
                    SELECT DISTINCT
                        d.accountcode  AS AccountCode,
                        a.description  AS Description
                    FROM cfn_trvldetail d
                    INNER JOIN cfn_account a
                        ON a.accountcode = d.accountcode
                    ORDER BY d.accountcode";

                var rows = await _context.Database
                    .SqlQueryRaw<TravelAccountRawRow>(sql)
                    .ToListAsync();

                return rows.Select(r => new TravelAccountDto
                {
                    AccountCode = r.AccountCode ?? string.Empty,
                    Description = r.Description ?? string.Empty
                });
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving travel accounts: " + ex.Message);
            }
        }

        public async Task<IEnumerable<TravelReportRowDto>> GetTravelReportAsync(TravelReportFilterModel filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter.AccountCode))
                    return Enumerable.Empty<TravelReportRowDto>();

                if (!DateTime.TryParse(filter.FromDate, out var fromDate) ||
                    !DateTime.TryParse(filter.ToDate, out var toDate))
                    return Enumerable.Empty<TravelReportRowDto>();

                var accountCodes = filter.AccountCode
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .ToList();

                if (!accountCodes.Any())
                    return Enumerable.Empty<TravelReportRowDto>();

                var paramNames = accountCodes.Select((_, i) => $"@p{i}").ToList();
                var inClause = string.Join(", ", paramNames);

                var parameters = new List<object>();
                for (int i = 0; i < accountCodes.Count; i++)
                    parameters.Add(new SqlParameter(paramNames[i], accountCodes[i]));

                parameters.Add(new SqlParameter("@fromDate", fromDate));
                parameters.Add(new SqlParameter("@toDate", toDate));

                var sql = $@"
            SELECT
                t.ctrl_accperiod                              AS AccPeriod,
                t.ctrl_onholdno                               AS OnholdNo,
                t.vchr_number                                 AS VoucherNumber,
                CONVERT(varchar(10), t.vchr_date, 120)        AS VoucherDate,
                t.vchr_refnumber                              AS VchrRefNumber,
                CONVERT(varchar(10), t.vchr_refdate, 120)     AS VchrRefDate,
                d.accountcode                                 AS AccountCode,
                a.description                                 AS Description,
                d.lineparticulars                             AS LineParticulars,
                d.costcentrecode                              AS CostCentreCode,
                ISNULL(cc.description, '')                    AS CostCentreDesc,
                d.subaccountcode                              AS SubAccountCode,
                d.costtype                                    AS CostType,
                d.expensetype                                 AS ExpenseType,
                d.referencenumber                             AS ReferenceNumber,
                CONVERT(varchar(10), d.referencedate, 120)    AS ReferenceDate,
                d.dbcrflag                                    AS DbCrFlag,
                d.dbcramount                                  AS Amount
            FROM cfn_travelvoucher t
            INNER JOIN cfn_trvldetail d
                ON t.ctrl_onholdno = d.ctrl_onholdno
            INNER JOIN cfn_account a
                ON a.accountcode = d.accountcode
            LEFT JOIN cfn_costcentre cc
                ON cc.costcentrecode = d.costcentrecode
            WHERE
                t.ctrl_status  = 'Post'
                AND t.vchr_date BETWEEN @fromDate AND @toDate
                AND d.accountcode IN ({inClause})
            ORDER BY d.accountcode, t.vchr_number, d.ctrl_sequenceno";


                var rows = await _context.Database
                    .SqlQueryRaw<TravelReportRawRow>(sql, parameters.ToArray())
                    .ToListAsync();

                return rows.Select(r => new TravelReportRowDto
                {
                    AccPeriod = r.AccPeriod,
                    AccountCode = r.AccountCode,
                    Description = r.Description,
                    VoucherNumber = r.VoucherNumber,
                    VoucherDate = r.VoucherDate,
                    LineParticulars = r.LineParticulars,
                    CostCentreCode = r.CostCentreCode,
                    CostCentreDesc = r.CostCentreDesc,
                    SubAccountCode = r.SubAccountCode,
                    CostType = r.CostType,
                    ExpenseType = r.ExpenseType,
                    ReferenceNumber = r.ReferenceNumber,
                    ReferenceDate = r.ReferenceDate,
                    DbCrFlag = r.DbCrFlag,
                    Amount = r.Amount,
                    VchrRefNumber = r.VchrRefNumber,
                    VchrRefDate = r.VchrRefDate
                });
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error executing Travel Report: " + ex.Message);
            }
        }
    }

    internal class TravelAccountRawRow
    {
        public string? AccountCode { get; set; }
        public string? Description { get; set; }
    }

    internal class TravelReportRawRow
    {
        public string? AccPeriod { get; set; }
        public string? AccountCode { get; set; }
        public string? Description { get; set; }
        public string? VoucherNumber { get; set; }
        public string? VoucherDate { get; set; }
        public string? LineParticulars { get; set; }
        public string? CostCentreCode { get; set; }
        // ✅ FIX: Added CostCentreDesc to RawRow (was missing — SqlQueryRaw silently drops
        //         columns it can't map, but the column was also absent from the DTO)
        public string? CostCentreDesc { get; set; }
        public string? SubAccountCode { get; set; }
        public string? CostType { get; set; }
        public string? ExpenseType { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? ReferenceDate { get; set; }
        public string? DbCrFlag { get; set; }
        public decimal? Amount { get; set; }
        public string? VchrRefNumber { get; set; }
        public string? VchrRefDate { get; set; }
    }
}