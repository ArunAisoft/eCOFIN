using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Acc_CostPiece_Tab")]
public partial class AccCostPieceTab
{
    [Key]
    [Column("Rc_No")]
    [StringLength(15)]
    [Unicode(false)]
    public string RcNo { get; set; } = null!;

    [Column("Rc_Date", TypeName = "datetime")]
    public DateTime? RcDate { get; set; }

    [Column("Article_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string ArticleNo { get; set; } = null!;

    [Column("Acc_Qty")]
    public double? AccQty { get; set; }

    [Column("WCLCos")]
    public double? Wclcos { get; set; }

    public double? CosPie { get; set; }

    [Column("Sub_Type")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SubType { get; set; }

    public double? Rate { get; set; }

    [Column("DCLcost")]
    public double? Dclcost { get; set; }

    [Column("DCLcostpie")]
    public double? Dclcostpie { get; set; }

    [Column("COPNcost")]
    public double? Copncost { get; set; }

    [Column("COPNcostpie")]
    public double? Copncostpie { get; set; }

    [Column("C_Date", TypeName = "datetime")]
    public DateTime? CDate { get; set; }
}
