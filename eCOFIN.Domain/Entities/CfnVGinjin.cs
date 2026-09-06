using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVGinjin
{
    [Column("gin_jin_number")]
    [StringLength(35)]
    [Unicode(false)]
    public string? GinJinNumber { get; set; }

    [Column("cheque_date", TypeName = "datetime")]
    public DateTime? ChequeDate { get; set; }
}
