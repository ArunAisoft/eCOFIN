using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Termcode", "Termcodeseq")]
[Table("CFN_TERMVALUE")]
public partial class CfnTermvalue
{
    [Key]
    [Column("TERMCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Termcode { get; set; } = null!;

    [Key]
    [Column("TERMCODESEQ", TypeName = "numeric(5, 0)")]
    public decimal Termcodeseq { get; set; }

    [Column("TERMTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Termtype { get; set; }

    [Column("TERMUSERDESC")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Termuserdesc { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
