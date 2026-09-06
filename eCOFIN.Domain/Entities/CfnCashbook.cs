using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_CASHBOOK")]
public partial class CfnCashbook
{
    [Key]
    [Column("CASHCONTROLACCOUNT")]
    [StringLength(10)]
    [Unicode(false)]
    public string Cashcontrolaccount { get; set; } = null!;

    [Column("ONHOLDAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Onholdamount { get; set; }

    [Column("WORKINGBALANCE", TypeName = "numeric(14, 2)")]
    public decimal? Workingbalance { get; set; }

    [Column("POSTEDCLOSINGBALANCE", TypeName = "numeric(14, 2)")]
    public decimal? Postedclosingbalance { get; set; }
}
