namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnReportcontrolService
    {
        Task<IEnumerable<CfnReportcontrolDto>> GetAllAsync();
        Task<CfnReportcontrolDto?> GetByIdAsync(string id);
        Task<CfnReportcontrolDto> CreateAsync(CfnReportcontrolDto dto);
        Task<CfnReportcontrolDto> UpdateAsync(CfnReportcontrolDto dto);
        Task<bool> DeleteAsync(string id);
    }
}