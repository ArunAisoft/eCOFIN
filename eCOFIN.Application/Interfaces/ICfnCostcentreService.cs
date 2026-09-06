namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnCostcentreService
    {
        Task<IEnumerable<CfnCostcentreDto>> GetAllAsync();
        Task<CfnCostcentreDto?> GetByIdAsync(string id);
        Task<CfnCostcentreDto> CreateAsync(CfnCostcentreDto dto);
        Task<CfnCostcentreDto> UpdateAsync(CfnCostcentreDto dto);
        Task<bool> DeleteAsync(string id);
    }
}