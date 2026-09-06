using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Sourcelocation", "Batchnumber", "Targetlocation")]
[Table("CFN_APPLOGCONTROL")]
public partial class CfnApplogcontrol
{
    [Key]
    [Column("SOURCELOCATION")]
    [StringLength(5)]
    [Unicode(false)]
    public string Sourcelocation { get; set; } = null!;

    [Key]
    [Column("BATCHNUMBER")]
    [StringLength(10)]
    [Unicode(false)]
    public string Batchnumber { get; set; } = null!;

    [Column("EXECUTIONSTATUS")]
    [StringLength(1)]
    [Unicode(false)]
    public string Executionstatus { get; set; } = null!;

    [Key]
    [Column("TARGETLOCATION")]
    [StringLength(5)]
    [Unicode(false)]
    public string Targetlocation { get; set; } = null!;

    [Column("LOGINOUT")]
    [StringLength(5)]
    [Unicode(false)]
    public string Loginout { get; set; } = null!;
}
