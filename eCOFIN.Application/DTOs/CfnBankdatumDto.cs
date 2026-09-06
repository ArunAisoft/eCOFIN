namespace eCOFIN.Application.DTOs
{
    public class CfnBankdatumDto
    {
        public decimal Serialnumber { get; set; }
        public string Accountcode { get; set; }
        public decimal? Amount { get; set; }
        public string Bankcode { get; set; }
        public string Costcentrecode { get; set; }
        public string Costtype { get; set; }
        public string CtrlAccperiod { get; set; }
        public string CtrlCancelflag { get; set; }
        public DateTime? CtrlCreatedon { get; set; }
        public DateTime? CtrlLastupdate { get; set; }
        public string CtrlLocationcode { get; set; }
        public string CtrlLogextract { get; set; }
        public string CtrlLogextracttype { get; set; }
        public string CtrlOnholdno { get; set; }
        public decimal? CtrlSequenceno { get; set; }
        public string CtrlStatus { get; set; }
        public string CtrlTrglocationcode { get; set; }
        public string CtrlUsername { get; set; }
        public DateTime? Dateofclearence { get; set; }
        public string Dbcrflag { get; set; }
        public string Employeecode { get; set; }
        public string Expensetype { get; set; }
        public string Instrument { get; set; }
        public string Instrumentbookno { get; set; }
        public string Instrumentcategory { get; set; }
        public DateTime? Instrumentdate { get; set; }
        public string Instrumentno { get; set; }
        public string Lineparticulars { get; set; }
        public string Match { get; set; }
        public string Payorreceiptflag { get; set; }
        public string Productcode { get; set; }
        public string Segcode2 { get; set; }
        public string Subaccountcode { get; set; }
        public DateTime? VchrDate { get; set; }
        public string Vouchernumber { get; set; }
    }
}
