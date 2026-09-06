using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class GstReportService : IGstReportService
    {
        private readonly BilzFinDbContext _context;

        public GstReportService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GstReportDto>> GetGstReportAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                const string sql = @"
                    SELECT
                        p.VCHR_NUMBER AS PjvNumber,
                        p.VCHR_DATE   AS PjvDate,
                        b.VCHR_REFNUMBER AS GinNumber,
                        g.GIN_DATE    AS GinDate,
                        v.VENDORNAME  AS Party,
                        g.INV_CHA_NO  AS InvoiceNo,
                        g.INV_CHA_DATE AS InvoiceDate,

                        (SELECT TOP 1 od.DES
                         FROM obi_code_entry od
                         WHERE od.ARTICLE_NO IN (
                             SELECT gd.ARTICLE_NO 
                             FROM GIN_DETAIL gd
                             WHERE gd.GIN_NO = b.VCHR_REFNUMBER
                         )) AS ItemDescription,

                        -- Basic Value
                        CAST(ISNULL((
                            SELECT SUM(ISNULL(gl.VOUCHERAMOUNT, 0))
                            FROM cfn_gldetail gl
                            WHERE gl.VCHR_NUMBER = p.VCHR_NUMBER
                              AND gl.ACCOUNTCODE IN (
                                    SELECT DISTINCT ACC_CODE FROM code_type
                                    UNION SELECT 'A061100'
                                    UNION SELECT 'A060301'
                                    UNION SELECT 'A061203'
                              )
                        ), 0) AS DECIMAL(18,2)) AS BasicValue,

                        -- Excise Duty
                        CAST(ISNULL((
                            SELECT SUM(ISNULL(gl.VOUCHERAMOUNT, 0))
                            FROM cfn_gldetail gl
                            WHERE gl.VCHR_NUMBER = p.VCHR_NUMBER
                              AND gl.ACCOUNTCODE IN ('A070200','A130200')
                        ), 0) AS DECIMAL(18,2)) AS ExciseDuty,

                        -- ED Cess
                        CAST(ISNULL((
                            SELECT SUM(ISNULL(gl.VOUCHERAMOUNT, 0))
                            FROM cfn_gldetail gl
                            WHERE gl.VCHR_NUMBER = p.VCHR_NUMBER
                              AND gl.ACCOUNTCODE IN ('A070201','A070202')
                        ), 0) AS DECIMAL(18,2)) AS EdCess,

                        -- HED Cess
                        CAST(ISNULL((
                            SELECT SUM(ISNULL(gl.VOUCHERAMOUNT, 0))
                            FROM cfn_gldetail gl
                            WHERE gl.VCHR_NUMBER = p.VCHR_NUMBER
                              AND gl.ACCOUNTCODE IN ('A070205','A070206')
                        ), 0) AS DECIMAL(18,2)) AS HedCess,

                        -- Others
                        CAST(0.00 AS DECIMAL(18,2)) AS Others,

                        -- Rate Of Tax
                        CAST(ISNULL((
                            SELECT SUM(ISNULL(gp.STP, 0))
                            FROM gin_price_domestic gp
                            WHERE gp.GIN_NO = b.VCHR_REFNUMBER
                        ), 0) AS DECIMAL(18,2)) AS RateOfTax,

                        -- VAT
                        CAST(ISNULL((
                            SELECT SUM(ISNULL(gl.VOUCHERAMOUNT, 0))
                            FROM cfn_gldetail gl
                            WHERE gl.VCHR_NUMBER = p.VCHR_NUMBER
                              AND gl.ACCOUNTCODE IN ('A070204')
                        ), 0) AS DECIMAL(18,2)) AS Vat,

                        -- Non VAT
                        CAST(ISNULL((
                            SELECT SUM(ISNULL(gp.ST, 0))
                            FROM gin_price_domestic gp
                            WHERE gp.GIN_NO = b.VCHR_REFNUMBER
                        ), 0) AS DECIMAL(18,2)) AS NonVat,

                        -- CST (FIXED ISSUE HERE)
                        CAST(ISNULL((
                            SELECT SUM(ISNULL(gp.CESS, 0))
                            FROM gin_price_domestic gp
                            WHERE gp.GIN_NO = b.VCHR_REFNUMBER
                        ), 0) AS DECIMAL(18,2)) AS Cst,

                        -- Total
                        CAST(
                            ISNULL((
                                SELECT SUM(ISNULL(gl.VOUCHERAMOUNT, 0))
                                FROM cfn_gldetail gl
                                WHERE gl.VCHR_NUMBER = p.VCHR_NUMBER
                            ), 0)
                        AS DECIMAL(18,2)) AS Total,

                        -- VAT Type
                        (SELECT TOP 1 gp.VAT_TYPE
                         FROM gin_price_domestic gp
                         WHERE gp.GIN_NO = b.VCHR_REFNUMBER) AS VatType,

                        v.CSTNUMBER AS CstNo,
                        v.TINNUMBER AS TinNo

                    FROM CFN_PURCHASEJNL p
                    JOIN CFN_VENDOR v ON v.VENDORCODE = p.SUBACCOUNTCODE
                    JOIN CFN_BILL b ON b.CTRL_ONHOLDNO = p.CTRL_ONHOLDNO
                    JOIN GIN_MASTER g ON g.GIN_NO = b.VCHR_REFNUMBER

                    WHERE p.VCHR_DATE BETWEEN @fromDate AND @toDate
                      AND p.CTRL_STATUS = 'Post'

                    ORDER BY p.VCHR_DATE, p.VCHR_NUMBER";

                var fromParam = new SqlParameter("@fromDate", fromDate.Date);
                var toParam = new SqlParameter("@toDate", toDate.Date.AddDays(1).AddTicks(-1));

                var result = await _context.Database
                    .SqlQueryRaw<GstReportDto>(sql, fromParam, toParam)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving GST report: " + ex.Message, ex);
            }
        }
    }
}