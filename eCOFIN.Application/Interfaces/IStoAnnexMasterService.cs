namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IStoAnnexMasterService
    {
        Task<IEnumerable<StoAnnexMasterDto>> GetAllAsync();
        Task<StoAnnexMasterDto?> GetByIdAsync(string id);
        Task<StoAnnexMasterDto> CreateAsync(StoAnnexMasterDto dto);
        Task<StoAnnexMasterDto> UpdateAsync(StoAnnexMasterDto dto);
        Task<bool> DeleteAsync(string id);
    }
}