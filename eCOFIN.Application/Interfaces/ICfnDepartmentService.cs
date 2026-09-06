namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnDepartmentService
    {
        Task<IEnumerable<CfnDepartmentDto>> GetAllAsync();
        Task<CfnDepartmentDto?> GetByIdAsync(string id);
        Task<CfnDepartmentDto> CreateAsync(CfnDepartmentDto dto);
        Task<CfnDepartmentDto> UpdateAsync(CfnDepartmentDto dto);
        Task<bool> DeleteAsync(string id);
    }
}