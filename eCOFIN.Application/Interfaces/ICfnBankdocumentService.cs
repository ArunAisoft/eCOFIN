namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBankdocumentService
    {
        Task<IEnumerable<CfnBankdocumentDto>> GetAllAsync();
        Task<CfnBankdocumentDto?> GetByIdAsync(string id);
        Task<CfnBankdocumentDto> CreateAsync(CfnBankdocumentDto dto);
        Task<CfnBankdocumentDto> UpdateAsync(CfnBankdocumentDto dto);
        Task<bool> DeleteAsync(string id);
    }
}