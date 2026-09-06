namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBnkretirementService
    {
        Task<IEnumerable<CfnBnkretirementDto>> GetAllAsync();
        Task<CfnBnkretirementDto?> GetByIdAsync(string id);
        Task<CfnBnkretirementDto> CreateAsync(CfnBnkretirementDto dto);
        Task<CfnBnkretirementDto> UpdateAsync(CfnBnkretirementDto dto);
        Task<bool> DeleteAsync(string id);
    }
}