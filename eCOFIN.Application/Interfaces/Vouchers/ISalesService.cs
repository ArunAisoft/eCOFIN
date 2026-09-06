namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISalesService
    {
        Task<IEnumerable<ExistingSaleDto>> GetAllSalesAsync(string accPeriod);
        Task<SaleWithDetailsDto> GetSaleWithDetailsAsync(string onHoldNo);
        Task<string> OnHoldSaleAsync(SalesRequestDto request);
        Task<string> PostSaleAsync(SalesRequestDto request);
        Task<IEnumerable<SaleExportModel>> GetSaleDomesticDataAsync(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<SaleExportModel>> GetSaleExportDataAsync(DateTime fromDate, DateTime toDate);
        Task<List<string>> OnHoldMultipleDomesticSalesAsync(List<string> invoiceNos, string voucherType, string accountingPeriod, string username, string locationCode, DateTime voucherDate);
        Task<List<string>> OnHoldMultipleExportSalesAsync(List<string> invoiceNos, string voucherType, string accountingPeriod, string username, string locationCode, DateTime voucherDate);
        Task<PostMultipleResult> PostMultipleSalesAsync(List<string> onHoldNumbers, string accountingPeriod, string username, string locationCode);
    }
}