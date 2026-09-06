namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnVoucherobjectService
    {
        Task<IEnumerable<CfnVoucherobjectDto>> GetAllAsync();
        Task<CfnVoucherobjectDto?> GetByIdAsync(string id);
        Task<CfnVoucherobjectDto> CreateAsync(CfnVoucherobjectDto dto);
        Task<CfnVoucherobjectDto> UpdateAsync(CfnVoucherobjectDto dto);
        Task<bool> DeleteAsync(string id);
    }
}