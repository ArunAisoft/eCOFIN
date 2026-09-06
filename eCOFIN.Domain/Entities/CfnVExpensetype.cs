using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVExpensetype
{
    [Column("parametercode")]
    [StringLength(5)]
    [Unicode(false)]
    public string Parametercode { get; set; } = null!;

    [Column("parameterdescription")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Parameterdescription { get; set; }
}
