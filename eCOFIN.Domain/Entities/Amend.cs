using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("amend")]
public partial class Amend
{
    [StringLength(20)]
    public string? Ordno { get; set; }

    [StringLength(12)]
    [Unicode(false)]
    public string? Artno { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Amendt { get; set; }

    public int? Amendqty { get; set; }

    [Column("cartno")]
    [StringLength(50)]
    public string? Cartno { get; set; }

    [Column("oadt", TypeName = "datetime")]
    public DateTime? Oadt { get; set; }

    [Column("qty")]
    public int? Qty { get; set; }

    [Column("pono")]
    [StringLength(50)]
    public string? Pono { get; set; }

    [Column("slno")]
    public int? Slno { get; set; }

    [Column("OA_Due_Date", TypeName = "datetime")]
    public DateTime? OaDueDate { get; set; }

    [Column("Oa_Des_Date", TypeName = "datetime")]
    public DateTime? OaDesDate { get; set; }

    [Column("Air_Qty")]
    public int? AirQty { get; set; }

    [Column("Sea_Qty")]
    public int? SeaQty { get; set; }

    [Column("Order_DispatchFrom")]
    [StringLength(2)]
    [Unicode(false)]
    public string? OrderDispatchFrom { get; set; }

    [Column("Amend_price")]
    public double? AmendPrice { get; set; }

    [Column("OA_Price")]
    public double? OaPrice { get; set; }

    [Column("Cust_Code")]
    [StringLength(25)]
    public string? CustCode { get; set; }

    [StringLength(300)]
    public string? Remarks { get; set; }

    [Column("Qty_Amend")]
    public int? QtyAmend { get; set; }

    [Column("New_OA_No")]
    [StringLength(25)]
    public string? NewOaNo { get; set; }

    [Column("Act_OA_No")]
    [StringLength(25)]
    public string? ActOaNo { get; set; }

    public double? Discount { get; set; }

    [Column("Amended_Date", TypeName = "datetime")]
    public DateTime? AmendedDate { get; set; }

    [Column("Amended_By")]
    [StringLength(25)]
    public string? AmendedBy { get; set; }
}
