using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Scheduleid")]
[Table("CFN_ENQPRICINGHD")]
public partial class CfnEnqpricinghd
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("SCHEDULEID", TypeName = "numeric(5, 0)")]
    public decimal Scheduleid { get; set; }

    [Column("SCHEDULETYPE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Scheduletype { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("TERMAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Termappl { get; set; }

    [Column("SELECTIND")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Selectind { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("PRODAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Prodappl { get; set; }
}
