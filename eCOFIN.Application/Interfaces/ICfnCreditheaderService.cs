namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnCreditheaderService
    {
        Task<IEnumerable<CfnCreditheaderDto>> GetAllAsync();
        Task<CfnCreditheaderDto?> GetByIdAsync(string id);
        Task<CfnCreditheaderDto> CreateAsync(CfnCreditheaderDto dto);
        Task<CfnCreditheaderDto> UpdateAsync(CfnCreditheaderDto dto);
        Task<bool> DeleteAsync(string id);
    }
}