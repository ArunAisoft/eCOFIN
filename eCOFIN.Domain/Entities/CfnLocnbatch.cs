using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Locationcode", "Accperiod", "Batchserialno")]
[Table("CFN_LOCNBATCH")]
public partial class CfnLocnbatch
{
    [Key]
    [Column("LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Locationcode { get; set; } = null!;

    [Key]
    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Key]
    [Column("BATCHSERIALNO", TypeName = "numeric(10, 0)")]
    public decimal Batchserialno { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
