using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Reqonholdno", "Productid")]
[Table("CFN_POENQUIRYPRLINK")]
public partial class CfnPoenquiryprlink
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("REQONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string Reqonholdno { get; set; } = null!;

    [Key]
    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal Productid { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("PRODUCTUSERDESC")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Productuserdesc { get; set; }

    [Column("LINESTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Linestatus { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("QUANTITY", TypeName = "numeric(12, 0)")]
    public decimal? Quantity { get; set; }

    [Column("UOM")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Uom { get; set; }
}
