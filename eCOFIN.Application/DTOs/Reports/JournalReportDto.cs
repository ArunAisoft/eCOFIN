namespace eCOFIN.Application.DTOs.Reports
{
    // ── Row returned from CFN_V_JOURNALREGISTER query ─────────────────────────
    public class JournalReportRowDto
    {
        public string?  AccPeriod          { get; set; }   // ACCPERIOD
        public string?  VoucherDate        { get; set; }   // VOUCHERDATE  (formatted dd/MM/yyyy)
        public string?  VoucherNumber      { get; set; }   // VOUCHERNUMBER
        public string?  Particulars        { get; set; }   // PARTICULARS
        public string?  AccountCode        { get; set; }   // ACCOUNTCODE
        public string?  SubCode            { get; set; }   // SUBCODE
        public string?  CostCentreCode     { get; set; }   // COSTCENTRECODE
        public string?  OnHoldNo           { get; set; }   // ONHOLDNO
        public string?  VoucherRefNumber   { get; set; }   // VOUCHERREFNUMBER
        public string?  VoucherRefDate     { get; set; }   // VOUCHERREFDATE   (formatted)
        public string?  Narration          { get; set; }   // NARRATION
        public string?  VchrType           { get; set; }   // VCHRTYPE
        public string?  Category           { get; set; }   // CATEGORY
        public string?  SysCategory        { get; set; }   // SYSCATEGORY
        public decimal? TotalAmount        { get; set; }   // TOTALAMOUNT
        public string?  Status             { get; set; }   // STATUS
        public string?  CancelFlag         { get; set; }   // CANCELFLAG
        public string?  LocationCode       { get; set; }   // LOCATIONCODE
        public string?  Username           { get; set; }   // USERNAME
        public string?  Description        { get; set; }   // DESCRIPTION  (account description)
        public string?  CostType           { get; set; }   // COSTTYPE
        public string?  ExpenseType        { get; set; }   // EXPENSETYPE
        public string?  ProductCode        { get; set; }   // PRODUCTCODE
        public string?  EmployeeCode       { get; set; }   // EMPLOYEECODE
        public string?  SegCode2           { get; set; }   // SEGCODE2
        public string?  SubCodeDescription { get; set; }   // CFN_V_SUBCODLNK.SUBCODEDESCRIPTION
        public string?  ReferenceNumber    { get; set; }   // REFERENCENUMBER
        public string?  ReferenceDate      { get; set; }   // REFERENCEDATE  (formatted)
        public decimal  Amount             { get; set; }   // AMOUNT
        public string?  DbCrFlag           { get; set; }   // DBCRFLAG  D | C
        public string?  CostTypeDesc       { get; set; }   // CFN_V_COSTTYPE.PARAMETERDESCRIPTION
        public string?  ExpenseTypeDesc    { get; set; }   // CFN_V_EXPENSETYPE.PARAMETERDESCRIPTION
        public string?  CostCentreDescription { get; set; } // CFN_COSTCENTRE.DESCRIPTION
    }

    // ── Filter model ──────────────────────────────────────────────────────────
    public class JournalReportFilterModel
    {
        public string AccPeriod { get; set; } = string.Empty;   // e.g. "FEB - 2026"
    }
}
