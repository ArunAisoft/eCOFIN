namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnConloccfgService
    {
        Task<IEnumerable<CfnConloccfgDto>> GetAllAsync();
        Task<CfnConloccfgDto?> GetByIdAsync(string id);
        Task<CfnConloccfgDto> CreateAsync(CfnConloccfgDto dto);
        Task<CfnConloccfgDto> UpdateAsync(CfnConloccfgDto dto);
        Task<bool> DeleteAsync(string id);
    }
}