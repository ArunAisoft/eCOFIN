using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Locationcode", "Associatedlocation")]
[Table("CFN_LOCNASSOCIATED")]
public partial class CfnLocnassociated
{
    [Key]
    [Column("LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Locationcode { get; set; } = null!;

    [Key]
    [Column("ASSOCIATEDLOCATION")]
    [StringLength(5)]
    [Unicode(false)]
    public string Associatedlocation { get; set; } = null!;

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
