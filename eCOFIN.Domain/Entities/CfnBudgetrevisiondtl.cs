using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlHdrsequenceno", "CtrlSequenceno", "Accperiod")]
[Table("CFN_BUDGETREVISIONDTL")]
public partial class CfnBudgetrevisiondtl
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_HDRSEQUENCENO", TypeName = "numeric(5, 0)")]
    public decimal CtrlHdrsequenceno { get; set; }

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(5, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Key]
    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("EDFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Edflag { get; set; }

    [Column("AMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Amount { get; set; }
}
