namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IVendcodeService
    {
        Task<IEnumerable<VendcodeDto>> GetAllAsync();
        Task<VendcodeDto?> GetByIdAsync(string id);
        Task<VendcodeDto> CreateAsync(VendcodeDto dto);
        Task<VendcodeDto> UpdateAsync(VendcodeDto dto);
        Task<bool> DeleteAsync(string id);
    }
}