using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_VIEW")]
public partial class CfnView
{
    [Key]
    [Column("VIEWID")]
    [StringLength(10)]
    [Unicode(false)]
    public string Viewid { get; set; } = null!;

    [Column("DESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("VIEWOBJECT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Viewobject { get; set; }

    [Column("REFR_OBJECT")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RefrObject { get; set; }
}
