using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_ACCOUNT")]
public partial class CfnAccount
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("DESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("NATUREOFACCOUNT")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Natureofaccount { get; set; }

    [Column("CREATEDON", TypeName = "datetime")]
    public DateTime Createdon { get; set; }

    [Column("ACCOUNTSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string Accountstatus { get; set; } = null!;

    [Column("CONTROLACCOUNT")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Controlaccount { get; set; }

    [Column("SUBACCOUNT")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Subaccount { get; set; }

    [Column("BANKCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Bankcode { get; set; }

    [Column("ACCOUNTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Accounttype { get; set; } = null!;

    [Column("BILLWISEAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Billwiseappl { get; set; }

    [Column("COSTAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Costappl { get; set; }

    [Column("STOCKAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Stockappl { get; set; }

    [Column("BUDGETAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Budgetappl { get; set; }

    [Column("SUBLEDGERAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Subledgerappl { get; set; }

    [Column("EMPLOYEEAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Employeeappl { get; set; }

    [Column("PRODUCTAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Productappl { get; set; }

    [Column("EXPENSEAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Expenseappl { get; set; }

    [Column("COSTTYPEAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Costtypeappl { get; set; }

    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlOnholdno { get; set; }

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

    [Column("CTRL_PREVREFR")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlPrevrefr { get; set; }

    [Column("CTRL_NEXTREFRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlNextrefrflag { get; set; }

    [Column("BUDGETTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Budgettype { get; set; }
}
