using eCOFIN.Application.DTOs.Masters;

namespace eCOFIN.Application.Interfaces.Masters
{
    public interface IBankService
    {
        Task<List<BankDto>> GetAllBanksAsync();
        Task<(bool Success, string Message)> SaveOrUpdateBankAsync(BankCreateModel model);
    }
}
