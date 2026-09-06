using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Username", "Costcentrecode")]
[Table("CFN_USERCOSTCENTRE")]
public partial class CfnUsercostcentre
{
    [Key]
    [Column("USERNAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Key]
    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Costcentrecode { get; set; } = null!;
}
