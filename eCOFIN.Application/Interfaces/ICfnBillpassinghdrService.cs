namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnBillpassinghdrService
    {
        Task<IEnumerable<CfnBillpassinghdrDto>> GetAllAsync();
        Task<CfnBillpassinghdrDto?> GetByIdAsync(string id);
        Task<CfnBillpassinghdrDto> CreateAsync(CfnBillpassinghdrDto dto);
        Task<CfnBillpassinghdrDto> UpdateAsync(CfnBillpassinghdrDto dto);
        Task<bool> DeleteAsync(string id);
    }
}