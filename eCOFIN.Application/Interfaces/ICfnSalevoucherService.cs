namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnSalevoucherService
    {
        Task<IEnumerable<CfnSalevoucherDto>> GetAllAsync();
        Task<CfnSalevoucherDto?> GetByIdAsync(string id);
        Task<CfnSalevoucherDto> CreateAsync(CfnSalevoucherDto dto);
        Task<CfnSalevoucherDto> UpdateAsync(CfnSalevoucherDto dto);
        Task<bool> DeleteAsync(string id);
    }
}