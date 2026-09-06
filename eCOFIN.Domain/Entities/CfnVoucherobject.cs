using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_VOUCHEROBJECT")]
public partial class CfnVoucherobject
{
    [Key]
    [Column("TASKID")]
    [StringLength(3)]
    [Unicode(false)]
    public string Taskid { get; set; } = null!;

    [Column("DATAOBJECT")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Dataobject { get; set; }

    [Column("DETAILOBJECT")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Detailobject { get; set; }
}
