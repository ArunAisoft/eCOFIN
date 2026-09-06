using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_QUERYTASKS")]
public partial class CfnQuerytask
{
    [Key]
    [Column("TASKFULLNAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Taskfullname { get; set; } = null!;

    [Column("SQL1")]
    [StringLength(300)]
    [Unicode(false)]
    public string? Sql1 { get; set; }

    [Column("SQL2")]
    [StringLength(300)]
    [Unicode(false)]
    public string? Sql2 { get; set; }

    [Column("SQL3")]
    [StringLength(300)]
    [Unicode(false)]
    public string? Sql3 { get; set; }

    [Column("NO_OF_SQL", TypeName = "numeric(1, 0)")]
    public decimal? NoOfSql { get; set; }
}
