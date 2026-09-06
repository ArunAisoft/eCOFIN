using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Locationcode", "Userid")]
[Table("CFN_CCMAIL")]
public partial class CfnCcmail
{
    [Key]
    [Column("LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Locationcode { get; set; } = null!;

    [Key]
    [Column("USERID")]
    [StringLength(15)]
    [Unicode(false)]
    public string Userid { get; set; } = null!;

    [Column("USERPASSWORD")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Userpassword { get; set; }

    [Column("POSTOFFICE")]
    [StringLength(15)]
    [Unicode(false)]
    public string Postoffice { get; set; } = null!;
}
