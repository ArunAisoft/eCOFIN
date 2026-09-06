using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("IMPORT_FIN_DebitNote")]
public partial class ImportFinDebitNote
{
    [Column("Report_Name")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ReportName { get; set; }

    [Column("DCNote_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? DcnoteNo { get; set; }

    [Column("DCNote_Date", TypeName = "datetime")]
    public DateTime? DcnoteDate { get; set; }

    [Column("Party_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PartyCode { get; set; }

    [Column("Currency_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CurrencyCode { get; set; }

    [Column("Currency_Rate")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CurrencyRate { get; set; }

    [Column("Item_IssueRate")]
    public double? ItemIssueRate { get; set; }

    [Column("Item_PPC")]
    public double? ItemPpc { get; set; }

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

    [Column("Sl_No")]
    public int? SlNo { get; set; }

    [Column("Article_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ArticleCode { get; set; }

    [Column("UOM")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Uom { get; set; }

    [Column("HSN_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? HsnCode { get; set; }

    [Column("DC_Qty")]
    public double? DcQty { get; set; }

    [Column("Item_Rate_FC")]
    public double? ItemRateFc { get; set; }

    [Column("Item_Rate_INR")]
    public double? ItemRateInr { get; set; }

    [Column("Item_Value_FC")]
    public double? ItemValueFc { get; set; }

    [Column("Item_Value_INR")]
    public double? ItemValueInr { get; set; }

    [Column("PF")]
    public double? Pf { get; set; }

    [Column("Other_Charges")]
    public double? OtherCharges { get; set; }

    [Column("Item_AssValue_FC")]
    public double? ItemAssValueFc { get; set; }

    [Column("Item_AssValue_INR")]
    public double? ItemAssValueInr { get; set; }

    [Column("GST_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? GstType { get; set; }

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

    [Column("Item_TotalValue")]
    public double? ItemTotalValue { get; set; }

    [Column("Other_Charges_OutSideGST")]
    public double? OtherChargesOutSideGst { get; set; }

    [Column("Item_TotalValue_OCOGST")]
    public double? ItemTotalValueOcogst { get; set; }

    [Column("Acc_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string AccCode { get; set; } = null!;
}
