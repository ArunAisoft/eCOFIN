using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accountcode", "Customercode")]
[Table("CFN_ACCCUSTOMER")]
public partial class CfnAcccustomer
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Key]
    [Column("CUSTOMERCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Customercode { get; set; } = null!;

    [Column("CUSTOMERSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Customerstatus { get; set; }
}
