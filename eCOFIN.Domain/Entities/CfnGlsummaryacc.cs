using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accountcode", "Vchrtype")]
[Table("CFN_GLSUMMARYACC")]
public partial class CfnGlsummaryacc
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Key]
    [Column("VCHRTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Vchrtype { get; set; } = null!;

    [Column("SUMMARY")]
    [StringLength(1)]
    [Unicode(false)]
    public string Summary { get; set; } = null!;

    [Column("VOUCHERTYPEDESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Vouchertypedescription { get; set; }
}
