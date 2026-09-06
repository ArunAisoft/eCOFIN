namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnDebitnoteService
    {
        Task<IEnumerable<CfnDebitnoteDto>> GetAllAsync();
        Task<CfnDebitnoteDto?> GetByIdAsync(string id);
        Task<CfnDebitnoteDto> CreateAsync(CfnDebitnoteDto dto);
        Task<CfnDebitnoteDto> UpdateAsync(CfnDebitnoteDto dto);
        Task<bool> DeleteAsync(string id);
    }
}