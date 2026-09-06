namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnInvcprncfgService
    {
        Task<IEnumerable<CfnInvcprncfgDto>> GetAllAsync();
        Task<CfnInvcprncfgDto?> GetByIdAsync(string id);
        Task<CfnInvcprncfgDto> CreateAsync(CfnInvcprncfgDto dto);
        Task<CfnInvcprncfgDto> UpdateAsync(CfnInvcprncfgDto dto);
        Task<bool> DeleteAsync(string id);
    }
}