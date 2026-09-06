using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Amendmentnumber")]
[Table("CFN_HSTPURCHASEORDER")]
public partial class CfnHstpurchaseorder
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
    [StringLength(20)]
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
    public string VchrType { get; set; } = null!;

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

    [Column("CURRENCYCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Currencycode { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("SUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("CREDITPERIOD", TypeName = "numeric(5, 0)")]
    public decimal? Creditperiod { get; set; }

    [Column("QTYVARIANCE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Qtyvariance { get; set; }

    [Column("RATEVARIANCE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Ratevariance { get; set; }

    [Column("QUANTITYPERCENTAGE", TypeName = "numeric(5, 0)")]
    public decimal? Quantitypercentage { get; set; }

    [Column("QUANTITYVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Quantityvalue { get; set; }

    [Column("RATEPERCENTAGE", TypeName = "numeric(5, 0)")]
    public decimal? Ratepercentage { get; set; }

    [Column("RATEVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Ratevalue { get; set; }

    [Column("POSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Postatus { get; set; }

    [Column("ORDERTYPE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Ordertype { get; set; }

    [Column("AMENDMENTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Amendmenttype { get; set; }

    [Key]
    [Column("AMENDMENTNUMBER")]
    [StringLength(3)]
    [Unicode(false)]
    public string Amendmentnumber { get; set; } = null!;

    [Column("USERAMENDMENTNO", TypeName = "numeric(3, 0)")]
    public decimal? Useramendmentno { get; set; }

    [Column("AMENDMENTREMARKS")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Amendmentremarks { get; set; }

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

    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

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

    [Column("FACTORGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Factorgroup { get; set; }

    [Column("TERMDESC1")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Termdesc1 { get; set; }

    [Column("TERMDESC2")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Termdesc2 { get; set; }

    [Column("TERMDESC3")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Termdesc3 { get; set; }

    [Column("TERMDESC4")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Termdesc4 { get; set; }

    [Column("TERMDESC5")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Termdesc5 { get; set; }

    [Column("TERMDESC6")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Termdesc6 { get; set; }

    [Column("ADVANCEAMT", TypeName = "numeric(14, 2)")]
    public decimal? Advanceamt { get; set; }
}
