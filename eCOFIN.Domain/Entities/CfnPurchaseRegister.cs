using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_PURCHASE_REGISTER")]
public partial class CfnPurchaseRegister
{
    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("is_selected")]
    [StringLength(10)]
    [Unicode(false)]
    public string IsSelected { get; set; } = null!;
}
