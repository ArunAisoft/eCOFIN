namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICodeTypeService
    {
        Task<IEnumerable<CodeTypeDto>> GetAllAsync();
        Task<CodeTypeDto?> GetByIdAsync(string id);
        Task<CodeTypeDto> CreateAsync(CodeTypeDto dto);
        Task<CodeTypeDto> UpdateAsync(CodeTypeDto dto);
        Task<bool> DeleteAsync(string id);
    }
}