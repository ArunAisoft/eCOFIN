namespace eCOFIN.Application.DTOs
{
    public class CfnBankbookDto
    {
        public string Bankcontrolaccount { get; set; }
        public decimal? Onholdamount { get; set; }
        public decimal? Postedclosingbalance { get; set; }
        public decimal? Workingbalance { get; set; }
    }
}
