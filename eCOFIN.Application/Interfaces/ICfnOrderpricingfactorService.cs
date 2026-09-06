namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnOrderpricingfactorService
    {
        Task<IEnumerable<CfnOrderpricingfactorDto>> GetAllAsync();
        Task<CfnOrderpricingfactorDto?> GetByIdAsync(string id);
        Task<CfnOrderpricingfactorDto> CreateAsync(CfnOrderpricingfactorDto dto);
        Task<CfnOrderpricingfactorDto> UpdateAsync(CfnOrderpricingfactorDto dto);
        Task<bool> DeleteAsync(string id);
    }
}