using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("GIN_Master")]
public partial class GinMaster
{
    [Column("Row_ID")]
    public long RowId { get; set; }

    [Key]
    [Column("GIN_NO")]
    [StringLength(25)]
    [Unicode(false)]
    public string GinNo { get; set; } = null!;

    [Column("GIN_Date", TypeName = "datetime")]
    public DateTime? GinDate { get; set; }

    [Column("Inv_Cha_No")]
    [StringLength(250)]
    [Unicode(false)]
    public string? InvChaNo { get; set; }

    [Column("Inv_Cha_Date", TypeName = "datetime")]
    public DateTime? InvChaDate { get; set; }

    [Column("PMT_D_Date", TypeName = "datetime")]
    public DateTime? PmtDDate { get; set; }

    [Column("PO_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PoNo { get; set; }

    [Column("Supp_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? SuppCode { get; set; }

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

    [Column("Curr_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CurrCode { get; set; }

    [Column("Curr_Rate")]
    public double? CurrRate { get; set; }

    [Column("Entered_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? EnteredBy { get; set; }

    [Column("Entered_Date", TypeName = "datetime")]
    public DateTime? EnteredDate { get; set; }

    [Column("Gin_App")]
    [StringLength(1)]
    [Unicode(false)]
    public string? GinApp { get; set; }

    [Column("GIN_AppBy")]
    [StringLength(150)]
    [Unicode(false)]
    public string? GinAppBy { get; set; }

    [Column("GIN_AppDate", TypeName = "datetime")]
    public DateTime? GinAppDate { get; set; }

    [Column("Stores_Approve")]
    [StringLength(1)]
    [Unicode(false)]
    public string? StoresApprove { get; set; }

    [Column("Stores_ApprovedBy")]
    [StringLength(150)]
    [Unicode(false)]
    public string? StoresApprovedBy { get; set; }

    [Column("Stores_ApprovedDate", TypeName = "datetime")]
    public DateTime? StoresApprovedDate { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Check { get; set; }

    [Column("C_Date", TypeName = "datetime")]
    public DateTime? CDate { get; set; }

    [Column("C_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? CBy { get; set; }

    [Column("GIN_Auto")]
    [StringLength(1)]
    [Unicode(false)]
    public string GinAuto { get; set; } = null!;

    [Column("GIN_Remarks")]
    [StringLength(100)]
    public string? GinRemarks { get; set; }

    [Column("Currency_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CurrencyCode { get; set; }

    [Column("Currency_Rate")]
    public double? CurrencyRate { get; set; }

    [Column("GST_PerM")]
    public double? GstPerM { get; set; }

    [Column("SGST_PerM")]
    public double? SgstPerM { get; set; }

    [Column("CGST_PerM")]
    public double? CgstPerM { get; set; }

    [Column("IGST_PerM")]
    public double? IgstPerM { get; set; }

    [Column("PF_Charges")]
    public double? PfCharges { get; set; }

    [Column("Other_Charges")]
    public double? OtherCharges { get; set; }

    [Column("Other_Charges_OutSideGST")]
    public double? OtherChargesOutSideGst { get; set; }

    [Column("TCS_Per")]
    public double TcsPer { get; set; }

    [Column("TDS_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TdsCode { get; set; }

    [Column("TDS_Per")]
    public double? TdsPer { get; set; }

    [Column("Inv_BasicAmount")]
    public double InvBasicAmount { get; set; }

    [Column("Clearance_BillNo")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ClearanceBillNo { get; set; }

    [Column("Clearance_BillDate", TypeName = "datetime")]
    public DateTime? ClearanceBillDate { get; set; }

    [Column("Clearance_PayDDate", TypeName = "datetime")]
    public DateTime? ClearancePayDdate { get; set; }

    [Column("Clearance_Val")]
    public double? ClearanceVal { get; set; }

    [Column("Freight_BillNo")]
    [StringLength(50)]
    [Unicode(false)]
    public string? FreightBillNo { get; set; }

    [Column("Freight_BillDate", TypeName = "datetime")]
    public DateTime? FreightBillDate { get; set; }

    [Column("Freight_PayDDate", TypeName = "datetime")]
    public DateTime? FreightPayDdate { get; set; }

    [Column("Freight_Val")]
    public double? FreightVal { get; set; }

    [Column("MAC_Address")]
    [StringLength(15)]
    [Unicode(false)]
    public string? MacAddress { get; set; }

    [Column("Client_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ClientName { get; set; }

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }

    [Column("Swipe_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? SwipeNo { get; set; }

    [Column("Created_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column("Created_Date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("PF")]
    public double? Pf { get; set; }

    [Column("App_date", TypeName = "datetime")]
    public DateTime? AppDate { get; set; }
}
