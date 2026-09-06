using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Locationname", "Accperiod", "Accountcode")]
[Table("CFN_CONSTB")]
public partial class CfnConstb
{
    [Key]
    [Column("LOCATIONNAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string Locationname { get; set; } = null!;

    [Key]
    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("DEBIT", TypeName = "numeric(14, 2)")]
    public decimal? Debit { get; set; }

    [Column("CREDIT", TypeName = "numeric(14, 2)")]
    public decimal? Credit { get; set; }
}
