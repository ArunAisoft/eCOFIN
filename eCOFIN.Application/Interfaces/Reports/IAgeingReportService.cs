using eCOFIN.Application.DTOs.Reports;

namespace eCOFIN.Application.Interfaces.Reports
{
    public interface IAgeingReportService
    {
        Task<IEnumerable<AgeingReportDto>> GetDebtorsAgeingAsync(DateTime reportDate);
        Task<IEnumerable<AgeingReportDto>> GetCreditorsAgeingAsync(DateTime reportDate);
    }
}
