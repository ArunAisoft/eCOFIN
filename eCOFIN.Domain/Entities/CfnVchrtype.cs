using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Vouchergroup", "Vouchertype")]
[Table("CFN_VCHRTYPE")]
public partial class CfnVchrtype
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

    [Column("VOUCHERTYPEDESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Vouchertypedescription { get; set; }

    [Column("ACTIVESTATUS")]
    [StringLength(1)]
    [Unicode(false)]
    public string Activestatus { get; set; } = null!;

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
