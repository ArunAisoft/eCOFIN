using eCOFIN.Application.DTOs.Reports;

namespace eCOFIN.Application.Interfaces.Reports
{
    public interface IPurchaseReportService
    {
        Task<IEnumerable<PurchaseAccountDto>>   GetPurchaseAccountsAsync();
        Task<IEnumerable<PurchaseReportRowDto>> GetPurchaseReportAsync(PurchaseReportFilterModel filter);
    }
}
