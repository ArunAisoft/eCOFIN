namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBankbookService
    {
        Task<IEnumerable<CfnBankbookDto>> GetAllAsync();
        Task<CfnBankbookDto?> GetByIdAsync(string id);
        Task<CfnBankbookDto> CreateAsync(CfnBankbookDto dto);
        Task<CfnBankbookDto> UpdateAsync(CfnBankbookDto dto);
        Task<bool> DeleteAsync(string id);
    }
}