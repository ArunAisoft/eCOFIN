using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Bankdocumentno")]
[Table("CFN_BNKRETDETAIL")]
public partial class CfnBnkretdetail
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("BANKDOCUMENTNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string Bankdocumentno { get; set; } = null!;

    [Column("BILLAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal Billamount { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
