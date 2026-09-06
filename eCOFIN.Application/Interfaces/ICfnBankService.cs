namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBankService
    {
        Task<IEnumerable<CfnBankDto>> GetAllAsync();
        Task<CfnBankDto?> GetByIdAsync(string id);
        Task<CfnBankDto> CreateAsync(CfnBankDto dto);
        Task<CfnBankDto> UpdateAsync(CfnBankDto dto);
        Task<bool> DeleteAsync(string id);
    }
}