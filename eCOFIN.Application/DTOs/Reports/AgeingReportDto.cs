namespace eCOFIN.Application.DTOs.Reports
{
    /// <summary>
    /// One flat row returned from the Debtors or Creditors ageing SQL.
    /// The Angular component groups these rows by party client-side.
    /// </summary>
    public class AgeingReportDto
    {
        /// <summary>"Bills" or "Payments"</summary>
        public string  Nature          { get; set; } = string.Empty;
        public string  AccountCode     { get; set; } = string.Empty;
        public string  SubAccountCode  { get; set; } = string.Empty;
        public string? PartyName       { get; set; }
        public string? VchrNumber      { get; set; }
        public DateTime? VchrDate      { get; set; }
        public string? BillNo          { get; set; }
        public DateTime? BillDueDate   { get; set; }
        public int     DaysOutstanding { get; set; }

        // Age buckets — matching the report columns in the screenshots
        public decimal Bucket0_30    { get; set; }   // <= 30
        public decimal Bucket30_45   { get; set; }   // 31–45
        public decimal Bucket46_90   { get; set; }   // 46–90
        public decimal Bucket91_120  { get; set; }   // 91–120
        public decimal Bucket121_180 { get; set; }   // 121–180
        public decimal Bucket180Plus { get; set; }   // > 180
        public decimal Total         { get; set; }
    }

    /// <summary>Request filter from Angular.</summary>
    public class AgeingReportFilter
    {
        /// <summary>"Debtors" or "Creditors"</summary>
        public string   ReportType { get; set; } = "Debtors";
        public DateTime ReportDate { get; set; }
    }
}
