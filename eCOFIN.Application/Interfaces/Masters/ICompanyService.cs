using eCOFIN.Application.DTOs.Masters;

namespace eCOFIN.Application.Interfaces.Masters
{
    public interface ICompanyService
    {
        Task<CompanyDto?> GetCompanyAsync();
        Task<(bool Success, string Message)> SaveOrUpdateCompanyAsync(CompanyCreateModel model);
    }
}
