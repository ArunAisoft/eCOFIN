using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("cfn_accncalender2")]
public partial class CfnAccncalender2
{
    [Column("financialyear")]
    [StringLength(10)]
    [Unicode(false)]
    public string Financialyear { get; set; } = null!;

    [Key]
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("accmonth")]
    [StringLength(20)]
    [Unicode(false)]
    public string Accmonth { get; set; } = null!;

    [Column("accyear")]
    [StringLength(4)]
    [Unicode(false)]
    public string Accyear { get; set; } = null!;

    [Column("periodfrom", TypeName = "datetime")]
    public DateTime Periodfrom { get; set; }

    [Column("periodto", TypeName = "datetime")]
    public DateTime Periodto { get; set; }

    [Column("sequence", TypeName = "numeric(6, 0)")]
    public decimal Sequence { get; set; }

    [Column("periodstate")]
    [StringLength(5)]
    [Unicode(false)]
    public string Periodstate { get; set; } = null!;

    [Column("ctrl_trglocationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
