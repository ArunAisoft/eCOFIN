using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_BNKDDETAIL")]
public partial class CfnBnkddetail
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("CTRL_BNKRONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlBnkronholdno { get; set; }

    [Column("CTRL_BNKRSEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal? CtrlBnkrsequenceno { get; set; }

    [Column("INSTRUMENT")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrument { get; set; }

    [Column("INSTRUMENTNUMBER")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentnumber { get; set; }

    [Column("INSTRUMENTDATE", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("AMOUNT", TypeName = "numeric(14, 2)")]
    public decimal Amount { get; set; }

    [Column("INST_CHECK")]
    [StringLength(1)]
    [Unicode(false)]
    public string? InstCheck { get; set; }

    [Column("LINEPARTICULARS")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Lineparticulars { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
