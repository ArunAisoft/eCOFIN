using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Amendmentnumber")]
[Table("CFN_BTLHSTDEPOSITHD")]
public partial class CfnBtlhstdeposithd
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("DEPOSIT_TYPE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? DepositType { get; set; }

    [Column("PAYABLE_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? PayableType { get; set; }

    [Column("DEPOSIT_CTRLNO")]
    [StringLength(5)]
    [Unicode(false)]
    public string? DepositCtrlno { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Key]
    [Column("AMENDMENTNUMBER")]
    [StringLength(3)]
    [Unicode(false)]
    public string Amendmentnumber { get; set; } = null!;

    [Column("REQUIRED_FOR")]
    [StringLength(5)]
    [Unicode(false)]
    public string? RequiredFor { get; set; }
}
