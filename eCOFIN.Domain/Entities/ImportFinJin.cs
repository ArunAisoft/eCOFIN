using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("IMPORT_FIN_JIN")]
public partial class ImportFinJin
{
    [Column("JINValue_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? JinvalueNo { get; set; }

    [Column("JINValue_Date", TypeName = "datetime")]
    public DateTime? JinvalueDate { get; set; }

    [Column("JIN_No")]
    [StringLength(25)]
    public string JinNo { get; set; } = null!;

    [Column("JIN_Date", TypeName = "datetime")]
    public DateTime? JinDate { get; set; }

    [Column("JIN_Insp_Date", TypeName = "datetime")]
    public DateTime? JinInspDate { get; set; }

    [Column("Vendor_Code")]
    [StringLength(25)]
    public string VendorCode { get; set; } = null!;

    [Column("Ann_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? AnnNo { get; set; }

    [Column("Ann_Date", TypeName = "datetime")]
    public DateTime? AnnDate { get; set; }

    [Column("Credit_Amount")]
    public double CreditAmount { get; set; }

    [Column("Debit_Amount")]
    public double DebitAmount { get; set; }

    [Column("TDS_Code")]
    [StringLength(50)]
    public string TdsCode { get; set; } = null!;

    [Column("TDS_Per")]
    public double TdsPer { get; set; }

    [Column("TDS_Amount")]
    public double? TdsAmount { get; set; }

    [Column("SGST_Value")]
    public double? SgstValue { get; set; }

    [Column("CGST_Value")]
    public double? CgstValue { get; set; }

    [Column("IGST_Value")]
    public double? IgstValue { get; set; }

    [Column("Total_Payable")]
    public double? TotalPayable { get; set; }

    [Column("Total_LandingCost")]
    public double? TotalLandingCost { get; set; }

    [Column("Bill_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? BillNo { get; set; }

    [Column("Bill_Date", TypeName = "datetime")]
    public DateTime? BillDate { get; set; }

    [Column("Payment_DueDate", TypeName = "datetime")]
    public DateTime? PaymentDueDate { get; set; }
}
