namespace eCOFIN.Application.DTOs.Reports
{
    public class SalesReportFilterModel
    {
        public string AccountCode { get; set; } = string.Empty;
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
    }

    public class SalesAccountDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class SalesReportRowDto
    {
        public string? CtrlAccPeriod { get; set; }
        public string? CtrlStatus { get; set; }
        public string? CtrlOnholdno { get; set; }
        public string? VchrNumber { get; set; }
        public string? VchrDate { get; set; }
        public string? HdrAccountCode { get; set; }
        public string? HdrAccountDesc { get; set; }
        public string? HdrSubAccountCode { get; set; }
        public string? HdrSubAccountDesc { get; set; }
        public string? VchrNarration { get; set; }
        public string? VchrRefDate { get; set; }
        public string? VchrRefNumber { get; set; }
        public string? BillNo { get; set; }
        public string? BillDate { get; set; }
        public decimal? BillAmount { get; set; }
        // ✅ FIX: Changed `internal set` → `set` so the service can populate these
        public string? TdsCode { get; set; }
        public string? TdsDescription { get; set; }
        public decimal? TdsAmount { get; set; }
        public string? DetailAccountCode { get; set; }
        public string? DetailAccountDesc { get; set; }
        public string? DetailSubAccountCode { get; set; }
        public string? DetailSubAccountDesc { get; set; }
        public string? DbCrFlag { get; set; }
        public decimal? Amount { get; set; }
        // ✅ FIX: Added CompanyName (was in raw row but missing from DTO)
        public string? CompanyName { get; set; }
    }
}