using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_AGEINGCFG")]
public partial class CfnAgeingcfg
{
    [Key]
    [Column("SLNO", TypeName = "numeric(18, 0)")]
    public decimal Slno { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("LOWERLIMIT", TypeName = "numeric(5, 0)")]
    public decimal? Lowerlimit { get; set; }

    [Column("UPPERLIMIT", TypeName = "numeric(5, 0)")]
    public decimal? Upperlimit { get; set; }
}
