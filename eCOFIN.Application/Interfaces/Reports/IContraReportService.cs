using eCOFIN.Application.DTOs.Reports;

namespace eCOFIN.Application.Interfaces.Reports
{
    public interface IContraReportService
    {
        Task<IEnumerable<ContraReportRowDto>> GetContraReportAsync(ContraReportFilterModel filter);
    }
}
