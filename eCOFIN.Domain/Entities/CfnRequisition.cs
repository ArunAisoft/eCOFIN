using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_REQUISITION")]
public partial class CfnRequisition
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
    public DateTime VchrDate { get; set; }

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

    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("REQUISITIONSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Requisitionstatus { get; set; }

    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

    [Column("BUDGETTOTAL", TypeName = "numeric(14, 0)")]
    public decimal? Budgettotal { get; set; }

    [Column("REQUISITIONPERIOD", TypeName = "datetime")]
    public DateTime? Requisitionperiod { get; set; }

    [Column("REMARK")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Remark { get; set; }

    [Column("REQUISITIONDOCUMENT1")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? Requisitiondocument1 { get; set; }

    [Column("REQUISITIONDOCUMENT2")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Requisitiondocument2 { get; set; }

    [Column("REQUISITIONDOCUMENT3")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Requisitiondocument3 { get; set; }

    [Column("REQUISITIONDOCUMENT4")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Requisitiondocument4 { get; set; }

    [Column("REQUISITIONDOCUMENT5")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Requisitiondocument5 { get; set; }

    [Column("REQUISITIONDOCUMENT6")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Requisitiondocument6 { get; set; }

    [Column("REQUISITIONDOCUMENT7")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Requisitiondocument7 { get; set; }

    [Column("REQUISITIONDOCUMENT8")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Requisitiondocument8 { get; set; }

    [Column("REQUISITIONDOCUMENT9")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Requisitiondocument9 { get; set; }

    [Column("REQUISITIONDOCUMENT10")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Requisitiondocument10 { get; set; }

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

    [Column("QUOTEBASEDRATECONTRACT")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Quotebasedratecontract { get; set; }

    [Column("VENDORCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Vendorcode { get; set; }
}
