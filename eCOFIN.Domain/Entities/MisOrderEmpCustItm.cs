using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class MisOrderEmpCustItm
{
    [Column("Period_Year")]
    public int? PeriodYear { get; set; }

    [Column("Period_Month")]
    public int? PeriodMonth { get; set; }

    [Column("Sales_Eng")]
    [StringLength(5)]
    public string SalesEng { get; set; } = null!;

    [StringLength(20)]
    public string? Custcode { get; set; }

    [Column("Article_No")]
    [StringLength(12)]
    public string? ArticleNo { get; set; }

    [Column("Order_Value")]
    public double? OrderValue { get; set; }
}
