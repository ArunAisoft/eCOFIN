namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnRepageingtemplateService
    {
        Task<IEnumerable<CfnRepageingtemplateDto>> GetAllAsync();
        Task<CfnRepageingtemplateDto?> GetByIdAsync(decimal id);
        Task<CfnRepageingtemplateDto> CreateAsync(CfnRepageingtemplateDto dto);
        Task<CfnRepageingtemplateDto> UpdateAsync(CfnRepageingtemplateDto dto);
        Task<bool> DeleteAsync(decimal id);
    }
}