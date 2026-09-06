namespace eCOFIN.Application.DTOs.Vouchers
{
    public class ExistingBankReceiptDto
    {
        public string CtrlOnHoldNo { get; set; } = null!;
        public string? VchrNumber { get; set; }
        public DateTime? VchrDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? BankCode { get; set; }
        public string? Description { get; set; }
        public string? VchrNarration { get; set; }
    }

    public class BankReceiptsDto
    {
        public string? CtrlOnHoldNo { get; set; } = string.Empty;
        public string? VoucherNumber { get; set; } = string.Empty;
        public DateTime? VoucherDate { get; set; }
        public string? BankCode { get; set; } = string.Empty;
        public string? BankAccount { get; set; } = string.Empty;
        public decimal? BankRate { get; set; }
        public string? CurrencyCode { get; set; }
        public string? VoucherType { get; set; } = string.Empty;
        public string? VoucherSysCategory { get; set; }
        public string? VoucherNarration { get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Balance { get; set; }
    }

    public class BankrDetailsDto
    {
        public string? CtrlOnHoldNo { get; set; } = string.Empty;
        public decimal? CtrlSequenceNo { get; set; }
        public string? DbCrFlag { get; set; } = string.Empty;
        public string? AccountCode { get; set; } = string.Empty;
        public string? SubAccountCode { get; set; } = string.Empty;
        public decimal? DrCrAmount { get; set; }
        public string? Instrument { get; set; } = string.Empty;
        public string? InstrumentNo { get; set; } = string.Empty;
        public DateTime? InstrumentDate { get; set; }
        public string? LineParticulars { get; set; } = string.Empty;
        public string? Automated { get; set; }
        public List<VoucherInvoiceDetailDto> InvoiceDetails { get; set; } = new();
        public List<VoucherCostCenterDetailDto> CostCenterDetails { get; set; } = new();
    }

    public class BankReceiptWithDetailsDto
    {
        public BankReceiptsDto Header { get; set; } = new BankReceiptsDto();
        public List<BankrDetailsDto> Details { get; set; } = new List<BankrDetailsDto>();
    }

    public class BankReceiptsRequestDto
    {
        public BankReceiptsDto VoucherData { get; set; } = new();
        public List<BankrDetailsDto> Details { get; set; } = new();
        public string? FinancialYear { get; set; } = string.Empty;
        public string? AccountingPeriod { get; set; } = string.Empty;
        public string? LocationCode { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
    }
}
