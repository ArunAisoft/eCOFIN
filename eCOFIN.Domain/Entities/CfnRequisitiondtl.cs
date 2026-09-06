using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Productid")]
[Table("CFN_REQUISITIONDTL")]
public partial class CfnRequisitiondtl
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

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

    [Column("PRORDEREDQTY", TypeName = "numeric(12, 0)")]
    public decimal? Prorderedqty { get; set; }

    [Column("PRGRNQTY", TypeName = "numeric(12, 0)")]
    public decimal? Prgrnqty { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("SANVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Sanvalue { get; set; }

    [Column("POVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Povalue { get; set; }

    [Column("POFACTORVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Pofactorvalue { get; set; }
}
