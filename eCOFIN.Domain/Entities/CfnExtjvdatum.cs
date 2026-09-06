using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlVchrno", "CtrlSequenceno")]
[Table("CFN_EXTJVDATA")]
public partial class CfnExtjvdatum
{
    [Key]
    [Column("CTRL_VCHRNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlVchrno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string Dbcrflag { get; set; } = null!;

    [Column("AMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Amount { get; set; }

    [Column("NARRATION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Narration { get; set; }

    [Column("UPDATEFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Updateflag { get; set; }

    [Column("DATACATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Datacategory { get; set; }
}
