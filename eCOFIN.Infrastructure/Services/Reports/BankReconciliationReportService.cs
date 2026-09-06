using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class BankReconciliationReportService : IBankReconciliationReportService
    {
        private readonly BilzFinDbContext _context;

        public BankReconciliationReportService(BilzFinDbContext context)
        {
            _context = context;
        }

        // ─────────────────────────────────────────────────────────────
        // Acc Periods
        // ─────────────────────────────────────────────────────────────
        public async Task<IEnumerable<AccPeriodDto>> GetAccPeriodsAsync()
        {
            try
            {
                return await _context.CfnAccncalenders
                    .AsNoTracking()
                    .OrderBy(x => x.Sequence)
                    .Select(x => new AccPeriodDto
                    {
                        AccPeriod = x.Accperiod,
                        PeriodFrom = x.Periodfrom.ToString("dd/MM/yyyy"),
                        PeriodTo = x.Periodto.ToString("dd/MM/yyyy"),
                        Sequence = (int?)x.Sequence,
                        FinancialYear = x.Financialyear
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving acc periods: " + ex.Message);
            }
        }

        // ─────────────────────────────────────────────────────────────
        // Cheque Issued Not Presented
        // ─────────────────────────────────────────────────────────────
        public async Task<IEnumerable<BankReconChequeIssuedDto>> GetChequeIssuedNotPresentedAsync(BankReconFilter f)
        {
            try
            {
                return await (
                    from br in _context.CfnBankreconcilliations

                    join bk in _context.CfnBanks
                        on br.Bankcode equals bk.Bankcode into bankJoin
                    from bk in bankJoin.DefaultIfEmpty()

                    where br.Dateofclearence == null
                       && br.Dbcrflag == "C"
                       && br.CtrlAccperiod == f.AccPeriod
                       && br.VchrDate <= f.AsAtDate

                    orderby br.VchrDate, br.Vouchernumber

                    select new BankReconChequeIssuedDto
                    {
                        VoucherNumber = br.Vouchernumber,
                        VchrDate = br.VchrDate.HasValue
                            ? br.VchrDate.Value.ToString("dd/MM/yyyy")
                            : null,
                        InstrumentNo = br.Instrumentno,
                        InstrumentDate = br.Instrumentdate.HasValue
                            ? br.Instrumentdate.Value.ToString("dd/MM/yyyy")
                            : null,
                        AccPeriod = br.CtrlAccperiod,
                        LineParticulars = br.Lineparticulars,
                        BankName = bk != null ? bk.Name : null,
                        Amount = br.Amount
                    }
                ).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Cheque Issued Not Presented: " + ex.Message);
            }
        }

        // ─────────────────────────────────────────────────────────────
        // Cheque Deposited Not Presented
        // ─────────────────────────────────────────────────────────────
        public async Task<IEnumerable<BankReconChequeDepositedDto>> GetChequeDepositedNotPresentedAsync(BankReconFilter f)
        {
            try
            {
                return await (
                    from br in _context.CfnBankreconcilliations

                    join bk in _context.CfnBanks
                        on br.Bankcode equals bk.Bankcode into bankJoin
                    from bk in bankJoin.DefaultIfEmpty()

                    join brd in _context.CfnBnkrdetails
                        on br.CtrlOnholdno equals brd.CtrlOnholdno into brdJoin
                    from brd in brdJoin
                        .Where(x => !string.IsNullOrEmpty(x.Subaccountcode))
                        .DefaultIfEmpty()

                    where br.Dateofclearence == null
                       && br.Dbcrflag == "D"
                       && br.CtrlAccperiod == f.AccPeriod
                       && br.VchrDate <= f.AsAtDate

                    orderby br.VchrDate, br.Vouchernumber

                    select new BankReconChequeDepositedDto
                    {
                        VoucherNumber = br.Vouchernumber,
                        VchrDate = br.VchrDate.HasValue
                            ? br.VchrDate.Value.ToString("dd/MM/yyyy")
                            : null,
                        InstrumentNo = br.Instrumentno,
                        InstrumentDate = br.Instrumentdate.HasValue
                            ? br.Instrumentdate.Value.ToString("dd/MM/yyyy")
                            : null,
                        LineParticulars = br.Lineparticulars,

                        PartyName = _context.CfnVSubcodeslinks
                            .Where(x => x.Accountcode == brd.Accountcode
                                     && x.Subcode == brd.Subaccountcode)
                            .Select(x => x.Subcodedescription)
                            .FirstOrDefault(),

                        BankName = bk != null ? bk.Name : null,

                        Amount = brd != null && !string.IsNullOrEmpty(brd.Subaccountcode)
                            ? brd.Drcramount
                            : br.Amount
                    }
                ).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Cheque Deposited Not Presented: " + ex.Message);
            }
        }

        // ─────────────────────────────────────────────────────────────
        // Debited By Bank Not Accounted
        // ─────────────────────────────────────────────────────────────
        public async Task<IEnumerable<BankReconBankDataDto>> GetDebitedByBankNotAccountedAsync(BankReconFilter f)
        {
            try
            {
                return await (
                    from bd in _context.CfnBankdata

                    join a in _context.CfnAccounts
                        on bd.Accountcode equals a.Accountcode into accJoin
                    from a in accJoin.DefaultIfEmpty()

                    where bd.Dbcrflag == "D"
                       && bd.CtrlOnholdno == null
                       && bd.Instrumentdate <= f.AsAtDate

                    orderby bd.VchrDate, bd.Vouchernumber

                    select new BankReconBankDataDto
                    {
                        VoucherNumber = bd.Vouchernumber,
                        VchrDate = bd.VchrDate.HasValue
                            ? bd.VchrDate.Value.ToString("dd/MM/yyyy")
                            : null,
                        InstrumentNo = bd.Instrumentno,
                        InstrumentDate = bd.Instrumentdate.HasValue
                            ? bd.Instrumentdate.Value.ToString("dd/MM/yyyy")
                            : null,
                        LineParticulars = bd.Lineparticulars,
                        AccountDesc = a != null ? a.Description : null,
                        DbCrFlag = bd.Dbcrflag,
                        Amount = bd.Amount
                    }
                ).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Debited By Bank Not Accounted: " + ex.Message);
            }
        }

        // ─────────────────────────────────────────────────────────────
        // Credited By Bank Not Accounted
        // ─────────────────────────────────────────────────────────────
        public async Task<IEnumerable<BankReconBankDataDto>> GetCreditedByBankNotAccountedAsync(BankReconFilter f)
        {
            try
            {
                return await (
                    from bd in _context.CfnBankdata

                    join a in _context.CfnAccounts
                        on bd.Accountcode equals a.Accountcode into accJoin
                    from a in accJoin.DefaultIfEmpty()

                    where bd.Dbcrflag == "C"
                       && bd.CtrlOnholdno == null
                       && bd.Instrumentdate <= f.AsAtDate

                    orderby bd.VchrDate, bd.Vouchernumber

                    select new BankReconBankDataDto
                    {
                        VoucherNumber = bd.Vouchernumber,
                        VchrDate = bd.VchrDate.HasValue
                            ? bd.VchrDate.Value.ToString("dd/MM/yyyy")
                            : null,
                        InstrumentNo = bd.Instrumentno,
                        InstrumentDate = bd.Instrumentdate.HasValue
                            ? bd.Instrumentdate.Value.ToString("dd/MM/yyyy")
                            : null,
                        LineParticulars = bd.Lineparticulars,
                        AccountDesc = a != null ? a.Description : null,
                        DbCrFlag = bd.Dbcrflag,
                        Amount = bd.Amount
                    }
                ).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Credited By Bank Not Accounted: " + ex.Message);
            }
        }
    }
}