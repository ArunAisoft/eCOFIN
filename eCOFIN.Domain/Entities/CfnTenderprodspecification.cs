using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Scheduleid", "Productid", "Specificationcode")]
[Table("CFN_TENDERPRODSPECIFICATION")]
public partial class CfnTenderprodspecification
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("SCHEDULEID", TypeName = "numeric(5, 0)")]
    public decimal Scheduleid { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Key]
    [Column("SPECIFICATIONCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Specificationcode { get; set; } = null!;

    [Column("USERSPECIFICATIONDESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Userspecificationdesc { get; set; }

    [Column("UOM")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Uom { get; set; }

    [Column("QTY", TypeName = "numeric(12, 0)")]
    public decimal? Qty { get; set; }

    [Column("TOTALQTY", TypeName = "numeric(12, 4)")]
    public decimal? Totalqty { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Key]
    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal Productid { get; set; }
}
