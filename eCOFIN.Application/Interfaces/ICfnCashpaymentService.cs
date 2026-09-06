namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnCashpaymentService
    {
        Task<IEnumerable<CfnCashpaymentDto>> GetAllAsync();
        Task<CfnCashpaymentDto?> GetByIdAsync(string id);
        Task<CfnCashpaymentDto> CreateAsync(CfnCashpaymentDto dto);
        Task<CfnCashpaymentDto> UpdateAsync(CfnCashpaymentDto dto);
        Task<bool> DeleteAsync(string id);
    }
}