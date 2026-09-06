using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_TDSACCOUNTSLNO")]
public partial class CfnTdsaccountslno
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("TDSCERTNO", TypeName = "numeric(10, 0)")]
    public decimal? Tdscertno { get; set; }

    [Column("TDSPREFIX")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Tdsprefix { get; set; }
}
