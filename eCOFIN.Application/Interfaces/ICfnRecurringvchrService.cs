namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnRecurringvchrService
    {
        Task<IEnumerable<CfnRecurringvchrDto>> GetAllAsync();
        Task<CfnRecurringvchrDto?> GetByIdAsync(string id);
        Task<CfnRecurringvchrDto> CreateAsync(CfnRecurringvchrDto dto);
        Task<CfnRecurringvchrDto> UpdateAsync(CfnRecurringvchrDto dto);
        Task<bool> DeleteAsync(string id);
    }
}