using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Pricingfactorcode", "Formulaid", "Sequence")]
[Table("CFN_FACTORFORMULA")]
public partial class CfnFactorformula
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

    [Key]
    [Column("SEQUENCE", TypeName = "numeric(5, 0)")]
    public decimal Sequence { get; set; }

    [Column("FORMULATYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Formulatype { get; set; }

    [Column("FORMULAELEMENT")]
    [StringLength(12)]
    [Unicode(false)]
    public string? Formulaelement { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("FACTORVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Factorvalue { get; set; }

    [Column("TCFACTORCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Tcfactorcode { get; set; }

    [Column("COMPONENTVALUE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Componentvalue { get; set; }
}
