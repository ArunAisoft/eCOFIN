namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnVendorService
    {
        Task<IEnumerable<CfnVendorDto>> GetAllAsync();
        Task<CfnVendorDto?> GetByIdAsync(string id);
        Task<CfnVendorDto> CreateAsync(CfnVendorDto dto);
        Task<CfnVendorDto> UpdateAsync(CfnVendorDto dto);
        Task<bool> DeleteAsync(string id);
    }
}