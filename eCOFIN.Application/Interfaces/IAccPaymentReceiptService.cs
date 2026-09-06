namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IAccPaymentReceiptService
    {
        Task<IEnumerable<AccPaymentReceiptDto>> GetAllAsync();
        Task<AccPaymentReceiptDto?> GetByIdAsync(string id);
        Task<AccPaymentReceiptDto> CreateAsync(AccPaymentReceiptDto dto);
        Task<AccPaymentReceiptDto> UpdateAsync(AccPaymentReceiptDto dto);
        Task<bool> DeleteAsync(string id);
    }
}