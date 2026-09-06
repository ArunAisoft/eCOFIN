namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnAgeingcfgService
    {
        Task<IEnumerable<CfnAgeingcfgDto>> GetAllAsync();
        Task<CfnAgeingcfgDto?> GetByIdAsync(decimal id);
        Task<CfnAgeingcfgDto> CreateAsync(CfnAgeingcfgDto dto);
        Task<CfnAgeingcfgDto> UpdateAsync(CfnAgeingcfgDto dto);
        Task<bool> DeleteAsync(decimal id);
    }
}