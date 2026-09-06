using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Scheduleid", "Amendmentnumber")]
[Table("CFN_BTLHSTORDRPRICINGHD")]
public partial class CfnBtlhstordrpricinghd
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("SCHEDULEID", TypeName = "numeric(5, 0)")]
    public decimal Scheduleid { get; set; }

    [Column("PRODAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Prodappl { get; set; }

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

    [Column("CONSIGNEEAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Consigneeappl { get; set; }

    [Key]
    [Column("AMENDMENTNUMBER")]
    [StringLength(3)]
    [Unicode(false)]
    public string Amendmentnumber { get; set; } = null!;

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
