using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVTransaction
{
    [Column("warehouse")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Warehouse { get; set; }

    [Column("voucherdate", TypeName = "datetime")]
    public DateTime? Voucherdate { get; set; }

    [Column("systemcategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Systemcategory { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Column("stocktype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Stocktype { get; set; }

    [Column("vouchernumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Vouchernumber { get; set; }

    [Column("imeino")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Imeino { get; set; }

    [Column("receivedqty")]
    [StringLength(1)]
    [Unicode(false)]
    public string Receivedqty { get; set; } = null!;

    [Column("issueqty")]
    [StringLength(1)]
    [Unicode(false)]
    public string Issueqty { get; set; } = null!;

    [Column("ctrl_accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlAccperiod { get; set; }
}
