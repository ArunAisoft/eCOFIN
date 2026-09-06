using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Productcode", "Salescustomercode", "Deliverydate")]
[Table("CFN_SALESCNSGDELIVERY")]
public partial class CfnSalescnsgdelivery
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("AMENDMENTNUMBER")]
    [StringLength(3)]
    [Unicode(false)]
    public string Amendmentnumber { get; set; } = null!;

    [Key]
    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Key]
    [Column("SALESCUSTOMERCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Salescustomercode { get; set; } = null!;

    [Column("SCHEDULEID", TypeName = "numeric(5, 0)")]
    public decimal Scheduleid { get; set; }

    [Key]
    [Column("DELIVERYDATE", TypeName = "datetime")]
    public DateTime Deliverydate { get; set; }

    [Column("QTYSCHEDULED", TypeName = "numeric(10, 0)")]
    public decimal Qtyscheduled { get; set; }

    [Column("QUANTITYDESPATCHED", TypeName = "numeric(10, 0)")]
    public decimal? Quantitydespatched { get; set; }

    [Column("DATEOFDESPATCH", TypeName = "datetime")]
    public DateTime? Dateofdespatch { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
