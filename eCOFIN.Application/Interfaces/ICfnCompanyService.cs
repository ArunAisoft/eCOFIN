namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnCompanyService
    {
        Task<IEnumerable<CfnCompanyDto>> GetAllAsync();
        Task<CfnCompanyDto?> GetByIdAsync(string id);
        Task<CfnCompanyDto> CreateAsync(CfnCompanyDto dto);
        Task<CfnCompanyDto> UpdateAsync(CfnCompanyDto dto);
        Task<bool> DeleteAsync(string id);
    }
}