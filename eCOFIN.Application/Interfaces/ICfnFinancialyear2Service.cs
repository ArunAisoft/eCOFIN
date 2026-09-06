namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnFinancialyear2Service
    {
        Task<IEnumerable<CfnFinancialyear2Dto>> GetAllAsync();
        Task<CfnFinancialyear2Dto?> GetByIdAsync(string id);
        Task<CfnFinancialyear2Dto> CreateAsync(CfnFinancialyear2Dto dto);
        Task<CfnFinancialyear2Dto> UpdateAsync(CfnFinancialyear2Dto dto);
        Task<bool> DeleteAsync(string id);
    }
}