namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBtlordoaService
    {
        Task<IEnumerable<CfnBtlordoaDto>> GetAllAsync();
        Task<CfnBtlordoaDto?> GetByIdAsync(string id);
        Task<CfnBtlordoaDto> CreateAsync(CfnBtlordoaDto dto);
        Task<CfnBtlordoaDto> UpdateAsync(CfnBtlordoaDto dto);
        Task<bool> DeleteAsync(string id);
    }
}