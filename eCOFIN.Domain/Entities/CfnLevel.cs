using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_LEVEL")]
public partial class CfnLevel
{
    [Key]
    [Column("LEVELNUMBER")]
    [StringLength(1)]
    [Unicode(false)]
    public string Levelnumber { get; set; } = null!;

    [Column("DESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Description { get; set; }
}
