using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Username", "Warehousecode")]
[Table("CFN_USERWAREHOUSE")]
public partial class CfnUserwarehouse
{
    [Key]
    [Column("USERNAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Key]
    [Column("WAREHOUSECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Warehousecode { get; set; } = null!;

    [Column("OBJECTSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Objectstatus { get; set; }
}
