using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Billreference")]
[Table("CFN_BNKDOCDETAIL")]
public partial class CfnBnkdocdetail
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("BILLREFERENCE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Billreference { get; set; } = null!;

    [Column("BILLDATE", TypeName = "datetime")]
    public DateTime? Billdate { get; set; }

    [Column("LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Locationcode { get; set; }

    [Column("BILLAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Billamount { get; set; }

    [Column("BANKCHARGES", TypeName = "numeric(10, 2)")]
    public decimal? Bankcharges { get; set; }

    [Column("INTERESTPERCENTAGES", TypeName = "numeric(6, 2)")]
    public decimal? Interestpercentages { get; set; }

    [Column("OTHERCHARGES", TypeName = "numeric(10, 2)")]
    public decimal? Othercharges { get; set; }

    [Column("OVERHEADCHARGES", TypeName = "numeric(10, 2)")]
    public decimal? Overheadcharges { get; set; }

    [Column("CREDITDAYS", TypeName = "numeric(3, 0)")]
    public decimal? Creditdays { get; set; }

    [Column("DUEDATE", TypeName = "datetime")]
    public DateTime? Duedate { get; set; }

    [Column("RETIRED")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Retired { get; set; }

    [Column("RETIREMENTNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Retirementnumber { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
