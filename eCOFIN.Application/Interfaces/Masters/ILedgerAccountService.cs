using eCOFIN.Application.DTOs.Masters;

namespace eCOFIN.Application.Interfaces.Masters
{
    public interface ILedgerAccountService
    {
        Task<List<LedgerAccountDto>> GetAllAccountsAsync();
        Task<List<ParameterDto>> GetAccountTypesAsync();
        Task<List<ParameterDto>> GetAccountNaturesAsync();
        Task<List<BankListDto>> GetBanksAsync();
        Task<List<EfcAccountDto>> GetEfcAccountsAsync();
        Task<(bool Success, string Message)> SaveOrUpdateAccountAsync(SaveAccountRequest model);
    }
}
