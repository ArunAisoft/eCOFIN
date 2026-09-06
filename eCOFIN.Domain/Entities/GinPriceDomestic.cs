using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Gin_Price_Domestic")]
//[Index("RowId", Name = "IX_Gin_Price_Domestic", IsUnique = true)]
public partial class GinPriceDomestic
{
    [Column("Row_ID")]
    public long RowId { get; set; }

    [Key]
    [Column("Gin_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string GinNo { get; set; } = null!;

    [Column("Po_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PoNo { get; set; }

    [Column("Basic_Value")]
    public double? BasicValue { get; set; }

    [Column("PF")]
    public double? Pf { get; set; }

    public double? Freight { get; set; }

    public double? Others { get; set; }

    [Column("ED")]
    public double? Ed { get; set; }

    [Column("STP")]
    public double? Stp { get; set; }

    [Column("ST")]
    public double? St { get; set; }

    [Column("EDCessP")]
    public double? EdcessP { get; set; }

    [Column("EDCess")]
    public double? Edcess { get; set; }

    public double? CessP { get; set; }

    public double? Cess { get; set; }

    [Column("Entry_TaxP")]
    public double? EntryTaxP { get; set; }

    [Column("Entry_Tax")]
    public double? EntryTax { get; set; }

    [Column("TCS_Per")]
    public double? TcsPer { get; set; }

    [Column("Tot_Amt1")]
    public double? TotAmt1 { get; set; }

    public double? Modvat { get; set; }

    [Column("Modvat_Rec")]
    public double? ModvatRec { get; set; }

    [Column("Tot_Amt2")]
    public double? TotAmt2 { get; set; }

    [Column("Vat_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? VatType { get; set; }

    [Column("Vat_Amt")]
    public double? VatAmt { get; set; }

    [Column("EDCess_Modvat")]
    public double? EdcessModvat { get; set; }

    [Column("EDCess_Mod_Rec")]
    public double? EdcessModRec { get; set; }

    [Column("EDCessP2")]
    public double? EdcessP2 { get; set; }

    [Column("EDCess2")]
    public double? Edcess2 { get; set; }

    [Column("EDCess2_Modvat")]
    public double? Edcess2Modvat { get; set; }

    [Column("EDCess2_Mod_Rec")]
    public double? Edcess2ModRec { get; set; }

    [Column("CST")]
    public double? Cst { get; set; }

    [Column("CSTVal")]
    public double? Cstval { get; set; }

    [Column("Trans_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TransNo { get; set; }

    [Column("Service_TaxP")]
    public double? ServiceTaxP { get; set; }

    [Column("Service_Tax")]
    public double? ServiceTax { get; set; }

    [Column("SBharat_CessP")]
    public double? SbharatCessP { get; set; }

    [Column("SBharat_Cess")]
    public double? SbharatCess { get; set; }

    [Column("Tax_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TaxType { get; set; }

    [Column("GST_PerM")]
    public double? GstPerM { get; set; }

    [Column("SGST_PerM")]
    public double? SgstPerM { get; set; }

    [Column("CGST_PerM")]
    public double? CgstPerM { get; set; }

    [Column("IGST_PerM")]
    public double? IgstPerM { get; set; }

    [Column("Add_Others")]
    public double? AddOthers { get; set; }

    [Column("Created_Date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("TDS_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TdsCode { get; set; }

    [Column("TDS_Per")]
    public double? TdsPer { get; set; }

    [Column("TDS_Value")]
    public double? TdsValue { get; set; }

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }
}
