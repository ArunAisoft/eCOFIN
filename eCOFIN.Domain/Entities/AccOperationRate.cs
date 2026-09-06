using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("ArticleNo", "OperationName")]
[Table("Acc_OperationRate")]
public partial class AccOperationRate
{
    [Key]
    [Column("Article_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string ArticleNo { get; set; } = null!;

    [Key]
    [Column("Operation_Name")]
    [StringLength(30)]
    [Unicode(false)]
    public string OperationName { get; set; } = null!;

    public double? Rate { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Remarks { get; set; }
}
