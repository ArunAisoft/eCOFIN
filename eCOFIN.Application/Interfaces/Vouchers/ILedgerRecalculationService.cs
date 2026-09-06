namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILedgerRecalculationService
    {
        Task RecalculateLedgersAsync(string accPeriod, List<VoucherLineDto> details, string mode);
        Task ReverseOnHoldAmountsAsync(string onHoldNo);
        Task OpenNewPeriodAsync(string newAccPeriod);
    }
}