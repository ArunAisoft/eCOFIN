using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("IMPORT_FIN_GIN")]
public partial class ImportFinGin
{
    [Column("GIN_NO")]
    [StringLength(25)]
    [Unicode(false)]
    public string GinNo { get; set; } = null!;

    [Column("GIN_Date", TypeName = "datetime")]
    public DateTime? GinDate { get; set; }

    [Column("Main_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? MainType { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? Type { get; set; }

    [Column("Sub_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? SubType { get; set; }

    [Column("Supplier_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? SupplierCode { get; set; }

    [Column("INV_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? InvNo { get; set; }

    [Column("INV_Date", TypeName = "datetime")]
    public DateTime? InvDate { get; set; }

    [Column("PO_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string PoNo { get; set; } = null!;

    [Column("PO_SlNo")]
    public int PoSlNo { get; set; }

    [Column("PMT_D_Date", TypeName = "datetime")]
    public DateTime? PmtDDate { get; set; }

    [Column("Article_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ArticleNo { get; set; }

    [Column("Item_Description")]
    [StringLength(100)]
    [Unicode(false)]
    public string ItemDescription { get; set; } = null!;

    [Column("UOM")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Uom { get; set; }

    [Column("HSN_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? HsnCode { get; set; }

    [Column("Cha_Qty")]
    public double? ChaQty { get; set; }

    [Column("Rec_Qty")]
    public double? RecQty { get; set; }

    [Column("Acc_Qty")]
    public double? AccQty { get; set; }

    [Column("Item_BasicPrice")]
    public double? ItemBasicPrice { get; set; }

    [Column("Item_BasicValue")]
    public double ItemBasicValue { get; set; }

    [Column("Item_AssValue")]
    public double? ItemAssValue { get; set; }

    [Column("Item_PF")]
    public double? ItemPf { get; set; }

    [Column("Item_OtherCharges")]
    public double? ItemOtherCharges { get; set; }

    [Column("GST_Per")]
    public double GstPer { get; set; }

    [Column("GST_Value")]
    public double? GstValue { get; set; }

    [Column("SGST_Per")]
    public double SgstPer { get; set; }

    [Column("SGST_Value")]
    public double? SgstValue { get; set; }

    [Column("CGST_Per")]
    public double CgstPer { get; set; }

    [Column("CGST_Value")]
    public double? CgstValue { get; set; }

    [Column("IGST_Per")]
    public double IgstPer { get; set; }

    [Column("IGST_Value")]
    public double? IgstValue { get; set; }

    [Column("TDS_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TdsCode { get; set; }

    [Column("TDS_Per")]
    public double? TdsPer { get; set; }

    [Column("TDS_Value")]
    public double? TdsValue { get; set; }

    [Column("Item_OtherChargesOutSideGST")]
    public double? ItemOtherChargesOutSideGst { get; set; }

    [Column("TCS_Per")]
    public double TcsPer { get; set; }

    [Column("TCS_Value")]
    public double? TcsValue { get; set; }

    [Column("Item_PayableToSupplier")]
    public double? ItemPayableToSupplier { get; set; }

    [Column("Item_TotalValue")]
    public double? ItemTotalValue { get; set; }

    [Column("Acc_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string AccCode { get; set; } = null!;

    [Column("GIN_ValueDate", TypeName = "datetime")]
    public DateTime? GinValueDate { get; set; }

    [Column("GIN_ValueBy")]
    [StringLength(150)]
    [Unicode(false)]
    public string? GinValueBy { get; set; }

    [Column("Gin_AppDate", TypeName = "datetime")]
    public DateTime? GinAppDate { get; set; }
}
