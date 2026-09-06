using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.DTOs.Vouchers;

namespace eCOFIN.Application.Interfaces.Masters
{
    public interface IBankInstrumentService
    {
        Task<IEnumerable<BanksAndAccountsDto>> GetBanksWithAccountsAsync();
        Task<IEnumerable<BankInstrumentDto>> GetAllInstrumentsAsync();
        Task<(bool Success, string Message)> SaveOrUpdateInstrumentAsync(BankInstrumentCreateModel model);
    }
}
