using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_GENHELP")]
public partial class CfnGenhelp
{
    //[Key]
    [Column("HELPID")]
    [StringLength(5)]
    [Unicode(false)]
    public string Helpid { get; set; } = null!;

    [Column("REGFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string Regflag { get; set; } = null!;

    [Column("HELPTOPIC")]
    [StringLength(30)]
    [Unicode(false)]
    public string Helptopic { get; set; } = null!;

    [Column("HELPOBJECTNAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Helpobjectname { get; set; }

    [Column("HELPUPDATEABLECOLUMN")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Helpupdateablecolumn { get; set; }

    [Column("TABLENAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Tablename { get; set; }

    [Column("DESTCOLUMN")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Destcolumn { get; set; }
}
