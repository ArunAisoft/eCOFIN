using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_BSCSEXPORT")]
public partial class CfnBscsexport
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("EXPENSETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Expensetype { get; set; } = null!;
}
