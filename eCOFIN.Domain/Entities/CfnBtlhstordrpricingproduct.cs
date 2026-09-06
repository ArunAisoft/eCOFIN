using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Scheduleid", "Productid", "Amendmentnumber")]
[Table("CFN_BTLHSTORDRPRICINGPRODUCT")]
public partial class CfnBtlhstordrpricingproduct
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("SCHEDULEID", TypeName = "numeric(5, 0)")]
    public decimal Scheduleid { get; set; }

    [Key]
    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal Productid { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("USERPRODUCTDESCRIPTION")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Userproductdescription { get; set; }

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

    [Key]
    [Column("AMENDMENTNUMBER")]
    [StringLength(3)]
    [Unicode(false)]
    public string Amendmentnumber { get; set; } = null!;

    [Column("CNSGASNGQTY", TypeName = "numeric(12, 0)")]
    public decimal? Cnsgasngqty { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("OACTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? OactrlOnholdno { get; set; }

    [Column("OANUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Oanumber { get; set; }

    [Column("LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Locationcode { get; set; }

    [Column("EXTRACTED")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Extracted { get; set; }

    [Column("SELECTED")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Selected { get; set; }

    [Column("QUANTITYBILLED", TypeName = "numeric(12, 0)")]
    public decimal? Quantitybilled { get; set; }
}
