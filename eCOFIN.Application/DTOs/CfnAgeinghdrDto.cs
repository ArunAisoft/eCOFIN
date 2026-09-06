namespace eCOFIN.Application.DTOs
{
    public class CfnAgeinghdrDto
    {
        public string CtrlOnholdno { get; set; }
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
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public decimal? Lowerlimit1 { get; set; }
        public decimal? Lowerlimit2 { get; set; }
        public DateTime Periodfrom { get; set; }
        public DateTime Periodto { get; set; }
        public string Reporttitle { get; set; }
        public decimal? Upperlimit1 { get; set; }
        public decimal? Upperlimit2 { get; set; }
    }
}
