namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnAmtInWordService
    {
        Task<IEnumerable<CfnAmtInWordDto>> GetAllAsync();
        Task<CfnAmtInWordDto?> GetByIdAsync(decimal id);
        Task<CfnAmtInWordDto> CreateAsync(CfnAmtInWordDto dto);
        Task<CfnAmtInWordDto> UpdateAsync(CfnAmtInWordDto dto);
        Task<bool> DeleteAsync(decimal id);
    }
}