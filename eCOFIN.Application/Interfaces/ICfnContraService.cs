namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnContraService
    {
        Task<IEnumerable<CfnContraDto>> GetAllAsync();
        Task<CfnContraDto?> GetByIdAsync(string id);
        Task<CfnContraDto> CreateAsync(CfnContraDto dto);
        Task<CfnContraDto> UpdateAsync(CfnContraDto dto);
        Task<bool> DeleteAsync(string id);
    }
}