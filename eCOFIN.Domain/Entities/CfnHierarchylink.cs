using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Hierarchyid", "Viewid")]
[Table("CFN_HIERARCHYLINK")]
public partial class CfnHierarchylink
{
    [Key]
    [Column("HIERARCHYID")]
    [StringLength(10)]
    [Unicode(false)]
    public string Hierarchyid { get; set; } = null!;

    [Key]
    [Column("VIEWID")]
    [StringLength(10)]
    [Unicode(false)]
    public string Viewid { get; set; } = null!;
}
