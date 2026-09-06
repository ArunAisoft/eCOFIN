namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBudgetstackService
    {
        Task<IEnumerable<CfnBudgetstackDto>> GetAllAsync();
        Task<CfnBudgetstackDto?> GetByIdAsync(decimal id);
        Task<CfnBudgetstackDto> CreateAsync(CfnBudgetstackDto dto);
        Task<CfnBudgetstackDto> UpdateAsync(CfnBudgetstackDto dto);
        Task<bool> DeleteAsync(decimal id);
    }
}