using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_ACCNCALENDER")]
public partial class CfnAccncalender
{
    [Column("FINANCIALYEAR")]
    [StringLength(10)]
    [Unicode(false)]
    public string Financialyear { get; set; } = null!;

    [Key]
    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("ACCMONTH")]
    [StringLength(20)]
    [Unicode(false)]
    public string Accmonth { get; set; } = null!;

    [Column("ACCYEAR")]
    [StringLength(4)]
    [Unicode(false)]
    public string Accyear { get; set; } = null!;

    [Column("PERIODFROM", TypeName = "datetime")]
    public DateTime Periodfrom { get; set; }

    [Column("PERIODTO", TypeName = "datetime")]
    public DateTime Periodto { get; set; }

    [Column("SEQUENCE", TypeName = "numeric(6, 0)")]
    public decimal Sequence { get; set; }

    [Column("PERIODSTATE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Periodstate { get; set; } = null!;

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
