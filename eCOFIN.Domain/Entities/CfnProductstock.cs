using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Productcode", "Stocktype", "Warehousecode", "Accperiod")]
[Table("CFN_PRODUCTSTOCK")]
public partial class CfnProductstock
{
    [Key]
    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Key]
    [Column("STOCKTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Stocktype { get; set; } = null!;

    [Key]
    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("PRODUCTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Producttype { get; set; }

    [Column("ONHOLDOPENINGSTOCK", TypeName = "numeric(10, 0)")]
    public decimal? Onholdopeningstock { get; set; }

    [Column("POSTEDOPENINGSTOCK", TypeName = "numeric(10, 0)")]
    public decimal? Postedopeningstock { get; set; }

    [Column("ONHOLDRECEIPT", TypeName = "numeric(10, 0)")]
    public decimal? Onholdreceipt { get; set; }

    [Column("POSTEDRECEIPT", TypeName = "numeric(10, 0)")]
    public decimal? Postedreceipt { get; set; }

    [Column("ONHOLDISSUE", TypeName = "numeric(10, 0)")]
    public decimal? Onholdissue { get; set; }

    [Column("POSTEDISSUE", TypeName = "numeric(10, 0)")]
    public decimal? Postedissue { get; set; }

    [Column("ONHOLDCURRENTSTOCK", TypeName = "numeric(10, 0)")]
    public decimal? Onholdcurrentstock { get; set; }

    [Column("POSTEDCURRENTSTOCK", TypeName = "numeric(10, 0)")]
    public decimal? Postedcurrentstock { get; set; }

    [Column("ONHOLDCLOSINGSTOCK", TypeName = "numeric(10, 0)")]
    public decimal? Onholdclosingstock { get; set; }

    [Column("POSTEDCLOSINGSTOCK", TypeName = "numeric(10, 0)")]
    public decimal? Postedclosingstock { get; set; }

    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

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

    [Key]
    [Column("WAREHOUSECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Warehousecode { get; set; } = null!;
}
