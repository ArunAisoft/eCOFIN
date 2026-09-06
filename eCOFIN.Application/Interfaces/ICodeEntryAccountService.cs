namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICodeEntryAccountService
    {
        Task<IEnumerable<CodeEntryAccountDto>> GetAllAsync();
        Task<CodeEntryAccountDto?> GetByIdAsync(string id);
        Task<CodeEntryAccountDto> CreateAsync(CodeEntryAccountDto dto);
        Task<CodeEntryAccountDto> UpdateAsync(CodeEntryAccountDto dto);
        Task<bool> DeleteAsync(string id);
    }
}