namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBudgetsanctionService
    {
        Task<IEnumerable<CfnBudgetsanctionDto>> GetAllAsync();
        Task<CfnBudgetsanctionDto?> GetByIdAsync(string id);
        Task<CfnBudgetsanctionDto> CreateAsync(CfnBudgetsanctionDto dto);
        Task<CfnBudgetsanctionDto> UpdateAsync(CfnBudgetsanctionDto dto);
        Task<bool> DeleteAsync(string id);
    }
}