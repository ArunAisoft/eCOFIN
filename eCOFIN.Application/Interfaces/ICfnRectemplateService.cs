namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnRectemplateService
    {
        Task<IEnumerable<CfnRectemplateDto>> GetAllAsync();
        Task<CfnRectemplateDto?> GetByIdAsync(string id);
        Task<CfnRectemplateDto> CreateAsync(CfnRectemplateDto dto);
        Task<CfnRectemplateDto> UpdateAsync(CfnRectemplateDto dto);
        Task<bool> DeleteAsync(string id);
    }
}