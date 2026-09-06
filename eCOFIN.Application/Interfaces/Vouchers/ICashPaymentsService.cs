namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICashPaymentsService
    {
        Task<IEnumerable<ExistingCashPaymentDto>> GetAllCashPaymentsAsync(string accPeriod);
        Task<CashPaymentWithDetailsDto> GetCashPaymentWithDetailsAsync(string onHoldNo);
        Task<string> OnHoldCashPaymentAsync(CashPaymentsRequestDto request);
        Task<string> PostCashPaymentAsync(CashPaymentsRequestDto request);
    }
}