using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("AnnNo", "RcNo", "VendCode", "OpnName")]
[Table("Sto_Annex_Detail")]
//[Index("RowNo", Name = "IX_Sto_Annex_Detail", IsUnique = true)]
public partial class StoAnnexDetail
{
    [Column("Row_No")]
    public long RowNo { get; set; }

    [Key]
    [Column("Ann_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string AnnNo { get; set; } = null!;

    [Column("Jin_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? JinNo { get; set; }

    [Column("Jin_Date", TypeName = "datetime")]
    public DateTime? JinDate { get; set; }

    [Column("Ann3_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Ann3No { get; set; }

    [Column("Ann_3")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Ann3 { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? Swipe { get; set; }

    [Key]
    [Column("Rc_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string RcNo { get; set; } = null!;

    [Column("Article_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string ArticleNo { get; set; } = null!;

    [Key]
    [Column("Vend_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string VendCode { get; set; } = null!;

    [Column("Ann_Qty")]
    public double? AnnQty { get; set; }

    [Column("Ret_Qty")]
    public double? RetQty { get; set; }

    [Column("Acc_Qty")]
    public double? AccQty { get; set; }

    [Column("Rej_Qty")]
    public double? RejQty { get; set; }

    [Column("Qua_Qty")]
    public double? QuaQty { get; set; }

    [Column("RejRet_Qty")]
    public double? RejRetQty { get; set; }

    [Column("Opn_No")]
    public double OpnNo { get; set; }

    [Key]
    [Column("Opn_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string OpnName { get; set; } = null!;

    [Column("Opn_Rate")]
    public double? OpnRate { get; set; }

    [Column("Tarrif_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TarrifNo { get; set; }

    [Column("Rm_Wt")]
    public double? RmWt { get; set; }

    [Column("Com_Wt")]
    public double? ComWt { get; set; }

    [Column("Store_Remarks")]
    [StringLength(300)]
    public string? StoreRemarks { get; set; }

    [Column("Return_Remarks")]
    [StringLength(300)]
    public string? ReturnRemarks { get; set; }

    [Column("Ins_Remarks")]
    [StringLength(300)]
    public string? InsRemarks { get; set; }

    [Column("Acc_Remarks")]
    [StringLength(300)]
    public string? AccRemarks { get; set; }

    [Column("Rc_Value")]
    public double? RcValue { get; set; }

    [Column("Acc_Value")]
    public double? AccValue { get; set; }

    [Column("Rej_Rate")]
    public double? RejRate { get; set; }

    [Column("Rej_Value")]
    public double? RejValue { get; set; }

    [Column("Iss_Qty")]
    public double? IssQty { get; set; }

    [Column("Iss_Rate")]
    public double? IssRate { get; set; }

    [Column("W_Opn")]
    [StringLength(1)]
    [Unicode(false)]
    public string? WOpn { get; set; }

    [Column("WOOPN_Reason")]
    [StringLength(50)]
    [Unicode(false)]
    public string? WoopnReason { get; set; }

    [Column("WO_OpnBy")]
    [StringLength(150)]
    [Unicode(false)]
    public string? WoOpnBy { get; set; }

    [Column("WO_Remarks")]
    [StringLength(150)]
    public string? WoRemarks { get; set; }

    [Column("Ins_Approve")]
    [StringLength(1)]
    [Unicode(false)]
    public string? InsApprove { get; set; }

    [Column("Jin_Approve")]
    [StringLength(1)]
    [Unicode(false)]
    public string? JinApprove { get; set; }

    [Column("Jin_Value")]
    public double? JinValue { get; set; }

    [Column("TDS_Acc_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TdsAccCode { get; set; }

    [Column("TDS_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TdsCode { get; set; }

    [Column("TDS_Percent")]
    public double? TdsPercent { get; set; }

    [Column("TDS_Des")]
    [StringLength(150)]
    [Unicode(false)]
    public string? TdsDes { get; set; }

    [Column("TDS_Value")]
    public double? TdsValue { get; set; }

    [Column("Jin_Val_Date", TypeName = "datetime")]
    public DateTime? JinValDate { get; set; }

    [Column("Color_Code")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ColorCode { get; set; }

    [Column("Ins_Date", TypeName = "datetime")]
    public DateTime? InsDate { get; set; }

    [Column("Opn_Qty")]
    public double? OpnQty { get; set; }

    [Column("Pro_Qty")]
    public double? ProQty { get; set; }

    [Column("JIN_AppDate", TypeName = "datetime")]
    public DateTime? JinAppDate { get; set; }

    [Column("JIN_AppBy")]
    [StringLength(150)]
    [Unicode(false)]
    public string? JinAppBy { get; set; }

    [Column("TDS_Value_Old")]
    public double? TdsValueOld { get; set; }

    [Column("Vend_AccQty")]
    public double? VendAccQty { get; set; }

    [Column("Vend_RejQty")]
    public double? VendRejQty { get; set; }

    [Column("Vend_RejReason")]
    [StringLength(200)]
    public string? VendRejReason { get; set; }

    [Column("Vend_LUDate", TypeName = "datetime")]
    public DateTime? VendLudate { get; set; }

    [Column("Vend_FAccDate", TypeName = "datetime")]
    public DateTime? VendFaccDate { get; set; }

    [Column("Vend_LAccDate", TypeName = "datetime")]
    public DateTime? VendLaccDate { get; set; }

    [Column("Ann_Suffix")]
    [StringLength(1)]
    [Unicode(false)]
    public string? AnnSuffix { get; set; }

    [Column("GST_Per")]
    public double? GstPer { get; set; }

    [Column("SGST_Per")]
    public double? SgstPer { get; set; }

    [Column("CGST_Per")]
    public double? CgstPer { get; set; }

    [Column("IGST_Per")]
    public double? IgstPer { get; set; }

    [Column("Bill_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? BillNo { get; set; }

    [Column("Bill_Date", TypeName = "datetime")]
    public DateTime? BillDate { get; set; }

    [Column("Bill_BasicValue")]
    public double? BillBasicValue { get; set; }

    [Column("Issue_Price")]
    public double? IssuePrice { get; set; }

    [Column("Process_Price")]
    public double? ProcessPrice { get; set; }

    [Column("PF")]
    public double? Pf { get; set; }

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }

    [Column("GP_Qty")]
    public double GpQty { get; set; }

    [Column("GP_RetQty")]
    public double GpRetQty { get; set; }

    [Column("UOM")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Uom { get; set; }
}
