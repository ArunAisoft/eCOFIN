using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_BUDGETBREAKUP")]
public partial class CfnBudgetbreakup
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(10, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("FINANCIALYEAR")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Financialyear { get; set; }

    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("BUDGETAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Budgetamount { get; set; }

    [Column("ALLOCATEDAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Allocatedamount { get; set; }

    [Column("ENCHANCEDAMT", TypeName = "numeric(14, 2)")]
    public decimal? Enchancedamt { get; set; }

    [Column("REQONHOLDBUDGET", TypeName = "numeric(14, 2)")]
    public decimal? Reqonholdbudget { get; set; }

    [Column("REQPOSTEDBUDGET", TypeName = "numeric(14, 2)")]
    public decimal? Reqpostedbudget { get; set; }

    [Column("POONHOLDBUDGET", TypeName = "numeric(14, 2)")]
    public decimal? Poonholdbudget { get; set; }

    [Column("POPOSTEDBUDGET", TypeName = "numeric(14, 2)")]
    public decimal? Popostedbudget { get; set; }

    [Column("DEPLETEDAMT", TypeName = "numeric(14, 2)")]
    public decimal? Depletedamt { get; set; }

    [Column("BUDGETTOTAL", TypeName = "numeric(14, 2)")]
    public decimal? Budgettotal { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("TOTALREVISION", TypeName = "numeric(14, 2)")]
    public decimal? Totalrevision { get; set; }

    [Column("CTRL_STATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("BUDGETACTUALS", TypeName = "numeric(14, 2)")]
    public decimal? Budgetactuals { get; set; }

    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }
}
