namespace eCOFIN.Application.DTOs
{
    public class CfnBudgetstackDto
    {
        public decimal CtrlSequenceno { get; set; }
        public string Accountcode { get; set; }
        public decimal? ActualamtCumm { get; set; }
        public decimal? ActualamtCurr { get; set; }
        public decimal? ActualamtNxt { get; set; }
        public decimal? AllocatedamountCumm { get; set; }
        public decimal? AllocatedamountCurr { get; set; }
        public decimal? AllocatedamountNxt { get; set; }
        public decimal? AllocvarCumm { get; set; }
        public decimal? AllocvarCurr { get; set; }
        public decimal? BudgetamountCumm { get; set; }
        public decimal? BudgetamountCurr { get; set; }
        public decimal? BudgetamountNxt { get; set; }
        public decimal? BudgetvarCumm { get; set; }
        public decimal? BudgetvarCurr { get; set; }
        public string Costcentrecode { get; set; }
        public string Fromperiod { get; set; }
        public string Toperiod { get; set; }
    }
}
