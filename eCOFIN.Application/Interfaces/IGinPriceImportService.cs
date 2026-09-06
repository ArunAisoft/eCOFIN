namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IGinPriceImportService
    {
        Task<IEnumerable<GinPriceImportDto>> GetAllAsync();
        Task<GinPriceImportDto?> GetByIdAsync(string id);
        Task<GinPriceImportDto> CreateAsync(GinPriceImportDto dto);
        Task<GinPriceImportDto> UpdateAsync(GinPriceImportDto dto);
        Task<bool> DeleteAsync(string id);
    }
}