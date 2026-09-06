using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class MisTotalEmpCustItm
{
    [Column("Period_Year")]
    public int? PeriodYear { get; set; }

    [Column("Period_Month")]
    public int? PeriodMonth { get; set; }

    [Column("Emp_No")]
    [StringLength(5)]
    public string EmpNo { get; set; } = null!;

    [Column("Emp_Name")]
    [StringLength(50)]
    public string? EmpName { get; set; }

    [Column("Cust_Code")]
    [StringLength(20)]
    public string? CustCode { get; set; }

    [Column("Cust_Name")]
    [StringLength(50)]
    public string? CustName { get; set; }

    [Column("Ven_Code")]
    [StringLength(50)]
    public string? VenCode { get; set; }

    [Column("Industry_Type")]
    [StringLength(50)]
    public string IndustryType { get; set; } = null!;

    [Column("Zone_Cat")]
    [StringLength(50)]
    public string ZoneCat { get; set; } = null!;

    [StringLength(50)]
    public string Zone { get; set; } = null!;

    [Column("Article_No")]
    [StringLength(12)]
    public string? ArticleNo { get; set; }

    [Column("Item_No")]
    [StringLength(10)]
    public string ItemNo { get; set; } = null!;

    [Column("Item_Grp")]
    [StringLength(50)]
    public string ItemGrp { get; set; } = null!;

    [Column("Item_Cat")]
    [StringLength(50)]
    public string? ItemCat { get; set; }

    [Column("Target_Value")]
    public double? TargetValue { get; set; }

    [Column("Order_Value")]
    public double? OrderValue { get; set; }

    [Column("Invoice_Value")]
    public double? InvoiceValue { get; set; }
}
