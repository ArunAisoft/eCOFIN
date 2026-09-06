using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("GLSUBLEDGER")]
public partial class Glsubledger
{
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("DBCRAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Dbcramount { get; set; }
}
