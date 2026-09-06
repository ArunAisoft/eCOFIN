namespace eCOFIN.Application.DTOs.Reports
{

    public class SubLedgerFilter
    {
        public string AccPeriod { get; set; } = string.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? UserName { get; set; }
    }

    public class SubLedgerReportDto
    {
        public string? SequenceNo { get; set; }
        public string? AccountCode { get; set; }
        public string? AccountDescription { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? VoucherAmount { get; set; }
        public string? VchrNumber { get; set; }
        public string? VchrDate { get; set; }
        public string? LineDetails { get; set; }
        public string? SubAccountCode { get; set; }
        public string? SubAccountDesc { get; set; }
    }
}
