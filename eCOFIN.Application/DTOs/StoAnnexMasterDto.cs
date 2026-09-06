namespace eCOFIN.Application.DTOs
{
    public class StoAnnexMasterDto
    {
        public string AnnNo { get; set; }
        public double? AmountCredit { get; set; }
        public double? AmountDebit { get; set; }
        public DateTime? AnnDDate { get; set; }
        public DateTime? AnnDate { get; set; }
        public string AnnNoSwipe { get; set; }
        public DateTime? AnnRDate { get; set; }
        public string AnnRemarks { get; set; }
        public string AnnReturn { get; set; }
        public double? AnnReturnQty { get; set; }
        public string AnnSwipeNo { get; set; }
        public string AnnType { get; set; }
        public double? AnnValue { get; set; }
        public string ClientName { get; set; }
        public string EmpCode { get; set; }
        public string ExtRcNo { get; set; }
        public double? IssueCost { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime? LeadTimeExSunday { get; set; }
        public string MacAddress { get; set; }
        public string OpnList { get; set; }
        public double? ProcessCost { get; set; }
        public string RateList { get; set; }
        public string RetEmpCode { get; set; }
        public string RetRemarks { get; set; }
        public long RowNo { get; set; }
        public string TrayDetails { get; set; }
        public int? TrayTokenNo { get; set; }
        public int TraysRetun { get; set; }
        public int TraysSent { get; set; }
    }
}
