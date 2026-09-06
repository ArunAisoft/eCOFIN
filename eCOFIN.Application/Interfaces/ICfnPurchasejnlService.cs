namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnPurchasejnlService
    {
        Task<IEnumerable<CfnPurchasejnlDto>> GetAllAsync();
        Task<CfnPurchasejnlDto?> GetByIdAsync(string id);
        Task<CfnPurchasejnlDto> CreateAsync(CfnPurchasejnlDto dto);
        Task<CfnPurchasejnlDto> UpdateAsync(CfnPurchasejnlDto dto);
        Task<bool> DeleteAsync(string id);
    }
}