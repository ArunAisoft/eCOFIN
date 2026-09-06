namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IAccDcnoteMasterService
    {
        Task<IEnumerable<AccDcnoteMasterDto>> GetAllAsync();
        Task<AccDcnoteMasterDto?> GetByIdAsync(string id);
        Task<AccDcnoteMasterDto> CreateAsync(AccDcnoteMasterDto dto);
        Task<AccDcnoteMasterDto> UpdateAsync(AccDcnoteMasterDto dto);
        Task<bool> DeleteAsync(string id);
    }
}