using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class SubLedgerReportService : ISubLedgerReportService
    {
        private readonly BilzFinDbContext _context;

        public SubLedgerReportService(BilzFinDbContext context)
        {
            _context = context;
        }

        // ─────────────────────────────────────────────
        // Acc Periods
        // ─────────────────────────────────────────────
        public async Task<IEnumerable<AccPeriodDto>> GetAccPeriodsAsync()
        {
            return await _context.CfnAccncalenders
                .AsNoTracking()
                .OrderBy(x => x.Sequence)
                .Select(x => new AccPeriodDto
                {
                    AccPeriod = x.Accperiod,
                    PeriodFrom = x.Periodfrom.ToString("dd/MM/yyyy"),
                    PeriodTo = x.Periodto.ToString("dd/MM/yyyy")
                })
                .ToListAsync();
        }

        // ============================================================
        // 🔹 CORE BUILDER (MATCHES UI EXACTLY)
        // ============================================================
        private IQueryable<SubLedgerReportDto> BuildLedger(
            string accountType,
            SubLedgerFilter f,
            bool userFilter = false)
        {
            // ── TRANSACTIONS
            var txn =
                from gl in _context.CfnGldetails
                join acc in _context.CfnAccounts on gl.Accountcode equals acc.Accountcode
                join sub in _context.CfnVSubcodeslinks
                    on new { gl.Accountcode, Sub = gl.Subaccountcode }
                    equals new { sub.Accountcode, Sub = sub.Subcode }

                where acc.Accounttype == accountType
                   && gl.CtrlStatus == "Post"
                   && gl.Voucheramount != 0
                   && gl.VchrDate >= f.FromDate
                   && gl.VchrDate <= f.ToDate

                select new SubLedgerReportDto
                {
                    SequenceNo = "6",
                    AccountCode = gl.Accountcode,
                    AccountDescription = acc.Description,
                    SubAccountCode = gl.Subaccountcode,
                    SubAccountDesc = sub.Subcodedescription,

                    VchrNumber = gl.VchrNumber,
                    VchrDate = gl.VchrDate.ToString("dd/MM/yyyy"),

                    LineDetails = gl.Linedetails ?? ".",

                    OpeningBalance = 0,

                    VoucherAmount = gl.Dbcrflag == "D"
                        ? gl.Voucheramount
                        : -gl.Voucheramount
                };

            // ── OPENING BALANCE
            var opening =
                from gl in _context.CfnGlsubledgers
                join acc in _context.CfnAccounts on gl.Accountcode equals acc.Accountcode
                join sub in _context.CfnVSubcodeslinks
                    on new { gl.Accountcode, Sub = gl.Subaccountcode }
                    equals new { sub.Accountcode, Sub = sub.Subcode }

                where acc.Accounttype == accountType
                   && gl.Accperiod == f.AccPeriod
                   && (
                        (gl.Postedopeningbalance ?? 0)
                      + (gl.Posteddebitamount ?? 0)
                      + (gl.Postedcreditamount ?? 0)
                   ) > 0

                select new SubLedgerReportDto
                {
                    SequenceNo = "2",
                    AccountCode = gl.Accountcode,
                    AccountDescription = acc.Description,
                    SubAccountCode = gl.Subaccountcode,
                    SubAccountDesc = sub.Subcodedescription,

                    VchrNumber = "",
                    VchrDate = f.FromDate.ToString("dd/MM/yyyy"),
                    LineDetails = "",

                    OpeningBalance = gl.Postedobdbcr == "D"
                        ? gl.Postedopeningbalance
                        : -gl.Postedopeningbalance,

                    VoucherAmount = 0
                };

            var query = txn.Concat(opening);

            // ── USER FILTER (ONLY CREDIT LEDGER)
            if (userFilter && !string.IsNullOrEmpty(f.UserName))
            {
                query =
                    from q in query
                    join link in _context.CfnAccountlinks
                        on q.AccountCode equals link.Accountcode
                    where link.Username == f.UserName
                    select q;
            }

            return query
                .OrderBy(x => x.AccountCode)
                .ThenBy(x => x.SubAccountCode)
                .ThenBy(x => x.SequenceNo)
                .ThenBy(x => x.VchrDate);
        }

        // ============================================================
        // 🔹 METHODS (USED BY UI)
        // ============================================================

        public async Task<IEnumerable<SubLedgerReportDto>> GetDebtorLedgerAsync(SubLedgerFilter f)
        {
            return await BuildLedger("DEBT", f).ToListAsync();
        }

        public async Task<IEnumerable<SubLedgerReportDto>> GetCreditLedgerAsync(SubLedgerFilter f)
        {
            return await BuildLedger("CRDT", f, true).ToListAsync();
        }

        public async Task<IEnumerable<SubLedgerReportDto>> GetStaffLoanLedgerAsync(SubLedgerFilter f)
        {
            return await BuildLedger("STPRD", f).ToListAsync();
        }

        public async Task<IEnumerable<SubLedgerReportDto>> GetStaffAdvanceLedgerAsync(SubLedgerFilter f)
        {
            return await BuildLedger("STADV", f).ToListAsync();
        }
    }
}