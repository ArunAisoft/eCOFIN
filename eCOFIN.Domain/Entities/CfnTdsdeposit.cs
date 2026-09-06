using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accperiod", "Tdscode")]
[Table("cfn_tdsdeposit")]
public partial class CfnTdsdeposit
{
    [Key]
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Key]
    [Column("tdscode")]
    [StringLength(5)]
    [Unicode(false)]
    public string Tdscode { get; set; } = null!;

    [Column("challan_no")]
    [StringLength(30)]
    [Unicode(false)]
    public string ChallanNo { get; set; } = null!;

    [Column("challan_dt", TypeName = "datetime")]
    public DateTime ChallanDt { get; set; }

    [Column("deposited_bank")]
    [StringLength(50)]
    [Unicode(false)]
    public string DepositedBank { get; set; } = null!;
}
