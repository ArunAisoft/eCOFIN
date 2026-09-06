namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnInvoiceService
    {
        Task<IEnumerable<CfnInvoiceDto>> GetAllAsync();
        Task<CfnInvoiceDto?> GetByIdAsync(string id);
        Task<CfnInvoiceDto> CreateAsync(CfnInvoiceDto dto);
        Task<CfnInvoiceDto> UpdateAsync(CfnInvoiceDto dto);
        Task<bool> DeleteAsync(string id);
    }
}