using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_MRPBILLITEMDETAIL")]
public partial class CfnMrpbillitemdetail
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("LEVYFLAG")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Levyflag { get; set; }

    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal? Productid { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("PRODUCTDESCRIPTION")]
    [StringLength(240)]
    [Unicode(false)]
    public string? Productdescription { get; set; }

    [Column("UOM")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Uom { get; set; }

    [Column("PORATE", TypeName = "numeric(18, 0)")]
    public decimal? Porate { get; set; }

    [Column("RATE", TypeName = "numeric(18, 0)")]
    public decimal? Rate { get; set; }

    [Column("ACPQUANTITY", TypeName = "numeric(18, 0)")]
    public decimal? Acpquantity { get; set; }

    [Column("GRNNUMBER")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Grnnumber { get; set; }

    [Column("CUMBILLEDQTY", TypeName = "numeric(18, 0)")]
    public decimal? Cumbilledqty { get; set; }

    [Column("BALANCEQTY", TypeName = "numeric(10, 0)")]
    public decimal? Balanceqty { get; set; }

    [Column("BILLEDQTY", TypeName = "numeric(15, 5)")]
    public decimal? Billedqty { get; set; }

    [Column("BILLEDRATE", TypeName = "numeric(14, 2)")]
    public decimal? Billedrate { get; set; }

    [Column("BILLEDVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Billedvalue { get; set; }

    [Column("VARIANCEVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Variancevalue { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("PO_LINE_ID")]
    public float? PoLineId { get; set; }

    [Column("LINE_LOCATION_ID")]
    public float? LineLocationId { get; set; }

    [Column("PO_DISTRIBUTION_ID")]
    public float? PoDistributionId { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("BALANCEVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Balancevalue { get; set; }

    [Column("ATTRIBUTE1")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Attribute1 { get; set; }
}
