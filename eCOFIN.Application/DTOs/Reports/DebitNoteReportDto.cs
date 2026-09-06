namespace eCOFIN.Application.DTOs.Reports
{
    // ── Row returned from CFN_V_DEBITNOTE query ───────────────────────────────
    public class DebitNoteReportRowDto
    {
        public string? AccPeriod { get; set; }   // ACCPERIOD
        public string? VoucherNumber { get; set; }   // VOUCHERNUMBER
        public string? VoucherDate { get; set; }   // VOUCHERDATE      (formatted dd/MM/yyyy)
        public string? LineNo1 { get; set; }   // LINENO1
        public string? Particulars { get; set; }   // PARTICULARS
        public string? AccountCode { get; set; }   // ACCOUNTCODE      (line-level account)
        public string? SubAccountCode { get; set; }   // SUBACCOUNTCODE
        public string? AccountDescription { get; set; }   // ACCOUNTDESCRIPTION
        public string? SubAccountDescription { get; set; }   // SUBACCOUNTDESCRIPTION
        public string? ReferenceNo { get; set; }   // REFERENCENO
        public string? ReferenceDate { get; set; }   // REFERENCEDATE    (formatted)
        public decimal Amount { get; set; }   // AMOUNT
        public string? DbCrFlag { get; set; }   // DBCRFLAG  D | C
        public string? CostType { get; set; }   // COSTTYPE
        public string? ProductCode { get; set; }   // PRODUCTCODE
        public string? ExpenseType { get; set; }   // EXPENSETYPE
        public string? EmployeeCode { get; set; }   // EMPLOYEECODE
        public string? CostCentreCode { get; set; }   // COSTCENTRECODE
        public string? Description { get; set; }   // DESCRIPTION       (cost-centre desc from view)
        public string? CostTypeDescription { get; set; }   // COSTTYPEDESCRIPTION
        public string? ExpenseTypeDescription { get; set; }   // EXPENSETYPEDESCRITPION
        public string? OnHoldNo { get; set; }   // ONHOLDNO
        public string? Narration { get; set; }   // NARRATION
        public string? Automated { get; set; }   // AUTOMATED
        public string? VoucherRefNumber { get; set; }
        public string? VoucherRefDate { get; set; }   // AUTOMATED

        // Header (party) fields — shown once per voucher
        public string? HdrAccountCode { get; set; }   // ACCOUNTCODE_HDR  → HDR_AC
        public string? HdrSubAccountCode { get; set; }   // SUBACCOUNTCODE_HDR → HDR_SUBAC
        public string? HdrSubAccountDesc { get; set; }   // CFN_V_SUBCODLNK_B.SUBCODEDESCRIPTION → HDR_SUBAC_DESC

        // LEFT JOIN fields
        public string? SubCodeDescription { get; set; }   // CFN_V_SUBCODLNK_A.SUBCODEDESCRIPTION
        public string? CostTypeDesc { get; set; }   // CFN_V_COSTTYPE.PARAMETERDESCRIPTION
        public string? ExpenseTypeDesc { get; set; }   // CFN_V_EXPENSETYPE.PARAMETERDESCRIPTION
        public string? CostCentreDescription { get; set; }   // CFN_COSTCENTRE.DESCRIPTION
    }

    // ── Filter model ──────────────────────────────────────────────────────────
    public class DebitNoteReportFilterModel
    {
        public string AccPeriod { get; set; } = string.Empty;
    }
}
