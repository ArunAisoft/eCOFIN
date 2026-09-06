using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_CONACCFG")]
public partial class CfnConaccfg
{
    [Key]
    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("COLNAMCREDIT")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Colnamcredit { get; set; }

    [Column("COLNAMDEBIT")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Colnamdebit { get; set; }

    [Column("COLNAMDBCR")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Colnamdbcr { get; set; }
}
