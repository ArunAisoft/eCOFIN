namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnChequerequestService
    {
        Task<IEnumerable<CfnChequerequestDto>> GetAllAsync();
        Task<CfnChequerequestDto?> GetByIdAsync(string id);
        Task<CfnChequerequestDto> CreateAsync(CfnChequerequestDto dto);
        Task<CfnChequerequestDto> UpdateAsync(CfnChequerequestDto dto);
        Task<bool> DeleteAsync(string id);
    }
}