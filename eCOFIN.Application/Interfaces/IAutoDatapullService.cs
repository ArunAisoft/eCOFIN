namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IAutoDatapullService
    {
        Task<IEnumerable<AutoDatapullDto>> GetAllAsync();
        Task<AutoDatapullDto?> GetByIdAsync(long id);
        Task<AutoDatapullDto> CreateAsync(AutoDatapullDto dto);
        Task<AutoDatapullDto> UpdateAsync(AutoDatapullDto dto);
        Task<bool> DeleteAsync(long id);
    }
}