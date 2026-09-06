using eCOFIN.Infrastructure;

namespace eCOFIN.Application.DTOs.Vouchers
{
    public class BanksAndAccountsDto
    {
        public string BankCode { get; set; }
        public string? BankName { get; set; }
        public string? AccountType { get; set; }
        public string? ObjectStatus { get; set; }
        public List<BankAccountDto> BankAccounts { get; set; } = new();
    }

    public class BankAccountDto
    {
        public string? BankCode { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountStatus { get; set; }
        public string AccountType { get; set; }
        public string? BillwiseAppl { get; set; }
        public string? CostAppl { get; set; }
        public string? StockAppl { get; set; }
        public string? BudgetAppl { get; set; }
        public string? SubledgerAppl { get; set; }
        public string? EmployeeAppl { get; set; }
        public string? ProductAppl { get; set; }
        public string? ExpenseAppl { get; set; }
        public string? CostTypeAppl { get; set; }
        public string? BudgetType { get; set; }
        public decimal? Balance { get; set; }
        public List<VoucherTypeDto> VoucherTypes { get; set; } = new();
    }

    public class VoucherTypeDto
    {
        public string VoucherType { get; set; }
        public string? VoucherDescription { get; set; }
        public string VoucherGroup { get; set; }
    }

    public class GroupAccountDto
    {
        public string AccountCode { get; set; } = default!;
        public string AccountName { get; set; } = default!;
        public string AccountType { get; set; } = default!;
        public string BillwiseAppl { get; set; } = default!;
        public string CostAppl { get; set; } = default!;
        public string StockAppl { get; set; } = default!;
        public string BudgetAppl { get; set; } = default!;
        public string SubledgerAppl { get; set; } = default!;
        public string EmployeeAppl { get; set; } = default!;
        public string ProductAppl { get; set; } = default!;
        public string ExpenseAppl { get; set; } = default!;
        public string CostTypeAppl { get; set; } = default!;
        public string BudgetType { get; set; } = default!;
    }

    public class GroupSubAccountDto
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

    public class CreditDebitBankAccountDto
    {
        public string? BankCode { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountStatus { get; set; }
        public string AccountType { get; set; }
        public string? BillwiseAppl { get; set; }
        public string? CostAppl { get; set; }
        public string? StockAppl { get; set; }
        public string? BudgetAppl { get; set; }
        public string? SubledgerAppl { get; set; }
        public string? EmployeeAppl { get; set; }
        public string? ProductAppl { get; set; }
        public string? ExpenseAppl { get; set; }
        public string? CostTypeAppl { get; set; }
        public string? BudgetType { get; set; }
        public decimal? Balance { get; set; }
        public List<VoucherTypeDto> VoucherTypes { get; set; } = new();
        public List<VendorDto> Vendors { get; set; } = new();
        public List<CustomerDto> Customers { get; set; } = new();
    }

    public class VendorDto
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

    public class CustomerDto
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

    public sealed class VchrCounterRow
    {
        public int CurrentOnHoldNo { get; set; }
        public string? PrefixType { get; set; }
    }

    public class ExportVendorInvoiceDto
    {
        public string? AccountNo { get; set; }
        public string? SubAccountNo { get; set; }
        public string? CtrlOnHoldNo { get; set; }
        public int? CtrlSequenceNo { get; set; }
        public string? VoucherNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string? BillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal BillAmount { get; set; }
        public decimal BillBalance { get; set; }
        public decimal AmountAdjusted { get; set; }
        public decimal OrginalBillBalance { get; set; }
        public decimal OrginalAmountAdjusted { get; set; }
        public decimal? AcceptedAmount { get; set; }
        public string? GINJINNo { get; set; }
    }

    public class BillDetailsDto
    {
        public string? CtrlOnHoldNo { get; set; }
        public string? CtrlSequenceNo { get; set; }
        public string? VoucherNo { get; set; }
        public string? BillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal BillAmount { get; set; }
        public decimal AmountAdjusted { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal? AcceptedAmount { get; set; }
        public string? Particulars { get; set; }
    }

    public class PaymentDetailsDto
    {
        public string? CtrlOnHoldNo { get; set; }
        public string? CtrlSequenceNo { get; set; }
        public string? VoucherNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string? InstrumentNo { get; set; }
        public DateTime? InstrumentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal AmountAdjusted { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal? AcceptedAmount { get; set; }
        public string? Particulars { get; set; }
    }

    public class BillAndPaymentDto
    {
        public List<BillDetailsDto> BillDetails { get; set; } = new();
        public List<PaymentDetailsDto> PaymentDetails { get; set; } = new();
    }

    public class BillPaymentAdjustmentRequestDto
    {
        public BillAdjustmentDto Bill { get; set; } = null!;
        public List<PaymentAdjustmentDto> Payments { get; set; } = new();
    }

    public class BillAdjustmentDto
    {
        public string VoucherNo { get; set; } = string.Empty;
        public string OnHoldNo { get; set; } = string.Empty;
        public int SequenceNo { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal AmountAdjusted { get; set; }
    }

    public class PaymentAdjustmentDto
    {
        public string VoucherNo { get; set; } = string.Empty;
        public string OnHoldNo { get; set; } = string.Empty;
        public int SequenceNo { get; set; }
        public decimal AmountAdjusted { get; set; }
        public decimal BalanceAmount { get; set; }
    }

    public class VoucherInvoiceDetailDto
    {
        public string? BillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal AcceptedAmount { get; set; }
        public decimal BillBalance { get; set; }
        public string? GroupAccount { get; set; }
        public string? SubAccount { get; set; }
        public string OnHoldNo { get; set; } = string.Empty;
        public int SequenceNo { get; set; }
    }

    public class VoucherCostCenterDetailDto
    {
        public string? CostCenter { get; set; }
        public decimal Amount { get; set; }
        public string? GroupAccount { get; set; }
    }

    public class VoucherLineDto
    {
        public string? AccountCode { get; set; } = string.Empty;
        public string? SubAccountCode { get; set; } = string.Empty;
        public string? DbCrFlag { get; set; } = string.Empty;
        public decimal? DrCrAmount { get; set; }
    }

    public class PostMultipleResult
    {
        public List<string> Posted { get; set; } = new();
        public List<PostMultipleFailure> Failed { get; set; } = new();
        public bool HasFailures => Failed.Any();
    }

    public class PostMultipleFailure
    {
        public string OnHoldNo { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }

    public class PostMultipleRequest
    {
        public List<string> OnHoldNumbers { get; set; } = new();
        public string AccountingPeriod { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string LocationCode { get; set; } = string.Empty;
    }
}
