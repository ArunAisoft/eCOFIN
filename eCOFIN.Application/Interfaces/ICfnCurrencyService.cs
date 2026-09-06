namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnCurrencyService
    {
        Task<IEnumerable<CfnCurrencyDto>> GetAllAsync();
        Task<CfnCurrencyDto?> GetByIdAsync(string id);
        Task<CfnCurrencyDto> CreateAsync(CfnCurrencyDto dto);
        Task<CfnCurrencyDto> UpdateAsync(CfnCurrencyDto dto);
        Task<bool> DeleteAsync(string id);
    }
}