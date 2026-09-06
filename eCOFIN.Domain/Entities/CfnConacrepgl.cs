using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accperiod", "Accountcode")]
[Table("CFN_CONACREPGL")]
public partial class CfnConacrepgl
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("OB_DEBIT", TypeName = "numeric(14, 2)")]
    public decimal? ObDebit { get; set; }

    [Column("OB_CREDIT", TypeName = "numeric(14, 2)")]
    public decimal? ObCredit { get; set; }

    [Column("CB_DEBIT", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit { get; set; }

    [Column("CB_CREDIT", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit { get; set; }

    [Key]
    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("DEBIT_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? DebitAmnt { get; set; }

    [Column("CREDIT_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? CreditAmnt { get; set; }

    [Column("YEAR_CREDIT_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? YearCreditAmnt { get; set; }

    [Column("YEAR_DEBIT_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? YearDebitAmnt { get; set; }

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
}
