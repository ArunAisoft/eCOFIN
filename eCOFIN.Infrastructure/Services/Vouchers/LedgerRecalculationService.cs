using eCOFIN.Application.DTOs.Vouchers;
using eCOFIN.Application.Interfaces.Vouchers;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCOFIN.Infrastructure.Services.Vouchers
{
    public class LedgerRecalculationService : ILedgerRecalculationService
    {
        private readonly BilzFinDbContext _context;
        private readonly ILogger<LedgerRecalculationService> _logger;

        public LedgerRecalculationService(BilzFinDbContext context, ILogger<LedgerRecalculationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task RecalculateLedgersAsync(string accPeriod, List<VoucherLineDto> details, string mode)
        {
            try
            {
                var calEntry = await _context.CfnAccncalenders.Where(x => x.Accperiod == accPeriod).Select(x => new { x.Sequence, x.Financialyear }).FirstOrDefaultAsync();
                if (calEntry == null)
                    throw new ApplicationException($"Accounting calendar not configured for period {accPeriod}.");

                var fromSeq = calEntry.Sequence;
                var financialYear = calEntry.Financialyear;

                var toSeq = await _context.CfnAccncalenders.Where(c => c.Periodstate == "OPNPR" && c.Financialyear == financialYear).OrderByDescending(c => c.Sequence).Select(c => c.Sequence).FirstOrDefaultAsync();
                if (toSeq == default || toSeq < fromSeq) toSeq = fromSeq;

                var periods = await _context.CfnAccncalenders.Where(x => x.Sequence >= fromSeq && x.Sequence <= toSeq).OrderBy(x => x.Sequence).Select(x => x.Accperiod).ToListAsync();
                var accounts = details.Select(x => x.AccountCode).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();

                if (mode == "ONHOLD")
                {
                    await ApplyOnholdDebitCreditAsync(accPeriod, details);
                    foreach (var acc in accounts)
                    {
                        var touchedSubs = details.Where(d => d.AccountCode == acc && !string.IsNullOrWhiteSpace(d.SubAccountCode)).Select(d => d.SubAccountCode!).Distinct().ToList();
                        await RecalculateOnholdGeneralLedger(acc, periods);
                        await RecalculateOnholdSubLedger(acc, periods, touchedSubs);
                    }
                }
                else
                {
                    await ApplyPostedDebitCreditAsync(accPeriod, details);
                    foreach (var acc in accounts)
                    {
                        var touchedSubs = details.Where(d => d.AccountCode == acc && !string.IsNullOrWhiteSpace(d.SubAccountCode)).Select(d => d.SubAccountCode!).Distinct().ToList();
                        await RecalculatePostedGeneralLedger(acc, periods);
                        await RecalculatePostedSubLedger(acc, periods, touchedSubs);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recalculating ledgers for period {AccPeriod}", accPeriod);
                throw new ApplicationException($"Error recalculating ledgers for period {accPeriod}: " + ex.Message, ex);
            }
        }

        public async Task ReverseOnHoldAmountsAsync(string onHoldNo)
        {
            try
            {
                var glDetails = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo && g.CtrlStatus == "Hold").ToListAsync();
                if (!glDetails.Any()) return;

                var accPeriod = glDetails.First().Accperiod;
                foreach (var row in glDetails)
                {
                    if (string.IsNullOrWhiteSpace(row.Accountcode)) continue;

                    var amount = row.Voucheramount ?? 0m;
                    var isDebit = string.Equals(row.Dbcrflag, "D", StringComparison.OrdinalIgnoreCase);
                    if (!string.IsNullOrWhiteSpace(row.Subaccountcode))
                    {
                        var sub = await _context.CfnGlsubledgers.FirstOrDefaultAsync(x => x.Accountcode == row.Accountcode && x.Subaccountcode == row.Subaccountcode && x.Accperiod == row.Accperiod);
                        if (sub == null) continue;

                        if (isDebit) sub.Onholddebitamount = (sub.Onholddebitamount ?? 0m) - amount;
                        else sub.Onholdcreditamount = (sub.Onholdcreditamount ?? 0m) - amount;
                    }
                    else
                    {
                        var gl = await _context.CfnGeneralledgers.FirstOrDefaultAsync(x => x.Accountcode == row.Accountcode && x.Accperiod == row.Accperiod);
                        if (gl == null) continue;

                        if (isDebit) gl.Onholddebitamount = (gl.Onholddebitamount ?? 0m) - amount;
                        else gl.Onholdcreditamount = (gl.Onholdcreditamount ?? 0m) - amount;
                    }
                }
                await _context.SaveChangesAsync();

                var accounts = glDetails.Select(g => g.Accountcode).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
                var (periods, _) = await GetPeriodRangeAsync(accPeriod);
                foreach (var acc in accounts)
                {
                    var touchedSubs = glDetails.Where(g => g.Accountcode == acc && !string.IsNullOrWhiteSpace(g.Subaccountcode)).Select(g => g.Subaccountcode!).Distinct().ToList();
                    await RecalculateOnholdGeneralLedger(acc, periods);
                    await RecalculateOnholdSubLedger(acc, periods, touchedSubs);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reversing OnHold amounts for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error reversing OnHold amounts for {onHoldNo}: " + ex.Message, ex);
            }
        }

        public async Task ReversePostedAmountsAsync(string onHoldNo)
        {
            try
            {
                var glDetails = await _context.CfnGldetails.Where(g => g.CtrlOnholdno == onHoldNo && g.CtrlStatus == "Post").ToListAsync();
                if (!glDetails.Any()) return;

                var accPeriod = glDetails.First().Accperiod;
                foreach (var row in glDetails)
                {
                    if (string.IsNullOrWhiteSpace(row.Accountcode)) continue;

                    var amount = row.Voucheramount ?? 0m;
                    var isDebit = string.Equals(row.Dbcrflag, "D", StringComparison.OrdinalIgnoreCase);
                    if (!string.IsNullOrWhiteSpace(row.Subaccountcode))
                    {
                        var sub = await _context.CfnGlsubledgers.FirstOrDefaultAsync(x => x.Accountcode == row.Accountcode && x.Subaccountcode == row.Subaccountcode && x.Accperiod == row.Accperiod);
                        if (sub == null) continue;

                        if (isDebit)
                        {
                            sub.Onholddebitamount = (sub.Onholddebitamount ?? 0m) - amount;
                            sub.Posteddebitamount = (sub.Posteddebitamount ?? 0m) - amount;
                        }
                        else
                        {
                            sub.Onholdcreditamount = (sub.Onholdcreditamount ?? 0m) - amount;
                            sub.Postedcreditamount = (sub.Postedcreditamount ?? 0m) - amount;
                        }
                    }
                    else
                    {
                        var gl = await _context.CfnGeneralledgers.FirstOrDefaultAsync(x => x.Accountcode == row.Accountcode && x.Accperiod == row.Accperiod);
                        if (gl == null) continue;

                        if (isDebit)
                        {
                            gl.Onholddebitamount = (gl.Onholddebitamount ?? 0m) - amount;
                            gl.Posteddebitamount = (gl.Posteddebitamount ?? 0m) - amount;
                        }
                        else
                        {
                            gl.Onholdcreditamount = (gl.Onholdcreditamount ?? 0m) - amount;
                            gl.Postedcreditamount = (gl.Postedcreditamount ?? 0m) - amount;
                        }
                    }
                }
                await _context.SaveChangesAsync();

                var accounts = glDetails.Select(g => g.Accountcode).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
                var (periods, _) = await GetPeriodRangeAsync(accPeriod);

                foreach (var acc in accounts)
                {
                    var touchedSubs = glDetails.Where(g => g.Accountcode == acc && !string.IsNullOrWhiteSpace(g.Subaccountcode)).Select(g => g.Subaccountcode!).Distinct().ToList();
                    await RecalculateOnholdGeneralLedger(acc, periods);
                    await RecalculateOnholdSubLedger(acc, periods, touchedSubs);
                    await RecalculatePostedGeneralLedger(acc, periods);
                    await RecalculatePostedSubLedger(acc, periods, touchedSubs);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reversing Posted amounts for {OnHoldNo}", onHoldNo);
                throw new ApplicationException($"Error reversing Posted amounts for {onHoldNo}: " + ex.Message, ex);
            }
        }

        private async Task ApplyOnholdDebitCreditAsync(string accPeriod, List<VoucherLineDto> details)
        {
            try
            {
                foreach (var d in details)
                {
                    if (string.IsNullOrWhiteSpace(d.AccountCode)) continue;

                    var amount = d.DrCrAmount ?? 0m;
                    var isDebit = string.IsNullOrEmpty(d.DbCrFlag) || d.DbCrFlag == "D";
                    if (!string.IsNullOrWhiteSpace(d.SubAccountCode))
                    {
                        var sub = await GetOrCreateSubLedgerAsync(accPeriod, d.AccountCode, d.SubAccountCode);
                        if (isDebit) sub.Onholddebitamount = (sub.Onholddebitamount ?? 0m) + amount;
                        else sub.Onholdcreditamount = (sub.Onholdcreditamount ?? 0m) + amount;
                    }
                    else
                    {
                        var gl = await GetOrCreateGeneralLedgerAsync(accPeriod, d.AccountCode);
                        if (isDebit) gl.Onholddebitamount = (gl.Onholddebitamount ?? 0m) + amount;
                        else gl.Onholdcreditamount = (gl.Onholdcreditamount ?? 0m) + amount;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error applying OnHold debit/credit");
                throw new ApplicationException("Error applying OnHold debit/credit: " + ex.Message, ex);
            }
        }

        private async Task ApplyPostedDebitCreditAsync(string accPeriod, List<VoucherLineDto> details)
        {
            try
            {
                foreach (var d in details)
                {
                    if (string.IsNullOrWhiteSpace(d.AccountCode)) continue;

                    var amount = d.DrCrAmount ?? 0m;
                    var isDebit = string.IsNullOrEmpty(d.DbCrFlag) || d.DbCrFlag == "D";
                    if (!string.IsNullOrWhiteSpace(d.SubAccountCode))
                    {
                        var sub = await GetOrCreateSubLedgerAsync(accPeriod, d.AccountCode, d.SubAccountCode);
                        if (isDebit)
                        {
                            sub.Onholddebitamount = (sub.Onholddebitamount ?? 0m) + amount;
                            sub.Posteddebitamount = (sub.Posteddebitamount ?? 0m) + amount;
                        }
                        else
                        {
                            sub.Onholdcreditamount = (sub.Onholdcreditamount ?? 0m) + amount;
                            sub.Postedcreditamount = (sub.Postedcreditamount ?? 0m) + amount;
                        }
                    }
                    else
                    {
                        var gl = await GetOrCreateGeneralLedgerAsync(accPeriod, d.AccountCode);
                        if (isDebit)
                        {
                            gl.Onholddebitamount = (gl.Onholddebitamount ?? 0m) + amount;
                            gl.Posteddebitamount = (gl.Posteddebitamount ?? 0m) + amount;
                        }
                        else
                        {
                            gl.Onholdcreditamount = (gl.Onholdcreditamount ?? 0m) + amount;
                            gl.Postedcreditamount = (gl.Postedcreditamount ?? 0m) + amount;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error applying Posted debit/credit");
                throw new ApplicationException("Error applying Posted debit/credit: " + ex.Message, ex);
            }
        }

        private async Task RecalculateOnholdGeneralLedger(string accountCode, List<string> periods)
        {
            try
            {
                decimal prevBal = 0m;
                string prevDbCr = "D";
                bool first = true;
                foreach (var p in periods)
                {
                    var gl = await GetOrCreateGeneralLedgerAsync(p, accountCode);
                    if (!first)
                    {
                        gl.Onholdopeningbalance = prevBal;
                        gl.Onholdobdbcr = prevDbCr;
                    }

                    decimal openingSigned = gl.Onholdobdbcr == "D" ? (gl.Onholdopeningbalance ?? 0m) : -(gl.Onholdopeningbalance ?? 0m);
                    decimal closingSigned = openingSigned + (gl.Onholddebitamount ?? 0m) - (gl.Onholdcreditamount ?? 0m);

                    gl.Onholdclosingbalance = Math.Abs(closingSigned);
                    gl.Onholdcbdbcr = closingSigned >= 0 ? "D" : "C";

                    prevBal = gl.Onholdclosingbalance ?? 0m;
                    prevDbCr = gl.Onholdcbdbcr;
                    first = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recalculating OnHold GL for {AccountCode}", accountCode);
                throw new ApplicationException($"Error recalculating OnHold GL for {accountCode}: " + ex.Message, ex);
            }
        }

        private async Task RecalculateOnholdSubLedger(string accountCode, List<string> periods, List<string>? touchedSubAccounts = null)
        {
            try
            {
                var subAccounts = (touchedSubAccounts != null && touchedSubAccounts.Any()) ? touchedSubAccounts.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().Cast<string?>().ToList() : await _context.CfnGlsubledgers.Where(x => x.Accountcode == accountCode).Select(x => x.Subaccountcode).Distinct().ToListAsync();
                foreach (var subAcc in subAccounts)
                {
                    decimal prevBal = 0m;
                    string prevDbCr = "D";
                    bool first = true;
                    foreach (var p in periods)
                    {
                        var sub = await GetOrCreateSubLedgerAsync(p, accountCode, subAcc!);
                        if (!first)
                        {
                            sub.Onholdopeningbalance = prevBal;
                            sub.Onholdobdbcr = prevDbCr;
                        }

                        decimal openingSigned = sub.Onholdobdbcr == "D" ? (sub.Onholdopeningbalance ?? 0m) : -(sub.Onholdopeningbalance ?? 0m);
                        decimal closingSigned = openingSigned + (sub.Onholddebitamount ?? 0m) - (sub.Onholdcreditamount ?? 0m);

                        sub.Onholdclosingbalance = Math.Abs(closingSigned);
                        sub.Onholdcbdbcr = closingSigned >= 0 ? "D" : "C";

                        prevBal = sub.Onholdclosingbalance ?? 0m;
                        prevDbCr = sub.Onholdcbdbcr;
                        first = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recalculating OnHold SubLedger for {AccountCode}", accountCode);
                throw new ApplicationException($"Error recalculating OnHold SubLedger for {accountCode}: " + ex.Message, ex);
            }
        }

        private async Task RecalculatePostedGeneralLedger(string accountCode, List<string> periods)
        {
            try
            {
                decimal prevBal = 0m;
                string prevDbCr = "D";
                bool first = true;
                foreach (var p in periods)
                {
                    var gl = await GetOrCreateGeneralLedgerAsync(p, accountCode);
                    if (!first)
                    {
                        gl.Postedopeningbalance = prevBal;
                        gl.Postedobdbcr = prevDbCr;
                    }

                    decimal openingSigned = gl.Postedobdbcr == "D" ? (gl.Postedopeningbalance ?? 0m) : -(gl.Postedopeningbalance ?? 0m);
                    decimal closingSigned = openingSigned + (gl.Posteddebitamount ?? 0m) - (gl.Postedcreditamount ?? 0m);

                    gl.Postedclosingbalance = Math.Abs(closingSigned);
                    gl.Postedcbdbcr = closingSigned >= 0 ? "D" : "C";

                    prevBal = gl.Postedclosingbalance ?? 0m;
                    prevDbCr = gl.Postedcbdbcr;
                    first = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recalculating Posted GL for {AccountCode}", accountCode);
                throw new ApplicationException($"Error recalculating Posted GL for {accountCode}: " + ex.Message, ex);
            }
        }

        private async Task RecalculatePostedSubLedger(string accountCode, List<string> periods, List<string>? touchedSubAccounts = null)
        {
            try
            {
                var subAccounts = (touchedSubAccounts != null && touchedSubAccounts.Any()) ? touchedSubAccounts.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().Cast<string?>().ToList() : await _context.CfnGlsubledgers.Where(x => x.Accountcode == accountCode).Select(x => x.Subaccountcode).Distinct().ToListAsync();
                foreach (var subAcc in subAccounts)
                {
                    decimal prevBal = 0m;
                    string prevDbCr = "D";
                    bool first = true;
                    foreach (var p in periods)
                    {
                        var sub = await GetOrCreateSubLedgerAsync(p, accountCode, subAcc!);
                        if (!first)
                        {
                            sub.Postedopeningbalance = prevBal;
                            sub.Postedobdbcr = prevDbCr;
                        }

                        decimal openingSigned = sub.Postedobdbcr == "D" ? (sub.Postedopeningbalance ?? 0m) : -(sub.Postedopeningbalance ?? 0m);
                        decimal closingSigned = openingSigned + (sub.Posteddebitamount ?? 0m) - (sub.Postedcreditamount ?? 0m);

                        sub.Postedclosingbalance = Math.Abs(closingSigned);
                        sub.Postedcbdbcr = closingSigned >= 0 ? "D" : "C";

                        prevBal = sub.Postedclosingbalance ?? 0m;
                        prevDbCr = sub.Postedcbdbcr;
                        first = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recalculating Posted SubLedger for {AccountCode}", accountCode);
                throw new ApplicationException($"Error recalculating Posted SubLedger for {accountCode}: " + ex.Message, ex);
            }
        }

        private async Task<(List<string> Periods, decimal ToSeq)> GetPeriodRangeAsync(string accPeriod)
        {
            var calEntry = await _context.CfnAccncalenders.Where(x => x.Accperiod == accPeriod).Select(x => new { x.Sequence, x.Financialyear }).FirstOrDefaultAsync();
            if (calEntry == null)
                return (new List<string> { accPeriod }, 0);

            var fromSeq = calEntry.Sequence;
            var financialYear = calEntry.Financialyear;

            var toSeq = await _context.CfnAccncalenders.Where(c => c.Periodstate == "OPNPR" && c.Financialyear == financialYear).OrderByDescending(c => c.Sequence).Select(c => c.Sequence).FirstOrDefaultAsync();
            if (toSeq == default || toSeq < fromSeq) toSeq = fromSeq;

            var periods = await _context.CfnAccncalenders.Where(x => x.Sequence >= fromSeq && x.Sequence <= toSeq).OrderBy(x => x.Sequence).Select(x => x.Accperiod).ToListAsync();
            return (periods, toSeq);
        }

        private async Task<CfnGeneralledger> GetOrCreateGeneralLedgerAsync(string accPeriod, string accountCode)
        {
            var tracked = _context.ChangeTracker.Entries<CfnGeneralledger>().FirstOrDefault(e => e.Entity.Accountcode == accountCode && e.Entity.Accperiod == accPeriod);
            if (tracked != null) return tracked.Entity;

            var existing = await _context.CfnGeneralledgers.FirstOrDefaultAsync(x => x.Accountcode == accountCode && x.Accperiod == accPeriod);
            if (existing != null) return existing;

            var currentSeq = await _context.CfnAccncalenders.Where(c => c.Accperiod == accPeriod).Select(c => c.Sequence).FirstOrDefaultAsync();

            var prev = await _context.CfnGeneralledgers
                .AsNoTracking()
                .Join(_context.CfnAccncalenders,
                    g => g.Accperiod,
                    c => c.Accperiod,
                    (g, c) => new { g, c.Sequence })
                .Where(x => x.g.Accountcode == accountCode
                         && x.Sequence < currentSeq
                         && ((x.g.Postedopeningbalance ?? 0m) != 0m
                          || (x.g.Posteddebitamount ?? 0m) != 0m
                          || (x.g.Postedcreditamount ?? 0m) != 0m
                          || (x.g.Onholddebitamount ?? 0m) != 0m
                          || (x.g.Onholdcreditamount ?? 0m) != 0m))
                .OrderByDescending(x => x.Sequence)
                .Select(x => new
                {
                    x.g.Postedclosingbalance,
                    x.g.Postedcbdbcr,
                    x.g.Onholdclosingbalance,
                    x.g.Onholdcbdbcr
                })
                .FirstOrDefaultAsync();

            var gl = new CfnGeneralledger
            {
                Accperiod = accPeriod,
                Accountcode = accountCode,
                Postedobdbcr = prev?.Postedcbdbcr ?? "D",
                Postedopeningbalance = prev?.Postedclosingbalance ?? 0m,
                Posteddebitamount = 0m,
                Postedcreditamount = 0m,
                Postedclosingbalance = prev?.Postedclosingbalance ?? 0m,
                Postedcbdbcr = prev?.Postedcbdbcr ?? "D",
                Onholdobdbcr = prev?.Onholdcbdbcr ?? "D",
                Onholdopeningbalance = prev?.Onholdclosingbalance ?? 0m,
                Onholddebitamount = 0m,
                Onholdcreditamount = 0m,
                Onholdclosingbalance = prev?.Onholdclosingbalance ?? 0m,
                Onholdcbdbcr = prev?.Onholdcbdbcr ?? "D"
            };

            _context.CfnGeneralledgers.Add(gl);
            return gl;
        }

        private async Task<CfnGlsubledger> GetOrCreateSubLedgerAsync(string accPeriod, string accountCode, string subAcc)
        {
            var tracked = _context.ChangeTracker.Entries<CfnGlsubledger>().FirstOrDefault(e => e.Entity.Accountcode == accountCode && e.Entity.Subaccountcode == subAcc && e.Entity.Accperiod == accPeriod);
            if (tracked != null) return tracked.Entity;

            var existing = await _context.CfnGlsubledgers.FirstOrDefaultAsync(x => x.Accountcode == accountCode && x.Subaccountcode == subAcc && x.Accperiod == accPeriod);
            if (existing != null) return existing;

            var currentSeq = await _context.CfnAccncalenders.Where(c => c.Accperiod == accPeriod).Select(c => c.Sequence).FirstOrDefaultAsync();
            var prev = await _context.CfnGlsubledgers
                .AsNoTracking()
                .Join(_context.CfnAccncalenders,
                    g => g.Accperiod,
                    c => c.Accperiod,
                    (g, c) => new { g, c.Sequence })
                .Where(x => x.g.Accountcode == accountCode
                         && x.g.Subaccountcode == subAcc
                         && x.Sequence < currentSeq
                         && ((x.g.Postedopeningbalance ?? 0m) != 0m
                          || (x.g.Posteddebitamount ?? 0m) != 0m
                          || (x.g.Postedcreditamount ?? 0m) != 0m
                          || (x.g.Onholddebitamount ?? 0m) != 0m
                          || (x.g.Onholdcreditamount ?? 0m) != 0m))
                .OrderByDescending(x => x.Sequence)
                .Select(x => new
                {
                    x.g.Postedclosingbalance,
                    x.g.Postedcbdbcr,
                    x.g.Onholdclosingbalance,
                    x.g.Onholdcbdbcr
                })
                .FirstOrDefaultAsync();

            var sub = new CfnGlsubledger
            {
                Accperiod = accPeriod,
                Accountcode = accountCode,
                Subaccountcode = subAcc,
                Postedobdbcr = prev?.Postedcbdbcr ?? "D",
                Postedopeningbalance = prev?.Postedclosingbalance ?? 0m,
                Posteddebitamount = 0m,
                Postedcreditamount = 0m,
                Postedclosingbalance = prev?.Postedclosingbalance ?? 0m,
                Postedcbdbcr = prev?.Postedcbdbcr ?? "D",
                Onholdobdbcr = prev?.Onholdcbdbcr ?? "D",
                Onholdopeningbalance = prev?.Onholdclosingbalance ?? 0m,
                Onholddebitamount = 0m,
                Onholdcreditamount = 0m,
                Onholdclosingbalance = prev?.Onholdclosingbalance ?? 0m,
                Onholdcbdbcr = prev?.Onholdcbdbcr ?? "D"
            };

            _context.CfnGlsubledgers.Add(sub);
            return sub;
        }

        public async Task OpenNewPeriodAsync(string newAccPeriod)
        {
            try
            {
                _logger.LogInformation("OpenNewPeriod started for {Period}", newAccPeriod);
                var allAccounts = await _context.CfnAccounts.Where(a => a.Accountstatus == "ACTVE").Select(a => a.Accountcode).ToListAsync();
                int total = allAccounts.Count;
                int current = 0;

                foreach (var accCode in allAccounts)
                {
                    await GetOrCreateGeneralLedgerAsync(newAccPeriod, accCode);

                    var subAccounts = await _context.CfnGlsubledgers.Where(x => x.Accountcode == accCode).Select(x => x.Subaccountcode).Distinct().ToListAsync();
                    foreach (var subAcc in subAccounts)
                        if (!string.IsNullOrWhiteSpace(subAcc))
                            await GetOrCreateSubLedgerAsync(newAccPeriod, accCode, subAcc!);

                    current++;

                    if (current % 100 == 0)
                    {
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("OpenNewPeriod progress: {Current}/{Total}", current, total);
                    }
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("OpenNewPeriod completed for {Period}. Total: {Total}", newAccPeriod, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OpenNewPeriodAsync for {Period}", newAccPeriod);
                throw;
            }
        }
    }
}