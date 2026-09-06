namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnCashbookService
    {
        Task<IEnumerable<CfnCashbookDto>> GetAllAsync();
        Task<CfnCashbookDto?> GetByIdAsync(string id);
        Task<CfnCashbookDto> CreateAsync(CfnCashbookDto dto);
        Task<CfnCashbookDto> UpdateAsync(CfnCashbookDto dto);
        Task<bool> DeleteAsync(string id);
    }
}