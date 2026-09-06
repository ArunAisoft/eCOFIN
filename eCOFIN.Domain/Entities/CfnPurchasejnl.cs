using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_PURCHASEJNL")]
public partial class CfnPurchasejnl
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("VCHR_NUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("VCHR_REFNUMBER")]
    [StringLength(35)]
    [Unicode(false)]
    public string? VchrRefnumber { get; set; }

    [Column("VCHR_REFDATE", TypeName = "datetime")]
    public DateTime? VchrRefdate { get; set; }

    [Column("VCHR_NARRATION")]
    [StringLength(400)]
    [Unicode(false)]
    public string? VchrNarration { get; set; }

    [Column("VCHR_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrType { get; set; }

    [Column("VCHR_CATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrCategory { get; set; }

    [Column("VCHR_SYSCATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrSyscategory { get; set; }

    [Column("VCHR_TOTALAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? VchrTotalamount { get; set; }

    [Column("PURCHASEBILLTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Purchasebilltype { get; set; }

    [Column("GAPCNO")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Gapcno { get; set; }

    [Column("ASSETLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Assetlocationcode { get; set; }

    [Column("ASSETGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Assetgroup { get; set; }

    [Column("ASSETCLASSIFICATION")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Assetclassification { get; set; }

    [Column("ASSETNUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Assetnumber { get; set; }

    [Column("CEPREF")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Cepref { get; set; }

    [Column("BANKDOCUMENTNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Bankdocumentno { get; set; }

    [Column("BANKDOCUMENTDATE", TypeName = "datetime")]
    public DateTime? Bankdocumentdate { get; set; }

    [Column("LCNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Lcnumber { get; set; }

    [Column("LCDATE", TypeName = "datetime")]
    public DateTime? Lcdate { get; set; }

    [Column("CURRENCYCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Currencycode { get; set; }

    [Column("EXCHANGERATE", TypeName = "numeric(10, 2)")]
    public decimal? Exchangerate { get; set; }

    [Column("BILLPASSINGAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Billpassingappl { get; set; }

    [Column("TRVL_SANCTIONNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TrvlSanctionno { get; set; }

    [Column("TRVL_EMPLOYEEACC")]
    [StringLength(10)]
    [Unicode(false)]
    public string? TrvlEmployeeacc { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("SUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("COSTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costtype { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("EXPENSETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Column("EMPLOYEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("SEGCODE2")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Segcode2 { get; set; }

    [Column("CTRL_STATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("CTRL_CANCELFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlCancelflag { get; set; }

    [Column("CTRL_LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlLocationcode { get; set; }

    [Column("CTRL_ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlAccperiod { get; set; }

    [Column("CTRL_USERNAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CtrlUsername { get; set; }

    [Column("CTRL_CREATEDON", TypeName = "datetime")]
    public DateTime? CtrlCreatedon { get; set; }

    [Column("CTRL_LASTUPDATE", TypeName = "datetime")]
    public DateTime? CtrlLastupdate { get; set; }

    [Column("CTRL_LOGEXTRACT")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextract { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("CTRL_LOGEXTRACTTYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextracttype { get; set; }
}
