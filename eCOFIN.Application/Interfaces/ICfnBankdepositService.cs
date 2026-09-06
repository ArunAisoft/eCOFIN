namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBankdepositService
    {
        Task<IEnumerable<CfnBankdepositDto>> GetAllAsync();
        Task<CfnBankdepositDto?> GetByIdAsync(string id);
        Task<CfnBankdepositDto> CreateAsync(CfnBankdepositDto dto);
        Task<CfnBankdepositDto> UpdateAsync(CfnBankdepositDto dto);
        Task<bool> DeleteAsync(string id);
    }
}