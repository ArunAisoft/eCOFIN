using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Specificationcode", "Productcode")]
[Table("CFN_PRODSPECLINK")]
public partial class CfnProdspeclink
{
    [Key]
    [Column("SPECIFICATIONCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Specificationcode { get; set; } = null!;

    [Key]
    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Column("SPECIFICATIONSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Specificationstatus { get; set; }

    [Column("CTRL_NEXTREFRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlNextrefrflag { get; set; }

    [Column("OBJECTSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Objectstatus { get; set; }

    [Column("CTRL_PREVREFR")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlPrevrefr { get; set; }
}
