namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnGeographyService
    {
        Task<IEnumerable<CfnGeographyDto>> GetAllAsync();
        Task<CfnGeographyDto?> GetByIdAsync(string id);
        Task<CfnGeographyDto> CreateAsync(CfnGeographyDto dto);
        Task<CfnGeographyDto> UpdateAsync(CfnGeographyDto dto);
        Task<bool> DeleteAsync(string id);
    }
}