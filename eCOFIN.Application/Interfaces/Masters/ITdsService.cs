namespace eCOFIN.Application.Interfaces.Masters
{
    using eCOFIN.Application.DTOs.Masters;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITdsService
    {
        Task<IEnumerable<TdsReportDto>> GetAllTDSAsync();
        Task<IEnumerable<TdsReportDto>> GetAllActiveTDSAsync();
        Task<(bool Success, string Message)> SaveOrUpdateTdsAsync(TdsCreateModel model);
    }
}