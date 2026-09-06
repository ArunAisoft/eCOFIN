using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVCreditoroutstanding
{
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("accountdesc")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Accountdesc { get; set; }

    [Column("subcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subcode { get; set; }

    [Column("vendorname")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Vendorname { get; set; }

    [Column("onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string Onholdno { get; set; } = null!;

    [Column("sequenceno", TypeName = "numeric(16, 0)")]
    public decimal Sequenceno { get; set; }

    [Column("vchrtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Vchrtype { get; set; }

    [Column("vchrcategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Vchrcategory { get; set; }

    [Column("billno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Billno { get; set; }

    [Column("billamount", TypeName = "numeric(16, 2)")]
    public decimal? Billamount { get; set; }

    [Column("billbalance", TypeName = "numeric(16, 2)")]
    public decimal? Billbalance { get; set; }

    [Column("billadjusted", TypeName = "numeric(16, 2)")]
    public decimal? Billadjusted { get; set; }
}
