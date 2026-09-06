using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Amendmentnumber", "Productcode", "Consigneecode")]
[Table("CFN_PRODCONSIGNEE")]
public partial class CfnProdconsignee
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

    [Column("QUANTITYORDERED", TypeName = "numeric(10, 0)")]
    public decimal Quantityordered { get; set; }

    [Column("QUANTITYBILLED", TypeName = "numeric(10, 0)")]
    public decimal? Quantitybilled { get; set; }

    [Column("PRIORITY", TypeName = "numeric(2, 0)")]
    public decimal? Priority { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
