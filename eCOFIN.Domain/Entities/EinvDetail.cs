using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Slno", "InvSlNo")]
[Table("einv_detail")]
//[Index("RowNo", Name = "IX_einv_detail", IsUnique = true)]
public partial class EinvDetail
{
    [Column("Row_No")]
    public long RowNo { get; set; }

    [Key]
    [Column("Inv_SlNo")]
    public int InvSlNo { get; set; }

    [Key]
    [Column("slno")]
    [StringLength(50)]
    [Unicode(false)]
    public string Slno { get; set; } = null!;

    [Column("inv_date", TypeName = "datetime")]
    public DateTime? InvDate { get; set; }

    [Column("order_wo_no")]
    [StringLength(100)]
    [Unicode(false)]
    public string OrderWoNo { get; set; } = null!;

    [Column("art_no")]
    [StringLength(25)]
    [Unicode(false)]
    public string ArtNo { get; set; } = null!;

    [Column("PoNo_Ref")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PoNoRef { get; set; }

    [Column("clt_art")]
    [StringLength(255)]
    [Unicode(false)]
    public string? CltArt { get; set; }

    [Column("qty")]
    public int? Qty { get; set; }

    [Column("Item_Price")]
    public double? ItemPrice { get; set; }

    [Column("Discount_Per")]
    public double? DiscountPer { get; set; }

    [Column("Addition_Per")]
    public double? AdditionPer { get; set; }

    [Column("price")]
    public double? Price { get; set; }

    [Column("ded", TypeName = "money")]
    public decimal? Ded { get; set; }

    [Column("add", TypeName = "money")]
    public decimal? Add { get; set; }

    [Column("rduty")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Rduty { get; set; }

    [Column("cst")]
    public double? Cst { get; set; }

    [Column("kst")]
    public double? Kst { get; set; }

    [Column("cess")]
    public double? Cess { get; set; }

    [Column("rg_slno")]
    public int? RgSlno { get; set; }

    [Column("rg_date", TypeName = "datetime")]
    public DateTime? RgDate { get; set; }

    [Column("oa_pur")]
    [StringLength(255)]
    [Unicode(false)]
    public string? OaPur { get; set; }

    [Column("oa_date", TypeName = "datetime")]
    public DateTime? OaDate { get; set; }

    [Column("name_ex_comm")]
    [StringLength(255)]
    [Unicode(false)]
    public string? NameExComm { get; set; }

    [Column("noti_date")]
    [StringLength(255)]
    [Unicode(false)]
    public string? NotiDate { get; set; }

    [Column("Tariff_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TariffNo { get; set; }

    [Column("posl")]
    public double Posl { get; set; }

    public double? Price2 { get; set; }

    public double? Tass { get; set; }

    [Column("HEdu_Cess_Per")]
    public double? HeduCessPer { get; set; }

    [Column("TCS_Per")]
    public double? TcsPer { get; set; }

    [Column("Item_Type")]
    [StringLength(50)]
    public string? ItemType { get; set; }

    [Column("Cell_Name")]
    [StringLength(50)]
    public string? CellName { get; set; }

    [Column("Item_Remarks")]
    [StringLength(500)]
    public string? ItemRemarks { get; set; }

    [Column("Item_Des_Date", TypeName = "datetime")]
    public DateTime? ItemDesDate { get; set; }

    [Column("Cust_PODate", TypeName = "datetime")]
    public DateTime? CustPodate { get; set; }

    [Column("Ref_PONo")]
    [StringLength(150)]
    public string? RefPono { get; set; }

    [Column("Ref_POSLNo")]
    public double? RefPoslno { get; set; }

    [Column("Item_SubType")]
    [StringLength(50)]
    public string? ItemSubType { get; set; }

    [Column("tarrif")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Tarrif { get; set; }

    [Column("GST_Per")]
    public double? GstPer { get; set; }

    [Column("SGST_Per")]
    public double? SgstPer { get; set; }

    [Column("CGST_Per")]
    public double? CgstPer { get; set; }

    [Column("IGST_Per")]
    public double? IgstPer { get; set; }

    [Column("UOM")]
    [StringLength(25)]
    [Unicode(false)]
    public string Uom { get; set; } = null!;

    [Column("PO_Qty")]
    public double? PoQty { get; set; }

    [Column("ForInvoice_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ForInvoiceNo { get; set; }

    [Column("Cust_ItemRefSlNo")]
    public int CustItemRefSlNo { get; set; }

    [Column("Cust_SlNo")]
    [StringLength(25)]
    [Unicode(false)]
    public string CustSlNo { get; set; } = null!;

    [Column("Cust_ItemRefSlNo_OLD")]
    public int CustItemRefSlNoOld { get; set; }

    [Column("InvD_Active")]
    [StringLength(1)]
    [Unicode(false)]
    public string InvDActive { get; set; } = null!;

    [Column("D3_Qty")]
    public double D3Qty { get; set; }

    [Column("Act_InvQty")]
    public double? ActInvQty { get; set; }

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }

    [Column("Group_Invoice")]
    [StringLength(50)]
    [Unicode(false)]
    public string? GroupInvoice { get; set; }

    [Column("Order_Category")]
    [StringLength(25)]
    [Unicode(false)]
    public string? OrderCategory { get; set; }

    [Column("Order_SubCategory")]
    [StringLength(25)]
    [Unicode(false)]
    public string? OrderSubCategory { get; set; }
}
