using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accountcode", "Employeecode")]
[Table("CFN_ACCEMPLOYEE")]
public partial class CfnAccemployee
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Key]
    [Column("EMPLOYEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Employeecode { get; set; } = null!;

    [Column("EMPLOYEESTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Employeestatus { get; set; }
}
