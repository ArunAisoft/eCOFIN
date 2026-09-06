using eCOFIN.Application.DTOs.Reports;

namespace eCOFIN.Application.Interfaces.Reports
{
    public interface ITravelReportService
    {
        Task<IEnumerable<TravelAccountDto>>   GetTravelAccountsAsync();
        Task<IEnumerable<TravelReportRowDto>> GetTravelReportAsync(TravelReportFilterModel filter);
    }
}
