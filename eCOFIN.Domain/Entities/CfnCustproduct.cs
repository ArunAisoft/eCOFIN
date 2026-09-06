using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Customercode", "Productcode")]
[Table("CFN_CUSTPRODUCT")]
public partial class CfnCustproduct
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

    [Column("CREDITLIMITAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Creditlimitamount { get; set; }

    [Column("TOTALOUTSTANDINGAMT", TypeName = "numeric(14, 2)")]
    public decimal? Totaloutstandingamt { get; set; }

    [Column("TOTALCOLLECTIONAMT", TypeName = "numeric(14, 2)")]
    public decimal? Totalcollectionamt { get; set; }

    [Column("ASSOCIATEDSALESPERSON")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Associatedsalesperson { get; set; }

    [Column("CREDITDAYS", TypeName = "numeric(3, 0)")]
    public decimal? Creditdays { get; set; }

    [Column("PRODUCTPRICE", TypeName = "numeric(12, 4)")]
    public decimal? Productprice { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("PARTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Partcode { get; set; }
}
