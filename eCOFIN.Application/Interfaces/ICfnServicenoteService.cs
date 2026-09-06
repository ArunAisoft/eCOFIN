namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnServicenoteService
    {
        Task<IEnumerable<CfnServicenoteDto>> GetAllAsync();
        Task<CfnServicenoteDto?> GetByIdAsync(string id);
        Task<CfnServicenoteDto> CreateAsync(CfnServicenoteDto dto);
        Task<CfnServicenoteDto> UpdateAsync(CfnServicenoteDto dto);
        Task<bool> DeleteAsync(string id);
    }
}