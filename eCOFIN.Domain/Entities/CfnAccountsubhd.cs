using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Subaccountcode", "Accountcode", "Accperiod")]
[Table("CFN_ACCOUNTSUBHD")]
public partial class CfnAccountsubhd
{
    [Key]
    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("OPENINGBALANCE", TypeName = "numeric(14, 2)")]
    public decimal Openingbalance { get; set; }

    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

    [Column("CLOSINGBALANCE", TypeName = "numeric(14, 2)")]
    public decimal Closingbalance { get; set; }

    [Column("STATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Status { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Key]
    [Column("SUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Subaccountcode { get; set; } = null!;
}
