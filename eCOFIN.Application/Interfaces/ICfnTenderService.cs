namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnTenderService
    {
        Task<IEnumerable<CfnTenderDto>> GetAllAsync();
        Task<CfnTenderDto?> GetByIdAsync(string id);
        Task<CfnTenderDto> CreateAsync(CfnTenderDto dto);
        Task<CfnTenderDto> UpdateAsync(CfnTenderDto dto);
        Task<bool> DeleteAsync(string id);
    }
}