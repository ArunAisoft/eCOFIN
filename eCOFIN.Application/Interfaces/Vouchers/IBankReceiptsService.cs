namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBankReceiptsService
    {
        Task<IEnumerable<ExistingBankReceiptDto>> GetAllBankReceiptsAsync(string accPeriod);
        Task<BankReceiptWithDetailsDto> GetBankReceiptWithDetailsAsync(string onHoldNo);
        Task<string> OnHoldBankReceiptAsync(BankReceiptsRequestDto request);
        Task<string> PostBankReceiptAsync(BankReceiptsRequestDto request);
    }
}