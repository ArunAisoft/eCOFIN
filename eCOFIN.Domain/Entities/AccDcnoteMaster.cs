using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Acc_DCNote_Master")]
public partial class AccDcnoteMaster
{
    [Column("DCNote_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? DcnoteType { get; set; }

    [Key]
    [Column("DCNote_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string DcnoteNo { get; set; } = null!;

    [Column("DCNote_Date", TypeName = "datetime")]
    public DateTime DcnoteDate { get; set; }

    [Column("TransactionID_SlNo")]
    public long? TransactionIdSlNo { get; set; }

    [Column("Party_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PartyCode { get; set; }

    [Column("GST_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? GstType { get; set; }

    [Column("PF")]
    public double? Pf { get; set; }

    [Column("Other_Charges")]
    public double? OtherCharges { get; set; }

    [Column("Other_Charges_OutSideGST")]
    public double? OtherChargesOutSideGst { get; set; }

    [Column("Contact_Person")]
    [StringLength(90)]
    [Unicode(false)]
    public string? ContactPerson { get; set; }

    [Column("Ship_Mode")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ShipMode { get; set; }

    [Column("Pay_terms")]
    [StringLength(90)]
    [Unicode(false)]
    public string? PayTerms { get; set; }

    [StringLength(90)]
    [Unicode(false)]
    public string? Insurance { get; set; }

    [Column("Pay_Due_Date", TypeName = "datetime")]
    public DateTime? PayDueDate { get; set; }

    [Column("App_Date", TypeName = "datetime")]
    public DateTime? AppDate { get; set; }

    [Column("App_By")]
    [StringLength(25)]
    [Unicode(false)]
    public string? AppBy { get; set; }

    [Column("Currency_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CurrencyCode { get; set; }

    [Column("Currency_Rate")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CurrencyRate { get; set; }

    [Column("HSN_CodeM")]
    [StringLength(25)]
    [Unicode(false)]
    public string? HsnCodeM { get; set; }

    [Column("GST_PerM")]
    public double? GstPerM { get; set; }

    [Column("SGST_PerM")]
    public double? SgstPerM { get; set; }

    [Column("CGST_PerM")]
    public double? CgstPerM { get; set; }

    [Column("IGST_PerM")]
    public double? IgstPerM { get; set; }

    [Column("DCNote_Cancel")]
    [StringLength(1)]
    [Unicode(false)]
    public string? DcnoteCancel { get; set; }

    [Unicode(false)]
    public string? Remarks { get; set; }

    [Column("Entered_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? EnteredBy { get; set; }

    [Column("MAC_Address")]
    [StringLength(15)]
    [Unicode(false)]
    public string? MacAddress { get; set; }

    [Column("Client_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ClientName { get; set; }

    [InverseProperty("DcnoteNoNavigation")]
    public virtual ICollection<AccDcnoteDetail> AccDcnoteDetails { get; set; } = new List<AccDcnoteDetail>();
}
