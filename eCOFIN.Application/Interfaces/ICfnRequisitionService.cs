namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnRequisitionService
    {
        Task<IEnumerable<CfnRequisitionDto>> GetAllAsync();
        Task<CfnRequisitionDto?> GetByIdAsync(string id);
        Task<CfnRequisitionDto> CreateAsync(CfnRequisitionDto dto);
        Task<CfnRequisitionDto> UpdateAsync(CfnRequisitionDto dto);
        Task<bool> DeleteAsync(string id);
    }
}