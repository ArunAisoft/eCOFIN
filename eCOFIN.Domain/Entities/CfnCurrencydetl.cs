using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Currencycode", "Effectivefrom", "Effectiveto")]
[Table("cfn_currencydetl")]
public partial class CfnCurrencydetl
{
    [Key]
    [Column("currencycode")]
    [StringLength(5)]
    [Unicode(false)]
    public string Currencycode { get; set; } = null!;

    [Key]
    [Column("effectivefrom", TypeName = "datetime")]
    public DateTime Effectivefrom { get; set; }

    [Key]
    [Column("effectiveto", TypeName = "datetime")]
    public DateTime Effectiveto { get; set; }

    [Column("exchangerate", TypeName = "numeric(14, 2)")]
    public decimal? Exchangerate { get; set; }

    [Column("bs_rate", TypeName = "numeric(14, 2)")]
    public decimal? BsRate { get; set; }

    [Column("ctrl_trglocationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("periodstate")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Periodstate { get; set; }

    [Column("sequence", TypeName = "numeric(5, 0)")]
    public decimal? Sequence { get; set; }
}
