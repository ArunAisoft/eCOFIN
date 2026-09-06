using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_accountottolink")]
public partial class CfnAccountottolink
{
    [Column("prod_prefix")]
    [StringLength(15)]
    [Unicode(false)]
    public string ProdPrefix { get; set; } = null!;

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("vchr_type")]
    [StringLength(1)]
    [Unicode(false)]
    public string VchrType { get; set; } = null!;

    [Column("prod_prefixtype")]
    [StringLength(1)]
    [Unicode(false)]
    public string ProdPrefixtype { get; set; } = null!;

    [Column("prod_levytype")]
    [StringLength(1)]
    [Unicode(false)]
    public string? ProdLevytype { get; set; }

    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

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

    [Column("objectstatus")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Objectstatus { get; set; }
}
