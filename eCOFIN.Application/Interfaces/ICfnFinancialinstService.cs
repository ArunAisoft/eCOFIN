namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnFinancialinstService
    {
        Task<IEnumerable<CfnFinancialinstDto>> GetAllAsync();
        Task<CfnFinancialinstDto?> GetByIdAsync(string id);
        Task<CfnFinancialinstDto> CreateAsync(CfnFinancialinstDto dto);
        Task<CfnFinancialinstDto> UpdateAsync(CfnFinancialinstDto dto);
        Task<bool> DeleteAsync(string id);
    }
}