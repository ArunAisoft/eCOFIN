using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accountcode", "Vouchergroup", "Vouchertype")]
[Table("CFN_VOUCHERACCOUNTS")]
public partial class CfnVoucheraccount
{
    [Key]
    [Column("VOUCHERGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string Vouchergroup { get; set; } = null!;

    [Key]
    [Column("VOUCHERTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Vouchertype { get; set; } = null!;

    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("STATUS")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Status { get; set; }

    [Column("NATUREOFACCOUNT")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Natureofaccount { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
