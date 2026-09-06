using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class Airsale
{
    [Column("Air_Qty")]
    public int? AirQty { get; set; }

    [Column("qty")]
    public int? Qty { get; set; }

    [Column("mode")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Mode { get; set; }
}
