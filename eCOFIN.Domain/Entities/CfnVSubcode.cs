using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVSubcode
{
    [Column("subcodenature")]
    [StringLength(8)]
    [Unicode(false)]
    public string Subcodenature { get; set; } = null!;

    [Column("subcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Subcode { get; set; } = null!;

    [Column("subcodedescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Subcodedescription { get; set; }
}
