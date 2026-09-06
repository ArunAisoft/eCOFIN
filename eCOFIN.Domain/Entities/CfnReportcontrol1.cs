using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_reportcontrol1")]
public partial class CfnReportcontrol1
{
    [Column("TASKID")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Taskid { get; set; }

    [Column("REPORTTITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Reporttitle { get; set; }

    [Column("REPORTOBJECT")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Reportobject { get; set; }

    [Column("EXCLUDE")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Exclude { get; set; }

    [Column("REPORTLONGDESC")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Reportlongdesc { get; set; }
}
