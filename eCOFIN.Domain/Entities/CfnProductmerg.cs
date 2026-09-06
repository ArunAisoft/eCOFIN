using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_PRODUCTMERG")]
public partial class CfnProductmerg
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(5, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Column("STOCKTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Stocktype { get; set; }

    [Column("ACCEPTEDQTY", TypeName = "numeric(10, 0)")]
    public decimal? Acceptedqty { get; set; }

    [Column("RECEIVEDQTY", TypeName = "numeric(10, 0)")]
    public decimal? Receivedqty { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("PONUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Ponumber { get; set; }

    [Column("BALANCEQTY", TypeName = "numeric(10, 0)")]
    public decimal? Balanceqty { get; set; }

    [Column("BILLEDQTY", TypeName = "numeric(10, 0)")]
    public decimal? Billedqty { get; set; }

    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal? Productid { get; set; }

    [Column("REQONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Reqonholdno { get; set; }
}
