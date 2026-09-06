using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_consolidated")]
public partial class CfnConsolidated
{
    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("subaccountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("postedcreditamount_u1", TypeName = "numeric(18, 2)")]
    public decimal? PostedcreditamountU1 { get; set; }

    [Column("posteddebitamount_u1", TypeName = "numeric(18, 2)")]
    public decimal? PosteddebitamountU1 { get; set; }

    [Column("postedcreditamount_u2", TypeName = "numeric(18, 2)")]
    public decimal? PostedcreditamountU2 { get; set; }

    [Column("posteddebitamount_u2", TypeName = "numeric(18, 2)")]
    public decimal? PosteddebitamountU2 { get; set; }

    [Column("cons_debitamount", TypeName = "numeric(18, 2)")]
    public decimal? ConsDebitamount { get; set; }

    [Column("cons_creditamount", TypeName = "numeric(18, 2)")]
    public decimal? ConsCreditamount { get; set; }
}
