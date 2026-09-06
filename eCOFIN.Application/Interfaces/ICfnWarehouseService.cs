namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnWarehouseService
    {
        Task<IEnumerable<CfnWarehouseDto>> GetAllAsync();
        Task<CfnWarehouseDto?> GetByIdAsync(string id);
        Task<CfnWarehouseDto> CreateAsync(CfnWarehouseDto dto);
        Task<CfnWarehouseDto> UpdateAsync(CfnWarehouseDto dto);
        Task<bool> DeleteAsync(string id);
    }
}