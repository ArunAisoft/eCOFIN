using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accperiod", "Groupcode", "Schdcode", "Accountcode")]
[Table("cfn_placcount_old")]
public partial class CfnPlaccountOld
{
    [Key]
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Key]
    [Column("groupcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Groupcode { get; set; } = null!;

    [Key]
    [Column("schdcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Schdcode { get; set; } = null!;

    [Key]
    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("amount", TypeName = "numeric(14, 2)")]
    public decimal? Amount { get; set; }
}
