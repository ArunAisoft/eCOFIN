using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("SlNoGin", "GinNo")]
[Table("GIN_Detail")]
public partial class GinDetail
{
    [Column("Row_ID")]
    public long RowId { get; set; }

    [Key]
    [Column("SlNo_GIN")]
    public int SlNoGin { get; set; }

    [Column("GIN_SlNo")]
    public int? GinSlNo { get; set; }

    [Key]
    [Column("GIN_NO")]
    [StringLength(25)]
    [Unicode(false)]
    public string GinNo { get; set; } = null!;

    [Column("Ind_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string IndNo { get; set; } = null!;

    [Column("Ind_SlNo")]
    public double IndSlNo { get; set; }

    [Column("PO_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PoNo { get; set; }

    [Column("PO_SlNo")]
    public double PoSlNo { get; set; }

    [Column("PMT_D_Date", TypeName = "datetime")]
    public DateTime? PmtDDate { get; set; }

    [Column("Stock_To")]
    [StringLength(1)]
    [Unicode(false)]
    public string StockTo { get; set; } = null!;

    [Column("Article_Type")]
    [StringLength(1)]
    [Unicode(false)]
    public string ArticleType { get; set; } = null!;

    [Column("ItemSuperSubType_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ItemSuperSubTypeCode { get; set; }

    [Column("UOM")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Uom { get; set; }

    [Column("Article_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string ArticleNo { get; set; } = null!;

    [Column("Cha_Qty")]
    public double? ChaQty { get; set; }

    [Column("Rec_Qty")]
    public double? RecQty { get; set; }

    [Column("Acc_Qty")]
    public double? AccQty { get; set; }

    [Column("Rej_Qty")]
    public double? RejQty { get; set; }

    [Column("RejRet_Qty")]
    public double? RejRetQty { get; set; }

    public double? Price { get; set; }

    [Column("PSL_Price")]
    public double? PslPrice { get; set; }

    [Unicode(false)]
    public string? Remarks { get; set; }

    [Column("Remarks_Inspection")]
    [Unicode(false)]
    public string? RemarksInspection { get; set; }

    [Column("Trans_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TransNo { get; set; }

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

    [Column("BCD_Per")]
    public double? BcdPer { get; set; }

    [Column("ECess_Per")]
    public double? EcessPer { get; set; }

    [Column("SHECess_Per")]
    public double? ShecessPer { get; set; }

    [Column("GP_Qty")]
    public double GpQty { get; set; }

    [Column("GP_RetQty")]
    public double? GpRetQty { get; set; }

    [Column("Acc_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? AccCode { get; set; }

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }
}
