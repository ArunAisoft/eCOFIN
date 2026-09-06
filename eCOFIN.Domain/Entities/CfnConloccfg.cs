using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_CONLOCCFG")]
public partial class CfnConloccfg
{
    [Key]
    [Column("LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Locationcode { get; set; } = null!;

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
