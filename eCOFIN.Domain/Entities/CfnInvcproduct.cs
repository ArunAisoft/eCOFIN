using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Productid", "Scheduleid")]
[Table("CFN_INVCPRODUCT")]
public partial class CfnInvcproduct
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Column("PRODUCTQUANTITY", TypeName = "numeric(10, 0)")]
    public decimal Productquantity { get; set; }

    [Column("BASICRATE", TypeName = "numeric(12, 4)")]
    public decimal? Basicrate { get; set; }

    [Column("PRODUCTVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Productvalue { get; set; }

    [Column("PACKAGETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Packagetype { get; set; }

    [Column("NOOFBOXES", TypeName = "numeric(10, 0)")]
    public decimal? Noofboxes { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("SERIALNO")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Serialno { get; set; }

    [Key]
    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal Productid { get; set; }

    [Column("STOCKTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Stocktype { get; set; }

    [Key]
    [Column("SCHEDULEID", TypeName = "numeric(5, 0)")]
    public decimal Scheduleid { get; set; }

    [Column("USERPRODUCTDESCRIPTION")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Userproductdescription { get; set; }

    [Column("UOM")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Uom { get; set; }
}
