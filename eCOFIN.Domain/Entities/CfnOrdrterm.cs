using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Amendmentnumber", "Termcode")]
[Table("CFN_ORDRTERM")]
public partial class CfnOrdrterm
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("AMENDMENTNUMBER")]
    [StringLength(3)]
    [Unicode(false)]
    public string Amendmentnumber { get; set; } = null!;

    [Key]
    [Column("TERMCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Termcode { get; set; } = null!;

    [Column("TERMDESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Termdescription { get; set; }

    [Column("TERMVALIDVALUE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Termvalidvalue { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
