using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_INVORDERRELATED")]
public partial class CfnInvorderrelated
{
    [Key]
    [Column("SERIALNO", TypeName = "numeric(4, 0)")]
    public decimal Serialno { get; set; }

    [Column("INVOICENO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Invoiceno { get; set; }

    [Column("ORDERCTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? OrderctrlOnholdno { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("PRODUCTQUANTITY", TypeName = "numeric(10, 0)")]
    public decimal? Productquantity { get; set; }

    [Column("BILLEDQUANTITY", TypeName = "numeric(10, 0)")]
    public decimal? Billedquantity { get; set; }

    [Column("SALESCUSTOMERCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Salescustomercode { get; set; }

    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal? Productid { get; set; }

    [Column("SCHEDULEID", TypeName = "numeric(5, 0)")]
    public decimal? Scheduleid { get; set; }
}
