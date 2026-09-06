namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IObiCodeEntryService
    {
        Task<IEnumerable<ObiCodeEntryDto>> GetAllAsync();
        Task<ObiCodeEntryDto?> GetByIdAsync(string id);
        Task<ObiCodeEntryDto> CreateAsync(ObiCodeEntryDto dto);
        Task<ObiCodeEntryDto> UpdateAsync(ObiCodeEntryDto dto);
        Task<bool> DeleteAsync(string id);
    }
}