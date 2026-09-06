using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Parametergroup", "Parametercode")]
[Table("CFN_CFPARVALUE")]
public partial class CfnCfparvalue
{
    [Key]
    [Column("PARAMETERGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string Parametergroup { get; set; } = null!;

    [Key]
    [Column("PARAMETERCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Parametercode { get; set; } = null!;

    [Column("PARAMETERDESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Parameterdescription { get; set; }

    [Column("ACTIVESTATUS")]
    [StringLength(1)]
    [Unicode(false)]
    public string Activestatus { get; set; } = null!;

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
