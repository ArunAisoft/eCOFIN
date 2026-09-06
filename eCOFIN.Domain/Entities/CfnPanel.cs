using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Taskid", "Panelid")]
[Table("CFN_PANEL")]
public partial class CfnPanel
{
    [Key]
    [Column("TASKID")]
    [StringLength(3)]
    [Unicode(false)]
    public string Taskid { get; set; } = null!;

    [Key]
    [Column("PANELID")]
    [StringLength(20)]
    [Unicode(false)]
    public string Panelid { get; set; } = null!;

    [Column("PANELFULLNAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Panelfullname { get; set; }

    [Column("PANELREFRID", TypeName = "numeric(3, 0)")]
    public decimal? Panelrefrid { get; set; }

    [Column("PANELINTERFACEREFR")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Panelinterfacerefr { get; set; }

    [Column("PANELTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Paneltype { get; set; }

    [Column("PANELORREFR")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Panelorrefr { get; set; }

    [Column("LOGFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Logflag { get; set; }

    [Column("PANELPICTURE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Panelpicture { get; set; }

    [Column("PANELHEADER")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Panelheader { get; set; }

    [Column("PANELPRIMARYKEY")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Panelprimarykey { get; set; }

    [Column("PARENTREF", TypeName = "numeric(3, 0)")]
    public decimal? Parentref { get; set; }

    [Column("NEXTLEVEL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Nextlevel { get; set; }

    [Column("PANELDESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Paneldescription { get; set; }
}
