using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_PURCHASEDETAIL")]
public partial class CfnPurchasedetail
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("POREQONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Poreqonholdno { get; set; }

    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal? Productid { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("PRODUCTDESCRIPTION")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Productdescription { get; set; }

    [Column("RATE", TypeName = "numeric(14, 2)")]
    public decimal? Rate { get; set; }

    [Column("QUANTITY", TypeName = "numeric(10, 0)")]
    public decimal? Quantity { get; set; }

    [Column("POVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Povalue { get; set; }

    [Column("EXCHANGERATE", TypeName = "numeric(14, 2)")]
    public decimal? Exchangerate { get; set; }

    [Column("EFFECTIVEDATE", TypeName = "datetime")]
    public DateTime? Effectivedate { get; set; }

    [Column("EQUIVALENTRATE", TypeName = "numeric(14, 2)")]
    public decimal? Equivalentrate { get; set; }

    [Column("LINESTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Linestatus { get; set; }

    [Column("BILLEDQUANTITY", TypeName = "numeric(5, 0)")]
    public decimal? Billedquantity { get; set; }

    [Column("BILLEDVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Billedvalue { get; set; }

    [Column("POBILLINGSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Pobillingstatus { get; set; }

    [Column("GRNQTY", TypeName = "numeric(14, 2)")]
    public decimal? Grnqty { get; set; }

    [Column("PRQTY", TypeName = "numeric(14, 2)")]
    public decimal? Prqty { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("AMENDMENTNUMBER")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Amendmentnumber { get; set; }

    [Column("PVVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Pvvalue { get; set; }

    [Column("NPVVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Npvvalue { get; set; }

    [Column("TOTALVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Totalvalue { get; set; }

    [Column("GRNVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Grnvalue { get; set; }

    [Column("SANVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Sanvalue { get; set; }

    [Column("DELIVERYDATE", TypeName = "datetime")]
    public DateTime? Deliverydate { get; set; }
}
