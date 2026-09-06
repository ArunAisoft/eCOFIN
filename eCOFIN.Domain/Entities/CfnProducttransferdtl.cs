using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Productcode")]
[Table("CFN_PRODUCTTRANSFERDTL")]
public partial class CfnProducttransferdtl
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

    [Column("QUANTITY", TypeName = "numeric(5, 0)")]
    public decimal Quantity { get; set; }

    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal? CtrlSequenceno { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
