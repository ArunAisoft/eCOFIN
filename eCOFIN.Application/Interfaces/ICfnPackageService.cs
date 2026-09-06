namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnPackageService
    {
        Task<IEnumerable<CfnPackageDto>> GetAllAsync();
        Task<CfnPackageDto?> GetByIdAsync(string id);
        Task<CfnPackageDto> CreateAsync(CfnPackageDto dto);
        Task<CfnPackageDto> UpdateAsync(CfnPackageDto dto);
        Task<bool> DeleteAsync(string id);
    }
}