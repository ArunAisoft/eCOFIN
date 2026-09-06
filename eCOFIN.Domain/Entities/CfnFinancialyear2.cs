using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("cfn_financialyear2")]
public partial class CfnFinancialyear2
{
    //[Key]
    [Column("financialyear")]
    [StringLength(10)]
    [Unicode(false)]
    public string Financialyear { get; set; } = null!;

    [Column("fromdate", TypeName = "datetime")]
    public DateTime Fromdate { get; set; }

    [Column("todate", TypeName = "datetime")]
    public DateTime Todate { get; set; }

    [Column("description")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("ctrl_onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlOnholdno { get; set; }

    [Column("ctrl_status")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("ctrl_cancelflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlCancelflag { get; set; }

    [Column("ctrl_locationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlLocationcode { get; set; }

    [Column("ctrl_accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlAccperiod { get; set; }

    [Column("ctrl_username")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CtrlUsername { get; set; }

    [Column("ctrl_createdon", TypeName = "datetime")]
    public DateTime? CtrlCreatedon { get; set; }

    [Column("ctrl_lastupdate", TypeName = "datetime")]
    public DateTime? CtrlLastupdate { get; set; }

    [Column("ctrl_logextract")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextract { get; set; }

    [Column("ctrl_trglocationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("ctrl_logextracttype")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextracttype { get; set; }

    [Column("ctrl_prevrefr")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlPrevrefr { get; set; }

    [Column("ctrl_nextrefrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlNextrefrflag { get; set; }
}
