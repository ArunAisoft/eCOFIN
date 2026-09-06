using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accountcode", "Vendorcode")]
[Table("CFN_ACCVENDOR")]
public partial class CfnAccvendor
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Key]
    [Column("VENDORCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Vendorcode { get; set; } = null!;

    [Column("VENDORSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Vendorstatus { get; set; }
}
