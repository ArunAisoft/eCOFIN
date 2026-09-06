namespace eCOFIN.Application.DTOs.Vouchers
{
    public class ExistingPurchaseBillDto
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

    public class PurchaseBillsDto
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

    public class PurjDetailsDto
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

    public class PurchaseBillDetails
    {
        public string? BillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? BillDueDate { get; set; }
        public string? PORefNo { get; set; }
        public DateTime? PODate { get; set; }
        public string? TDSCode { get; set; }
        public decimal? BillAmount { get; set; }
        public decimal? DeduAmount { get; set; }
        public decimal? TDSAmount { get; set; }
    }

    public class PurchaseBillWithDetailsDto
    {
        public PurchaseBillsDto Header { get; set; } = new PurchaseBillsDto();
        public PurchaseBillDetails BillDetails { get; set; } = new PurchaseBillDetails();
        public List<PurjDetailsDto> Details { get; set; } = new List<PurjDetailsDto>();
    }

    public class PurchaseBillsRequestDto
    {
        public PurchaseBillsDto VoucherData { get; set; } = new();
        public PurchaseBillDetails BillDetails { get; set; } = new PurchaseBillDetails();
        public List<PurjDetailsDto> Details { get; set; } = new();
        public string? FinancialYear { get; set; } = string.Empty;
        public string? AccountingPeriod { get; set; } = string.Empty;
        public string? LocationCode { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
    }

    public class GINImportModel
    {
        public string GINNo { get; set; } = "";
        public DateTime? GINDate { get; set; }
        public string InvoiceNo { get; set; } = "";
        public DateTime? InvoiceDate { get; set; }
        public DateTime? PmtDDate { get; set; }
        public string PONo { get; set; } = "";
        public string SuppCode { get; set; } = "";
        public string VendorName { get; set; } = "";
        public DateTime? AppDate { get; set; }
        public decimal AccQtyPSLPrice { get; set; }
        public bool VendorMasterExists { get; set; }
        public bool VendorAccountExists { get; set; }
        public bool ArticleMappingExists { get; set; }
        public bool IsValid => VendorMasterExists && VendorAccountExists && ArticleMappingExists;
        public string ValidationMessage { get; set; } = string.Empty;
    }

    public class JINImportModel
    {
        public string JINNo { get; set; } = "";
        public DateTime? JINDate { get; set; }
        public string BankCode { get; set; } = "";
        public string AccountCode { get; set; } = "";
        public string VendorCode { get; set; } = "";
        public string VendorName { get; set; } = "";
        public string AccRemarks { get; set; } = "";
        public string TDSCode { get; set; } = "";
        public decimal ProductValue { get; set; }
        public decimal RejValue { get; set; }
        public decimal TDSValue { get; set; }
        public bool VendorMasterExists { get; set; }
        public bool VendorAccountExists { get; set; }
        public bool BankCodeExists { get; set; }
        public bool IsValid => VendorMasterExists && VendorAccountExists && BankCodeExists;
        public string ValidationMessage { get; set; } = string.Empty;
    }

    public class PurchaseERPRequestDto
    {
        public List<string> DocumentNumbers { get; set; } = new();
        public string? VoucherType { get; set; }
        public string? AccountingPeriod { get; set; }
        public string? Username { get; set; }
        public string? LocationCode { get; set; }
        public DateTime VoucherDate { get; set; }
    }
}
