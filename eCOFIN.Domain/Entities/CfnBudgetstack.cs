using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_BUDGETSTACK")]
public partial class CfnBudgetstack
{
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

    [Column("FROMPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Fromperiod { get; set; }

    [Column("TOPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Toperiod { get; set; }

    [Column("BUDGETAMOUNT_CURR", TypeName = "numeric(14, 2)")]
    public decimal? BudgetamountCurr { get; set; }

    [Column("ALLOCATEDAMOUNT_CURR", TypeName = "numeric(14, 2)")]
    public decimal? AllocatedamountCurr { get; set; }

    [Column("ACTUALAMT_CURR", TypeName = "numeric(14, 2)")]
    public decimal? ActualamtCurr { get; set; }

    [Column("BUDGETVAR_CURR", TypeName = "numeric(14, 2)")]
    public decimal? BudgetvarCurr { get; set; }

    [Column("ALLOCVAR_CURR", TypeName = "numeric(14, 2)")]
    public decimal? AllocvarCurr { get; set; }

    [Column("BUDGETAMOUNT_CUMM", TypeName = "numeric(14, 2)")]
    public decimal? BudgetamountCumm { get; set; }

    [Column("ALLOCATEDAMOUNT_CUMM", TypeName = "numeric(14, 2)")]
    public decimal? AllocatedamountCumm { get; set; }

    [Column("ACTUALAMT_CUMM", TypeName = "numeric(14, 2)")]
    public decimal? ActualamtCumm { get; set; }

    [Column("BUDGETVAR_CUMM", TypeName = "numeric(14, 2)")]
    public decimal? BudgetvarCumm { get; set; }

    [Column("ALLOCVAR_CUMM", TypeName = "numeric(14, 2)")]
    public decimal? AllocvarCumm { get; set; }

    [Column("BUDGETAMOUNT_NXT", TypeName = "numeric(14, 2)")]
    public decimal? BudgetamountNxt { get; set; }

    [Column("ALLOCATEDAMOUNT_NXT", TypeName = "numeric(14, 2)")]
    public decimal? AllocatedamountNxt { get; set; }

    [Column("ACTUALAMT_NXT", TypeName = "numeric(14, 2)")]
    public decimal? ActualamtNxt { get; set; }
}
