namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnAccountService
    {
        Task<IEnumerable<CfnAccountDto>> GetAllAsync();
        Task<CfnAccountDto?> GetByIdAsync(string id);
        Task<CfnAccountDto> CreateAsync(CfnAccountDto dto);
        Task<CfnAccountDto> UpdateAsync(CfnAccountDto dto);
        Task<bool> DeleteAsync(string id);
    }
}