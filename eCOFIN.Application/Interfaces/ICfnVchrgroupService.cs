namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnVchrgroupService
    {
        Task<IEnumerable<CfnVchrgroupDto>> GetAllAsync();
        Task<CfnVchrgroupDto?> GetByIdAsync(string id);
        Task<CfnVchrgroupDto> CreateAsync(CfnVchrgroupDto dto);
        Task<CfnVchrgroupDto> UpdateAsync(CfnVchrgroupDto dto);
        Task<bool> DeleteAsync(string id);
    }
}