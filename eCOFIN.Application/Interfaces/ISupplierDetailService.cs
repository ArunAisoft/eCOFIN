namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ISupplierDetailService
    {
        Task<IEnumerable<SupplierDetailDto>> GetAllAsync();
        Task<SupplierDetailDto?> GetByIdAsync(string id);
        Task<SupplierDetailDto> CreateAsync(SupplierDetailDto dto);
        Task<SupplierDetailDto> UpdateAsync(SupplierDetailDto dto);
        Task<bool> DeleteAsync(string id);
    }
}