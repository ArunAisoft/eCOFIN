using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("Advance_Table")]
public partial class AdvanceTable
{
    [Column("Adv_No")]
    [StringLength(10)]
    [Unicode(false)]
    public string? AdvNo { get; set; }
}
