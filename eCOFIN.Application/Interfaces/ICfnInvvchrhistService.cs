namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnInvvchrhistService
    {
        Task<IEnumerable<CfnInvvchrhistDto>> GetAllAsync();
        Task<CfnInvvchrhistDto?> GetByIdAsync(string id);
        Task<CfnInvvchrhistDto> CreateAsync(CfnInvvchrhistDto dto);
        Task<CfnInvvchrhistDto> UpdateAsync(CfnInvvchrhistDto dto);
        Task<bool> DeleteAsync(string id);
    }
}