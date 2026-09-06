namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnTrLinkService
    {
        Task<IEnumerable<CfnTrLinkDto>> GetAllAsync();
        Task<CfnTrLinkDto?> GetByIdAsync(string id);
        Task<CfnTrLinkDto> CreateAsync(CfnTrLinkDto dto);
        Task<CfnTrLinkDto> UpdateAsync(CfnTrLinkDto dto);
        Task<bool> DeleteAsync(string id);
    }
}