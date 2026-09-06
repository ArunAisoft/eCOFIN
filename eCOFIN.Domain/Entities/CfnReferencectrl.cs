using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_REFERENCECTRL")]
public partial class CfnReferencectrl
{
    [Key]
    [Column("REFERENCETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Referencetype { get; set; } = null!;

    [Column("REFERENCEDESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Referencedesc { get; set; }

    [Column("ONHOLDNUMBER", TypeName = "numeric(10, 0)")]
    public decimal? Onholdnumber { get; set; }

    [Column("POSTEDNUMBER", TypeName = "numeric(10, 0)")]
    public decimal? Postednumber { get; set; }

    [Column("PREFIXTYPE")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Prefixtype { get; set; }

    [Column("POSTCOLUMNNAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Postcolumnname { get; set; }
}
