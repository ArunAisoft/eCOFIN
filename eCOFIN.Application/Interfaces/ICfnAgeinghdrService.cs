namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnAgeinghdrService
    {
        Task<IEnumerable<CfnAgeinghdrDto>> GetAllAsync();
        Task<CfnAgeinghdrDto?> GetByIdAsync(string id);
        Task<CfnAgeinghdrDto> CreateAsync(CfnAgeinghdrDto dto);
        Task<CfnAgeinghdrDto> UpdateAsync(CfnAgeinghdrDto dto);
        Task<bool> DeleteAsync(string id);
    }
}