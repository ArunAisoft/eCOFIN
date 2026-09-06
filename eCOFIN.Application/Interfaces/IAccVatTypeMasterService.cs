namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IAccVatTypeMasterService
    {
        Task<IEnumerable<AccVatTypeMasterDto>> GetAllAsync();
        Task<AccVatTypeMasterDto?> GetByIdAsync(int id);
        Task<AccVatTypeMasterDto> CreateAsync(AccVatTypeMasterDto dto);
        Task<AccVatTypeMasterDto> UpdateAsync(AccVatTypeMasterDto dto);
        Task<bool> DeleteAsync(int id);
    }
}