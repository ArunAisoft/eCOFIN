namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnJournalService
    {
        Task<IEnumerable<CfnJournalDto>> GetAllAsync();
        Task<CfnJournalDto?> GetByIdAsync(string id);
        Task<CfnJournalDto> CreateAsync(CfnJournalDto dto);
        Task<CfnJournalDto> UpdateAsync(CfnJournalDto dto);
        Task<bool> DeleteAsync(string id);
    }
}