namespace eCOFIN.Application.DTOs.Reports
{
    // ── Row returned from cfn_v_contra query ──────────────────────────────────
    public class ContraReportRowDto
    {
        public string?   VoucherDate            { get; set; }   // voucherdate  (formatted dd-MM-yyyy)
        public string?   VoucherNumber          { get; set; }   // vouchernumber
        public string?   AccountCode            { get; set; }   // accountcode
        public string?   SubAccountCode         { get; set; }   // subaccountcode
        public string?   AccountDesc            { get; set; }   // accountdesc
        public string?   LineParticulars        { get; set; }   // lineparticulars
        public string?   ReferenceNo            { get; set; }   // referenceno
        public string?   ReferenceDate          { get; set; }   // referencedate (formatted)
        public decimal   Amount                 { get; set; }   // amount
        public string?   DbCrFlag               { get; set; }   // dbcrflag  D | C
        public string?   CostCentreCode         { get; set; }   // costcentrecode
        public string?   ProductCode            { get; set; }   // productcode
        public string?   ExpenseType            { get; set; }   // expensetype
        public string?   AccPeriod              { get; set; }   // accperiod
        public string?   CostType               { get; set; }   // costtype
        public string?   InstrumentNo           { get; set; }   // instrumentno
        public string?   InstrumentDate         { get; set; }   // instrumentdate (formatted)
        public string?   OnHoldNo               { get; set; }   // onholdno
        public string?   Narration              { get; set; }   // narration
        public string?   VchrRefNumber          { get; set; }   // vchrrefnumber
        public string?   VchrRefDate            { get; set; }   // vchrrefdate (formatted)
        public string?   VchrType               { get; set; }   // vchrtype
        public string?   VchrCategory           { get; set; }   // vchrcatagory
        public string?   SysCategory            { get; set; }   // syscatagory
        public decimal?  TotalAmount            { get; set; }   // totalamount
        public string?   ChqAuthorize           { get; set; }   // chqauthorize
        public string?   Status                 { get; set; }   // status
        public string?   CancelFlag             { get; set; }   // cancelflag
        public string?   LocationCode           { get; set; }   // locationcode
        public string?   Username               { get; set; }   // username
        public string?   SubCodeDescription     { get; set; }   // cfn_v_subcodlnk.subcodedescription
        public string?   ExpenseTypeDesc        { get; set; }   // cfn_v_expensetype.parameterdescription
        public string?   CostTypeDesc           { get; set; }   // cfn_v_costtype.parameterdescription
        public string?   CostCentreDescription  { get; set; }   // cfn_costcentre.description
    }

    // ── Filter model ──────────────────────────────────────────────────────────
    public class ContraReportFilterModel
    {
        public string AccPeriod { get; set; } = string.Empty;   // e.g. "FEB - 2026"
    }
}
