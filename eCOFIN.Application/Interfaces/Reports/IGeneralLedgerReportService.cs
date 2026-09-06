using eCOFIN.Application.DTOs.Reports;

namespace eCOFIN.Application.Interfaces.Reports
{
    public interface IGeneralLedgerReportService
    {
        Task<IEnumerable<AccPeriodDto>>     GetAccPeriodsAsync();
        Task<IEnumerable<GeneralLedgerReportDto>> GetGeneralLedgerAsync(GeneralLedgerFilter filter);
    }
}
