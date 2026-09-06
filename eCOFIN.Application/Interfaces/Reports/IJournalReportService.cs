using eCOFIN.Application.DTOs.Reports;

namespace eCOFIN.Application.Interfaces.Reports
{
    public interface IJournalReportService
    {
        Task<IEnumerable<JournalReportRowDto>> GetJournalReportAsync(JournalReportFilterModel filter);
    }
}
