namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnTermService
    {
        Task<IEnumerable<CfnTermDto>> GetAllAsync();
        Task<CfnTermDto?> GetByIdAsync(string id);
        Task<CfnTermDto> CreateAsync(CfnTermDto dto);
        Task<CfnTermDto> UpdateAsync(CfnTermDto dto);
        Task<bool> DeleteAsync(string id);
    }
}