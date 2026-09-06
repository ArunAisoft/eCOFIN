namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnInvorderrelatedService
    {
        Task<IEnumerable<CfnInvorderrelatedDto>> GetAllAsync();
        Task<CfnInvorderrelatedDto?> GetByIdAsync(decimal id);
        Task<CfnInvorderrelatedDto> CreateAsync(CfnInvorderrelatedDto dto);
        Task<CfnInvorderrelatedDto> UpdateAsync(CfnInvorderrelatedDto dto);
        Task<bool> DeleteAsync(decimal id);
    }
}