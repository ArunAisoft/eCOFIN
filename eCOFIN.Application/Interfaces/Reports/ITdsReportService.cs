namespace eCOFIN.Application.Interfaces.Reports
{
    using eCOFIN.Application.DTOs.Reports;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITdsReportService
    {
        Task<IEnumerable<TdsAccountDto>> GetTdsAccountsAsync();
        Task<IEnumerable<TdsReportRowDto>> GetTdsReportAsync(TdsReportFilterModel filter);
        Task<IEnumerable<VoucherDetailDto>> GetVoucherDetailsAsync(string vchrNumber);
    }
}