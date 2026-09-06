using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_FYREFCONTROL")]
public partial class CfnFyrefcontrol
{
    //[Key]
    [Column("FINANCIALYEAR")]
    [StringLength(10)]
    [Unicode(false)]
    public string Financialyear { get; set; } = null!;

    [Column("CTRL_STARTNO1", TypeName = "numeric(6, 0)")]
    public decimal? CtrlStartno1 { get; set; }

    [Column("CTRL_ENDNO1", TypeName = "numeric(6, 0)")]
    public decimal? CtrlEndno1 { get; set; }

    [Column("CTRL_STARTNO2", TypeName = "numeric(6, 0)")]
    public decimal? CtrlStartno2 { get; set; }

    [Column("CTRL_ENDNO2", TypeName = "numeric(6, 0)")]
    public decimal? CtrlEndno2 { get; set; }

    [Column("CTRL_STARTNO3", TypeName = "numeric(6, 0)")]
    public decimal? CtrlStartno3 { get; set; }

    [Column("CTRL_ENDNO3", TypeName = "numeric(6, 0)")]
    public decimal? CtrlEndno3 { get; set; }

    [Column("CTRL_STARTNO4", TypeName = "numeric(6, 0)")]
    public decimal? CtrlStartno4 { get; set; }

    [Column("CTRL_ENDNO4", TypeName = "numeric(6, 0)")]
    public decimal? CtrlEndno4 { get; set; }

    [Column("CTRL_STARTNO5", TypeName = "numeric(6, 0)")]
    public decimal? CtrlStartno5 { get; set; }

    [Column("CTRL_ENDNO5", TypeName = "numeric(6, 0)")]
    public decimal? CtrlEndno5 { get; set; }
}
