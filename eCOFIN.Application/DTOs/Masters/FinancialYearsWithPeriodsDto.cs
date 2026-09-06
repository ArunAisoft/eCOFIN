using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCOFIN.Application.DTOs.Masters
{
    public class FinancialYearsWithPeriodsDto
    {
        public string Financialyear { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public string? Description { get; set; }
        public List<FinancialYearPeriodsDto> Periods { get; set; } = new();
    }

    public class FinancialYearPeriodsDto
    {
        public string Financialyear { get; set; }
        public string Accperiod { get; set; }
        public string Accmonth { get; set; }
        public string Accyear { get; set; }
        public DateTime Periodfrom { get; set; }
        public DateTime Periodto { get; set; }
        public decimal Sequence { get; set; }
    }

    public class FinancialYearDto
    {
        public string FinancialYear { get; set; } = string.Empty;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Description { get; set; }
    }

    public class FinancialYearCreateModel
    {
        public string financialYear { get; set; } = string.Empty;
        public string fromDate { get; set; } = string.Empty;
        public string toDate { get; set; } = string.Empty;
        public string? description { get; set; }
        public string? accPeriod { get; set; }
        public string? location { get; set; }
        public string? username { get; set; }
    }

    public class AccountingPeriodDto
    {
        public string Accperiod { get; set; } = string.Empty;
        public string? Accmonth { get; set; }
        public DateTime? Periodfrom { get; set; }
        public DateTime? Periodto { get; set; }
        public decimal? Sequence { get; set; }
        public string? Accyear { get; set; }
    }

    public class AccountingPeriodCreateModel
    {
        public string? FinancialYear { get; set; }
        public string Accperiod { get; set; } = string.Empty;
        public string? Accmonth { get; set; }
        public string Periodfrom { get; set; } = string.Empty;
        public string Periodto { get; set; } = string.Empty;
        public decimal? Sequence { get; set; }
        public string? Accyear { get; set; }
    }
}