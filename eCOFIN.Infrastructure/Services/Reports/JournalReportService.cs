using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class JournalReportService : IJournalReportService
    {
        private readonly BilzFinDbContext _context;

        public JournalReportService(BilzFinDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<JournalReportRowDto>> GetJournalReportAsync(JournalReportFilterModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.AccPeriod))
                return Enumerable.Empty<JournalReportRowDto>();

            try
            {
                var accPeriod = filter.AccPeriod.Trim();

                var query =
                    from j in _context.CfnJournals

                        // INNER JOIN detail (mandatory)
                    join d in _context.CfnJrnldetails
                        on j.CtrlOnholdno equals d.CtrlOnholdno

                    // LEFT JOIN account
                    join a in _context.CfnAccounts
                        on d.Accountcode equals a.Accountcode into aGroup
                    from account in aGroup.DefaultIfEmpty()

                        // LEFT JOIN subcode
                    join sc in _context.CfnVSubcodes
                        on d.Subaccountcode equals sc.Subcode into scGroup
                    from subcode in scGroup.DefaultIfEmpty()

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

                    where j.CtrlStatus == "Post"
                       && j.CtrlAccperiod == accPeriod

                    orderby j.VchrDate ascending,
                            j.VchrNumber ascending

                    select new JournalReportRowDto
                    {
                        AccPeriod = j.CtrlAccperiod,

                        VoucherDate = j.VchrDate != null
                            ? j.VchrDate.ToString("dd/MM/yyyy")
                            : null,

                        VoucherNumber = j.VchrNumber,
                        Particulars = d.Lineparticulars,
                        AccountCode = d.Accountcode,
                        SubCode = d.Subaccountcode,
                        CostCentreCode = d.Costcentrecode,
                        OnHoldNo = j.CtrlOnholdno,
                        VoucherRefNumber = j.VchrRefnumber,

                        VoucherRefDate = j.VchrRefdate != null
                            ? j.VchrRefdate.Value.ToString("dd/MM/yyyy")
                            : null,

                        Narration = j.VchrNarration,
                        VchrType = j.VchrType,
                        Category = j.VchrCategory,
                        SysCategory = j.VchrSyscategory,
                        TotalAmount = j.VchrTotalamount,
                        Status = j.CtrlStatus,
                        CancelFlag = j.CtrlCancelflag,
                        LocationCode = j.CtrlLocationcode,
                        Username = j.CtrlUsername,
                        Description = account != null ? account.Description : null,

                        CostType = d.Costtype,
                        ExpenseType = d.Expensetype,
                        ProductCode = d.Productcode,
                        EmployeeCode = d.Employeecode,
                        SegCode2 = d.Segcode2,
                        ReferenceNumber = d.Referencenumber,

                        ReferenceDate = d.Referencedate != null
                            ? d.Referencedate.Value.ToString("dd/MM/yyyy")
                            : null,

                        Amount = d.Dbcramount,
                        DbCrFlag = d.Dbcrflag,

                        // ✅ LEFT JOIN safe fields
                        SubCodeDescription = subcode != null ? subcode.Subcodedescription : null,
                        ExpenseTypeDesc = expenseType != null ? expenseType.Parameterdescription : null,
                        CostTypeDesc = costType != null ? costType.Parameterdescription : null,
                        CostCentreDescription = costCentre != null ? costCentre.Description : null
                    };

                return await query.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error executing Journal report: " + ex.Message);
            }
        }
    }
}
