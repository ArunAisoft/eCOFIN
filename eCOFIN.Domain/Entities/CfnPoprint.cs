using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_POPRINT")]
public partial class CfnPoprint
{
    [Column("PRODUCTDESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Productdescription { get; set; }

    [Column("QUANTITY", TypeName = "numeric(10, 0)")]
    public decimal? Quantity { get; set; }

    [Column("UOM")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Uom { get; set; }

    [Column("RATE", TypeName = "numeric(13, 2)")]
    public decimal? Rate { get; set; }

    [Column("POVALUE", TypeName = "numeric(13, 2)")]
    public decimal? Povalue { get; set; }

    [Column("SLNO", TypeName = "numeric(5, 0)")]
    public decimal? Slno { get; set; }

    [Column("CTRL_SEQUENCENO", TypeName = "numeric(5, 0)")]
    public decimal? CtrlSequenceno { get; set; }
}
