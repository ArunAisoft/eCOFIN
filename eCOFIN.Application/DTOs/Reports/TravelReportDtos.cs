namespace eCOFIN.Application.DTOs.Reports
{
    public class TravelReportFilterModel
    {
        public string AccountCode { get; set; } = string.Empty;
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
    }

    public class TravelAccountDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class TravelReportRowDto
    {
        public string? AccPeriod { get; set; }
        public string? AccountCode { get; set; }
        public string? Description { get; set; }
        public string? VoucherNumber { get; set; }
        public string? VoucherDate { get; set; }
        public string? LineParticulars { get; set; }
        public string? CostCentreCode { get; set; }
        // ✅ FIX: Added CostCentreDesc — was selected in SQL (c.description) 
        //         but missing from DTO and RawRow, so frontend always got undefined
        public string? CostCentreDesc { get; set; }
        public string? SubAccountCode { get; set; }
        public string? CostType { get; set; }
        public string? ExpenseType { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? ReferenceDate { get; set; }
        public string? DbCrFlag { get; set; }
        public decimal? Amount { get; set; }
        public string? VchrRefNumber { get; set; }
        public string? VchrRefDate { get; set; }
    }
}