using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_bal_confirm")]
public partial class CfnBalConfirm
{
    [Column("letter_format")]
    [StringLength(8000)]
    [Unicode(false)]
    public string? LetterFormat { get; set; }
}
