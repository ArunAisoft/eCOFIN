using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVCfparcode
{
    [Column("PARAMETERGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string Parametergroup { get; set; } = null!;

    [Column("PARAMETERCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Parametercode { get; set; } = null!;

    [Column("PARAMETERDESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Parameterdescription { get; set; }
}
