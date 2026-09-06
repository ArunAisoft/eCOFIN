using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class CreditNoteReportService : ICreditNoteReportService
    {
        private readonly BilzFinDbContext _context;

        public CreditNoteReportService(BilzFinDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CreditNoteReportRowDto>> GetCreditNoteReportAsync(CreditNoteReportFilterModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.AccPeriod))
                return Enumerable.Empty<CreditNoteReportRowDto>();

            try
            {
                var accPeriod = filter.AccPeriod.Trim();

                var query =
                    from cn in _context.CfnCreditnotes

                        // INNER JOIN detail
                    join d in _context.CfnCrdndetails
                        on cn.CtrlOnholdno equals d.CtrlOnholdno

                    // LEFT JOIN account
                    join a in _context.CfnAccounts
                        on d.Accountcode equals a.Accountcode into aGroup
                    from account in aGroup.DefaultIfEmpty()

                        // LEFT JOIN subcode (line)
                    join scA in _context.CfnVSubcodlnks
                        on d.Subaccountcode equals scA.Subcode into scAGroup
                    from subcodeA in scAGroup.DefaultIfEmpty()

                        // LEFT JOIN subcode (header)
                    join scB in _context.CfnVSubcodlnks
                        on cn.Subaccountcode equals scB.Subcode into scBGroup
                    from subcodeB in scBGroup.DefaultIfEmpty()

                        // LEFT JOIN cost centre
                    join cc in _context.CfnCostcentres
                        on d.Costcentrecode equals cc.Costcentrecode into ccGroup
                    from costCentre in ccGroup.DefaultIfEmpty()

                        // LEFT JOIN cost type
                    join ct in _context.CfnVCosttypes
                        on d.Costtype equals ct.Parametercode into ctGroup
                    from costType in ctGroup.DefaultIfEmpty()

                        // LEFT JOIN expense type
                    join et in _context.CfnVExpensetypes
                        on d.Expensetype equals et.Parametercode into etGroup
                    from expenseType in etGroup.DefaultIfEmpty()

                    where cn.CtrlStatus == "Post"
                       && cn.CtrlAccperiod == accPeriod

                       // 🔥 Important condition from your SQL
                       && (cn.Accountcode != d.Accountcode
                           || cn.Subaccountcode != d.Subaccountcode)

                    orderby cn.VchrDate ascending,
                            cn.VchrNumber ascending

                    select new CreditNoteReportRowDto
                    {
                        AccPeriod = cn.CtrlAccperiod,
                        VoucherNumber = cn.VchrNumber,
                        VoucherDate = cn.VchrDate != null
                            ? cn.VchrDate.ToString("dd/MM/yyyy")
                            : null,

                        LineNo1 = d.CtrlSequenceno.ToString(),
                        Particulars = d.Lineparticulars,
                        AccountCode = d.Accountcode,
                        SubAccountCode = d.Subaccountcode,
                        AccountDescription = account != null ? account.Description : null,
                        SubAccountDescription = subcodeA != null ? subcodeA.Subcodedescription : null,

                        ReferenceNo = d.Referencenumber,
                        ReferenceDate = d.Referencedate != null
                            ? d.Referencedate.Value.ToString("dd/MM/yyyy")
                            : null,

                        Amount = d.Dbcramount,
                        DbCrFlag = d.Dbcrflag,

                        CostType = d.Costtype,
                        ProductCode = d.Productcode,
                        ExpenseType = d.Expensetype,
                        EmployeeCode = d.Employeecode,
                        CostCentreCode = d.Costcentrecode,

                        Description = account != null ? account.Description : null,
                        CostTypeDesc = costType != null ? costType.Parameterdescription : null,
                        ExpenseTypeDesc = expenseType != null ? expenseType.Parameterdescription : null,
                        CostCentreDescription = costCentre != null ? costCentre.Description : null,

                        OnHoldNo = cn.CtrlOnholdno,
                        Narration = cn.VchrNarration,
                        Automated = d.Automated,

                        // Header info
                        HdrAccountCode = cn.Accountcode,
                        HdrSubAccountCode = cn.Subaccountcode,
                        HdrSubAccountDesc = subcodeB != null ? subcodeB.Subcodedescription : null
                    };

                return await query.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error executing Credit Note report: " + ex.Message);
            }
        }
    }
}
