namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnHierarchyService
    {
        Task<IEnumerable<CfnHierarchyDto>> GetAllAsync();
        Task<CfnHierarchyDto?> GetByIdAsync(string id);
        Task<CfnHierarchyDto> CreateAsync(CfnHierarchyDto dto);
        Task<CfnHierarchyDto> UpdateAsync(CfnHierarchyDto dto);
        Task<bool> DeleteAsync(string id);
    }
}