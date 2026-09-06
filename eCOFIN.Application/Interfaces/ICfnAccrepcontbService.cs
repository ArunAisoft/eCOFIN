namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnAccrepcontbService
    {
        Task<IEnumerable<CfnAccrepcontbDto>> GetAllAsync();
        Task<CfnAccrepcontbDto?> GetByIdAsync(string id);
        Task<CfnAccrepcontbDto> CreateAsync(CfnAccrepcontbDto dto);
        Task<CfnAccrepcontbDto> UpdateAsync(CfnAccrepcontbDto dto);
        Task<bool> DeleteAsync(string id);
    }
}