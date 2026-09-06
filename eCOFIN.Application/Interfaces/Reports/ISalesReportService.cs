using eCOFIN.Application.DTOs.Reports;

namespace eCOFIN.Application.Interfaces.Reports
{
    public interface ISalesReportService
    {
        Task<IEnumerable<SalesAccountDto>>   GetSalesAccountsAsync();
        Task<IEnumerable<SalesReportRowDto>> GetSalesReportAsync(SalesReportFilterModel filter);
    }
}
