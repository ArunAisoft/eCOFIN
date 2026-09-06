using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVTdsregister
{
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("vchr_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("vchr_date", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("billno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Billno { get; set; }

    [Column("billdate", TypeName = "datetime")]
    public DateTime? Billdate { get; set; }

    [Column("vchr_type")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrType { get; set; }

    [Column("ctrl_status")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("subaccountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("billamount", TypeName = "numeric(16, 2)")]
    public decimal? Billamount { get; set; }

    [Column("tdsdedamount", TypeName = "numeric(16, 2)")]
    public decimal? Tdsdedamount { get; set; }

    [Column("tdsamount", TypeName = "numeric(16, 2)")]
    public decimal? Tdsamount { get; set; }

    [Column("ctrl_sequenceno", TypeName = "numeric(16, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("vendorname")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Vendorname { get; set; }

    [Column("tdscode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Tdscode { get; set; }

    [Column("tdsdescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Tdsdescription { get; set; }

    [Column("tdsperc", TypeName = "numeric(5, 3)")]
    public decimal? Tdsperc { get; set; }
}
