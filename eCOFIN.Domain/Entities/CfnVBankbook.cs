using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVBankbook
{
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("bankcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Bankcode { get; set; } = null!;

    [Column("bankname")]
    [StringLength(100)]
    [Unicode(false)]
    public string Bankname { get; set; } = null!;

    [Column("voucherdate", TypeName = "datetime")]
    public DateTime Voucherdate { get; set; }

    [Column("vouchernumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Vouchernumber { get; set; }

    [Column("linedetails")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Linedetails { get; set; }

    [Column("instrumentnumber")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentnumber { get; set; }

    [Column("instrumentdate", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("referencenumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referencenumber { get; set; }

    [Column("referencedate", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("dbcrindication")]
    [StringLength(1)]
    [Unicode(false)]
    public string Dbcrindication { get; set; } = null!;

    [Column("drcramount", TypeName = "numeric(14, 2)")]
    public decimal Drcramount { get; set; }
}
