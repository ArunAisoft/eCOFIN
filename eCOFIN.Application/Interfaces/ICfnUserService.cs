namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnUserService
    {
        Task<IEnumerable<CfnUserDto>> GetAllAsync();
        Task<CfnUserDto?> GetByIdAsync(string id);
        Task<CfnUserDto> CreateAsync(CfnUserDto dto);
        Task<CfnUserDto> UpdateAsync(CfnUserDto dto);
        Task<bool> DeleteAsync(string id);
    }
}