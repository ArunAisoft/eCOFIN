namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBankPaymentsService
    {
        Task<IEnumerable<ExistingBankPaymentDto>> GetAllBankPaymentsAsync(string accPeriod);
        Task<BankPaymentWithDetailsDto> GetBankPaymentWithDetailsAsync(string onHoldNo);
        Task<string> OnHoldBankPaymentAsync(BankPaymentsRequestDto request);
        Task<string> PostBankPaymentAsync(BankPaymentsRequestDto request);
    }
}