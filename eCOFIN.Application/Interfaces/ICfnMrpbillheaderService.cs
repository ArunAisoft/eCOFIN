namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnMrpbillheaderService
    {
        Task<IEnumerable<CfnMrpbillheaderDto>> GetAllAsync();
        Task<CfnMrpbillheaderDto?> GetByIdAsync(string id);
        Task<CfnMrpbillheaderDto> CreateAsync(CfnMrpbillheaderDto dto);
        Task<CfnMrpbillheaderDto> UpdateAsync(CfnMrpbillheaderDto dto);
        Task<bool> DeleteAsync(string id);
    }
}