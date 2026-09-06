namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBankpaymentService
    {
        Task<IEnumerable<CfnBankpaymentDto>> GetAllAsync();
        Task<CfnBankpaymentDto?> GetByIdAsync(string id);
        Task<CfnBankpaymentDto> CreateAsync(CfnBankpaymentDto dto);
        Task<CfnBankpaymentDto> UpdateAsync(CfnBankpaymentDto dto);
        Task<bool> DeleteAsync(string id);
    }
}