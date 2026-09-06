namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnLevelService
    {
        Task<IEnumerable<CfnLevelDto>> GetAllAsync();
        Task<CfnLevelDto?> GetByIdAsync(string id);
        Task<CfnLevelDto> CreateAsync(CfnLevelDto dto);
        Task<CfnLevelDto> UpdateAsync(CfnLevelDto dto);
        Task<bool> DeleteAsync(string id);
    }
}