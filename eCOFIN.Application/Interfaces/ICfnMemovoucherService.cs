namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnMemovoucherService
    {
        Task<IEnumerable<CfnMemovoucherDto>> GetAllAsync();
        Task<CfnMemovoucherDto?> GetByIdAsync(string id);
        Task<CfnMemovoucherDto> CreateAsync(CfnMemovoucherDto dto);
        Task<CfnMemovoucherDto> UpdateAsync(CfnMemovoucherDto dto);
        Task<bool> DeleteAsync(string id);
    }
}