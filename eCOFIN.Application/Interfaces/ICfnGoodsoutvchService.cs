namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnGoodsoutvchService
    {
        Task<IEnumerable<CfnGoodsoutvchDto>> GetAllAsync();
        Task<CfnGoodsoutvchDto?> GetByIdAsync(string id);
        Task<CfnGoodsoutvchDto> CreateAsync(CfnGoodsoutvchDto dto);
        Task<CfnGoodsoutvchDto> UpdateAsync(CfnGoodsoutvchDto dto);
        Task<bool> DeleteAsync(string id);
    }
}