namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnEmployeeService
    {
        Task<IEnumerable<CfnEmployeeDto>> GetAllAsync();
        Task<CfnEmployeeDto?> GetByIdAsync(string id);
        Task<CfnEmployeeDto> CreateAsync(CfnEmployeeDto dto);
        Task<CfnEmployeeDto> UpdateAsync(CfnEmployeeDto dto);
        Task<bool> DeleteAsync(string id);
    }
}