namespace eCOFIN.Application.Interfaces.Masters
{
    using eCOFIN.Application.DTOs.Masters;

    public interface IVendorService
    {
        Task<IEnumerable<VendorDto>> GetAllVendorAsync();
        Task<IEnumerable<VendorDto>> GetAllActiveVendorsAsync();
        Task<(bool Success, string Message)> SaveOrUpdateVendorAsync(VendorCreateModel model);

        Task<IEnumerable<AccVendorDto>> GetByVendorAsync(string vendorCode);
        Task<IEnumerable<AccVendorAccountDto>> GetVendorAccountsAsync();
        Task<(bool Success, string Message)> SaveAccVendorAsync(AccVendorCreateModel model);

        Task<IEnumerable<ImportableSupplierDto>> GetImportableSuppliersAsync();
        Task<IEnumerable<ImportableVendorDto>> GetImportableVendorsAsync();
        Task<(bool Success, string Message)> ImportVendorAsync(ImportVendorModel model);
        Task<(bool Success, string Message)> ImportVendorsAsync(IEnumerable<ImportVendorModel> models);
    }
}