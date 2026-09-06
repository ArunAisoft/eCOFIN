using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Pricingfactorcode", "Formulaid")]
[Table("CFN_FACTORFORMULAHEADER")]
public partial class CfnFactorformulaheader
{
    [Key]
    [Column("PRICINGFACTORCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Pricingfactorcode { get; set; } = null!;

    [Key]
    [Column("FORMULAID")]
    [StringLength(5)]
    [Unicode(false)]
    public string Formulaid { get; set; } = null!;

    [Column("FORMULADESC")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Formuladesc { get; set; }

    [Column("FORMULAFLAG")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Formulaflag { get; set; }

    [Column("FORMULADESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Formuladescription { get; set; }
}
