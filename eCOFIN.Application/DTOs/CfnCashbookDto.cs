namespace eCOFIN.Application.DTOs
{
    public class CfnCashbookDto
    {
        public string Cashcontrolaccount { get; set; }
        public decimal? Onholdamount { get; set; }
        public decimal? Postedclosingbalance { get; set; }
        public decimal? Workingbalance { get; set; }
    }
}
