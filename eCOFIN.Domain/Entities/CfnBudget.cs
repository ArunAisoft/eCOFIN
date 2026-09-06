using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_BUDGET")]
public partial class CfnBudget
{
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("BUDGETAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Budgetamount { get; set; }

    [Column("ADJUSTEDAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Adjustedamount { get; set; }

    [Column("BALANCEAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Balanceamount { get; set; }

    [Column("FINANCIALYEAR")]
    [StringLength(10)]
    [Unicode(false)]
    public string Financialyear { get; set; } = null!;

    [Column("ENCHANCEDAMT", TypeName = "numeric(14, 2)")]
    public decimal? Enchancedamt { get; set; }

    [Column("DEPLETEDAMT", TypeName = "numeric(14, 2)")]
    public decimal? Depletedamt { get; set; }

    [Column("BUDGETTOTAL", TypeName = "numeric(14, 2)")]
    public decimal? Budgettotal { get; set; }

    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("CTRL_STATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("CTRL_CANCELFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlCancelflag { get; set; }

    [Column("CTRL_LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlLocationcode { get; set; }

    [Column("CTRL_ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlAccperiod { get; set; }

    [Column("CTRL_USERNAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CtrlUsername { get; set; }

    [Column("CTRL_CREATEDON", TypeName = "datetime")]
    public DateTime? CtrlCreatedon { get; set; }

    [Column("CTRL_LASTUPDATE", TypeName = "datetime")]
    public DateTime? CtrlLastupdate { get; set; }

    [Column("CTRL_LOGEXTRACT")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextract { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("CTRL_LOGEXTRACTTYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextracttype { get; set; }

    [Column("TOTALREVISION", TypeName = "numeric(14, 2)")]
    public decimal? Totalrevision { get; set; }

    [Column("AUTOMATED")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Automated { get; set; }

    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }
}
