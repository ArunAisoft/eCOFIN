namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnInterbranchService
    {
        Task<IEnumerable<CfnInterbranchDto>> GetAllAsync();
        Task<CfnInterbranchDto?> GetByIdAsync(string id);
        Task<CfnInterbranchDto> CreateAsync(CfnInterbranchDto dto);
        Task<CfnInterbranchDto> UpdateAsync(CfnInterbranchDto dto);
        Task<bool> DeleteAsync(string id);
    }
}