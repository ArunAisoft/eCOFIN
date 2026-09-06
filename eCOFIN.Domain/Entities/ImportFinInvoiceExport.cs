using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("IMPORT_FIN_InvoiceExport")]
public partial class ImportFinInvoiceExport
{
    [Column("Inv_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string InvNo { get; set; } = null!;

    [Column("Inv_Date", TypeName = "datetime")]
    public DateTime? InvDate { get; set; }

    [Column("Inv_Type")]
    [StringLength(255)]
    [Unicode(false)]
    public string? InvType { get; set; }

    [Column("Inv_SubType")]
    [StringLength(255)]
    [Unicode(false)]
    public string? InvSubType { get; set; }

    [Column("Inv_Category")]
    [StringLength(25)]
    [Unicode(false)]
    public string? InvCategory { get; set; }

    [Column("Inv_SUBCategory")]
    [StringLength(25)]
    [Unicode(false)]
    public string? InvSubcategory { get; set; }

    [Column("Indigenous_Inv")]
    [StringLength(1)]
    [Unicode(false)]
    public string? IndigenousInv { get; set; }

    [Column("Customer_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CustomerCode { get; set; }

    [Column("Consignee_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string ConsigneeCode { get; set; } = null!;

    [Column("Currency_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string CurrencyCode { get; set; } = null!;

    [Column("Currency_Rate")]
    public double? CurrencyRate { get; set; }

    [Column("HSN_CodeM")]
    [StringLength(25)]
    [Unicode(false)]
    public string? HsnCodeM { get; set; }

    [Column("Inv_Qty")]
    public int? InvQty { get; set; }

    [Column("Tot_ItemAssValue")]
    public double? TotItemAssValue { get; set; }

    [Column("Inv_AssValue")]
    public double? InvAssValue { get; set; }

    [Column("BCD_Per")]
    public double BcdPer { get; set; }

    [Column("BCD_Value")]
    public double? BcdValue { get; set; }

    [Column("VCD_Per")]
    public double VcdPer { get; set; }

    [Column("VCD_Value")]
    public double? VcdValue { get; set; }

    [Column("Cess_Per")]
    public double CessPer { get; set; }

    [Column("Cess_Value")]
    public double? CessValue { get; set; }

    [Column("HECess_Per")]
    public double HecessPer { get; set; }

    [Column("HECess_Value")]
    public double? HecessValue { get; set; }

    [Column("Total_ImportedDuty")]
    public double? TotalImportedDuty { get; set; }

    [Column("GST_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? GstType { get; set; }

    [Column("GST_PerM")]
    public double GstPerM { get; set; }

    [Column("GST_Value")]
    public double? GstValue { get; set; }

    [Column("SGST_PerM")]
    public double SgstPerM { get; set; }

    [Column("SGST_Value")]
    public double? SgstValue { get; set; }

    [Column("CGST_PerM")]
    public double CgstPerM { get; set; }

    [Column("CGST_Value")]
    public double? CgstValue { get; set; }

    [Column("IGST_PerM")]
    public double IgstPerM { get; set; }

    [Column("IGST_Value")]
    public double? IgstValue { get; set; }

    [Column("TCS_Per")]
    public double TcsPer { get; set; }

    [Column("Tot_InvValue")]
    public double? TotInvValue { get; set; }

    [Column("TCS_Value")]
    public double? TcsValue { get; set; }

    [Column("freight")]
    public double Freight { get; set; }

    [Column("GrandTot_InvValue")]
    public double? GrandTotInvValue { get; set; }
}
