using System;

namespace eCOFIN.Application.DTOs.Vouchers
{
    public class TrialBalanceDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string? AccountType { get; set; }
        public string? Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }

    public class GLDetailDto
    {
        public string? SequenceNo { get; set; }
        public string AccountCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string? VoucherNumber { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string? LineDetails { get; set; }
        public string? VoucherType { get; set; }
    }

    public class SubledgerScheduleDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string SubAccountCode { get; set; } = string.Empty;
        public string? SubCodeDescription { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }

    public class SubledgerAccountDto
    {
        public string? SequenceNo { get; set; }
        public string AccountCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string? VoucherNumber { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string? InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? LineDetails { get; set; }
        public string? SubAccountCode { get; set; }
        public string? SubCodeDescription { get; set; }
        public string? LocationCode { get; set; }

        public decimal OpeningBalance { get; set; }
    }

    public class BillPaymentDto
    {
        public string? Nature { get; set; }
        public string AccountCode { get; set; } = string.Empty;
        public string? SubAccountCode { get; set; }
        public string? VoucherNumber { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string? BillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal? BillBalance { get; set; }
        public string? BillReferenceNo { get; set; }
        public string? SubCodeDescription { get; set; }
        public string? VoucherNarration { get; set; }
        public string? BillRefNo { get; set; }
    }

    public class VoucherEntryDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? SubAccountCode { get; set; }
        public string? SubCodeDescription { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string? VoucherNarration { get; set; }
        public string? LineDetails { get; set; }
        public string? DbCrFlag { get; set; }
        public decimal VoucherAmount { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string? InstrumentNo { get; set; }
        public DateTime? InstrumentDate { get; set; }
        public decimal TdsAmount { get; set; }
        public int CtrlSequenceNo { get; set; }
    }

    public class CostProductEntryDto
    {
        public string? AccountCode { get; set; }
        public string? Description { get; set; }
        public string? CostCentreCode { get; set; }
        public decimal VoucherAmount { get; set; }
        public string? CtrlStatus { get; set; }
        public int CtrlSequenceNo { get; set; }
        public string? CostCentreDescription { get; set; }
    }

    public class BillsPaymentsAdjustedDto
    {
        public string? VoucherNumber { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string? ReferenceNumber { get; set; }
        public DateTime? ReferenceDate { get; set; }
        public string? BillNumber { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal? BillAdjustedAmount { get; set; }
    }
}