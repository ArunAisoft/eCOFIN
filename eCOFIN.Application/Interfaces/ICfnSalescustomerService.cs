namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnSalescustomerService
    {
        Task<IEnumerable<CfnSalescustomerDto>> GetAllAsync();
        Task<CfnSalescustomerDto?> GetByIdAsync(string id);
        Task<CfnSalescustomerDto> CreateAsync(CfnSalescustomerDto dto);
        Task<CfnSalescustomerDto> UpdateAsync(CfnSalescustomerDto dto);
        Task<bool> DeleteAsync(string id);
    }
}