namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnReferencectrlService
    {
        Task<IEnumerable<CfnReferencectrlDto>> GetAllAsync();
        Task<CfnReferencectrlDto?> GetByIdAsync(string id);
        Task<CfnReferencectrlDto> CreateAsync(CfnReferencectrlDto dto);
        Task<CfnReferencectrlDto> UpdateAsync(CfnReferencectrlDto dto);
        Task<bool> DeleteAsync(string id);
    }
}