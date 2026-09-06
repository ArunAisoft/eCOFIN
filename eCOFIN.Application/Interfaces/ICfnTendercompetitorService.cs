namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnTendercompetitorService
    {
        Task<IEnumerable<CfnTendercompetitorDto>> GetAllAsync();
        Task<CfnTendercompetitorDto?> GetByIdAsync(string id);
        Task<CfnTendercompetitorDto> CreateAsync(CfnTendercompetitorDto dto);
        Task<CfnTendercompetitorDto> UpdateAsync(CfnTendercompetitorDto dto);
        Task<bool> DeleteAsync(string id);
    }
}