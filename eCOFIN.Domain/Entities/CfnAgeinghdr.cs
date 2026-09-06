using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_AGEINGHDR")]
public partial class CfnAgeinghdr
{
    [Column("REPORTTITLE")]
    [StringLength(150)]
    [Unicode(false)]
    public string Reporttitle { get; set; } = null!;

    [Column("PERIODFROM", TypeName = "datetime")]
    public DateTime Periodfrom { get; set; }

    [Column("PERIODTO", TypeName = "datetime")]
    public DateTime Periodto { get; set; }

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

    [Column("DESCRIPTION1")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Description1 { get; set; }

    [Column("LOWERLIMIT1", TypeName = "numeric(5, 0)")]
    public decimal? Lowerlimit1 { get; set; }

    [Column("UPPERLIMIT1", TypeName = "numeric(5, 0)")]
    public decimal? Upperlimit1 { get; set; }

    [Column("DESCRIPTION2")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Description2 { get; set; }

    [Column("LOWERLIMIT2", TypeName = "numeric(5, 0)")]
    public decimal? Lowerlimit2 { get; set; }

    [Column("UPPERLIMIT2", TypeName = "numeric(5, 0)")]
    public decimal? Upperlimit2 { get; set; }
}
