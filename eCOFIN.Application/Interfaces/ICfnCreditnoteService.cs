namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnCreditnoteService
    {
        Task<IEnumerable<CfnCreditnoteDto>> GetAllAsync();
        Task<CfnCreditnoteDto?> GetByIdAsync(string id);
        Task<CfnCreditnoteDto> CreateAsync(CfnCreditnoteDto dto);
        Task<CfnCreditnoteDto> UpdateAsync(CfnCreditnoteDto dto);
        Task<bool> DeleteAsync(string id);
    }
}