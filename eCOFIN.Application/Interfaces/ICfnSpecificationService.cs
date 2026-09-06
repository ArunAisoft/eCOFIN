namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnSpecificationService
    {
        Task<IEnumerable<CfnSpecificationDto>> GetAllAsync();
        Task<CfnSpecificationDto?> GetByIdAsync(string id);
        Task<CfnSpecificationDto> CreateAsync(CfnSpecificationDto dto);
        Task<CfnSpecificationDto> UpdateAsync(CfnSpecificationDto dto);
        Task<bool> DeleteAsync(string id);
    }
}