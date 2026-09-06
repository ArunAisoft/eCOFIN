using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_purchase_register_bilzusers")]
public partial class CfnPurchaseRegisterBilzuser
{
    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("is_selected")]
    [StringLength(1)]
    [Unicode(false)]
    public string? IsSelected { get; set; }
}
