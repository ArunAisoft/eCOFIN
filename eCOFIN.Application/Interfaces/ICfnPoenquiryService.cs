namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnPoenquiryService
    {
        Task<IEnumerable<CfnPoenquiryDto>> GetAllAsync();
        Task<CfnPoenquiryDto?> GetByIdAsync(string id);
        Task<CfnPoenquiryDto> CreateAsync(CfnPoenquiryDto dto);
        Task<CfnPoenquiryDto> UpdateAsync(CfnPoenquiryDto dto);
        Task<bool> DeleteAsync(string id);
    }
}