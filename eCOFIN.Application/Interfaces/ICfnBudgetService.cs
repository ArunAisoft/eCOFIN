namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBudgetService
    {
        Task<IEnumerable<CfnBudgetDto>> GetAllAsync();
        Task<CfnBudgetDto?> GetByIdAsync(string id);
        Task<CfnBudgetDto> CreateAsync(CfnBudgetDto dto);
        Task<CfnBudgetDto> UpdateAsync(CfnBudgetDto dto);
        Task<bool> DeleteAsync(string id);
    }
}