using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_accountlink")]
public partial class CfnAccountlink
{
    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("username")]
    [StringLength(10)]
    [Unicode(false)]
    public string Username { get; set; } = null!;
}
