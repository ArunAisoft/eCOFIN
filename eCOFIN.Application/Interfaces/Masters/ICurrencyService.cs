namespace eCOFIN.Application.Interfaces.Masters
{
    using eCOFIN.Application.DTOs.Masters;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDto>> GetAllCurrenciesAsync();
        Task<IEnumerable<CurrencyDto>> GetAllActiveCurrenciesAsync();
        Task<(bool Success, string Message)> SaveOrUpdateCurrencyAsync(CurrencyDto model);
    }
}