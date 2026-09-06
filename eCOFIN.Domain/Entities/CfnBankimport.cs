using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_BANKIMPORT")]
public partial class CfnBankimport
{
    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string Dbcrflag { get; set; } = null!;

    [Column("AMOUNT", TypeName = "numeric(15, 2)")]
    public decimal Amount { get; set; }

    [Column("CLEARENCEDATE", TypeName = "datetime")]
    public DateTime Clearencedate { get; set; }

    [Column("INSTRUMENTNO")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }
}
