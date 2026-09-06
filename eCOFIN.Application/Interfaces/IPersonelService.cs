namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface IPersonelService
    {
        Task<IEnumerable<PersonelDto>> GetAllAsync();
        Task<PersonelDto?> GetByIdAsync(string id);
        Task<PersonelDto> CreateAsync(PersonelDto dto);
        Task<PersonelDto> UpdateAsync(PersonelDto dto);
        Task<bool> DeleteAsync(string id);
    }
}