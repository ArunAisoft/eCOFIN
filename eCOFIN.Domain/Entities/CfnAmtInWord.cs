using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_AMT_IN_WORDS")]
public partial class CfnAmtInWord
{
    [Key]
    [Column("VALUE", TypeName = "numeric(12, 0)")]
    public decimal Value { get; set; }

    [Column("WORDS")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Words { get; set; }
}
