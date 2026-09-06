using eCOFIN.Application.DTOs.Masters;

namespace eCOFIN.Application.Interfaces.Masters
{
    public interface IDefinePrefixService
    {
        Task<PagedResult<PrefixLinkDto>> GetLinksAsync(
            string vchrType,
            string prefixType,
            string? search = null,
            int page = 1,
            int pageSize = 50);

        Task<PrefixLookupsDto> GetLookupsAsync();

        Task<List<AccountLookupDto>> GetAccountsAsync();

        Task<List<CustomerLookupDto>> GetCustomersAsync();

        /// <summary>
        /// Permission, cfn_genhelp metadata and lookups for the Add button.
        /// Implemented by DefinePrefixService.GetAddContextAsync.
        /// </summary>
        Task<PrefixAddContextDto> GetAddContextAsync(string username);

        /// <summary>
        /// ctrlStatus is "Post" or "ONHOLD", mirroring the legacy toolbar.
        /// The single-argument overload no longer exists.
        /// </summary>
        Task<(bool Success, string Message)> SaveLinksAsync(
            SavePrefixLinksRequest model,
            string ctrlStatus);

        Task<(bool Success, string Message)> DeleteLinkAsync(DeletePrefixLinkRequest model);

        void InvalidateLookupCache();
    }
}