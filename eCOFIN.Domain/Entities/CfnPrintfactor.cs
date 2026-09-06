using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_PRINTFACTOR")]
public partial class CfnPrintfactor
{
    [Column("FACTOR")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Factor { get; set; }

    [Column("PER", TypeName = "numeric(16, 2)")]
    public decimal? Per { get; set; }

    [Column("AMOUNT", TypeName = "numeric(16, 2)")]
    public decimal? Amount { get; set; }

    [Column("SEQUENCE", TypeName = "numeric(16, 0)")]
    public decimal? Sequence { get; set; }
}
