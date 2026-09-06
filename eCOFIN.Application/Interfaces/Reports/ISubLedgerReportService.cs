using eCOFIN.Application.DTOs.Reports;

namespace eCOFIN.Application.Interfaces.Reports
{
    public interface ISubLedgerReportService
    {
        Task<IEnumerable<AccPeriodDto>>  GetAccPeriodsAsync();
        Task<IEnumerable<SubLedgerReportDto>>  GetDebtorLedgerAsync(SubLedgerFilter filter);
        Task<IEnumerable<SubLedgerReportDto>>  GetCreditLedgerAsync(SubLedgerFilter filter);
        Task<IEnumerable<SubLedgerReportDto>>  GetStaffLoanLedgerAsync(SubLedgerFilter filter);
        Task<IEnumerable<SubLedgerReportDto>>  GetStaffAdvanceLedgerAsync(SubLedgerFilter filter);
    }
}
