using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Hierarchyid", "Levelno")]
[Table("CFN_HIERARCHYLEVEL")]
public partial class CfnHierarchylevel
{
    [Key]
    [Column("HIERARCHYID")]
    [StringLength(10)]
    [Unicode(false)]
    public string Hierarchyid { get; set; } = null!;

    [Key]
    [Column("LEVELNO", TypeName = "numeric(3, 0)")]
    public decimal Levelno { get; set; }

    [Column("DEPENDENCY")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dependency { get; set; }

    [Column("REFR_OBJECT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RefrObject { get; set; }

    [Column("COLUMNNAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Columnname { get; set; }

    [Column("COLTYPE")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Coltype { get; set; }

    [Column("COLLENGTH")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Collength { get; set; }
}
