using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace eCOFIN.Infrastructure.Services.Reports
{
    public class AgeingReportService : IAgeingReportService
    {
        private readonly BilzFinDbContext _context;

        public AgeingReportService(BilzFinDbContext context)
        {
            _context = context;
        }

        // ── Buckets follow the screenshots exactly: <=30, 31-45, 46-90, 91-120, 121-180, >180
        // NOTE: The SQL files provided use different bucket ranges (<30, 30-60 etc).
        //       The screenshots show <=30, 31-45, 46-90, 91-120, 121-180, >180 — those are used.

        // ── DEBTORS ───────────────────────────────────────────────────────
        public async Task<IEnumerable<AgeingReportDto>> GetDebtorsAgeingAsync(DateTime reportDate)
        {
            try
            {
                const string sql = @"
-- ── BILLS ────────────────────────────────────────────────────────────────
SELECT
    'Bills'                                             AS Nature,
    cfn_bill.ACCOUNTCODE,
    cfn_bill.SUBACCOUNTCODE,
    cfn_customer.CUSTOMERNAME                           AS PartyName,
    cfn_bill.VCHR_NUMBER                                AS VchrNumber,
    cfn_bill.VCHR_DATE                                  AS VchrDate,
    cfn_bill.BILLNO                                     AS BillNo,
    ISNULL(cfn_bill.BILLDUEDATE,
           ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)) AS BillDueDate,
    DATEDIFF(DAY,
        ISNULL(cfn_bill.BILLDUEDATE,
               ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
        @reportDate)                                    AS DaysOutstanding,

    -- <= 30
    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) <= 30
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket0_30,
    -- 31 to 45
    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) BETWEEN 31 AND 45
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket30_45,
    -- 46 to 90
    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) BETWEEN 46 AND 90
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket46_90,
    -- 91 to 120
    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) BETWEEN 91 AND 120
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket91_120,
    -- 121 to 180
    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) BETWEEN 121 AND 180
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket121_180,
    -- > 180
    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) > 180
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket180Plus,

    cfn_bill.BILLBALANCE                                AS Total

FROM cfn_bill
LEFT  JOIN cfn_account  ON cfn_bill.ACCOUNTCODE   = cfn_account.ACCOUNTCODE
INNER JOIN cfn_customer ON cfn_bill.SUBACCOUNTCODE = cfn_customer.CUSTOMERCODE

WHERE cfn_bill.BILLBALANCE        > 0
  AND cfn_bill.CTRL_STATUS        = 'Post'
  AND cfn_account.ACCOUNTTYPE     = 'DEBT'
  AND ISNULL(cfn_bill.BILLDUEDATE,
             ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)) <= @reportDate

UNION ALL

-- ── PAYMENTS ─────────────────────────────────────────────────────────────
SELECT
    'Payments'                                          AS Nature,
    cfn_payments.ACCOUNTCODE,
    cfn_payments.SUBACCOUNTCODE,
    cfn_customer.CUSTOMERNAME                           AS PartyName,
    cfn_payments.VCHR_NUMBER                            AS VchrNumber,
    cfn_payments.VCHR_DATE                              AS VchrDate,
    cfn_payments.INSTRUMENTNO                           AS BillNo,
    ISNULL(cfn_payments.INSTRUMENTDATE,
           cfn_payments.VCHR_DATE)                      AS BillDueDate,
    DATEDIFF(DAY,
        ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE),
        @reportDate)                                    AS DaysOutstanding,

    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) <= 30
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket0_30,
    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) BETWEEN 31 AND 45
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket30_45,
    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) BETWEEN 46 AND 90
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket46_90,
    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) BETWEEN 91 AND 120
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket91_120,
    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) BETWEEN 121 AND 180
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket121_180,
    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) > 180
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket180Plus,

    -cfn_payments.PAYMENTAMOUNTBALANCE                  AS Total

FROM cfn_payments
LEFT  JOIN cfn_account  ON cfn_payments.ACCOUNTCODE   = cfn_account.ACCOUNTCODE
INNER JOIN cfn_customer ON cfn_payments.SUBACCOUNTCODE = cfn_customer.CUSTOMERCODE

WHERE cfn_payments.PAYMENTAMOUNTBALANCE > 0
  AND cfn_payments.CTRL_STATUS          = 'Post'
  AND cfn_account.ACCOUNTTYPE           = 'DEBT'
  AND ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE) <= @reportDate

