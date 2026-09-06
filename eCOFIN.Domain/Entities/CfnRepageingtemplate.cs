using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_REPAGEINGTEMPLATE")]
public partial class CfnRepageingtemplate
{
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlOnholdno { get; set; }

    [Column("CTRL_SEQUENCENO", TypeName = "numeric(5, 0)")]
    public decimal? CtrlSequenceno { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("SUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("BILLNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Billno { get; set; }

    [Column("BILLDATE", TypeName = "datetime")]
    public DateTime? Billdate { get; set; }

    [Column("BILLDUEDATE", TypeName = "datetime")]
    public DateTime? Billduedate { get; set; }

    [Column("BILLAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Billamount { get; set; }

    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

    [Column("BILLBALANCE", TypeName = "numeric(14, 2)")]
    public decimal? Billbalance { get; set; }

    [Column("AMOUNT1", TypeName = "numeric(14, 2)")]
    public decimal? Amount1 { get; set; }

    [Column("AMOUNT2", TypeName = "numeric(14, 2)")]
    public decimal? Amount2 { get; set; }

    [Column("AMOUNT3", TypeName = "numeric(14, 2)")]
    public decimal? Amount3 { get; set; }

    [Column("AMOUNT4", TypeName = "numeric(14, 2)")]
    public decimal? Amount4 { get; set; }

    [Column("AMOUNT5", TypeName = "numeric(14, 2)")]
    public decimal? Amount5 { get; set; }

    [Column("AMOUNT6", TypeName = "numeric(14, 2)")]
    public decimal? Amount6 { get; set; }

    [Column("AMOUNT7", TypeName = "numeric(14, 2)")]
    public decimal? Amount7 { get; set; }

    [Column("AMOUNT8", TypeName = "numeric(14, 2)")]
    public decimal? Amount8 { get; set; }

    [Column("AMOUNT9", TypeName = "numeric(14, 2)")]
    public decimal? Amount9 { get; set; }

    [Column("AMOUNT10", TypeName = "numeric(14, 2)")]
    public decimal? Amount10 { get; set; }

    [Column("AMOUNT11", TypeName = "numeric(14, 2)")]
    public decimal? Amount11 { get; set; }

    [Column("AMOUNT12", TypeName = "numeric(14, 2)")]
    public decimal? Amount12 { get; set; }

    [Key]
    [Column("SRRECNO", TypeName = "numeric(5, 0)")]
    public decimal Srrecno { get; set; }
}
