namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnTenderdeposithdService
    {
        Task<IEnumerable<CfnTenderdeposithdDto>> GetAllAsync();
        Task<CfnTenderdeposithdDto?> GetByIdAsync(string id);
        Task<CfnTenderdeposithdDto> CreateAsync(CfnTenderdeposithdDto dto);
        Task<CfnTenderdeposithdDto> UpdateAsync(CfnTenderdeposithdDto dto);
        Task<bool> DeleteAsync(string id);
    }
}