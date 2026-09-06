namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(string id);
        Task<CustomerDto> CreateAsync(CustomerDto dto);
        Task<CustomerDto> UpdateAsync(CustomerDto dto);
        Task<bool> DeleteAsync(string id);
    }
}