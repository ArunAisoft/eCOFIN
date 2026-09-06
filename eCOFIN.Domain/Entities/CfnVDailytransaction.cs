using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVDailytransaction
{
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Column("RECEIVEDQTY", TypeName = "numeric(10, 0)")]
    public decimal? Receivedqty { get; set; }

    [Column("issuedqty", TypeName = "numeric(10, 0)")]
    public decimal Issuedqty { get; set; }

    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("CTRL_ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlAccperiod { get; set; }

    [Column("warehousecode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Warehousecode { get; set; }

    [Column("stocktype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Stocktype { get; set; }
}
