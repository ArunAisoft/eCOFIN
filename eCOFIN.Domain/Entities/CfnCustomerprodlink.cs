using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Customercode", "Productcode", "Customerproductcode")]
[Table("CFN_CUSTOMERPRODLINK")]
public partial class CfnCustomerprodlink
{
    [Key]
    [Column("CUSTOMERCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Customercode { get; set; } = null!;

    [Key]
    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Key]
    [Column("CUSTOMERPRODUCTCODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string Customerproductcode { get; set; } = null!;
}
