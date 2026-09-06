namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnTravelsancService
    {
        Task<IEnumerable<CfnTravelsancDto>> GetAllAsync();
        Task<CfnTravelsancDto?> GetByIdAsync(string id);
        Task<CfnTravelsancDto> CreateAsync(CfnTravelsancDto dto);
        Task<CfnTravelsancDto> UpdateAsync(CfnTravelsancDto dto);
        Task<bool> DeleteAsync(string id);
    }
}