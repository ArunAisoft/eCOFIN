namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBilladjustmentService
    {
        Task<IEnumerable<CfnBilladjustmentDto>> GetAllAsync();
        Task<CfnBilladjustmentDto?> GetByIdAsync(decimal id);
        Task<CfnBilladjustmentDto> CreateAsync(CfnBilladjustmentDto dto);
        Task<CfnBilladjustmentDto> UpdateAsync(CfnBilladjustmentDto dto);
        Task<bool> DeleteAsync(decimal id);
    }
}