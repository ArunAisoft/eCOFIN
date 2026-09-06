using eCOFIN.Application.DTOs.Reports;

namespace eCOFIN.Application.Interfaces.Reports
{
    public interface IBankReconciliationReportService
    {
        Task<IEnumerable<AccPeriodDto>>               GetAccPeriodsAsync();
        Task<IEnumerable<BankReconChequeIssuedDto>>    GetChequeIssuedNotPresentedAsync(BankReconFilter filter);
        Task<IEnumerable<BankReconChequeDepositedDto>> GetChequeDepositedNotPresentedAsync(BankReconFilter filter);
        Task<IEnumerable<BankReconBankDataDto>>        GetDebitedByBankNotAccountedAsync(BankReconFilter filter);
        Task<IEnumerable<BankReconBankDataDto>>        GetCreditedByBankNotAccountedAsync(BankReconFilter filter);
    }
}
