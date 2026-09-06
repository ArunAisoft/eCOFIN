using eCOFIN.Application.DTOs.Reports;

namespace eCOFIN.Application.Interfaces.Reports
{
    public interface ICreditNoteReportService
    {
        Task<IEnumerable<CreditNoteReportRowDto>> GetCreditNoteReportAsync(CreditNoteReportFilterModel filter);
    }
}
