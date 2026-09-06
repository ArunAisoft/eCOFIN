using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Scheduleid", "Productid", "Salescustomercode")]
[Table("CFN_SALESPRODCONSIGNEE")]
public partial class CfnSalesprodconsignee
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
    public string Productcode { get; set; } = null!;

    [Key]
    [Column("SALESCUSTOMERCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Salescustomercode { get; set; } = null!;

    [Column("QUANTITYORDERED", TypeName = "numeric(10, 0)")]
    public decimal Quantityordered { get; set; }

    [Column("QUANTITYBILLED", TypeName = "numeric(10, 0)")]
    public decimal? Quantitybilled { get; set; }

    [Column("PRIORITY", TypeName = "numeric(5, 0)")]
    public decimal? Priority { get; set; }

    [Column("AMENDMENTNUMBER")]
    [StringLength(3)]
    [Unicode(false)]
    public string Amendmentnumber { get; set; } = null!;

    [Column("BALANCEQTY", TypeName = "numeric(10, 0)")]
    public decimal? Balanceqty { get; set; }

    [Column("DUMMYQTY1", TypeName = "numeric(10, 0)")]
    public decimal? Dummyqty1 { get; set; }

    [Column("DUMMYQTY2", TypeName = "numeric(10, 0)")]
    public decimal? Dummyqty2 { get; set; }

    [Column("DUMMYQTY3", TypeName = "numeric(10, 0)")]
    public decimal? Dummyqty3 { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("PRODUCTDESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Productdescription { get; set; }
}
