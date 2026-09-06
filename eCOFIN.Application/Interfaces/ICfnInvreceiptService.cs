namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnInvreceiptService
    {
        Task<IEnumerable<CfnInvreceiptDto>> GetAllAsync();
        Task<CfnInvreceiptDto?> GetByIdAsync(string id);
        Task<CfnInvreceiptDto> CreateAsync(CfnInvreceiptDto dto);
        Task<CfnInvreceiptDto> UpdateAsync(CfnInvreceiptDto dto);
        Task<bool> DeleteAsync(string id);
    }
}