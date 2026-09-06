using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Username", "Parametergroup", "Parametercode")]
[Table("CFN_USERVCHRSYSCAT")]
public partial class CfnUservchrsyscat
{
    [Key]
    [Column("USERNAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Key]
    [Column("PARAMETERGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string Parametergroup { get; set; } = null!;

    [Key]
    [Column("PARAMETERCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Parametercode { get; set; } = null!;
}
