using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("cfn_voucherheader")]
public partial class CfnVoucherheader
{
    [Column("vchr_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("vchr_date", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("vchr_refnumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrRefnumber { get; set; }

    [Column("vchr_refdate", TypeName = "datetime")]
    public DateTime? VchrRefdate { get; set; }

    [Column("vchr_narration")]
    [StringLength(400)]
    [Unicode(false)]
    public string? VchrNarration { get; set; }

    [Column("vchr_type")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrType { get; set; }

    [Column("vchr_category")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrCategory { get; set; }

    [Column("vchr_syscategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrSyscategory { get; set; }

    [Column("vchr_totalamount", TypeName = "numeric(14, 2)")]
    public decimal? VchrTotalamount { get; set; }

    [Column("assetlocationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Assetlocationcode { get; set; }

    [Column("assetgroup")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Assetgroup { get; set; }

    [Column("assetclassification")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Assetclassification { get; set; }

    [Column("assetnumber")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Assetnumber { get; set; }

    [Column("cepref")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Cepref { get; set; }

    [Column("trvl_sanctionno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TrvlSanctionno { get; set; }

    [Column("trvl_employeeacc")]
    [StringLength(10)]
    [Unicode(false)]
    public string? TrvlEmployeeacc { get; set; }

    [Column("bankcode")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Bankcode { get; set; }

    [Column("bankname")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Bankname { get; set; }

    [Column("favourof")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Favourof { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("costcentrecode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("costcentrecodedescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Costcentrecodedescription { get; set; }

    [Column("subaccountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("subaccountcodedescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Subaccountcodedescription { get; set; }

    [Column("costtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costtype { get; set; }

    [Column("costtypedescription")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Costtypedescription { get; set; }

    [Column("productcode")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("productdescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Productdescription { get; set; }

    [Column("expensetype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Column("expensetypedescription")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Expensetypedescription { get; set; }

    [Column("employeecode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("employeename")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Employeename { get; set; }

    [Column("segcode2")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Segcode2 { get; set; }

    [Column("instrumentcategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrumentcategory { get; set; }

    [Column("instrument")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrument { get; set; }

    [Column("instrumentno")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("instrumentdate", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("instrumentbookno", TypeName = "numeric(5, 0)")]
    public decimal? Instrumentbookno { get; set; }

    [Column("gapcno")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Gapcno { get; set; }

    [Column("bankdocumentno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Bankdocumentno { get; set; }

    [Column("bankdocumentdate", TypeName = "datetime")]
    public DateTime? Bankdocumentdate { get; set; }

    [Column("purchasebilltype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Purchasebilltype { get; set; }

    [Column("lcnumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Lcnumber { get; set; }

    [Column("lcdate", TypeName = "datetime")]
    public DateTime? Lcdate { get; set; }

    [Column("currencycode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Currencycode { get; set; }

    [Column("exchangerate", TypeName = "numeric(10, 2)")]
    public decimal? Exchangerate { get; set; }

    [Column("foreigncurr", TypeName = "numeric(14, 4)")]
    public decimal? Foreigncurr { get; set; }

    [Column("chqauthorize")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Chqauthorize { get; set; }

    [Column("billpassingappl")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Billpassingappl { get; set; }

    [Key]
    [Column("ctrl_onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("ctrl_status")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("ctrl_cancelflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlCancelflag { get; set; }

    [Column("ctrl_locationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlLocationcode { get; set; }

    [Column("ctrl_accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlAccperiod { get; set; }

    [Column("ctrl_username")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CtrlUsername { get; set; }

    [Column("ctrl_createdon", TypeName = "datetime")]
    public DateTime? CtrlCreatedon { get; set; }

    [Column("ctrl_lastupdate", TypeName = "datetime")]
    public DateTime? CtrlLastupdate { get; set; }

    [Column("ctrl_logextract")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextract { get; set; }

    [Column("ctrl_trglocationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("ctrl_logextracttype")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextracttype { get; set; }

    [Column("vendorcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Vendorcode { get; set; }

    [Column("chqgenerate")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Chqgenerate { get; set; }

    [Column("bankaccount")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Bankaccount { get; set; }

    [Column("partycode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Partycode { get; set; }

    [Column("paidto")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Paidto { get; set; }

    [Column("cashaccount")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Cashaccount { get; set; }

    [Column("vendorname")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Vendorname { get; set; }

    [Column("traveltype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Traveltype { get; set; }

    [Column("travelamount", TypeName = "numeric(14, 2)")]
    public decimal? Travelamount { get; set; }

    [Column("localconveyance", TypeName = "numeric(10, 2)")]
    public decimal? Localconveyance { get; set; }

    [Column("hotel", TypeName = "numeric(10, 2)")]
    public decimal? Hotel { get; set; }

    [Column("sundries", TypeName = "numeric(10, 2)")]
    public decimal? Sundries { get; set; }

    [Column("entertainment", TypeName = "numeric(10, 2)")]
    public decimal? Entertainment { get; set; }

    [Column("traveltotal", TypeName = "numeric(14, 2)")]
    public decimal? Traveltotal { get; set; }

    [Column("sanctionno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Sanctionno { get; set; }

    [Column("sanctionedamount", TypeName = "numeric(14, 2)")]
    public decimal? Sanctionedamount { get; set; }

    [Column("warehousecode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Warehousecode { get; set; }

    [Column("givvchrtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Givvchrtype { get; set; }

    [Column("givvchrsys_type")]
    [StringLength(5)]
    [Unicode(false)]
    public string? GivvchrsysType { get; set; }

    [Column("creditvchr_type")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CreditvchrType { get; set; }

    [Column("creditvchr_systemtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CreditvchrSystemtype { get; set; }

    [Column("invctrl_accperiod")]
    [StringLength(5)]
    [Unicode(false)]
    public string? InvctrlAccperiod { get; set; }

    [Column("bank_rate", TypeName = "numeric(14, 2)")]
    public decimal? BankRate { get; set; }
}
