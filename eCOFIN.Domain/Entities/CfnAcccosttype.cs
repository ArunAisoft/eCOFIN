using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accountcode", "Costtype")]
[Table("CFN_ACCCOSTTYPE")]
public partial class CfnAcccosttype
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Key]
    [Column("COSTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Costtype { get; set; } = null!;
}
