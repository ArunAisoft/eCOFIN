namespace eCOFIN.Application.Interfaces.Vouchers
{
    using eCOFIN.Application.DTOs.Vouchers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITrialBalanceService
    {
        Task<IEnumerable<TrialBalanceDto>> GetTrialBalanceAsync(string accPeriod);
        Task<IEnumerable<GLDetailDto>> GetGLDetailsAsync(string accPeriod, string accCode, DateTime fromDate, DateTime toDate);
        Task<IEnumerable<SubledgerScheduleDto>> GetSubledgerScheduleAsync(string accPeriod, string accCode);
        Task<IEnumerable<SubledgerAccountDto>> GetSubledgerAccountDetailsAsync(string accPeriod, string accCode, string subCode, DateTime fromDate, DateTime toDate);
        Task<IEnumerable<BillPaymentDto>> GetBillsAndPaymentsAsync(string accCode, string subCode);
        Task<IEnumerable<VoucherEntryDto>> GetVoucherEntriesAsync(string voucherNumber, DateTime voucherDate);
        Task<IEnumerable<CostProductEntryDto>> GetCostProductEntriesAsync(string voucherNumber);
        Task<IEnumerable<BillsPaymentsAdjustedDto>> GetBillsPaymentsAdjustedAsync(string voucherNumber);
    }
}