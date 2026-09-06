using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_INVREGCFG")]
public partial class CfnInvregcfg
{
    [Key]
    [Column("PRICINGFACTORCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Pricingfactorcode { get; set; } = null!;

    [Column("COLUMNNAME")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Columnname { get; set; }
}
