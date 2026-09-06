using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_tempcost")]
public partial class CfnTempcost
{
    [Column("n_costcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string NCostcode { get; set; } = null!;

    [Column("n_costdesc")]
    [StringLength(100)]
    [Unicode(false)]
    public string? NCostdesc { get; set; }
}
