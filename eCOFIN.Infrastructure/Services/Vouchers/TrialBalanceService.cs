using eCOFIN.Application.DTOs.Vouchers;
using eCOFIN.Application.Interfaces.Vouchers;
using eCOFIN.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace eCOFIN.Infrastructure.Services.Vouchers
{
    public class TrialBalanceService : ITrialBalanceService
    {
        private readonly BilzFinDbContext _context;
        private readonly string _connectionString;

        public TrialBalanceService(BilzFinDbContext context, IConfiguration config)
        {
            _context = context;
            _connectionString = config.GetConnectionString("Default")
                ?? throw new Exception("Connection string not found.");
        }

        public async Task<IEnumerable<TrialBalanceDto>> GetTrialBalanceAsync(string accPeriod)
        {
            if (string.IsNullOrWhiteSpace(accPeriod))
                throw new ArgumentException("AccPeriod is required.", nameof(accPeriod));

            try
            {
                var sql = @"
                    SELECT 
                        A.ACCOUNTCODE AS AccountCode,
                        A.ACCOUNTTYPE AS AccountType,
                        A.DESCRIPTION AS Description,
                        CASE WHEN SUM(X.BAL) >= 0 THEN SUM(X.BAL) ELSE 0 END AS Debit,
                        CASE WHEN SUM(X.BAL) < 0 THEN -SUM(X.BAL) ELSE 0 END AS Credit
                    FROM CFN_ACCOUNT A
                    LEFT JOIN (
                        SELECT 
                            ACCOUNTCODE,
                            CASE POSTEDCBDBCR 
                                WHEN 'D' THEN POSTEDCLOSINGBALANCE 
                                WHEN 'C' THEN -POSTEDCLOSINGBALANCE 
                            END AS BAL
                        FROM CFN_GENERALLEDGER
                        WHERE ACCPERIOD = @AccPeriod
                        UNION ALL
                        SELECT 
                            ACCOUNTCODE,
                            CASE POSTEDCBDBCR 
                                WHEN 'D' THEN POSTEDCLOSINGBALANCE 
                                WHEN 'C' THEN -POSTEDCLOSINGBALANCE 
                            END
                        FROM CFN_GLSUBLEDGER
                        WHERE ACCPERIOD = @AccPeriod
                    ) X ON X.ACCOUNTCODE = A.ACCOUNTCODE
                    GROUP BY 
                        A.ACCOUNTCODE,
                        A.ACCOUNTTYPE,
                        A.DESCRIPTION
                    ORDER BY A.DESCRIPTION;";

                return await ExecListAsync(sql, r => new TrialBalanceDto
                {
                    AccountCode = r.GetString(r.GetOrdinal("AccountCode")),
                    AccountType = r.IsDBNull(r.GetOrdinal("AccountType")) ? null : r.GetString(r.GetOrdinal("AccountType")),
                    Description = r.IsDBNull(r.GetOrdinal("Description")) ? null : r.GetString(r.GetOrdinal("Description")),
                    Debit = r.IsDBNull(r.GetOrdinal("Debit")) ? 0 : r.GetDecimal(r.GetOrdinal("Debit")),
                    Credit = r.IsDBNull(r.GetOrdinal("Credit")) ? 0 : r.GetDecimal(r.GetOrdinal("Credit"))
                },
                new SqlParameter("@AccPeriod", accPeriod));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving Trial Balance. AccPeriod={accPeriod} :" + ex);
            }
        }

        /// <summary>
        /// GetGLDetailsAsync - Returns GL details with opening balance, transactions, and GL detail lines
        /// Based on your original working query with all 6 UNION blocks and 28 columns
        /// </summary>

        public async Task<IEnumerable<GLDetailDto>> GetGLDetailsAsync(
     string accPeriod,
     string accCode,
     DateTime fromDate,
     DateTime toDate)
        {
            if (string.IsNullOrWhiteSpace(accCode))
                throw new ArgumentException("AccCode is required.", nameof(accCode));
            if (string.IsNullOrWhiteSpace(accPeriod))
                throw new ArgumentException("AccPeriod is required.", nameof(accPeriod));
            if (toDate < fromDate)
                throw new ArgumentException("ToDate must be >= FromDate.", nameof(toDate));

            try
            {
                var derivedAccPeriod = DerivePeriodFromDate(fromDate);

                var sql = @"
            -- =========================================================
            -- BLOCK 1 (seq 6): Transactions for NON-summary accounts
            -- =========================================================
            SELECT
                '6' AS SequenceNo,
                GD.ACCOUNTCODE AS AccountCode,
                ACC.DESCRIPTION AS Description,
                CASE WHEN GD.DBCRFLAG = 'D' THEN GD.VOUCHERAMOUNT ELSE 0 END AS Debit,
                CASE WHEN GD.DBCRFLAG = 'C' THEN GD.VOUCHERAMOUNT ELSE 0 END AS Credit,
                GD.VCHR_NUMBER AS VoucherNumber,
                GD.VCHR_DATE AS VoucherDate,
                GD.LINEDETAILS AS LineDetails,
                GD.VCHR_TYPE AS VoucherType
            FROM CFN_GLDETAIL GD
            INNER JOIN CFN_ACCOUNT ACC ON ACC.ACCOUNTCODE = GD.ACCOUNTCODE
            WHERE GD.CTRL_STATUS = 'Post'
              AND GD.ACCOUNTCODE = @AccCode
              AND GD.ACCOUNTCODE NOT IN (
                  SELECT ACCOUNTCODE FROM CFN_GLSUMMARYACC
              )
              AND GD.VCHR_DATE BETWEEN @FromDate AND @ToDate

            UNION ALL

            -- =========================================================
            -- BLOCK 2 (seq 6): Transactions for summary accounts SUMMARY='N'
            -- =========================================================
            SELECT
                '6',
                GD.ACCOUNTCODE,
                ACC.DESCRIPTION,
                CASE WHEN GD.DBCRFLAG = 'D' THEN GD.VOUCHERAMOUNT ELSE 0 END,
                CASE WHEN GD.DBCRFLAG = 'C' THEN GD.VOUCHERAMOUNT ELSE 0 END,
                GD.VCHR_NUMBER,
                GD.VCHR_DATE,
                GD.LINEDETAILS,
                GD.VCHR_TYPE
            FROM CFN_GLDETAIL GD
            INNER JOIN CFN_ACCOUNT ACC ON ACC.ACCOUNTCODE = GD.ACCOUNTCODE
            INNER JOIN CFN_GLSUMMARYACC SA
                ON SA.ACCOUNTCODE = GD.ACCOUNTCODE
               AND SA.VCHRTYPE = GD.VCHR_TYPE
               AND SA.SUMMARY = 'N'
            WHERE GD.CTRL_STATUS = 'Post'
              AND GD.ACCOUNTCODE = @AccCode
              AND GD.VCHR_DATE BETWEEN @FromDate AND @ToDate

            UNION ALL

            -- =========================================================
            -- BLOCK 3 (seq 7): Summary aggregates for SUMMARY='Y'
            -- =========================================================
            SELECT
                '7',
                GD.ACCOUNTCODE,
                ACC.DESCRIPTION,
                SUM(CASE WHEN GD.DBCRFLAG = 'D' THEN GD.VOUCHERAMOUNT ELSE 0 END),
                SUM(CASE WHEN GD.DBCRFLAG = 'C' THEN GD.VOUCHERAMOUNT ELSE 0 END),
                '',
                MAX(GD.VCHR_DATE),
                'Summary of ' + GD.VCHR_TYPE,
                GD.VCHR_TYPE
            FROM CFN_GLDETAIL GD
            INNER JOIN CFN_ACCOUNT ACC ON ACC.ACCOUNTCODE = GD.ACCOUNTCODE
            INNER JOIN CFN_GLSUMMARYACC SA
                ON SA.ACCOUNTCODE = GD.ACCOUNTCODE
               AND SA.VCHRTYPE = GD.VCHR_TYPE
               AND SA.SUMMARY = 'Y'
            WHERE GD.CTRL_STATUS = 'Post'
              AND GD.ACCOUNTCODE = @AccCode
              AND GD.VCHR_DATE BETWEEN @FromDate AND @ToDate
            GROUP BY GD.ACCOUNTCODE, ACC.DESCRIPTION, GD.VCHR_TYPE
            HAVING SUM(CASE WHEN GD.DBCRFLAG = 'D' THEN GD.VOUCHERAMOUNT
                            WHEN GD.DBCRFLAG = 'C' THEN -GD.VOUCHERAMOUNT
                            ELSE 0 END) <> 0

            UNION ALL

            -- =========================================================
            -- BLOCK 4 (seq 2): Opening balance — NON-subledger accounts
            -- =========================================================
            SELECT
                '2',
                GL.ACCOUNTCODE,
                ACC.DESCRIPTION,
                CASE GL.POSTEDOBDBCR WHEN 'D' THEN ISNULL(GL.POSTEDOPENINGBALANCE, 0) ELSE 0 END,
                CASE GL.POSTEDOBDBCR WHEN 'C' THEN ISNULL(GL.POSTEDOPENINGBALANCE, 0) ELSE 0 END,
                '',
                @FromDate,
                'Opening Balance',
                ''
            FROM CFN_GENERALLEDGER GL
            INNER JOIN CFN_ACCOUNT ACC ON ACC.ACCOUNTCODE = GL.ACCOUNTCODE
            WHERE GL.ACCOUNTCODE = @AccCode
              AND GL.ACCPERIOD = @DerivedAccPeriod
              AND ISNULL(ACC.ACCOUNTTYPE, '') NOT IN ('CRDT','DEBT','STADV','STPRD','EMPL')
              AND ISNULL(GL.POSTEDOPENINGBALANCE, 0) <> 0

            UNION ALL

            -- =========================================================
            -- BLOCK 5 (seq 2): Opening balance — SUBLEDGER accounts
            -- =========================================================
            SELECT
                '2',
                SL.ACCOUNTCODE,
                ACC.DESCRIPTION,
                CASE WHEN SUM(CASE SL.POSTEDOBDBCR
                                  WHEN 'D' THEN SL.POSTEDOPENINGBALANCE
                                  WHEN 'C' THEN -SL.POSTEDOPENINGBALANCE
                                  ELSE 0 END) > 0
                     THEN SUM(CASE SL.POSTEDOBDBCR
                                  WHEN 'D' THEN SL.POSTEDOPENINGBALANCE
                                  WHEN 'C' THEN -SL.POSTEDOPENINGBALANCE
                                  ELSE 0 END)
                     ELSE 0 END,
                CASE WHEN SUM(CASE SL.POSTEDOBDBCR
                                  WHEN 'D' THEN SL.POSTEDOPENINGBALANCE
                                  WHEN 'C' THEN -SL.POSTEDOPENINGBALANCE
                                  ELSE 0 END) < 0
                     THEN -SUM(CASE SL.POSTEDOBDBCR
                                   WHEN 'D' THEN SL.POSTEDOPENINGBALANCE
                                   WHEN 'C' THEN -SL.POSTEDOPENINGBALANCE
                                   ELSE 0 END)
                     ELSE 0 END,
                '',
                @FromDate,
                'Opening Balance',
                ''
            FROM CFN_GLSUBLEDGER SL
            INNER JOIN CFN_ACCOUNT ACC ON ACC.ACCOUNTCODE = SL.ACCOUNTCODE
            WHERE SL.ACCOUNTCODE = @AccCode
              AND SL.ACCPERIOD = @DerivedAccPeriod
              AND ACC.ACCOUNTTYPE IN ('CRDT','DEBT','STADV','STPRD','EMPL')
            GROUP BY SL.ACCOUNTCODE, ACC.DESCRIPTION
            HAVING SUM(CASE SL.POSTEDOBDBCR
                           WHEN 'D' THEN SL.POSTEDOPENINGBALANCE
                           WHEN 'C' THEN -SL.POSTEDOPENINGBALANCE
                           ELSE 0 END) <> 0

            ORDER BY VoucherDate, SequenceNo
            OPTION (RECOMPILE);";

                return await ExecListAsync(sql, r => new GLDetailDto
                {
                    SequenceNo = r["SequenceNo"]?.ToString(),
                    AccountCode = r["AccountCode"]?.ToString(),
                    Description = r["Description"]?.ToString(),
                    Debit = r["Debit"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Debit"]),
                    Credit = r["Credit"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Credit"]),
                    VoucherNumber = r["VoucherNumber"]?.ToString(),
                    VoucherDate = r["VoucherDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["VoucherDate"]),
                    LineDetails = r["LineDetails"]?.ToString(),
                    VoucherType = r["VoucherType"]?.ToString()
                },
                new SqlParameter("@AccCode", accCode),
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate),
                new SqlParameter("@DerivedAccPeriod", derivedAccPeriod));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    $"Error retrieving GL Details. AccCode={accCode}, AccPeriod={accPeriod}, FromDate={fromDate:yyyy-MM-dd}, ToDate={toDate:yyyy-MM-dd}. ERROR: {ex.Message}",
                    ex);
            }
        }

        private string GetPreviousPeriod(string accPeriod)
        {
            var parts = accPeriod.Split('-');
            var month = parts[0].Trim();
            var year = int.Parse(parts[1]);

            var months = new[]
            {
        "JAN","FEB","MAR","APR","MAY","JUN",
        "JUL","AUG","SEP","OCT","NOV","DEC"
    };

            int index = Array.IndexOf(months, month);

            if (index == 0)
                return $"DEC - {year - 1}";

            return $"{months[index - 1]} - {year}";
        }

        // ── GetSubledgerScheduleAsync ─────────────────────────────────────────
        public async Task<IEnumerable<SubledgerScheduleDto>> GetSubledgerScheduleAsync(string accPeriod, string accCode)
        {
            if (string.IsNullOrWhiteSpace(accPeriod)) throw new ArgumentException("AccPeriod is required.", nameof(accPeriod));
            if (string.IsNullOrWhiteSpace(accCode)) throw new ArgumentException("AccCode is required.", nameof(accCode));

            try
            {
                // NOTE: SQL matches the legacy query exactly — returns a single
                // signed BALANCE column (positive = debit, negative = credit).
                // The Debit/Credit split is done in C# below so the existing
                // SubledgerScheduleDto contract stays unchanged for the UI.
                var sql = @"
SELECT
    ACC.ACCOUNTCODE        AS AccountCode,
    ACC.DESCRIPTION        AS Description,
    S.SUBACCOUNTCODE       AS SubAccountCode,
    V.SUBCODEDESCRIPTION   AS SubCodeDescription,
    CASE S.POSTEDCBDBCR
        WHEN 'D' THEN S.POSTEDCLOSINGBALANCE
        WHEN 'C' THEN -S.POSTEDCLOSINGBALANCE
    END                    AS Balance
FROM CFN_ACCOUNT ACC
INNER JOIN CFN_GLSUBLEDGER S
    ON ACC.ACCOUNTCODE = S.ACCOUNTCODE
INNER JOIN CFN_V_SUBCODESLINK V
    ON S.ACCOUNTCODE    = V.ACCOUNTCODE
   AND S.SUBACCOUNTCODE = V.SUBCODE
WHERE S.ACCPERIOD   = @AccPeriod
  AND S.ACCOUNTCODE = @AccCode;";

                return await ExecListAsync(sql, r =>
                {
                    var balOrdinal = r.GetOrdinal("Balance");
                    decimal balance = r.IsDBNull(balOrdinal) ? 0m : r.GetDecimal(balOrdinal);

                    return new SubledgerScheduleDto
                    {
                        AccountCode = r.GetString(r.GetOrdinal("AccountCode")),
                        Description = r.IsDBNull(r.GetOrdinal("Description")) ? null : r.GetString(r.GetOrdinal("Description")),
                        SubAccountCode = r.GetString(r.GetOrdinal("SubAccountCode")),
                        SubCodeDescription = r.IsDBNull(r.GetOrdinal("SubCodeDescription")) ? null : r.GetString(r.GetOrdinal("SubCodeDescription")),
                        Debit = balance >= 0 ? balance : 0m,
                        Credit = balance < 0 ? -balance : 0m
                    };
                },
                new SqlParameter("@AccPeriod", accPeriod),
                new SqlParameter("@AccCode", accCode));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving Subledger Schedule. AccPeriod={accPeriod}, AccCode={accCode} :" + ex);
            }
        }

        // ── GetSubledgerAccountDetailsAsync ───────────────────────────────────
        public async Task<IEnumerable<SubledgerAccountDto>> GetSubledgerAccountDetailsAsync(
    string accPeriod, string accCode, string subCode, DateTime fromDate, DateTime toDate)
        {
            if (string.IsNullOrWhiteSpace(accPeriod))
                throw new ArgumentException("AccPeriod is required.", nameof(accPeriod));
            if (string.IsNullOrWhiteSpace(accCode))
                throw new ArgumentException("AccCode is required.", nameof(accCode));
            if (string.IsNullOrWhiteSpace(subCode))
                throw new ArgumentException("SubCode is required.", nameof(subCode));
            if (toDate < fromDate)
                throw new ArgumentException("ToDate must be >= FromDate.", nameof(toDate));

            try
            {
                var derivedAccPeriod = DerivePeriodFromDate(fromDate);

                var sql = @"
            -- Opening balance block (seq 2)
            SELECT
                '2' AS SequenceNo,
                S.ACCOUNTCODE AS AccountCode,
                A.DESCRIPTION AS Description,
                0 AS Debit,
                0 AS Credit,
                CAST(NULL AS NVARCHAR(50)) AS VoucherNumber,
                @FromDate AS VoucherDate,
                '' AS InvoiceNo,
                '' AS InvoiceDate,
                S.SUBACCOUNTCODE AS SubAccountCode,
                VS.SUBCODEDESCRIPTION AS SubCodeDescription,
                ISNULL(CASE S.POSTEDOBDBCR
                    WHEN 'D' THEN S.POSTEDOPENINGBALANCE
                    WHEN 'C' THEN -S.POSTEDOPENINGBALANCE
                END, 0) AS OpeningBalance
            FROM CFN_GLSUBLEDGER S
            INNER JOIN CFN_ACCOUNT A
                ON A.ACCOUNTCODE = S.ACCOUNTCODE
            OUTER APPLY (
                SELECT TOP 1 SUBCODEDESCRIPTION
                FROM CFN_V_SUBCODESLINK VS
                WHERE VS.ACCOUNTCODE = S.ACCOUNTCODE
                  AND VS.SUBCODE = S.SUBACCOUNTCODE
            ) VS
            WHERE S.ACCOUNTCODE = @AccCode
              AND S.SUBACCOUNTCODE = @SubCode
              AND S.ACCPERIOD = @DerivedAccPeriod
              AND ISNULL(S.POSTEDOPENINGBALANCE, 0)
                + ISNULL(S.POSTEDDEBITAMOUNT, 0)
                + ISNULL(S.POSTEDCREDITAMOUNT, 0) > 0

            UNION ALL

            -- Transactions block (seq 6)
            SELECT
                '6' AS SequenceNo,
                GD.ACCOUNTCODE AS AccountCode,
                A.DESCRIPTION AS Description,
                CASE WHEN GD.DBCRFLAG = 'D' THEN GD.VOUCHERAMOUNT ELSE 0 END AS Debit,
                CASE WHEN GD.DBCRFLAG = 'C' THEN GD.VOUCHERAMOUNT ELSE 0 END AS Credit,
                GD.VCHR_NUMBER AS VoucherNumber,
                GD.VCHR_DATE AS VoucherDate,
                ISNULL(GD.VCHR_REFNUMBER, GD.VCHR_NARRATION) AS InvoiceNo,
                GD.VCHR_REFDATE AS InvoiceDate,
                GD.SUBACCOUNTCODE AS SubAccountCode,
                VS.SUBCODEDESCRIPTION AS SubCodeDescription,
                0 AS OpeningBalance
            FROM CFN_GLDETAIL GD
            INNER JOIN CFN_ACCOUNT A
                ON A.ACCOUNTCODE = GD.ACCOUNTCODE
            OUTER APPLY (
                SELECT TOP 1 SUBCODEDESCRIPTION
                FROM CFN_V_SUBCODESLINK VS
                WHERE VS.ACCOUNTCODE = GD.ACCOUNTCODE
                  AND VS.SUBCODE = GD.SUBACCOUNTCODE
            ) VS
            WHERE GD.ACCOUNTCODE = @AccCode
              AND GD.SUBACCOUNTCODE = @SubCode
              AND GD.CTRL_STATUS = 'Post'
              AND GD.VOUCHERAMOUNT <> 0
              AND GD.VCHR_DATE BETWEEN @FromDate AND @ToDate

            ORDER BY SequenceNo, VoucherDate
            OPTION (RECOMPILE);";

                return await ExecListAsync(sql, r => new SubledgerAccountDto
                {
                    SequenceNo = r["SequenceNo"]?.ToString(),
                    AccountCode = r["AccountCode"]?.ToString(),
                    Description = r["Description"]?.ToString(),
                    Debit = r["Debit"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Debit"]),
                    Credit = r["Credit"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Credit"]),
                    VoucherNumber = r["VoucherNumber"]?.ToString(),
                    VoucherDate = r["VoucherDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["VoucherDate"]),
                    InvoiceNo = r["InvoiceNo"]?.ToString(),
                    InvoiceDate = r["InvoiceDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["InvoiceDate"]),
                    SubAccountCode = r["SubAccountCode"]?.ToString(),
                    SubCodeDescription = r["SubCodeDescription"]?.ToString(),
                    OpeningBalance = r["OpeningBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(r["OpeningBalance"])
                },
                new SqlParameter("@AccCode", accCode),
                new SqlParameter("@SubCode", subCode),
                new SqlParameter("@AccPeriod", accPeriod),
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate),
                new SqlParameter("@DerivedAccPeriod", derivedAccPeriod));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    $"Error retrieving Subledger Account Details. AccPeriod={accPeriod}, AccCode={accCode}, SubCode={subCode}.",
                    ex);
            }
        }

        // ── GetBillsAndPaymentsAsync ──────────────────────────────────────────
        public async Task<IEnumerable<BillPaymentDto>> GetBillsAndPaymentsAsync(string accCode, string subCode)
        {
            if (string.IsNullOrWhiteSpace(accCode)) throw new ArgumentException("AccCode is required.", nameof(accCode));
            if (string.IsNullOrWhiteSpace(subCode)) throw new ArgumentException("SubCode is required.", nameof(subCode));

            try
            {
                // Mirrors the legacy query structure (which ran well for years),
                // translated from old *= outer-join syntax to modern LEFT JOIN.
                // Key difference from our earlier attempt: filter CFN_BILL only on
                // SUBACCOUNTCODE, and gate the ACCOUNTCODE via a join to CFN_ACCOUNT.
                // This matches the legacy query's access pattern and whatever plan
                // SQL Server has cached for it.
                var sql = @"
SELECT
    'Bills' AS Nature,
    B.ACCOUNTCODE AS AccountCode,
    B.SUBACCOUNTCODE AS SubAccountCode,
    B.VCHR_NUMBER AS VoucherNumber,
    B.VCHR_DATE AS VoucherDate,
    B.BILLNO AS BillNo,
    ISNULL(B.VCHR_REFNUMBER, B.BILLNO) AS BillRefNo,
    ISNULL(B.BILLDUEDATE, ISNULL(B.BILLDATE, B.VCHR_DATE)) AS BillDate,
    B.BILLBALANCE AS BillBalance,
    L.SUBCODEDESCRIPTION AS SubCodeDescription,
    D.VCHR_NARRATION AS VoucherNarration
FROM CFN_BILL B
INNER JOIN CFN_ACCOUNT A
    ON A.ACCOUNTCODE = B.ACCOUNTCODE
INNER JOIN CFN_V_SUBCODLNK L
    ON L.SUBCODE = B.SUBACCOUNTCODE
LEFT JOIN CFN_GLDETAIL D
    ON D.CTRL_ONHOLDNO = B.CTRL_ONHOLDNO
   AND D.CTRL_SEQUENCENO = B.CTRL_SEQUENCENO
WHERE B.SUBACCOUNTCODE = @SubAccCode
  AND A.ACCOUNTCODE = @AccCode
  AND B.CTRL_STATUS = 'Post'
  AND B.BILLBALANCE > 0

UNION ALL

SELECT
    'Payments' AS Nature,
    P.ACCOUNTCODE AS AccountCode,
    P.SUBACCOUNTCODE AS SubAccountCode,
    P.VCHR_NUMBER AS VoucherNumber,
    P.VCHR_DATE AS VoucherDate,
    '' AS BillNo,
    P.INSTRUMENTNO AS BillRefNo,
    ISNULL(P.INSTRUMENTDATE, P.VCHR_DATE) AS BillDate,
    -P.PAYMENTAMOUNTBALANCE AS BillBalance,
    L.SUBCODEDESCRIPTION AS SubCodeDescription,
    D.VCHR_NARRATION AS VoucherNarration
FROM CFN_PAYMENTS P
INNER JOIN CFN_ACCOUNT A
    ON A.ACCOUNTCODE = P.ACCOUNTCODE
INNER JOIN CFN_V_SUBCODLNK L
    ON L.SUBCODE = P.SUBACCOUNTCODE
LEFT JOIN CFN_GLDETAIL D
    ON D.CTRL_ONHOLDNO = P.CTRL_ONHOLDNO
   AND D.CTRL_SEQUENCENO = P.CTRL_SEQUENCENO
WHERE P.SUBACCOUNTCODE = @SubAccCode
  AND A.ACCOUNTCODE = @AccCode
  AND P.CTRL_STATUS = 'Post'
  AND P.PAYMENTAMOUNTBALANCE > 0;";

                return await ExecListAsync(sql, r => new BillPaymentDto
                {
                    Nature = r.IsDBNull(r.GetOrdinal("Nature")) ? null : r.GetString(r.GetOrdinal("Nature")),
                    AccountCode = r.GetString(r.GetOrdinal("AccountCode")),
                    SubAccountCode = r.IsDBNull(r.GetOrdinal("SubAccountCode")) ? null : r.GetString(r.GetOrdinal("SubAccountCode")),
                    VoucherNumber = r.IsDBNull(r.GetOrdinal("VoucherNumber")) ? null : r.GetString(r.GetOrdinal("VoucherNumber")),
                    VoucherDate = r.IsDBNull(r.GetOrdinal("VoucherDate")) ? null : r.GetDateTime(r.GetOrdinal("VoucherDate")),
                    BillNo = r.IsDBNull(r.GetOrdinal("BillNo")) ? null : r.GetString(r.GetOrdinal("BillNo")),
                    BillRefNo = r.IsDBNull(r.GetOrdinal("BillRefNo")) ? null : r.GetString(r.GetOrdinal("BillRefNo")),
                    BillDate = r.IsDBNull(r.GetOrdinal("BillDate")) ? null : r.GetDateTime(r.GetOrdinal("BillDate")),
                    BillBalance = r.IsDBNull(r.GetOrdinal("BillBalance")) ? null : r.GetDecimal(r.GetOrdinal("BillBalance")),
                    SubCodeDescription = r.IsDBNull(r.GetOrdinal("SubCodeDescription")) ? null : r.GetString(r.GetOrdinal("SubCodeDescription")),
                    VoucherNarration = r.IsDBNull(r.GetOrdinal("VoucherNarration")) ? null : r.GetString(r.GetOrdinal("VoucherNarration"))
                },
                commandTimeoutSeconds: 180,
                new SqlParameter("@AccCode", accCode),
                new SqlParameter("@SubAccCode", subCode));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving Bills and Payments. AccCode={accCode}, SubCode={subCode} :" + ex);
            }
        }

        // ── GetVoucherEntriesAsync ────────────────────────────────────────────
        public async Task<IEnumerable<VoucherEntryDto>> GetVoucherEntriesAsync(string voucherNumber, DateTime voucherDate)
        {
            if (string.IsNullOrWhiteSpace(voucherNumber))
                throw new ArgumentException("VoucherNumber is required.", nameof(voucherNumber));

            try
            {
                var sql = @"
SELECT
    GD.ACCOUNTCODE AS AccountCode,
    A.DESCRIPTION AS Description,
    GD.SUBACCOUNTCODE AS SubAccountCode,
    VS.SUBCODEDESCRIPTION AS SubCodeDescription,
    GD.VCHR_DATE AS VoucherDate,
    GD.VCHR_NARRATION AS VoucherNarration,
    GD.LINEDETAILS AS LineDetails,
    GD.DBCRFLAG AS DbCrFlag,
    GD.VOUCHERAMOUNT AS VoucherAmount,
    CASE WHEN GD.DBCRFLAG = 'D' THEN GD.VOUCHERAMOUNT ELSE 0 END AS Debit,
    CASE WHEN GD.DBCRFLAG = 'C' THEN GD.VOUCHERAMOUNT ELSE 0 END AS Credit,
    GD.INSTRUMENTNO AS InstrumentNo,
    GD.INSTRUMENTDATE AS InstrumentDate,
    GD.TDSAMOUNT AS TdsAmount,
    GD.CTRL_SEQUENCENO AS CtrlSequenceNo
FROM CFN_GLDETAIL GD
INNER JOIN CFN_ACCOUNT A ON A.ACCOUNTCODE = GD.ACCOUNTCODE
LEFT JOIN CFN_V_SUBCODESLINK VS
    ON GD.SUBACCOUNTCODE = VS.SUBCODE AND GD.ACCOUNTCODE = VS.ACCOUNTCODE
WHERE GD.VCHR_NUMBER = @VoucherNumber
    AND GD.VCHR_DATE = @VoucherDate
ORDER BY GD.CTRL_SEQUENCENO;";

                return await ExecListAsync(sql, r => new VoucherEntryDto
                {
                    AccountCode = r["AccountCode"] == DBNull.Value ? null : r["AccountCode"].ToString(),
                    Description = r["Description"] == DBNull.Value ? null : r["Description"].ToString(),
                    SubAccountCode = r["SubAccountCode"] == DBNull.Value ? null : r["SubAccountCode"].ToString(),
                    SubCodeDescription = r["SubCodeDescription"] == DBNull.Value ? null : r["SubCodeDescription"].ToString(),
                    VoucherDate = r["VoucherDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["VoucherDate"]),
                    VoucherNarration = r["VoucherNarration"] == DBNull.Value ? null : r["VoucherNarration"].ToString(),
                    LineDetails = r["LineDetails"] == DBNull.Value ? null : r["LineDetails"].ToString(),
                    DbCrFlag = r["DbCrFlag"] == DBNull.Value ? null : r["DbCrFlag"].ToString(),
                    VoucherAmount = r["VoucherAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(r["VoucherAmount"]),
                    Debit = r["Debit"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Debit"]),
                    Credit = r["Credit"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Credit"]),
                    InstrumentNo = r["InstrumentNo"] == DBNull.Value ? null : r["InstrumentNo"].ToString(),
                    InstrumentDate = r["InstrumentDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["InstrumentDate"]),
                    TdsAmount = r["TdsAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(r["TdsAmount"]),
                    CtrlSequenceNo = r["CtrlSequenceNo"] == DBNull.Value ? 0 : Convert.ToInt32(r["CtrlSequenceNo"])
                },
                new SqlParameter("@VoucherNumber", voucherNumber),
                new SqlParameter("@VoucherDate", voucherDate));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving Voucher Entries. VoucherNumber={voucherNumber}, VoucherDate={voucherDate:yyyy-MM-dd} :" + ex);
            }
        }

        public async Task<IEnumerable<CostProductEntryDto>> GetCostProductEntriesAsync(string onHoldNo)
        {
            if (string.IsNullOrWhiteSpace(onHoldNo))
                throw new ArgumentException("OnHoldNo is required.", nameof(onHoldNo));

            try
            {
                var result = await (
                    from cd in _context.CfnCostdetails
                    join acc in _context.CfnAccounts
                        on cd.Accountcode equals acc.Accountcode
                    join cc in _context.CfnCostcentres
                        on cd.Costcentrecode equals cc.Costcentrecode
                    where cd.CtrlOnholdno == onHoldNo
                    select new CostProductEntryDto
                    {
                        AccountCode = acc.Accountcode,
                        Description = acc.Description,
                        CostCentreCode = cd.Costcentrecode,
                        VoucherAmount = cd.Voucheramount ?? 0,
                        CtrlStatus = cd.CtrlStatus,
                        CtrlSequenceNo = Convert.ToInt32(cd.CtrlSequenceno),
                        CostCentreDescription = cc.Description
                    }
                ).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving Cost/Product Entries. OnHoldNo={onHoldNo} : {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<BillsPaymentsAdjustedDto>> GetBillsPaymentsAdjustedAsync(string voucherNumber)
        {
            if (string.IsNullOrWhiteSpace(voucherNumber))
                throw new ArgumentException("VoucherNumber is required.", nameof(voucherNumber));

            try
            {
                var result = await (
                    from ba in _context.CfnBilladjustments
                    join b in _context.CfnBills
                        on ba.BillCtrlonholdno equals b.CtrlOnholdno
                    where ba.PaymentCtrlonholdno == voucherNumber
                    orderby b.VchrDate
                    select new BillsPaymentsAdjustedDto
                    {
                        VoucherNumber = b.VchrNumber,
                        VoucherDate = b.VchrDate,
                        ReferenceNumber = b.VchrRefnumber,
                        ReferenceDate = b.VchrRefdate,
                        BillNumber = b.Billno,
                        BillDate = b.Billdate,
                        BillAdjustedAmount = ba.Billadjusted
                    }
                ).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving Bills/Payments Adjusted. VoucherNumber={voucherNumber} : {ex.Message}", ex);
            }
        }

        private static string DerivePeriodFromDate(DateTime date)
        {
            var months = new[] { "JAN","FEB","MAR","APR","MAY","JUN", "JUL","AUG","SEP","OCT","NOV","DEC" };
            return $"{months[date.Month - 1]} - {date.Year}";
        }

        private async Task<List<T>> ExecListAsync<T>(string sql, Func<SqlDataReader, T> map, params SqlParameter[] parameters)
            => await ExecListAsync(sql, map, 60, parameters);

        private async Task<List<T>> ExecListAsync<T>(string sql, Func<SqlDataReader, T> map, int commandTimeoutSeconds, params SqlParameter[] parameters)
        {
            var list = new List<T>();
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(sql, conn);

                if (parameters is { Length: > 0 })
                    cmd.Parameters.AddRange(parameters);

                cmd.CommandTimeout = commandTimeoutSeconds;
                await conn.OpenAsync().ConfigureAwait(false);

                using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleResult).ConfigureAwait(false);
                while (await reader.ReadAsync().ConfigureAwait(false))
                    list.Add(map(reader));
            }
            catch (Exception ex)
            {
                var paramDump = (parameters == null || parameters.Length == 0)
                    ? "none"
                    : string.Join(", ", parameters.Select(p =>
                        $"{p.ParameterName}={(p.Value == null || p.Value == DBNull.Value ? "NULL" : p.Value)}"));
                throw new ApplicationException($"Database execution failed. Params=[{paramDump}] :" + ex);
            }
            return list;
        }
    }
}