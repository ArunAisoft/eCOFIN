using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Productcode", "Serialno")]
[Table("CFN_GOVSRNO")]
public partial class CfnGovsrno
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Key]
    [Column("SERIALNO")]
    [StringLength(100)]
    [Unicode(false)]
    public string Serialno { get; set; } = null!;

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("CTRL_STATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("STOCKTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Stocktype { get; set; }

    [Column("CTRL_GIVONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlGivonholdno { get; set; }
}
