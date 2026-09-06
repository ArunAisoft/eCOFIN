using eCOFIN.Application.DTOs.Dashboard;

namespace eCOFIN.Application.Interfaces.Dashboard
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync(string userName, string? bankCode = null, string? accPeriod = null, string? finYear = null, CancellationToken ct = default);

        Task<VoucherTypeDrillDto> GetVoucherTypeDrillAsync(string voucherSysCategory, string userName, string? bankCode = null, string? accPeriod = null, CancellationToken ct = default);

        /// <summary>
        /// bankCode added so the Bank filter scopes the trend chart too.
        /// Previously the trend had no bank parameter, so selecting a bank
        /// silently left this chart showing every bank.
        /// </summary>
        Task<List<MonthlyTrendDto>> GetMonthlyTrendAsync(string? voucherGroup = null, string? accPeriod = null, string? bankCode = null, string? finYear = null, CancellationToken ct = default);

        Task<List<BankSummaryDto>> GetBankSummaryAsync(string userName, string? bankCode = null, string? accPeriod = null, CancellationToken ct = default);

        Task<List<RecentVoucherDto>> GetRecentVouchersAsync(string? voucherSysCategory = null, string? bankCode = null, string? ctrlStatus = null, int top = 20, string? accPeriod = null, CancellationToken ct = default);
    }
}