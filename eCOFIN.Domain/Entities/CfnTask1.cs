using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_task1")]
public partial class CfnTask1
{
    [Column("taskid")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Taskid { get; set; }

    [Column("taskshortname")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Taskshortname { get; set; }

    [Column("taskfullname")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Taskfullname { get; set; }

    [Column("taskrefrid", TypeName = "decimal(16, 0)")]
    public decimal? Taskrefrid { get; set; }

    [Column("taskinterfacerefr")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Taskinterfacerefr { get; set; }

    [Column("tasktype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Tasktype { get; set; }

    [Column("taskorrefr")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Taskorrefr { get; set; }

    [Column("logflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Logflag { get; set; }

    [Column("taskpicture")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Taskpicture { get; set; }

    [Column("taskheader")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taskheader { get; set; }

    [Column("taskprimarykey")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taskprimarykey { get; set; }

    [Column("parentref", TypeName = "decimal(16, 0)")]
    public decimal? Parentref { get; set; }

    [Column("nextlevel")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Nextlevel { get; set; }

    [Column("taskdescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taskdescription { get; set; }

    [Column("objectstatus")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Objectstatus { get; set; }

    [Column("vouchergroup")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Vouchergroup { get; set; }
}
