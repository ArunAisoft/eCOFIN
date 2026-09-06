using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_SANDETAIL")]
public partial class CfnSandetail
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlSequenceno { get; set; } = null!;

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Column("PERCENTAGEORVALUE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Percentageorvalue { get; set; }

    [Column("PERCENTAGE", TypeName = "numeric(10, 2)")]
    public decimal? Percentage { get; set; }

    [Column("VALUE", TypeName = "numeric(14, 4)")]
    public decimal? Value { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("PONUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ponumber { get; set; }

    [Column("BALANCEQTY", TypeName = "numeric(10, 0)")]
    public decimal? Balanceqty { get; set; }

    [Column("BILLEDQTY", TypeName = "numeric(10, 0)")]
    public decimal? Billedqty { get; set; }

    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal? Productid { get; set; }

    [Column("BALANCEVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Balancevalue { get; set; }

    [Column("ACCEPTEDVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Acceptedvalue { get; set; }

    [Column("REQONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Reqonholdno { get; set; }
}
