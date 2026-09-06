using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Warehousecode", "Stocktype")]
[Table("CFN_WHSTOCKTYPELINK")]
public partial class CfnWhstocktypelink
{
    [Key]
    [Column("WAREHOUSECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Warehousecode { get; set; } = null!;

    [Key]
    [Column("STOCKTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Stocktype { get; set; } = null!;

    [Column("OBJECTSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string Objectstatus { get; set; } = null!;

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
