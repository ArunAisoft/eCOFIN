using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_POQUOTATIONDTL")]
public partial class CfnPoquotationdtl
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal? Productid { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("PRODUCTUSERDESC")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Productuserdesc { get; set; }

    [Column("QUANTITY", TypeName = "numeric(12, 0)")]
    public decimal? Quantity { get; set; }

    [Column("UOM")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Uom { get; set; }

    [Column("RATE", TypeName = "numeric(12, 4)")]
    public decimal? Rate { get; set; }

    [Column("VALUE", TypeName = "numeric(20, 4)")]
    public decimal? Value { get; set; }

    [Column("EXCHANGERATE", TypeName = "numeric(12, 4)")]
    public decimal? Exchangerate { get; set; }

    [Column("EFFECTIVEDATE", TypeName = "datetime")]
    public DateTime? Effectivedate { get; set; }

    [Column("EQUIVALENTCURRENCYVALUE", TypeName = "numeric(14, 4)")]
    public decimal? Equivalentcurrencyvalue { get; set; }

    [Column("LINESTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Linestatus { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("AMENDMENTNUMBER")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Amendmentnumber { get; set; }

    [Column("REQONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Reqonholdno { get; set; }

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }
}
