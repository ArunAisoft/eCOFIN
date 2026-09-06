namespace eCOFIN.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using eCOFIN.Application.DTOs;

    public interface ICfnAccncalender2Service
    {
        Task<IEnumerable<CfnAccncalender2Dto>> GetAllAsync();
        Task<CfnAccncalender2Dto?> GetByIdAsync(string id);
        Task<CfnAccncalender2Dto> CreateAsync(CfnAccncalender2Dto dto);
        Task<CfnAccncalender2Dto> UpdateAsync(CfnAccncalender2Dto dto);
        Task<bool> DeleteAsync(string id);
    }
}