namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnCashreceiptService
    {
        Task<IEnumerable<CfnCashreceiptDto>> GetAllAsync();
        Task<CfnCashreceiptDto?> GetByIdAsync(string id);
        Task<CfnCashreceiptDto> CreateAsync(CfnCashreceiptDto dto);
        Task<CfnCashreceiptDto> UpdateAsync(CfnCashreceiptDto dto);
        Task<bool> DeleteAsync(string id);
    }
}