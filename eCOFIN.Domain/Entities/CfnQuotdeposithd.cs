using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_QUOTDEPOSITHD")]
public partial class CfnQuotdeposithd
{
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlOnholdno { get; set; }

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
}
