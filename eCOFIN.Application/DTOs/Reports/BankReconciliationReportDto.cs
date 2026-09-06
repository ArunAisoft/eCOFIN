namespace eCOFIN.Application.DTOs.Reports
{
    public class AccPeriodDto
    {
        public string? AccPeriod { get; set; }
        public string? PeriodFrom { get; set; }
        public string? PeriodTo { get; set; }
        public int? Sequence { get; set; }
        public string? FinancialYear { get; set; }
    }

    public class BankReconFilter
    {
        public string AccPeriod { get; set; } = string.Empty;
        public DateTime AsAtDate { get; set; }
    }

    public class BankReconChequeIssuedDto
    {
        public string? VoucherNumber { get; set; }
        public string? VchrDate { get; set; }
        public string? InstrumentNo { get; set; }
        public string? InstrumentDate { get; set; }
        public string? AccPeriod { get; set; }
        public string? LineParticulars { get; set; }
        public string? BankName { get; set; }
        public decimal? Amount { get; set; }
    }

    public class BankReconChequeDepositedDto
    {
        public string? VoucherNumber { get; set; }
        public string? VchrDate { get; set; }
        public string? InstrumentNo { get; set; }
        public string? InstrumentDate { get; set; }
        public string? LineParticulars { get; set; }
        public string? PartyName { get; set; }
        public string? BankName { get; set; }
        public decimal? Amount { get; set; }
    }

    public class BankReconBankDataDto
    {
        public string? VoucherNumber { get; set; }
        public string? VchrDate { get; set; }
        public string? InstrumentNo { get; set; }
        public string? InstrumentDate { get; set; }
        public string? LineParticulars { get; set; }
        public string? AccountDesc { get; set; }
        public string? DbCrFlag { get; set; }
        public decimal? Amount { get; set; }
    }
}
