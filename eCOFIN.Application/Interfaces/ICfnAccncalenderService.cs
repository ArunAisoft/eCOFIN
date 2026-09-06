namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnAccncalenderService
    {
        Task<IEnumerable<CfnAccncalenderDto>> GetAllAsync();
        Task<CfnAccncalenderDto?> GetByIdAsync(string id);
        Task<CfnAccncalenderDto> CreateAsync(CfnAccncalenderDto dto);
        Task<CfnAccncalenderDto> UpdateAsync(CfnAccncalenderDto dto);
        Task<bool> DeleteAsync(string id);
    }
}