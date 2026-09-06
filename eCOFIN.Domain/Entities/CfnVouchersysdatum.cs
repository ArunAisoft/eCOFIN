using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Code", "Vouchertype", "Vouchergroup")]
[Table("CFN_VOUCHERSYSDATA")]
public partial class CfnVouchersysdatum
{
    [Key]
    [Column("CODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Key]
    [Column("VOUCHERTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Vouchertype { get; set; } = null!;

    [Key]
    [Column("VOUCHERGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string Vouchergroup { get; set; } = null!;
}
