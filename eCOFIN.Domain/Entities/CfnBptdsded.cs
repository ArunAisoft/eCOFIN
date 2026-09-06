using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_BPTDSDED")]
public partial class CfnBptdsded
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("INSTRUMENTNO")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("INSTRUMENTDATE", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("PAYMENTAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Paymentamount { get; set; }

    [Column("PAYMENTAMOUNTADJUSTED", TypeName = "numeric(14, 2)")]
    public decimal? Paymentamountadjusted { get; set; }

    [Column("PAYMENTAMOUNTBALANCE", TypeName = "numeric(14, 2)")]
    public decimal? Paymentamountbalance { get; set; }

    [Column("ACCEPTAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Acceptamount { get; set; }

    [Column("TDSDEDAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Tdsdedamount { get; set; }

    [Column("TDSAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Tdsamount { get; set; }

    [Column("TDSCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Tdscode { get; set; }

    [Column("LINEPARTICULARS")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Lineparticulars { get; set; }

    [Column("REFERENCENUMBERS")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referencenumbers { get; set; }

    [Column("REFERENCEDATE", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("SUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }
}
