using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_REPORTCONTROL")]
public partial class CfnReportcontrol
{
    [Key]
    [Column("TASKID")]
    [StringLength(3)]
    [Unicode(false)]
    public string Taskid { get; set; } = null!;

    [Column("REPORTTITLE")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Reporttitle { get; set; }

    [Column("REPORTOBJECT")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Reportobject { get; set; }

    [Column("EXCLUDE")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Exclude { get; set; }

    [Column("REPORTLONGDESC")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Reportlongdesc { get; set; }
}
