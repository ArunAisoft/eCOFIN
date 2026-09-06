using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Username", "Vouchergroup", "Vouchertype")]
[Table("CFN_USERVCHR")]
public partial class CfnUservchr
{
    [Key]
    [Column("USERNAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Key]
    [Column("VOUCHERGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string Vouchergroup { get; set; } = null!;

    [Key]
    [Column("VOUCHERTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Vouchertype { get; set; } = null!;
}
