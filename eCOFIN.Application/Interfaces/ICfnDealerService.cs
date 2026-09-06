namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnDealerService
    {
        Task<IEnumerable<CfnDealerDto>> GetAllAsync();
        Task<CfnDealerDto?> GetByIdAsync(string id);
        Task<CfnDealerDto> CreateAsync(CfnDealerDto dto);
        Task<CfnDealerDto> UpdateAsync(CfnDealerDto dto);
        Task<bool> DeleteAsync(string id);
    }
}