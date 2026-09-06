namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnConaccfgService
    {
        Task<IEnumerable<CfnConaccfgDto>> GetAllAsync();
        Task<CfnConaccfgDto?> GetByIdAsync(string id);
        Task<CfnConaccfgDto> CreateAsync(CfnConaccfgDto dto);
        Task<CfnConaccfgDto> UpdateAsync(CfnConaccfgDto dto);
        Task<bool> DeleteAsync(string id);
    }
}