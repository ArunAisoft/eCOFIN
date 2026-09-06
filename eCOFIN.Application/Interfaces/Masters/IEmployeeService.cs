using eCOFIN.Application.DTOs.Masters;

namespace eCOFIN.Application.Interfaces.Masters
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeeAsync();
        Task<IEnumerable<EmployeeDto>> GetAllActiveEmployeesAsync();
        Task<(bool Success, string Message)> SaveOrUpdateEmployeeAsync(EmployeeCreateModel model);

        Task<IEnumerable<AccEmployeeDto>> GetByEmployeeAsync(string employeeCode);
        Task<IEnumerable<AccountDropdownDto>> GetEmployeeAccountsAsync();
        Task<(bool Success, string Message)> SaveAccEmployeeAsync(AccEmployeeCreateModel model);

        Task<IEnumerable<PendingPersonnelDto>> GetPendingPersonnelAsync();
        Task<IEnumerable<AccountDropdownDto>> GetEmpAccountsAsync();
        Task<(bool Success, string Message)> ImportEmployeeAsync(ImportEmployeeModel model);
        Task<(bool Success, string Message)> ImportEmployeesAsync(IEnumerable<ImportEmployeeModel> models);
    }
}