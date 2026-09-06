namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnTdService
    {
        Task<IEnumerable<CfnTdDto>> GetAllAsync();
        Task<CfnTdDto?> GetByIdAsync(string id);
        Task<CfnTdDto> CreateAsync(CfnTdDto dto);
        Task<CfnTdDto> UpdateAsync(CfnTdDto dto);
        Task<bool> DeleteAsync(string id);
    }
}