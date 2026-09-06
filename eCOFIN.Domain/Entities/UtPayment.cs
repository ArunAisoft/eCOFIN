using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("UT_Payments")]
public partial class UtPayment
{
    [Column("VCHR_NUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime VchrDate { get; set; }

    [Column("vchr_refnumber")]
    [StringLength(35)]
    [Unicode(false)]
    public string? VchrRefnumber { get; set; }

    [Column("INSTRUMENTNO")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("INSTRUMENTDATE", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("VOUCHERAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Voucheramount { get; set; }

    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }
}
