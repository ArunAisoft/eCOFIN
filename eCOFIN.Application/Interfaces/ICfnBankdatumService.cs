namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBankdatumService
    {
        Task<IEnumerable<CfnBankdatumDto>> GetAllAsync();
        Task<CfnBankdatumDto?> GetByIdAsync(decimal id);
        Task<CfnBankdatumDto> CreateAsync(CfnBankdatumDto dto);
        Task<CfnBankdatumDto> UpdateAsync(CfnBankdatumDto dto);
        Task<bool> DeleteAsync(decimal id);
    }
}