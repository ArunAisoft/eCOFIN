using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_TESTTASK")]
public partial class CfnTesttask
{
    [Column("TASKID")]
    [StringLength(3)]
    [Unicode(false)]
    public string Taskid { get; set; } = null!;

    [Column("TASKSHORTNAME")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Taskshortname { get; set; }

    [Column("TASKFULLNAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Taskfullname { get; set; }

    [Column("TASKREFRID", TypeName = "numeric(3, 0)")]
    public decimal? Taskrefrid { get; set; }

    [Column("TASKINTERFACEREFR")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Taskinterfacerefr { get; set; }

    [Column("TASKTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Tasktype { get; set; }

    [Column("TASKORREFR")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Taskorrefr { get; set; }

    [Column("LOGFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Logflag { get; set; }

    [Column("TASKPICTURE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Taskpicture { get; set; }

    [Column("TASKHEADER")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taskheader { get; set; }

    [Column("TASKPRIMARYKEY")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taskprimarykey { get; set; }

    [Column("PARENTREF", TypeName = "numeric(3, 0)")]
    public decimal? Parentref { get; set; }

    [Column("NEXTLEVEL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Nextlevel { get; set; }

    [Column("TASKDESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taskdescription { get; set; }

    [Column("OBJECTSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Objectstatus { get; set; }

    [Column("VOUCHERGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Vouchergroup { get; set; }
}
