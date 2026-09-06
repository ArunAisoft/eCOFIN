namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnTdsaccountslnoService
    {
        Task<IEnumerable<CfnTdsaccountslnoDto>> GetAllAsync();
        Task<CfnTdsaccountslnoDto?> GetByIdAsync(string id);
        Task<CfnTdsaccountslnoDto> CreateAsync(CfnTdsaccountslnoDto dto);
        Task<CfnTdsaccountslnoDto> UpdateAsync(CfnTdsaccountslnoDto dto);
        Task<bool> DeleteAsync(string id);
    }
}