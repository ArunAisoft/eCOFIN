namespace eCOFIN.Application.DTOs
{
    public class CfnBankpaymentDto
    {
        public string CtrlOnholdno { get; set; }
        public decimal? BankRate { get; set; }
        public string Bankaccount { get; set; }
        public string Bankcode { get; set; }
        public DateTime? Bankdocumentdate { get; set; }
        public string Bankdocumentno { get; set; }
        public string Chqauthorize { get; set; }
        public string Chqgenerate { get; set; }
        public string CtrlAccperiod { get; set; }
        public string CtrlCancelflag { get; set; }
        public DateTime? CtrlCreatedon { get; set; }
        public DateTime? CtrlLastupdate { get; set; }
        public string CtrlLocationcode { get; set; }
        public string CtrlLogextract { get; set; }
        public string CtrlLogextracttype { get; set; }
        public string CtrlStatus { get; set; }
        public string CtrlTrglocationcode { get; set; }
        public string CtrlUsername { get; set; }
        public string Currencycode { get; set; }
        public decimal? Exchangerate { get; set; }
        public string Favourof { get; set; }
        public decimal? Foreigncurr { get; set; }
        public string Gapcno { get; set; }
        public string Instrument { get; set; }
        public decimal? Instrumentbookno { get; set; }
        public string Instrumentcategory { get; set; }
        public DateTime? Instrumentdate { get; set; }
        public string Instrumentno { get; set; }
        public DateTime? Lcdate { get; set; }
        public string Lcnumber { get; set; }
        public string Partycode { get; set; }
        public string VchrCategory { get; set; }
        public DateTime VchrDate { get; set; }
        public string VchrNarration { get; set; }
        public string VchrNumber { get; set; }
        public DateTime? VchrRefdate { get; set; }
        public string VchrRefnumber { get; set; }
        public string VchrSyscategory { get; set; }
        public decimal? VchrTotalamount { get; set; }
        public string VchrType { get; set; }
    }
}
