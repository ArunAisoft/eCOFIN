namespace eCOFIN.Application.Interfaces.Reports
{
    using eCOFIN.Application.DTOs.Reports;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGstReportService
    {
        Task<IEnumerable<GstReportDto>> GetGstReportAsync(DateTime fromDate, DateTime toDate);
    }
}