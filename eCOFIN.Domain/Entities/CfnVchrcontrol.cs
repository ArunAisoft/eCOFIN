using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Vouchergroup", "Vouchertype", "Accperiod")]
[Table("CFN_VCHRCONTROL")]
public partial class CfnVchrcontrol
{
    [Key]
    [Column("VOUCHERGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string Vouchergroup { get; set; } = null!;

    [Key]
    [Column("VOUCHERTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Vouchertype { get; set; } = null!;

    [Key]
    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("STVOUCHERPOSTED", TypeName = "numeric(10, 0)")]
    public decimal? Stvoucherposted { get; set; }

    [Column("ENVOUCHERPOSTED", TypeName = "numeric(10, 0)")]
    public decimal? Envoucherposted { get; set; }

    [Column("STVOUCHERONHOLDSLNUM", TypeName = "numeric(10, 0)")]
    public decimal? Stvoucheronholdslnum { get; set; }

    [Column("ENVOUCHERONHOLDSLNUM", TypeName = "numeric(10, 0)")]
    public decimal? Envoucheronholdslnum { get; set; }

    [Column("CURRENTONHOLDNO", TypeName = "numeric(10, 0)")]
    public decimal? Currentonholdno { get; set; }

    [Column("CURRENTPOSTEDNO", TypeName = "numeric(10, 0)")]
    public decimal? Currentpostedno { get; set; }

    [Column("PREFIXTYPE")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Prefixtype { get; set; }

    [Column("POSTCOLUMNNAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Postcolumnname { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
