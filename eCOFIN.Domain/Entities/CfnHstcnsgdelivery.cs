using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Amendmentnumber", "Productcode", "Consigneecode", "Deliverydate")]
[Table("CFN_HSTCNSGDELIVERY")]
public partial class CfnHstcnsgdelivery
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
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
    [Column("CONSIGNEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Consigneecode { get; set; } = null!;

    [Key]
    [Column("DELIVERYDATE", TypeName = "datetime")]
    public DateTime Deliverydate { get; set; }

    [Column("QTYSCHEDULED", TypeName = "numeric(10, 0)")]
    public decimal? Qtyscheduled { get; set; }

    [Column("QUANTITYDESPATCHED", TypeName = "numeric(10, 0)")]
    public decimal? Quantitydespatched { get; set; }

    [Column("DATEOFDESPATCH", TypeName = "datetime")]
    public DateTime? Dateofdespatch { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
