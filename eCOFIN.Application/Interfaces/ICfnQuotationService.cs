namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnQuotationService
    {
        Task<IEnumerable<CfnQuotationDto>> GetAllAsync();
        Task<CfnQuotationDto?> GetByIdAsync(string id);
        Task<CfnQuotationDto> CreateAsync(CfnQuotationDto dto);
        Task<CfnQuotationDto> UpdateAsync(CfnQuotationDto dto);
        Task<bool> DeleteAsync(string id);
    }
}