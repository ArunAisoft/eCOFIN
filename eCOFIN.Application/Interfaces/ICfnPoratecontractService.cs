namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnPoratecontractService
    {
        Task<IEnumerable<CfnPoratecontractDto>> GetAllAsync();
        Task<CfnPoratecontractDto?> GetByIdAsync(string id);
        Task<CfnPoratecontractDto> CreateAsync(CfnPoratecontractDto dto);
        Task<CfnPoratecontractDto> UpdateAsync(CfnPoratecontractDto dto);
        Task<bool> DeleteAsync(string id);
    }
}