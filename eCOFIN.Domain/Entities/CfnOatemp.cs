using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_OATEMP")]
public partial class CfnOatemp
{
    [Column("OALINESTRING")]
    [StringLength(80)]
    [Unicode(false)]
    public string? Oalinestring { get; set; }

    [Column("SLNO")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Slno { get; set; }
}
