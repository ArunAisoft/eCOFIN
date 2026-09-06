namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnViewService
    {
        Task<IEnumerable<CfnViewDto>> GetAllAsync();
        Task<CfnViewDto?> GetByIdAsync(string id);
        Task<CfnViewDto> CreateAsync(CfnViewDto dto);
        Task<CfnViewDto> UpdateAsync(CfnViewDto dto);
        Task<bool> DeleteAsync(string id);
    }
}