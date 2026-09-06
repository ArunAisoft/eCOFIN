using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_INVCPRNCFG")]
public partial class CfnInvcprncfg
{
    [Key]
    [Column("PRICINGFACTORCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Pricingfactorcode { get; set; } = null!;

    [Column("FORMULATEXT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Formulatext { get; set; }

    [Column("SEQUENCE", TypeName = "numeric(2, 0)")]
    public decimal? Sequence { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("AMOUNT", TypeName = "numeric(16, 4)")]
    public decimal? Amount { get; set; }
}
