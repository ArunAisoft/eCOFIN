namespace eCOFIN.Application.Interfaces.Masters
{
    using eCOFIN.Application.DTOs.Masters;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomerAsync();
        Task<IEnumerable<CustomerDto>> GetAllActiveCustomersAsync();
        Task<(bool Success, string Message)> SaveOrUpdateCustomerAsync(CustomerCreateModel model);

        Task<IEnumerable<ImportableCustomerDto>> GetImportableCustomersAsync();
        Task<(bool Success, string Message)> ImportCustomerAsync(ImportCustomerModel model);
        Task<(bool Success, string Message)> ImportCustomersAsync(IEnumerable<ImportCustomerModel> models);

        Task<IEnumerable<AccountDto>> GetDebtorAccountsAsync();
        Task<IEnumerable<AccCustomerDto>> GetLinkedAccountsAsync(string customerCode);
        Task<(bool Success, string Message)> SaveAccountLinkAsync(AccountLinkModel model);
    }


}