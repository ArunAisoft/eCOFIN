using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_TR_LINK")]
public partial class CfnTrLink
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("FLCT_ACCOUNT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? FlctAccount { get; set; }

    [Column("TRNS_TYPE")]
    [StringLength(2)]
    [Unicode(false)]
    public string? TrnsType { get; set; }
}
