using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_QUOTPRICINGDETAIL")]
public partial class CfnQuotpricingdetail
{
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlOnholdno { get; set; }

    [Column("SCHEDULEID")]
    [StringLength(10)]
    [Unicode(false)]
    public string Scheduleid { get; set; } = null!;

    [Column("PRODID")]
    [StringLength(20)]
    [Unicode(false)]
    public string Prodid { get; set; } = null!;

    [Column("PRODUCTDESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Productdescription { get; set; }

    [Column("QUANTITY", TypeName = "numeric(10, 0)")]
    public decimal? Quantity { get; set; }

    [Column("RATE", TypeName = "numeric(12, 4)")]
    public decimal? Rate { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
