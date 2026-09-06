namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBankreceiptService
    {
        Task<IEnumerable<CfnBankreceiptDto>> GetAllAsync();
        Task<CfnBankreceiptDto?> GetByIdAsync(string id);
        Task<CfnBankreceiptDto> CreateAsync(CfnBankreceiptDto dto);
        Task<CfnBankreceiptDto> UpdateAsync(CfnBankreceiptDto dto);
        Task<bool> DeleteAsync(string id);
    }
}