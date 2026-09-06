namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnCustomerService
    {
        Task<IEnumerable<CfnCustomerDto>> GetAllAsync();
        Task<CfnCustomerDto?> GetByIdAsync(string id);
        Task<CfnCustomerDto> CreateAsync(CfnCustomerDto dto);
        Task<CfnCustomerDto> UpdateAsync(CfnCustomerDto dto);
        Task<bool> DeleteAsync(string id);
    }
}