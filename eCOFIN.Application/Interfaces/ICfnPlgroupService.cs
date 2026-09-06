namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnPlgroupService
    {
        Task<IEnumerable<CfnPlgroupDto>> GetAllAsync();
        Task<CfnPlgroupDto?> GetByIdAsync(string id);
        Task<CfnPlgroupDto> CreateAsync(CfnPlgroupDto dto);
        Task<CfnPlgroupDto> UpdateAsync(CfnPlgroupDto dto);
        Task<bool> DeleteAsync(string id);
    }
}