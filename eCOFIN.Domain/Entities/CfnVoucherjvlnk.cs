using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdvoucherno", "CtrlOnholdautomatedjv")]
[Table("CFN_VOUCHERJVLNK")]
public partial class CfnVoucherjvlnk
{
    [Key]
    [Column("CTRL_ONHOLDVOUCHERNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdvoucherno { get; set; } = null!;

    [Key]
    [Column("CTRL_ONHOLDAUTOMATEDJV")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdautomatedjv { get; set; } = null!;

    [Column("VCHR_VOUCHERNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrVoucherno { get; set; }

    [Column("VCHR_AUTOMATEDJV")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrAutomatedjv { get; set; }
}
