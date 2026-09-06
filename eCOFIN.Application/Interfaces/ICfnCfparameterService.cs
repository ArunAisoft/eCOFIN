namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnCfparameterService
    {
        Task<IEnumerable<CfnCfparameterDto>> GetAllAsync();
        Task<CfnCfparameterDto?> GetByIdAsync(string id);
        Task<CfnCfparameterDto> CreateAsync(CfnCfparameterDto dto);
        Task<CfnCfparameterDto> UpdateAsync(CfnCfparameterDto dto);
        Task<bool> DeleteAsync(string id);
    }
}