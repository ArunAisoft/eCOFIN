namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBscsexportService
    {
        Task<IEnumerable<CfnBscsexportDto>> GetAllAsync();
        Task<CfnBscsexportDto?> GetByIdAsync(string id);
        Task<CfnBscsexportDto> CreateAsync(CfnBscsexportDto dto);
        Task<CfnBscsexportDto> UpdateAsync(CfnBscsexportDto dto);
        Task<bool> DeleteAsync(string id);
    }
}