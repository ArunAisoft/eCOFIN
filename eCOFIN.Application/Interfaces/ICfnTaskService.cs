namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnTaskService
    {
        Task<IEnumerable<CfnTaskDto>> GetAllAsync();
        Task<CfnTaskDto?> GetByIdAsync(string id);
        Task<CfnTaskDto> CreateAsync(CfnTaskDto dto);
        Task<CfnTaskDto> UpdateAsync(CfnTaskDto dto);
        Task<bool> DeleteAsync(string id);
    }
}