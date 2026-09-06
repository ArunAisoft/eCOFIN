namespace eCOFIN.Application.Interfaces.Masters
{
    using eCOFIN.Application.DTOs.Masters;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFinancialYearsWithPeriodsService
    {
        Task<IEnumerable<FinancialYearsWithPeriodsDto>> GetFinancialYearPeriodsAsync();

        Task<IEnumerable<FinancialYearDto>> GetFinancialYearsAsync();
        Task<(bool Success, string Message)> CreateFinancialYearAsync(FinancialYearCreateModel model);

        Task<IEnumerable<FinancialYearDto>> GetFinancialYears2Async();
        Task<(bool Success, string Message)> CreateFinancialYear2Async(FinancialYearCreateModel model);

        Task<IEnumerable<AccountingPeriodDto>> GetPeriodsByYearAsync(string financialYear);
        Task<(bool Success, string Message)> SaveOrUpdatePeriodAsync(AccountingPeriodCreateModel model);
    }
}