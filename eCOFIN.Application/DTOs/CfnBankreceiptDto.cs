namespace eCOFIN.Application.DTOs
{
    public class CfnBankreceiptDto
    {
        public string CtrlOnholdno { get; set; }
        public decimal? BankRate { get; set; }
        public string Bankaccount { get; set; }
        public string Bankcode { get; set; }
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
