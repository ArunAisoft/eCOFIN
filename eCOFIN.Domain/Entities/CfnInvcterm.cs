using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Termcode", "Termcodeseq")]
[Table("CFN_INVCTERM")]
public partial class CfnInvcterm
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("TERMCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Termcode { get; set; } = null!;

    [Key]
    [Column("TERMCODESEQ", TypeName = "numeric(5, 0)")]
    public decimal Termcodeseq { get; set; }

    [Column("SCHEDULEID", TypeName = "numeric(5, 0)")]
    public decimal? Scheduleid { get; set; }

    [Column("TERMUSERDESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Termuserdesc { get; set; }

    [Column("TERMVALIDVALUE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Termvalidvalue { get; set; }

    [Column("TERMTYPE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Termtype { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
