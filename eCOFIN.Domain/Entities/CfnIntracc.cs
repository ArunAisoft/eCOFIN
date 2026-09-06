using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_INTRACC")]
public partial class CfnIntracc
{
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("ACTIVEFROMDT", TypeName = "datetime")]
    public DateTime? Activefromdt { get; set; }

    [Column("ACTIVETODT", TypeName = "datetime")]
    public DateTime? Activetodt { get; set; }

    [Column("OBJECTSTATE", TypeName = "numeric(5, 0)")]
    public decimal? Objectstate { get; set; }

    [Column("CMANUPDFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Cmanupdflag { get; set; }

    [Column("FAMUPDFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Famupdflag { get; set; }

    [Column("LCUPDFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Lcupdflag { get; set; }

    [Column("PAYUPDFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Payupdflag { get; set; }
}
