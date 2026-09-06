namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnPoquotationService
    {
        Task<IEnumerable<CfnPoquotationDto>> GetAllAsync();
        Task<CfnPoquotationDto?> GetByIdAsync(string id);
        Task<CfnPoquotationDto> CreateAsync(CfnPoquotationDto dto);
        Task<CfnPoquotationDto> UpdateAsync(CfnPoquotationDto dto);
        Task<bool> DeleteAsync(string id);
    }
}