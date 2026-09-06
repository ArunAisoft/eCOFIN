using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_HIERARCHY")]
public partial class CfnHierarchy
{
    [Key]
    [Column("HIERARCHYID")]
    [StringLength(10)]
    [Unicode(false)]
    public string Hierarchyid { get; set; } = null!;

    [Column("DESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("LEAFLEVELNO", TypeName = "numeric(3, 0)")]
    public decimal? Leaflevelno { get; set; }

    [Column("REFR_OBJECT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RefrObject { get; set; }

    [Column("TABLENAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Tablename { get; set; }

    [Column("CREATED")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Created { get; set; }
}
