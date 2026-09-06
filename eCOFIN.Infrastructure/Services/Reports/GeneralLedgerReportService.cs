using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class GeneralLedgerReportService : IGeneralLedgerReportService
    {
        private readonly BilzFinDbContext _context;
        public GeneralLedgerReportService(BilzFinDbContext context) => _context = context;

        // ── Acc Periods ───────────────────────────────────────────────────────
        public async Task<IEnumerable<AccPeriodDto>> GetAccPeriodsAsync()
        {
            try
            {
                return await _context.CfnAccncalenders.AsNoTracking()
                    .OrderBy(x => x.Sequence)
                    .Select(x => new AccPeriodDto
                    {
                        AccPeriod = x.Accperiod,
                        PeriodFrom = x.Periodfrom.ToString("dd/MM/yyyy"),
                        PeriodTo = x.Periodto.ToString("dd/MM/yyyy"),
                        Sequence = (int?)x.Sequence,
                        FinancialYear = x.Financialyear
                    }).ToListAsync();
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving acc periods: " + ex.Message); }
        }

        // ── General Ledger ────────────────────────────────────────────────────
        public async Task<IEnumerable<GeneralLedgerReportDto>> GetGeneralLedgerAsync(GeneralLedgerFilter f)
        {
            try
            {
                const string sql = @"
SELECT '6' AS SequenceNo,
       gl.ACCOUNTCODE,
       a.DESCRIPTION AS AccountDescription,
       (CASE gl.DBCRFLAG WHEN 'D' THEN gl.VOUCHERAMOUNT WHEN 'C' THEN -gl.VOUCHERAMOUNT END) AS VoucherAmount,
       0             AS ClosingBalance,
       gl.VCHR_NUMBER As VchrNumber,
       CONVERT(varchar(12), gl.VCHR_DATE, 103)      AS VchrDate,
       gl.LINEDETAILS,
       gl.VCHR_REFNUMBER,
       gl.INSTRUMENTNO
FROM   CFN_GLDETAIL gl
JOIN   CFN_ACCOUNT  a ON a.ACCOUNTCODE = gl.ACCOUNTCODE
WHERE  gl.CTRL_STATUS = 'Post'
  AND  gl.ACCOUNTCODE NOT IN (SELECT ACCOUNTCODE FROM CFN_GLSUMMARYACC)
  AND  gl.VCHR_DATE BETWEEN @fromDate AND @toDate

UNION ALL

SELECT '6' AS SequenceNo,
       gl.ACCOUNTCODE,
       a.DESCRIPTION,
       (CASE gl.DBCRFLAG WHEN 'D' THEN gl.VOUCHERAMOUNT WHEN 'C' THEN -gl.VOUCHERAMOUNT END),
       0,
       gl.VCHR_NUMBER,
       CONVERT(varchar(12), gl.VCHR_DATE, 103),
       gl.LINEDETAILS,
       gl.VCHR_REFNUMBER,
       gl.INSTRUMENTNO
FROM   CFN_GLDETAIL gl
JOIN   CFN_ACCOUNT       a  ON a.ACCOUNTCODE  = gl.ACCOUNTCODE
JOIN   CFN_GLSUMMARYACC  gs ON gs.ACCOUNTCODE = gl.ACCOUNTCODE
                            AND gs.VCHRTYPE   = gl.VCHR_TYPE
                            AND gs.SUMMARY    = 'N'
WHERE  gl.CTRL_STATUS = 'Post'
  AND  gl.VCHR_DATE BETWEEN @fromDate AND @toDate

UNION ALL

SELECT '7' AS SequenceNo,
       gl.ACCOUNTCODE,
       a.DESCRIPTION,
       SUM(CASE gl.DBCRFLAG WHEN 'D' THEN gl.VOUCHERAMOUNT WHEN 'C' THEN -gl.VOUCHERAMOUNT END),
       0,
       '',
       CONVERT(varchar(12), cal.PERIODTO, 103),
       'Summary of ' + gl.VCHR_TYPE + ' : ' + vt.VOUCHERTYPEDESCRIPTION,
       '',
       ''
FROM   CFN_GLDETAIL      gl
JOIN   CFN_ACCNCALENDER  cal ON cal.ACCPERIOD  = gl.ACCPERIOD
JOIN   CFN_ACCOUNT       a   ON a.ACCOUNTCODE  = gl.ACCOUNTCODE
JOIN   CFN_VCHRTYPE      vt  ON vt.VOUCHERTYPE = gl.VCHR_TYPE
JOIN   CFN_GLSUMMARYACC  gs  ON gs.ACCOUNTCODE = gl.ACCOUNTCODE
                             AND gs.VCHRTYPE   = gl.VCHR_TYPE
                             AND gs.SUMMARY    = 'Y'
WHERE  gl.CTRL_STATUS = 'Post'
  AND  gl.VCHR_DATE BETWEEN @fromDate AND @toDate
GROUP BY gl.ACCOUNTCODE, a.DESCRIPTION, cal.PERIODTO,
         gl.VCHR_TYPE, vt.VOUCHERTYPEDESCRIPTION
HAVING SUM(CASE gl.DBCRFLAG WHEN 'D' THEN gl.VOUCHERAMOUNT WHEN 'C' THEN -gl.VOUCHERAMOUNT END) <> 0

ORDER BY AccountCode, SequenceNo, VchrDate, VchrNumber";

                return await _context.Database.SqlQueryRaw<GeneralLedgerReportDto>(sql,
                    new SqlParameter("@fromDate", f.FromDate.Date),
                    new SqlParameter("@toDate",   f.ToDate.Date))
                    .ToListAsync();
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving General Ledger: " + ex.Message); }
        }
    }
}
