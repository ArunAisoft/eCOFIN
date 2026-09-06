using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("Acc_Life")]
public partial class AccLife
{
    [Column("Life_Date")]
    [StringLength(25)]
    [Unicode(false)]
    public string? LifeDate { get; set; }

    [Column("CC_No")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CcNo { get; set; }

    [Column("Act_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ActNo { get; set; }

    [Column("HW")]
    public double? Hw { get; set; }

    public double? Val { get; set; }

    [Column("RPH")]
    public double? Rph { get; set; }
}
