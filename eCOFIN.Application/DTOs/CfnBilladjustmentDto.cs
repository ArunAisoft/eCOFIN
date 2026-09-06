namespace eCOFIN.Application.DTOs
{
    public class CfnBilladjustmentDto
    {
        public decimal Billserialno { get; set; }
        public string Accountcode { get; set; }
        public string Bankdocumentno { get; set; }
        public string BillCtrlonholdno { get; set; }
        public decimal? BillCtrlsequenceno { get; set; }
        public decimal? Billadjusted { get; set; }
        public DateTime? Billdate { get; set; }
        public string Billno { get; set; }
        public string Costcentrecode { get; set; }
        public string Costtype { get; set; }
        public string Dbcrflag { get; set; }
        public string Employeecode { get; set; }
        public string Expensetype { get; set; }
        public string Instrument { get; set; }
        public string Instrumentbookno { get; set; }
        public string Instrumentcategory { get; set; }
        public DateTime? Instrumentdate { get; set; }
        public string Instrumentno { get; set; }
        public string Lcnumber { get; set; }
        public string PaymentCtrlonholdno { get; set; }
        public decimal? PaymentCtrlsequenceno { get; set; }
        public string Productcode { get; set; }
        public string Segcode2 { get; set; }
        public string Subaccountcode { get; set; }
        public string VchrCategory { get; set; }
        public DateTime? VchrDate { get; set; }
        public string VchrNarration { get; set; }
        public string VchrNumber { get; set; }
        public DateTime? VchrRefdate { get; set; }
        public string VchrRefnumber { get; set; }
        public string VchrSyscategory { get; set; }
        public decimal? VchrTotalamount { get; set; }
        public string VchrType { get; set; }
    }
}
