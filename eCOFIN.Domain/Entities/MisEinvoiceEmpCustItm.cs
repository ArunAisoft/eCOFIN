using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class MisEinvoiceEmpCustItm
{
    [Column("Period_Year")]
    public int? PeriodYear { get; set; }

    [Column("Period_Month")]
    public int? PeriodMonth { get; set; }

    [Column("Sales_Eng")]
    [StringLength(5)]
    public string SalesEng { get; set; } = null!;

    [Column("cust_code")]
    [StringLength(255)]
    [Unicode(false)]
    public string? CustCode { get; set; }

    [Column("Article_No")]
    [StringLength(12)]
    public string ArticleNo { get; set; } = null!;

    [Column("Invoice_Value")]
    public double? InvoiceValue { get; set; }
}
