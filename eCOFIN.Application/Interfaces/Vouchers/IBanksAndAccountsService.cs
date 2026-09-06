using eCOFIN.Application.DTOs.Vouchers;

namespace eCOFIN.Application.Interfaces.Vouchers
{
    public interface IBanksAndAccountsService
    {
        Task<IEnumerable<BanksAndAccountsDto>> GetAllBanksWithAccountsAsync(CancellationToken ct = default);
        Task<IEnumerable<BanksAndAccountsDto>> GetAllBanksWithAccountsVouchersBalancesAsync(string userName, string voucherGroup, CancellationToken ct = default);
        Task<IEnumerable<BankAccountDto>> GetAllAccountsVouchersBalancesAsync(string userName, string accountType, string voucherGroup, CancellationToken ct = default);
        Task<IEnumerable<BanksAndAccountsDto>> GetAllCreditAccountsVouchersAsync(string userName, string voucherGroup, CancellationToken ct = default);
        Task<IEnumerable<BanksAndAccountsDto>> GetAllCreditDebitAccountsVouchersAsync(string userName, string voucherGroup, CancellationToken ct = default);
        Task<IEnumerable<BanksAndAccountsDto>> GetAllDebitAccountsVouchersAsync(string userName, string voucherGroup, CancellationToken ct = default);
        Task<IEnumerable<VoucherTypeDto>> GetAllDebitCreditVoucherTypesAsync(string userName, string voucherGroup, CancellationToken ct = default);
        Task<IEnumerable<GroupAccountDto>> GetAllGroupAccountsByVoucherTypeAsync(string voucherType, CancellationToken ct = default);
        Task<List<GroupSubAccountDto>> GetAllSubAccountsByCodeAndTypeAsync(string accountCode, string accountType, bool includeCostCentres = false, CancellationToken ct = default);
        Task<IEnumerable<ExportVendorInvoiceDto>> GetVendorInvoicesAsync(string invoiceAccount, string invoiceVendor, CancellationToken ct = default);
        Task<BillAndPaymentDto> GetBillAndPaymentDetailsAsync(string accountCode, string subAccountCode, CancellationToken ct = default);
        Task SaveBillAndPaymentAdjustmentAsync(BillPaymentAdjustmentRequestDto request, CancellationToken ct = default);
        Task<bool> IsBillAlreadyExistsAsync(string bankCode, string bankAccount, string billNo, DateTime billDate, string? excludeOnHoldNo = null, CancellationToken ct = default);
    }
}