namespace eCOFIN.Application.Interfaces.Masters
{
    using eCOFIN.Application.DTOs.Masters;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICostCentreService
    {
        Task<IEnumerable<CostCentreDto>> GetAllCostCentreAsync();
        Task<IEnumerable<CostCentreDto>> GetAllActiveCostCentresAsync();
        Task<(bool Success, string Message)> SaveOrUpdateCostCentreAsync(CostCentreCreateModel model);
    }
}
