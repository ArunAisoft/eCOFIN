using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_BUDGETREVISION")]
public partial class CfnBudgetrevision
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(5, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("REVISIONDATE", TypeName = "datetime")]
    public DateTime Revisiondate { get; set; }

    [Column("REVISIONAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Revisionamount { get; set; }

    [Column("FROMACCPERIOD")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Fromaccperiod { get; set; }

    [Column("TOACCPERIOD")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Toaccperiod { get; set; }

    [Column("NARRATION")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Narration { get; set; }

    [Column("DEPLETEDENCHAN")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Depletedenchan { get; set; }
}
