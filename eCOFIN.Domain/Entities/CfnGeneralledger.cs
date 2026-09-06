using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accperiod", "Accountcode")]
[Table("cfn_generalledger")]
public partial class CfnGeneralledger
{
    [Key]
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Key]
    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("onholdobdbcr")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Onholdobdbcr { get; set; }

    [Column("onholdopeningbalance", TypeName = "decimal(16, 2)")]
    public decimal? Onholdopeningbalance { get; set; }

    [Column("postedobdbcr")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Postedobdbcr { get; set; }

    [Column("postedopeningbalance", TypeName = "decimal(16, 2)")]
    public decimal? Postedopeningbalance { get; set; }

    [Column("onholdcbdbcr")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Onholdcbdbcr { get; set; }

    [Column("onholdclosingbalance", TypeName = "decimal(16, 2)")]
    public decimal? Onholdclosingbalance { get; set; }

    [Column("postedcbdbcr")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Postedcbdbcr { get; set; }

    [Column("postedclosingbalance", TypeName = "decimal(16, 2)")]
    public decimal? Postedclosingbalance { get; set; }

    [Column("onholddebitamount", TypeName = "decimal(16, 2)")]
    public decimal? Onholddebitamount { get; set; }

    [Column("posteddebitamount", TypeName = "decimal(16, 2)")]
    public decimal? Posteddebitamount { get; set; }

    [Column("onholdcreditamount", TypeName = "decimal(16, 2)")]
    public decimal? Onholdcreditamount { get; set; }

    [Column("postedcreditamount", TypeName = "decimal(16, 2)")]
    public decimal? Postedcreditamount { get; set; }
}
