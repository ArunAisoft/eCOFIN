namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnProductService
    {
        Task<IEnumerable<CfnProductDto>> GetAllAsync();
        Task<CfnProductDto?> GetByIdAsync(string id);
        Task<CfnProductDto> CreateAsync(CfnProductDto dto);
        Task<CfnProductDto> UpdateAsync(CfnProductDto dto);
        Task<bool> DeleteAsync(string id);
    }
}