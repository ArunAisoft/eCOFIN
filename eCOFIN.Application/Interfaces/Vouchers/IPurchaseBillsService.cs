namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPurchaseBillsService
    {
        Task<IEnumerable<ExistingPurchaseBillDto>> GetAllPurchaseBillsAsync(string accPeriod);
        Task<PurchaseBillWithDetailsDto> GetPurchaseBillWithDetailsAsync(string onHoldNo);
        Task<string> OnHoldPurchaseBillAsync(PurchaseBillsRequestDto request);
        Task<string> PostPurchaseBillAsync(PurchaseBillsRequestDto request);
        Task<IEnumerable<GINImportModel>> GetGINImportDataAsync(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<JINImportModel>> GetJINImportDataAsync(DateTime fromDate, DateTime toDate);
        Task<List<string>> OnHoldMultipleGINAsync(List<string> ginNumbers, string voucherType, string accountingPeriod, string username, string locationCode, DateTime voucherDate);
        Task<List<string>> OnHoldMultipleJINAsync(List<string> jinNumbers, string voucherType, string accountingPeriod, string username, string locationCode, DateTime voucherDate);
        Task<PostMultipleResult> PostMultiplePurchaseBillsAsync(List<string> onHoldNumbers, string accountingPeriod, string username, string locationCode);
    }
}