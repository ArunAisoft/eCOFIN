namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnLocationService
    {
        Task<IEnumerable<CfnLocationDto>> GetAllAsync();
        Task<CfnLocationDto?> GetByIdAsync(string id);
        Task<CfnLocationDto> CreateAsync(CfnLocationDto dto);
        Task<CfnLocationDto> UpdateAsync(CfnLocationDto dto);
        Task<bool> DeleteAsync(string id);
    }
}