using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Gin_Price_Import")]
//[Index("RowId", Name = "IX_Gin_Price_Import", IsUnique = true)]
public partial class GinPriceImport
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

    [Column("Other_Charges")]
    public double? OtherCharges { get; set; }

    [Column("Inv_Value")]
    public double? InvValue { get; set; }

    [Column("Currency_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CurrencyCode { get; set; }

    [Column("Conversion_Rate")]
    public double? ConversionRate { get; set; }

    [Column("PayTo_Supplier")]
    public double? PayToSupplier { get; set; }

    public double? Freight { get; set; }

    public double? Insurance { get; set; }

    [Column("CIF_OC")]
    public double? CifOc { get; set; }

    [Column("CIF")]
    public double? Cif { get; set; }

    [Column("HandlingCharges_Per")]
    public double? HandlingChargesPer { get; set; }

    [Column("HandlingCharges_Val")]
    public double? HandlingChargesVal { get; set; }

    [Column("Ass_Value")]
    public double? AssValue { get; set; }

    [Column("BCD_Per")]
    public double? BcdPer { get; set; }

    [Column("BCD_Val")]
    public double? BcdVal { get; set; }

    [Column("BCDECess_Per")]
    public double? BcdecessPer { get; set; }

    [Column("BCDECess_Val")]
    public double? BcdecessVal { get; set; }

    [Column("BCDSHECess_Per")]
    public double? BcdshecessPer { get; set; }

    [Column("BCDSHECess_Val")]
    public double? BcdshecessVal { get; set; }

    [Column("AssWBCD_Val")]
    public double? AssWbcdVal { get; set; }

    [Column("BCDGST_Per")]
    public double? BcdgstPer { get; set; }

    [Column("BCD_OC")]
    public double? BcdOc { get; set; }

    [Column("BCD_IVal")]
    public double? BcdIval { get; set; }

    [Column("CSupp_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CsuppCode { get; set; }

    [Column("CSAC_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CsacCode { get; set; }

    [Column("CBasicTax_Val")]
    public double? CbasicTaxVal { get; set; }

    [Column("CBasicNonTax_Val")]
    public double? CbasicNonTaxVal { get; set; }

    [Column("CSGST_Per")]
    public double? CsgstPer { get; set; }

    [Column("CSGST_Val")]
    public double? CsgstVal { get; set; }

    [Column("CCGST_Per")]
    public double? CcgstPer { get; set; }

    [Column("CCGST_Val")]
    public double? CcgstVal { get; set; }

    [Column("CIGST_Val")]
    public double? CigstVal { get; set; }

    [Column("CIGST_Per")]
    public double? CigstPer { get; set; }

    [Column("CTDS_Per")]
    public double? CtdsPer { get; set; }

    [Column("CTDS_Val")]
    public double? CtdsVal { get; set; }

    [Column("COC_Val")]
    public double? CocVal { get; set; }

    [Column("Clearance_Val")]
    public double? ClearanceVal { get; set; }

    [Column("FSupp_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? FsuppCode { get; set; }

    [Column("FSAC_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? FsacCode { get; set; }

    [Column("FBasicTax_Val")]
    public double? FbasicTaxVal { get; set; }

    [Column("FBasicNonTax_Val")]
    public double? FbasicNonTaxVal { get; set; }

    [Column("FSGST_Per")]
    public double? FsgstPer { get; set; }

    [Column("FSGST_Val")]
    public double? FsgstVal { get; set; }

    [Column("FCGST_Per")]
    public double? FcgstPer { get; set; }

    [Column("FCGST_Val")]
    public double? FcgstVal { get; set; }

    [Column("FIGST_Val")]
    public double? FigstVal { get; set; }

    [Column("FIGST_Per")]
    public double? FigstPer { get; set; }

    [Column("FTDS_Per")]
    public double? FtdsPer { get; set; }

    [Column("FTDS_Val")]
    public double? FtdsVal { get; set; }

    [Column("FOC_Val")]
    public double? FocVal { get; set; }

    [Column("Freight_Val")]
    public double? FreightVal { get; set; }

    [Column("BD_Per")]
    public double? BdPer { get; set; }

    [Column("BD")]
    public double? Bd { get; set; }

    [Column("CVD_Per")]
    public double? CvdPer { get; set; }

    [Column("CVD")]
    public double? Cvd { get; set; }

    [Column("SAD_Per")]
    public double? SadPer { get; set; }

    [Column("SAD")]
    public double? Sad { get; set; }

    [Column("CF_Freight")]
    public double? CfFreight { get; set; }

    [Column("CF_Insurance")]
    public double? CfInsurance { get; set; }

    [Column("CF_Charge")]
    public double? CfCharge { get; set; }

    [Column("MOD_Rec")]
    public double? ModRec { get; set; }

    [Column("CED")]
    public double? Ced { get; set; }

    [Column("ET_Con_Tool")]
    public double? EtConTool { get; set; }

    [Column("ET_Comp")]
    public double? EtComp { get; set; }

    [Column("ET_Pla_Mac")]
    public double? EtPlaMac { get; set; }

    [Column("Lan_Cost")]
    public double? LanCost { get; set; }

    [Column("FAcc_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? FaccCode { get; set; }

    [Column("CAcc_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CaccCode { get; set; }

    [Column("CVD_Cess_Per")]
    public double? CvdCessPer { get; set; }

    [Column("CVD_Cess")]
    public double? CvdCess { get; set; }

    [Column("BD_Cess_Per")]
    public double? BdCessPer { get; set; }

    [Column("BD_Cess")]
    public double? BdCess { get; set; }

    [Column("CF_Postage")]
    public double? CfPostage { get; set; }

    [Column("MOD_Rec_Cess")]
    public double? ModRecCess { get; set; }

    [Column("CED_Cess")]
    public double? CedCess { get; set; }

    [Column("Entered_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? EnteredBy { get; set; }

    [Column("Entered_Date", TypeName = "datetime")]
    public DateTime? EnteredDate { get; set; }

    [Column("Lan_Char")]
    public double LanChar { get; set; }

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }
}
