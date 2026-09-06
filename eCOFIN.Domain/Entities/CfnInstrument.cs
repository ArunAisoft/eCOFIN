using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnInstrument
{
    [Column("gin_jin_no")]
    [StringLength(35)]
    [Unicode(false)]
    public string? GinJinNo { get; set; }

    [Column("bill_ctrlonholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string BillCtrlonholdno { get; set; } = null!;

    [Column("payment_ctrlonholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? PaymentCtrlonholdno { get; set; }

    [Column("INSTRUMENTNO")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("INSTRUMENTDATE", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime VchrDate { get; set; }

    [Column("payment_amount", TypeName = "numeric(14, 2)")]
    public decimal? PaymentAmount { get; set; }
}
