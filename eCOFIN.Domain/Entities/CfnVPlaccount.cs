using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVPlaccount
{
    [Column("formatcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Formatcode { get; set; } = null!;

    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("for_month", TypeName = "numeric(38, 2)")]
    public decimal? ForMonth { get; set; }

    [Column("upto_month", TypeName = "numeric(38, 2)")]
    public decimal? UptoMonth { get; set; }

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("groupcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Groupcode { get; set; } = null!;

    [Column("groupdescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Groupdescription { get; set; }

    [Column("schedulecode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Schedulecode { get; set; } = null!;

    [Column("scheduledescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Scheduledescription { get; set; }

    [Column("NATUREOFACCOUNT")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Natureofaccount { get; set; }

    [Column("naturedescription")]
    [StringLength(11)]
    [Unicode(false)]
    public string? Naturedescription { get; set; }

    [Column("pl_bs")]
    [StringLength(2)]
    [Unicode(false)]
    public string? PlBs { get; set; }
}
