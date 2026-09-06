using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVPlaccountmem
{
    [Column("FORMATCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Formatcode { get; set; } = null!;

    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("FOR_MONTH", TypeName = "numeric(38, 2)")]
    public decimal? ForMonth { get; set; }

    [Column("UPTO_MONTH", TypeName = "numeric(38, 2)")]
    public decimal? UptoMonth { get; set; }

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("GROUPCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Groupcode { get; set; } = null!;

    [Column("GROUPDESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Groupdescription { get; set; }

    [Column("SCHEDULECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Schedulecode { get; set; } = null!;

    [Column("SCHEDULEDESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Scheduledescription { get; set; }

    [Column("NATUREOFACCOUNT")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Natureofaccount { get; set; }

    [Column("NATUREDESCRIPTION")]
    [StringLength(11)]
    [Unicode(false)]
    public string? Naturedescription { get; set; }

    [Column("PL_BS")]
    [StringLength(2)]
    [Unicode(false)]
    public string? PlBs { get; set; }
}
