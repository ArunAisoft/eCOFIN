namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IGinMasterService
    {
        Task<IEnumerable<GinMasterDto>> GetAllAsync();
        Task<GinMasterDto?> GetByIdAsync(string id);
        Task<GinMasterDto> CreateAsync(GinMasterDto dto);
        Task<GinMasterDto> UpdateAsync(GinMasterDto dto);
        Task<bool> DeleteAsync(string id);
    }
}