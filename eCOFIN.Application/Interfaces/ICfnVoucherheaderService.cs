namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnVoucherheaderService
    {
        Task<IEnumerable<CfnVoucherheaderDto>> GetAllAsync();
        Task<CfnVoucherheaderDto?> GetByIdAsync(string id);
        Task<CfnVoucherheaderDto> CreateAsync(CfnVoucherheaderDto dto);
        Task<CfnVoucherheaderDto> UpdateAsync(CfnVoucherheaderDto dto);
        Task<bool> DeleteAsync(string id);
    }
}