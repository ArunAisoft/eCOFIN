namespace eCOFIN.Application.DTOs.Vouchers
{
    public class ExistingSaleDto
    {
        public string CtrlOnHoldNo { get; set; } = null!;
        public string? VchrNumber { get; set; }
        public string? BillNumber { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal? BillAmount { get; set; }
        public string? BankCode { get; set; }
        public string? Description { get; set; }
        public string? VchrNarration { get; set; }
    }

    public class SalesDto
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

    public class SalvDetailsDto
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

    public class SaleBillDetails
    {
        public string? BillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal? BillAmount { get; set; }
    }

    public class SaleWithDetailsDto
    {
        public SalesDto Header { get; set; } = new SalesDto();
        public SaleBillDetails BillDetails { get; set; } = new SaleBillDetails();
        public List<SalvDetailsDto> Details { get; set; } = new List<SalvDetailsDto>();
    }

    public class SalesRequestDto
    {
        public SalesDto VoucherData { get; set; } = new();
        public List<SalvDetailsDto> Details { get; set; } = new();
        public SaleBillDetails BillDetails { get; set; } = new SaleBillDetails();
        public string? FinancialYear { get; set; } = string.Empty;
        public string? AccountingPeriod { get; set; } = string.Empty;
        public string? LocationCode { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
    }

    public class SaleExportModel
    {
        public string? InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public bool CustomerExists { get; set; }
        public bool AccountMappingExists { get; set; }
        public bool LinkedAccountExists { get; set; }
        public bool IsValid => CustomerExists && AccountMappingExists && LinkedAccountExists;
        public string ValidationMessage { get; set; } = string.Empty;
    }

    public class SalesERPRequestDto
    {
        public List<string> InvoiceNumbers { get; set; } = new();
        public string? VoucherType { get; set; }
        public string? AccountingPeriod { get; set; }
        public string? Username { get; set; }
        public string? LocationCode { get; set; }
        public DateTime VoucherDate { get; set; }
    }
}
