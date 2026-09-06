using eCOFIN.Application.DTOs.Reports;

namespace eCOFIN.Application.Interfaces.Reports
{
    public interface IDebitNoteReportService
    {
        Task<IEnumerable<DebitNoteReportRowDto>> GetDebitNoteReportAsync(DebitNoteReportFilterModel filter);
    }
}
