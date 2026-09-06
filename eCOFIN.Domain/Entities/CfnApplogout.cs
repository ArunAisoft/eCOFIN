using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Sourcelocation", "Applicationloginid", "Logsequence", "Logtype", "Targetlocation")]
[Table("CFN_APPLOGOUT")]
public partial class CfnApplogout
{
    [Key]
    [Column("SOURCELOCATION")]
    [StringLength(5)]
    [Unicode(false)]
    public string Sourcelocation { get; set; } = null!;

    [Key]
    [Column("APPLICATIONLOGINID", TypeName = "numeric(8, 0)")]
    public decimal Applicationloginid { get; set; }

    [Key]
    [Column("LOGSEQUENCE", TypeName = "numeric(4, 0)")]
    public decimal Logsequence { get; set; }

    [Column("TRANSACTIONDATE", TypeName = "datetime")]
    public DateTime Transactiondate { get; set; }

    [Column("TASKID")]
    [StringLength(3)]
    [Unicode(false)]
    public string Taskid { get; set; } = null!;

    [Column("LOGSQLSTATEMENT")]
    [StringLength(2000)]
    [Unicode(false)]
    public string Logsqlstatement { get; set; } = null!;

    [Key]
    [Column("LOGTYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string Logtype { get; set; } = null!;

    [Column("BATCHNUMBER")]
    [StringLength(10)]
    [Unicode(false)]
    public string Batchnumber { get; set; } = null!;

    [Column("LOGEXECUTIONFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string Logexecutionflag { get; set; } = null!;

    [Column("USERNAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Key]
    [Column("TARGETLOCATION")]
    [StringLength(5)]
    [Unicode(false)]
    public string Targetlocation { get; set; } = null!;
}
