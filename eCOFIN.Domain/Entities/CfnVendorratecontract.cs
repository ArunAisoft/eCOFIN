using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Vendorcode", "Rcfromperiod", "Rctoperiod")]
[Table("CFN_VENDORRATECONTRACT")]
public partial class CfnVendorratecontract
{
    [Key]
    [Column("VENDORCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Vendorcode { get; set; } = null!;

    [Key]
    [Column("RCFROMPERIOD", TypeName = "datetime")]
    public DateTime Rcfromperiod { get; set; }

    [Key]
    [Column("RCTOPERIOD", TypeName = "datetime")]
    public DateTime Rctoperiod { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("CUMULATIVEQTY", TypeName = "numeric(10, 0)")]
    public decimal? Cumulativeqty { get; set; }
}
