using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Productcode", "Warehousecode")]
[Table("CFN_PRODUCTWAREHOUSE")]
public partial class CfnProductwarehouse
{
    [Key]
    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Key]
    [Column("WAREHOUSECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Warehousecode { get; set; } = null!;

    [Column("OBJECTSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Objectstatus { get; set; }
}
