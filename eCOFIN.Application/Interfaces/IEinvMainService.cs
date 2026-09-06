namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IEinvMainService
    {
        Task<IEnumerable<EinvMainDto>> GetAllAsync();
        Task<EinvMainDto?> GetByIdAsync(string id);
        Task<EinvMainDto> CreateAsync(EinvMainDto dto);
        Task<EinvMainDto> UpdateAsync(EinvMainDto dto);
        Task<bool> DeleteAsync(string id);
    }
}