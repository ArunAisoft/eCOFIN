using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accountcode", "Costcentrecode")]
[Table("CFN_ACCCOSTCENTRE")]
public partial class CfnAcccostcentre
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Key]
    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Costcentrecode { get; set; } = null!;

    [Column("COSTCENTRESTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrestatus { get; set; }
}
