namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnEnquiryService
    {
        Task<IEnumerable<CfnEnquiryDto>> GetAllAsync();
        Task<CfnEnquiryDto?> GetByIdAsync(string id);
        Task<CfnEnquiryDto> CreateAsync(CfnEnquiryDto dto);
        Task<CfnEnquiryDto> UpdateAsync(CfnEnquiryDto dto);
        Task<bool> DeleteAsync(string id);
    }
}