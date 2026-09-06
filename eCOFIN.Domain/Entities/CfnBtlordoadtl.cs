using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Locationcode")]
[Table("CFN_BTLORDOADTL")]
public partial class CfnBtlordoadtl
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("ORDACCUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ordaccumber { get; set; }

    [Key]
    [Column("LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Locationcode { get; set; } = null!;

    [Column("SPECIALINSTRUCTION")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Specialinstruction { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
