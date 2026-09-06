namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnTravelvoucherService
    {
        Task<IEnumerable<CfnTravelvoucherDto>> GetAllAsync();
        Task<CfnTravelvoucherDto?> GetByIdAsync(string id);
        Task<CfnTravelvoucherDto> CreateAsync(CfnTravelvoucherDto dto);
        Task<CfnTravelvoucherDto> UpdateAsync(CfnTravelvoucherDto dto);
        Task<bool> DeleteAsync(string id);
    }
}