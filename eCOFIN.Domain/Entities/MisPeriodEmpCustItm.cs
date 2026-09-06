using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class MisPeriodEmpCustItm
{
    [Column("Period_Year")]
    public int? PeriodYear { get; set; }

    [Column("Period_Month")]
    public int? PeriodMonth { get; set; }

    [StringLength(5)]
    public string EmpNo { get; set; } = null!;

    [StringLength(20)]
    public string? CustCode { get; set; }

    [Column("Article_No")]
    [StringLength(12)]
    public string? ArticleNo { get; set; }

    [Column("Parent_Code")]
    [StringLength(10)]
    public string ParentCode { get; set; } = null!;

    [Column("Group_Name")]
    [StringLength(50)]
    public string GroupName { get; set; } = null!;

    [StringLength(50)]
    public string? Category { get; set; }
}
