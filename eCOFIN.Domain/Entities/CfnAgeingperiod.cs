using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_AGEINGPERIODS")]
public partial class CfnAgeingperiod
{
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("AGEINGPERIOD", TypeName = "numeric(5, 0)")]
    public decimal Ageingperiod { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("LOWERLIMIT", TypeName = "numeric(3, 0)")]
    public decimal Lowerlimit { get; set; }

    [Column("UPPERLIMIT", TypeName = "numeric(3, 0)")]
    public decimal Upperlimit { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
