namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IInvMainService
    {
        Task<IEnumerable<InvMainDto>> GetAllAsync();
        Task<InvMainDto?> GetByIdAsync(string id);
        Task<InvMainDto> CreateAsync(InvMainDto dto);
        Task<InvMainDto> UpdateAsync(InvMainDto dto);
        Task<bool> DeleteAsync(string id);
    }
}