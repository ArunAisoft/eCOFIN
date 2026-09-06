using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVCashbook
{
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("accountdescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string Accountdescription { get; set; } = null!;

    [Column("vouchernumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Vouchernumber { get; set; }

    [Column("voucherdate", TypeName = "datetime")]
    public DateTime Voucherdate { get; set; }

    [Column("particulars")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Particulars { get; set; }

    [Column("referencenumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referencenumber { get; set; }

    [Column("referencedate", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("dbcramount", TypeName = "numeric(14, 2)")]
    public decimal Dbcramount { get; set; }

    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string Dbcrflag { get; set; } = null!;

    [Column("linenumber", TypeName = "numeric(3, 0)")]
    public decimal Linenumber { get; set; }
}
