namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IGinPriceDomesticService
    {
        Task<IEnumerable<GinPriceDomesticDto>> GetAllAsync();
        Task<GinPriceDomesticDto?> GetByIdAsync(string id);
        Task<GinPriceDomesticDto> CreateAsync(GinPriceDomesticDto dto);
        Task<GinPriceDomesticDto> UpdateAsync(GinPriceDomesticDto dto);
        Task<bool> DeleteAsync(string id);
    }
}