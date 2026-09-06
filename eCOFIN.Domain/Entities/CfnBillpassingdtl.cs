using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_BILLPASSINGDTL")]
public partial class CfnBillpassingdtl
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal? Productid { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("PRODUCTDESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Productdescription { get; set; }

    [Column("UOM")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Uom { get; set; }

    [Column("PORATE", TypeName = "numeric(14, 2)")]
    public decimal? Porate { get; set; }

    [Column("POQUANTITY", TypeName = "numeric(10, 0)")]
    public decimal? Poquantity { get; set; }

    [Column("GRNNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Grnnumber { get; set; }

    [Column("GRNQTY", TypeName = "numeric(10, 0)")]
    public decimal? Grnqty { get; set; }

    [Column("POVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Povalue { get; set; }

    [Column("BALANCEQTY", TypeName = "numeric(10, 0)")]
    public decimal? Balanceqty { get; set; }

    [Column("BILLEDQTY", TypeName = "numeric(10, 0)")]
    public decimal? Billedqty { get; set; }

    [Column("BILLEDRATE", TypeName = "numeric(14, 2)")]
    public decimal? Billedrate { get; set; }

    [Column("BILLEDVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Billedvalue { get; set; }

    [Column("VARIANCEVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Variancevalue { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("SITENAMES")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Sitenames { get; set; }

    [Column("REQONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Reqonholdno { get; set; }
}
