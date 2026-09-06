namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnPostagetrackService
    {
        Task<IEnumerable<CfnPostagetrackDto>> GetAllAsync();
        Task<CfnPostagetrackDto?> GetByIdAsync(string id);
        Task<CfnPostagetrackDto> CreateAsync(CfnPostagetrackDto dto);
        Task<CfnPostagetrackDto> UpdateAsync(CfnPostagetrackDto dto);
        Task<bool> DeleteAsync(string id);
    }
}