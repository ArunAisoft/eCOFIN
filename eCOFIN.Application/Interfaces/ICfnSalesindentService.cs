namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnSalesindentService
    {
        Task<IEnumerable<CfnSalesindentDto>> GetAllAsync();
        Task<CfnSalesindentDto?> GetByIdAsync(string id);
        Task<CfnSalesindentDto> CreateAsync(CfnSalesindentDto dto);
        Task<CfnSalesindentDto> UpdateAsync(CfnSalesindentDto dto);
        Task<bool> DeleteAsync(string id);
    }
}