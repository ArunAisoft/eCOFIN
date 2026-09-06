namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnInvregcfgService
    {
        Task<IEnumerable<CfnInvregcfgDto>> GetAllAsync();
        Task<CfnInvregcfgDto?> GetByIdAsync(string id);
        Task<CfnInvregcfgDto> CreateAsync(CfnInvregcfgDto dto);
        Task<CfnInvregcfgDto> UpdateAsync(CfnInvregcfgDto dto);
        Task<bool> DeleteAsync(string id);
    }
}