namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnGoodsinvchService
    {
        Task<IEnumerable<CfnGoodsinvchDto>> GetAllAsync();
        Task<CfnGoodsinvchDto?> GetByIdAsync(string id);
        Task<CfnGoodsinvchDto> CreateAsync(CfnGoodsinvchDto dto);
        Task<CfnGoodsinvchDto> UpdateAsync(CfnGoodsinvchDto dto);
        Task<bool> DeleteAsync(string id);
    }
}