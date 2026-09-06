using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVTrialbalance
{
    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("natureofaccount")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Natureofaccount { get; set; }

    [Column("closingbalance", TypeName = "numeric(38, 2)")]
    public decimal? Closingbalance { get; set; }

    [Column("sequence", TypeName = "numeric(6, 0)")]
    public decimal Sequence { get; set; }

    [Column("natureofdescription")]
    [StringLength(11)]
    [Unicode(false)]
    public string? Natureofdescription { get; set; }
}
