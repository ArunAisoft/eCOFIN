namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnQuerytaskService
    {
        Task<IEnumerable<CfnQuerytaskDto>> GetAllAsync();
        Task<CfnQuerytaskDto?> GetByIdAsync(string id);
        Task<CfnQuerytaskDto> CreateAsync(CfnQuerytaskDto dto);
        Task<CfnQuerytaskDto> UpdateAsync(CfnQuerytaskDto dto);
        Task<bool> DeleteAsync(string id);
    }
}