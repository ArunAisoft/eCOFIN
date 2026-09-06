using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVExchange
{
    [Column("vchr_type")]
    [StringLength(5)]
    [Unicode(false)]
    public string VchrType { get; set; } = null!;

    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("ctrl_onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

    [Column("voucheramount", TypeName = "numeric(14, 2)")]
    public decimal? Voucheramount { get; set; }
}
