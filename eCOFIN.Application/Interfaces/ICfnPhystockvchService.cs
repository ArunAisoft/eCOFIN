namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnPhystockvchService
    {
        Task<IEnumerable<CfnPhystockvchDto>> GetAllAsync();
        Task<CfnPhystockvchDto?> GetByIdAsync(string id);
        Task<CfnPhystockvchDto> CreateAsync(CfnPhystockvchDto dto);
        Task<CfnPhystockvchDto> UpdateAsync(CfnPhystockvchDto dto);
        Task<bool> DeleteAsync(string id);
    }
}