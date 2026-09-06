namespace eCOFIN.Application.DTOs
{
    public class AccDcnoteMasterDto
    {
        public string DcnoteNo { get; set; }
        public string AppBy { get; set; }
        public DateTime? AppDate { get; set; }
        public double? CgstPerM { get; set; }
        public string ClientName { get; set; }
        public string ContactPerson { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyRate { get; set; }
        public string DcnoteCancel { get; set; }
        public DateTime DcnoteDate { get; set; }
        public string DcnoteType { get; set; }
        public string EnteredBy { get; set; }
        public double? GstPerM { get; set; }
        public string GstType { get; set; }
        public string HsnCodeM { get; set; }
        public double? IgstPerM { get; set; }
        public string Insurance { get; set; }
        public string MacAddress { get; set; }
        public double? OtherCharges { get; set; }
        public double? OtherChargesOutSideGst { get; set; }
        public string PartyCode { get; set; }
        public DateTime? PayDueDate { get; set; }
        public string PayTerms { get; set; }
        public double? Pf { get; set; }
        public string Remarks { get; set; }
        public double? SgstPerM { get; set; }
        public string ShipMode { get; set; }
        public long? TransactionIdSlNo { get; set; }
    }
}
