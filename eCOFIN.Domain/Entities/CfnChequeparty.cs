using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnChequeparty
{
    [Column("subaccountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("partyname")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Partyname { get; set; }

    [Column("instrumentno")]
    public double? Instrumentno { get; set; }
}
