namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICashReceiptsService
    {
        Task<IEnumerable<ExistingCashReceiptDto>> GetAllCashReceiptsAsync(string accPeriod);
        Task<CashReceiptWithDetailsDto> GetCashReceiptWithDetailsAsync(string onHoldNo);
        Task<string> OnHoldCashReceiptAsync(CashReceiptsRequestDto request);
        Task<string> PostCashReceiptAsync(CashReceiptsRequestDto request);
    }
}