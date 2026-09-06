namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnGenhelpService
    {
        Task<IEnumerable<CfnGenhelpDto>> GetAllAsync();
        Task<CfnGenhelpDto?> GetByIdAsync(string id);
        Task<CfnGenhelpDto> CreateAsync(CfnGenhelpDto dto);
        Task<CfnGenhelpDto> UpdateAsync(CfnGenhelpDto dto);
        Task<bool> DeleteAsync(string id);
    }
}