using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("Ann_Opn_Report")]
public partial class AnnOpnReport
{
    [Column("PArticle_No")]
    [StringLength(50)]
    public string? ParticleNo { get; set; }

    [StringLength(50)]
    public string? Description { get; set; }

    [Column("Drawing_No")]
    [StringLength(50)]
    public string? DrawingNo { get; set; }

    [Column("Cell_Name")]
    [StringLength(50)]
    public string? CellName { get; set; }

    [Column("Rc_No")]
    [StringLength(50)]
    public string? RcNo { get; set; }

    [Column("Ann_No")]
    [StringLength(50)]
    public string? AnnNo { get; set; }

    [Column("Ann_Date", TypeName = "datetime")]
    public DateTime? AnnDate { get; set; }

    [Column("Due_Date", TypeName = "datetime")]
    public DateTime? DueDate { get; set; }

    [Column("Ann_Qty")]
    public double? AnnQty { get; set; }

    [Column("Jin_No")]
    [StringLength(50)]
    public string? JinNo { get; set; }

    [Column("Jin_Date", TypeName = "datetime")]
    public DateTime? JinDate { get; set; }

    [Column("Ret_Qty")]
    public double? RetQty { get; set; }

    [Column("Ins_date", TypeName = "datetime")]
    public DateTime? InsDate { get; set; }

    [Column("Ann_Lead_Time")]
    public double? AnnLeadTime { get; set; }

    [Column("Ann_Due_Jin")]
    public double? AnnDueJin { get; set; }

    [Column("Insp_Lead_Time")]
    public double? InspLeadTime { get; set; }

    [StringLength(50)]
    public string? FirstOpnName { get; set; }

    [StringLength(50)]
    public string? LastOpnName { get; set; }

    [Column("Vendor_Name")]
    [StringLength(50)]
    public string? VendorName { get; set; }

    [Column("GST_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? GstNo { get; set; }

    [Column("URV")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Urv { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? State { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Status { get; set; }

    [Column("Annex_Remarks")]
    [Unicode(false)]
    public string? AnnexRemarks { get; set; }

    [Column("Return_Remarks")]
    [Unicode(false)]
    public string? ReturnRemarks { get; set; }
}
