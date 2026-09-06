using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class DebitNoteReportService : IDebitNoteReportService
    {
        private readonly BilzFinDbContext _context;

        public DebitNoteReportService(BilzFinDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<DebitNoteReportRowDto>> GetDebitNoteReportAsync(DebitNoteReportFilterModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.AccPeriod))
                return Enumerable.Empty<DebitNoteReportRowDto>();

            try
            {
                var accPeriod = filter.AccPeriod.Trim();

                var query =
                    from dn in _context.CfnDebitnotes

                        // INNER JOIN detail
                    join d in _context.CfnDebndetails
                        on dn.CtrlOnholdno equals d.CtrlOnholdno

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
                        on dn.Subaccountcode equals scB.Subcode into scBGroup
                    from subcodeB in scBGroup.DefaultIfEmpty()

                        // LEFT JOIN cost centre
                    join cc in _context.CfnCostcentres
                        on d.Costcentrecode equals cc.Costcentrecode into ccGroup
                    from costCentre in ccGroup.DefaultIfEmpty()

                        // LEFT JOIN cost type (A.parametergroup = costtype)
                    join ct in _context.CfnCfparvalues
                        on d.Costtype equals ct.Parametergroup into ctGroup
                    from costType in ctGroup.DefaultIfEmpty()

                        // LEFT JOIN expense type (B.parametergroup = expensetype)
                    join et in _context.CfnCfparvalues
                        on d.Expensetype equals et.Parametergroup into etGroup
                    from expenseType in etGroup.DefaultIfEmpty()

                    where dn.CtrlStatus == "Post"
                       && dn.CtrlAccperiod == accPeriod

                       // 🔥 IMPORTANT (matches SQL logic)
                       && (dn.Accountcode != d.Accountcode
                           || dn.Subaccountcode != d.Subaccountcode)

                    orderby dn.VchrDate ascending,
                            dn.VchrNumber ascending

                    select new DebitNoteReportRowDto
                    {
                        AccPeriod = dn.CtrlAccperiod,
                        VoucherNumber = dn.VchrNumber,
                        VoucherDate = dn.VchrDate != null
                            ? dn.VchrDate.ToString("dd/MM/yyyy")
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
                        CostTypeDescription = costType != null ? costType.Parameterdescription : null,
                        ExpenseTypeDescription = expenseType != null ? expenseType.Parameterdescription : null,
                        CostCentreDescription = costCentre != null ? costCentre.Description : null,

                        OnHoldNo = dn.CtrlOnholdno,
                        Narration = dn.VchrNarration,
                        VoucherRefNumber = dn.VchrRefnumber,
                        VoucherRefDate = dn.VchrRefdate != null
                            ? dn.VchrRefdate.Value.ToString("dd/MM/yyyy")
                            : null,

                        Automated = d.Automated,

                        // Header fields
                        HdrAccountCode = dn.Accountcode,
                        HdrSubAccountCode = dn.Subaccountcode,
                        HdrSubAccountDesc = subcodeB != null ? subcodeB.Subcodedescription : null
                    };

                return await query.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error executing Debit Note report: " + ex.Message);
            }
        }
    }
}
