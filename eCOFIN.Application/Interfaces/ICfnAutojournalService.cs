namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnAutojournalService
    {
        Task<IEnumerable<CfnAutojournalDto>> GetAllAsync();
        Task<CfnAutojournalDto?> GetByIdAsync(string id);
        Task<CfnAutojournalDto> CreateAsync(CfnAutojournalDto dto);
        Task<CfnAutojournalDto> UpdateAsync(CfnAutojournalDto dto);
        Task<bool> DeleteAsync(string id);
    }
}