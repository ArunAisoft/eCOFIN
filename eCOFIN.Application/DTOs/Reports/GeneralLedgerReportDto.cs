namespace eCOFIN.Application.DTOs.Reports
{
    public class LedgerFilter
    {
        public string AccPeriod { get; set; } = string.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class GeneralLedgerFilter
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class GeneralLedgerReportDto
    {
        public string? SequenceNo { get; set; }
        public string? AccountCode { get; set; }
        public string? AccountDescription { get; set; }
        public decimal? VoucherAmount { get; set; }
        public decimal? ClosingBalance { get; set; }
        public string? VchrNumber { get; set; }
        public string? VchrDate { get; set; }
        public string? LineDetails { get; set; }
        public string? VchrRefNumber { get; set; }
        public string? InstrumentNo { get; set; }
    }
}
