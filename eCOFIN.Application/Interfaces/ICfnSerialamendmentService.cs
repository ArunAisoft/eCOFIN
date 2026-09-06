namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnSerialamendmentService
    {
        Task<IEnumerable<CfnSerialamendmentDto>> GetAllAsync();
        Task<CfnSerialamendmentDto?> GetByIdAsync(decimal id);
        Task<CfnSerialamendmentDto> CreateAsync(CfnSerialamendmentDto dto);
        Task<CfnSerialamendmentDto> UpdateAsync(CfnSerialamendmentDto dto);
        Task<bool> DeleteAsync(decimal id);
    }
}