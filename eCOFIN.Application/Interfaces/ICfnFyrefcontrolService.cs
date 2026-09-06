namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnFyrefcontrolService
    {
        Task<IEnumerable<CfnFyrefcontrolDto>> GetAllAsync();
        Task<CfnFyrefcontrolDto?> GetByIdAsync(string id);
        Task<CfnFyrefcontrolDto> CreateAsync(CfnFyrefcontrolDto dto);
        Task<CfnFyrefcontrolDto> UpdateAsync(CfnFyrefcontrolDto dto);
        Task<bool> DeleteAsync(string id);
    }
}