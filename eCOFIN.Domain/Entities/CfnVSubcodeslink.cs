using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVSubcodeslink
{
    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string Dbcrflag { get; set; } = null!;

    [Column("SUBCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Subcode { get; set; } = null!;

    [Column("SUBCODEDESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Subcodedescription { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;
}
