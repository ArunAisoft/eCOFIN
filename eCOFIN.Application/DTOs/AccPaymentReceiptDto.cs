namespace eCOFIN.Application.DTOs
{
    public class AccPaymentReceiptDto
    {
        public string PayReceiptId { get; set; }
        public double? ActAdvanceAmount { get; set; }
        public string Advance { get; set; }
        public double? AdvanceAmount { get; set; }
        public string BankName { get; set; }
        public double? ChequeAmount { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string ChequeNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CustCode { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string PayMode { get; set; }
        public DateTime? PayReceiptDate { get; set; }
    }
}
