using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Sourcelocation", "Batchnumber", "Applicationloginid", "Logsequence")]
[Table("CFN_APPLOGERROR")]
public partial class CfnApplogerror
{
    [Key]
    [Column("SOURCELOCATION")]
    [StringLength(5)]
    [Unicode(false)]
    public string Sourcelocation { get; set; } = null!;

    [Key]
    [Column("BATCHNUMBER")]
    [StringLength(10)]
    [Unicode(false)]
    public string Batchnumber { get; set; } = null!;

    [Key]
    [Column("APPLICATIONLOGINID", TypeName = "numeric(8, 0)")]
    public decimal Applicationloginid { get; set; }

    [Key]
    [Column("LOGSEQUENCE", TypeName = "numeric(4, 0)")]
    public decimal Logsequence { get; set; }

    [Column("TASKID")]
    [StringLength(3)]
    [Unicode(false)]
    public string Taskid { get; set; } = null!;

    [Column("LOGSQLSTATEMENT")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Logsqlstatement { get; set; } = null!;

    [Column("LOGERRORCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Logerrorcode { get; set; } = null!;

    [Column("LOGERRORTEXT")]
    [StringLength(300)]
    [Unicode(false)]
    public string Logerrortext { get; set; } = null!;
}
