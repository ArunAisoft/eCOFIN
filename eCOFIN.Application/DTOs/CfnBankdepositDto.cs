namespace eCOFIN.Application.DTOs
{
    public class CfnBankdepositDto
    {
        public string CtrlOnholdno { get; set; }
        public string Accountcode { get; set; }
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
        public DateTime Depositslipdate { get; set; }
        public string Depositslipno { get; set; }
        public string Instrumentcategory { get; set; }
        public decimal? Totalamount { get; set; }
    }
}
