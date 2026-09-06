namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnInvvchrtypeService
    {
        Task<IEnumerable<CfnInvvchrtypeDto>> GetAllAsync();
        Task<CfnInvvchrtypeDto?> GetByIdAsync(string id);
        Task<CfnInvvchrtypeDto> CreateAsync(CfnInvvchrtypeDto dto);
        Task<CfnInvvchrtypeDto> UpdateAsync(CfnInvvchrtypeDto dto);
        Task<bool> DeleteAsync(string id);
    }
}