ORDER BY PartyName, VchrDate, VchrNumber";

                var p = new SqlParameter("@reportDate", reportDate.Date);
                return await _context.Database
                    .SqlQueryRaw<AgeingReportDto>(sql, p)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Debtors ageing: " + ex.Message);
            }
        }

        // ── CREDITORS ─────────────────────────────────────────────────────
        public async Task<IEnumerable<AgeingReportDto>> GetCreditorsAgeingAsync(DateTime reportDate)
        {
            try
            {
                const string sql = @"
-- ── BILLS ────────────────────────────────────────────────────────────────
SELECT
    'Bills'                                             AS Nature,
    cfn_bill.ACCOUNTCODE,
    cfn_bill.SUBACCOUNTCODE,
    cfn_vendor.VENDORNAME                               AS PartyName,
    cfn_bill.VCHR_NUMBER                                AS VchrNumber,
    cfn_bill.VCHR_DATE                                  AS VchrDate,
    cfn_bill.BILLNO                                     AS BillNo,
    ISNULL(cfn_bill.BILLDUEDATE,
           ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)) AS BillDueDate,
    DATEDIFF(DAY,
        ISNULL(cfn_bill.BILLDUEDATE,
               ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
        @reportDate)                                    AS DaysOutstanding,

    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) <= 30
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket0_30,
    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) BETWEEN 31 AND 45
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket30_45,
    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) BETWEEN 46 AND 90
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket46_90,
    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) BETWEEN 91 AND 120
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket91_120,
    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) BETWEEN 121 AND 180
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket121_180,
    CASE WHEN DATEDIFF(DAY,
             ISNULL(cfn_bill.BILLDUEDATE, ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)),
             @reportDate) > 180
         THEN cfn_bill.BILLBALANCE ELSE 0.00 END        AS Bucket180Plus,

    cfn_bill.BILLBALANCE                                AS Total

FROM cfn_bill
LEFT  JOIN cfn_account ON cfn_bill.ACCOUNTCODE   = cfn_account.ACCOUNTCODE
INNER JOIN cfn_vendor  ON cfn_bill.SUBACCOUNTCODE = cfn_vendor.VENDORCODE

WHERE cfn_bill.BILLBALANCE        > 0
  AND cfn_bill.CTRL_STATUS        = 'Post'
  AND cfn_account.NATUREOFACCOUNT = '2LIAB'
  AND cfn_account.ACCOUNTTYPE     = 'CRDT'
  AND ISNULL(cfn_bill.BILLDUEDATE,
             ISNULL(cfn_bill.BILLDATE, cfn_bill.VCHR_DATE)) <= @reportDate

UNION ALL

-- ── PAYMENTS ─────────────────────────────────────────────────────────────
SELECT
    'Payments'                                          AS Nature,
    cfn_payments.ACCOUNTCODE,
    cfn_payments.SUBACCOUNTCODE,
    cfn_vendor.VENDORNAME                               AS PartyName,
    cfn_payments.VCHR_NUMBER                            AS VchrNumber,
    cfn_payments.VCHR_DATE                              AS VchrDate,
    cfn_payments.INSTRUMENTNO                           AS BillNo,
    ISNULL(cfn_payments.INSTRUMENTDATE,
           cfn_payments.VCHR_DATE)                      AS BillDueDate,
    DATEDIFF(DAY,
        ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE),
        @reportDate)                                    AS DaysOutstanding,

    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) <= 30
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket0_30,
    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) BETWEEN 31 AND 45
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket30_45,
    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) BETWEEN 46 AND 90
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket46_90,
    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) BETWEEN 91 AND 120
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket91_120,
    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) BETWEEN 121 AND 180
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket121_180,
    CASE WHEN DATEDIFF(DAY, ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE), @reportDate) > 180
         THEN -cfn_payments.PAYMENTAMOUNTBALANCE ELSE 0.00 END AS Bucket180Plus,

    -cfn_payments.PAYMENTAMOUNTBALANCE                  AS Total

FROM cfn_payments
LEFT  JOIN cfn_account ON cfn_payments.ACCOUNTCODE   = cfn_account.ACCOUNTCODE
INNER JOIN cfn_vendor  ON cfn_payments.SUBACCOUNTCODE = cfn_vendor.VENDORCODE

WHERE cfn_payments.PAYMENTAMOUNTBALANCE > 0
  AND cfn_payments.CTRL_STATUS          = 'Post'
  AND cfn_account.NATUREOFACCOUNT       = '2LIAB'
  AND cfn_account.ACCOUNTTYPE           = 'CRDT'
  AND ISNULL(cfn_payments.INSTRUMENTDATE, cfn_payments.VCHR_DATE) <= @reportDate

ORDER BY PartyName, VchrDate, VchrNumber";

                var p = new SqlParameter("@reportDate", reportDate.Date);
                return await _context.Database
                    .SqlQueryRaw<AgeingReportDto>(sql, p)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Creditors ageing: " + ex.Message);
            }
        }
    }
}
