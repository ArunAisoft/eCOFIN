namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBtlcustomerorderService
    {
        Task<IEnumerable<CfnBtlcustomerorderDto>> GetAllAsync();
        Task<CfnBtlcustomerorderDto?> GetByIdAsync(string id);
        Task<CfnBtlcustomerorderDto> CreateAsync(CfnBtlcustomerorderDto dto);
        Task<CfnBtlcustomerorderDto> UpdateAsync(CfnBtlcustomerorderDto dto);
        Task<bool> DeleteAsync(string id);
    }
}