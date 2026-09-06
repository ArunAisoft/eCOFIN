namespace eCOFIN.Application.DTOs.Reports
{
    public class GstReportDto
    {
        public string? PjvNumber { get; set; }
        public DateTime? PjvDate { get; set; }
        public string? GinNumber { get; set; }
        public DateTime? GinDate { get; set; }
        public string? Party { get; set; }
        public string? InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? ItemDescription { get; set; }
        public decimal BasicValue { get; set; }
        public decimal ExciseDuty { get; set; }
        public decimal EdCess { get; set; }
        public decimal HedCess { get; set; }
        public decimal Others { get; set; }
        public decimal RateOfTax { get; set; }
        public decimal Vat { get; set; }
        public decimal NonVat { get; set; }
        public decimal Cst { get; set; }
        public decimal Total { get; set; }
        public string? VatType { get; set; }
        public string? CstNo { get; set; }
        public string? TinNo { get; set; }
    }
}
