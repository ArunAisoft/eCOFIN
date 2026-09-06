using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Productcode")]
[Table("CFN_PHYSDETAIL")]
public partial class CfnPhysdetail
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

    [Column("DEFECTIVEQTY", TypeName = "numeric(10, 0)")]
    public decimal? Defectiveqty { get; set; }

    [Column("GOODQTY", TypeName = "numeric(10, 0)")]
    public decimal? Goodqty { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("STOCKTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Stocktype { get; set; } = null!;

    [Column("STOCKQTY", TypeName = "numeric(10, 0)")]
    public decimal? Stockqty { get; set; }
}
