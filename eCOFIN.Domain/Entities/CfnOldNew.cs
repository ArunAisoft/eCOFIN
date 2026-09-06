using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_old_new")]
public partial class CfnOldNew
{
    [Column("old_cost")]
    [StringLength(5)]
    [Unicode(false)]
    public string OldCost { get; set; } = null!;

    [Column("new_cost")]
    [StringLength(5)]
    [Unicode(false)]
    public string? NewCost { get; set; }
}
