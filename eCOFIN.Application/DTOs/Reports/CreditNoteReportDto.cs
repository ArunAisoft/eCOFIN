namespace eCOFIN.Application.DTOs.Reports
{
    // ── Row returned from CFN_V_CREDITNOTE query ──────────────────────────────
    public class CreditNoteReportRowDto
    {
        public string?  AccPeriod              { get; set; }
        public string?  VoucherNumber          { get; set; }
        public string?  VoucherDate            { get; set; }   // formatted dd/MM/yyyy
        public string?  LineNo1                { get; set; }
        public string?  Particulars            { get; set; }
        public string?  AccountCode            { get; set; }   // line-level account
        public string?  SubAccountCode         { get; set; }
        public string?  AccountDescription     { get; set; }
        public string?  SubAccountDescription  { get; set; }
        public string?  ReferenceNo            { get; set; }
        public string?  ReferenceDate          { get; set; }   // formatted
        public decimal  Amount                 { get; set; }
        public string?  DbCrFlag               { get; set; }   // D | C
        public string?  CostType               { get; set; }
        public string?  ProductCode            { get; set; }
        public string?  ExpenseType            { get; set; }
        public string?  EmployeeCode           { get; set; }
        public string?  CostCentreCode         { get; set; }
        public string?  Description            { get; set; }
        public string?  CostTypeDescription    { get; set; }
        public string?  ExpenseTypeDescription { get; set; }
        public string?  OnHoldNo               { get; set; }
        public string?  Narration              { get; set; }
        public string?  Automated              { get; set; }

        // Header (party) fields — shown once per voucher group
        public string?  HdrAccountCode         { get; set; }   // ACCOUNTCODE_HDR
        public string?  HdrSubAccountCode      { get; set; }   // SUBACCOUNTCODE_HDR
        public string?  HdrSubAccountDesc      { get; set; }   // CFN_V_SUBCODLNK_B.SUBCODEDESCRIPTION

        // LEFT JOIN resolved fields
        public string?  SubCodeDescription     { get; set; }   // CFN_V_SUBCODLNK_A.SUBCODEDESCRIPTION
        public string?  CostTypeDesc           { get; set; }   // CFN_V_COSTTYPE.PARAMETERDESCRIPTION
        public string?  ExpenseTypeDesc        { get; set; }   // CFN_V_EXPENSETYPE.PARAMETERDESCRIPTION
        public string?  CostCentreDescription  { get; set; }   // CFN_COSTCENTRE.DESCRIPTION
    }

    public class CreditNoteReportFilterModel
    {
        public string AccPeriod { get; set; } = string.Empty;
    }
}
