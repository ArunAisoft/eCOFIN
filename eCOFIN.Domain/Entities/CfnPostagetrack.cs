using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_POSTAGETRACK")]
public partial class CfnPostagetrack
{
    [Key]
    [Column("PONUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string Ponumber { get; set; } = null!;

    [Column("POSTAGE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Postage { get; set; }

    [Column("NARRATION")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Narration { get; set; }

    [Column("BILLNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Billno { get; set; }

    [Column("BILLDATE", TypeName = "datetime")]
    public DateTime? Billdate { get; set; }
}
