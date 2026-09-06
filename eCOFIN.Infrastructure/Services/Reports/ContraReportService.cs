using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Reports
{
    public class ContraReportService : IContraReportService
    {
        private readonly BilzFinDbContext _context;

        public ContraReportService(BilzFinDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ContraReportRowDto>> GetContraReportAsync(ContraReportFilterModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.AccPeriod))
                return Enumerable.Empty<ContraReportRowDto>();

            try
            {
                var accPeriod = filter.AccPeriod.Trim();

                // LEFT JOIN pattern in LINQ: join ... into group, from x in group.DefaultIfEmpty()
                // Matches the SQL LEFT JOINs exactly — rows with null costcentrecode /
                // costtype / expensetype / subaccountcode are still included.
                var query =
                    from contra in _context.CfnVContras

                        // LEFT JOIN cfn_costcentre ON costcentrecode = costcentrecode
                    join cc in _context.CfnCostcentres
                        on contra.Costcentrecode equals cc.Costcentrecode into ccGroup
                    from costCentre in ccGroup.DefaultIfEmpty()

                        // LEFT JOIN cfn_v_costtype ON costtype = parametercode
                    join ct in _context.CfnVCosttypes
                        on contra.Costtype equals ct.Parametercode into ctGroup
                    from costType in ctGroup.DefaultIfEmpty()

                        // LEFT JOIN cfn_v_expensetype ON expensetype = parametercode
                    join et in _context.CfnVExpensetypes
                        on contra.Expensetype equals et.Parametercode into etGroup
                    from expenseType in etGroup.DefaultIfEmpty()

                        // LEFT JOIN cfn_v_subcodlnk ON subaccountcode = subcode
                    join sl in _context.CfnVSubcodlnks
                        on contra.Subaccountcode equals sl.Subcode into slGroup
                    from subcodLink in slGroup.DefaultIfEmpty()

                    where contra.Status == "Post"
                       && contra.Accperiod == accPeriod
                    orderby contra.Voucherdate ascending,
                            contra.Vouchernumber ascending

                    select new ContraReportRowDto
                    {
                        VoucherDate = contra.Voucherdate != null ? contra.Voucherdate.ToString("dd/MM/yyyy") : null,
                        VoucherNumber = contra.Vouchernumber,
                        AccountCode = contra.Accountcode,
                        SubAccountCode = contra.Subaccountcode,
                        AccountDesc = contra.Accountdesc,
                        LineParticulars = contra.Lineparticulars,
                        ReferenceNo = contra.Referenceno,
                        ReferenceDate = contra.Referencedate != null ? contra.Referencedate.Value.ToString("dd/MM/yyyy") : null,
                        Amount = contra.Amount,
                        DbCrFlag = contra.Dbcrflag,
                        CostCentreCode = contra.Costcentrecode,
                        ProductCode = contra.Productcode,
                        ExpenseType = contra.Expensetype,
                        AccPeriod = contra.Accperiod,
                        CostType = contra.Costtype,
                        InstrumentNo = contra.Instrumentno,
                        InstrumentDate = contra.Instrumentdate != null ? contra.Instrumentdate.Value.ToString("dd/MM/yyyy") : null,
                        OnHoldNo = contra.Onholdno,
                        Narration = contra.Narration,
                        VchrRefNumber = contra.Vchrrefnumber,
                        VchrRefDate = contra.Vchrrefdate != null ? contra.Vchrrefdate.Value.ToString("dd/MM/yyyy") : null,
                        VchrType = contra.Vchrtype,
                        VchrCategory = contra.Vchrcatagory,
                        SysCategory = contra.Syscatagory,
                        TotalAmount = contra.Totalamount,
                        ChqAuthorize = contra.Chqauthorize,
                        Status = contra.Status,
                        CancelFlag = contra.Cancelflag,
                        LocationCode = contra.Locationcode,
                        Username = contra.Username,
                        // Null-safe: LEFT JOIN means these may be null when no match
                        SubCodeDescription = subcodLink != null ? subcodLink.Subcodedescription : null,
                        ExpenseTypeDesc = expenseType != null ? expenseType.Parameterdescription : null,
                        CostTypeDesc = costType != null ? costType.Parameterdescription : null,
                        CostCentreDescription = costCentre != null ? costCentre.Description : null
                    };

                return await query.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error executing Contra report: " + ex.Message);
            }
        }
    }
}