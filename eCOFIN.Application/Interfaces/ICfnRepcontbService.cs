namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnRepcontbService
    {
        Task<IEnumerable<CfnRepcontbDto>> GetAllAsync();
        Task<CfnRepcontbDto?> GetByIdAsync(string id);
        Task<CfnRepcontbDto> CreateAsync(CfnRepcontbDto dto);
        Task<CfnRepcontbDto> UpdateAsync(CfnRepcontbDto dto);
        Task<bool> DeleteAsync(string id);
    }
}