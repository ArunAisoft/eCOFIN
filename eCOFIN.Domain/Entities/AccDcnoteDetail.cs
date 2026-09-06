using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("SlNo", "DcnoteNo")]
[Table("Acc_DCNote_Detail")]
public partial class AccDcnoteDetail
{
    [Key]
    [Column("Sl_No")]
    public int SlNo { get; set; }

    [Key]
    [Column("DCNote_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string DcnoteNo { get; set; } = null!;

    [Column("Main_VchNo")]
    [StringLength(25)]
    [Unicode(false)]
    public string? MainVchNo { get; set; }

    [Column("Vch_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? VchNo { get; set; }

    [Column("Sub_VchNo")]
    [StringLength(90)]
    [Unicode(false)]
    public string? SubVchNo { get; set; }

    [Column("Article_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ArticleCode { get; set; }

    [Column("UOM")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Uom { get; set; }

    [Column("Ret_Qty")]
    public double? RetQty { get; set; }

    [Column("Item_Rate")]
    public double? ItemRate { get; set; }

    [Column("Item_PPC")]
    public double? ItemPpc { get; set; }

    [Column("Item_IssueRate")]
    public double? ItemIssueRate { get; set; }

    [Column("Item_Remarks")]
    [Unicode(false)]
    public string? ItemRemarks { get; set; }

    [Column("HSN_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? HsnCode { get; set; }

    [Column("GST_Per")]
    public double? GstPer { get; set; }

    [Column("SGST_Per")]
    public double? SgstPer { get; set; }

    [Column("CGST_Per")]
    public double? CgstPer { get; set; }

    [Column("IGST_Per")]
    public double? IgstPer { get; set; }

    [Column("Item_Cancel")]
    [StringLength(1)]
    [Unicode(false)]
    public string? ItemCancel { get; set; }

    [Column("Item_AccRate")]
    public double? ItemAccRate { get; set; }

    [ForeignKey("DcnoteNo")]
    [InverseProperty("AccDcnoteDetails")]
    public virtual AccDcnoteMaster DcnoteNoNavigation { get; set; } = null!;
}
