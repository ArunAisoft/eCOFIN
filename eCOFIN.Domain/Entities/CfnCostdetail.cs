using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

//[Keyless]
[Table("cfn_costdetail")]
public partial class CfnCostdetail
{
    [Column("ctrl_onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("ctrl_sequenceno", TypeName = "decimal(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("costcentrecode")]
    [StringLength(5)]
    [Unicode(false)]
    public string Costcentrecode { get; set; } = null!;

    [Column("voucheramount", TypeName = "decimal(14, 2)")]
    public decimal? Voucheramount { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("cost_sequenceno", TypeName = "decimal(3, 0)")]
    public decimal? CostSequenceno { get; set; }

    [Column("gl_sequenceno", TypeName = "decimal(3, 0)")]
    public decimal GlSequenceno { get; set; }

    [Column("ctrl_status")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("chk_flag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? ChkFlag { get; set; }
}
