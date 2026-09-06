namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnFinancialyearService
    {
        Task<IEnumerable<CfnFinancialyearDto>> GetAllAsync();
        Task<CfnFinancialyearDto?> GetByIdAsync(string id);
        Task<CfnFinancialyearDto> CreateAsync(CfnFinancialyearDto dto);
        Task<CfnFinancialyearDto> UpdateAsync(CfnFinancialyearDto dto);
        Task<bool> DeleteAsync(string id);
    }
}