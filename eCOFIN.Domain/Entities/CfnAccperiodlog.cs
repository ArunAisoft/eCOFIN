using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_accperiodlog")]
public partial class CfnAccperiodlog
{
    [Column("accperiod")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("time_of_operation", TypeName = "datetime")]
    public DateTime? TimeOfOperation { get; set; }

    [Column("username")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Username { get; set; }

    [Column("curr_state")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CurrState { get; set; }

    [Column("reason")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Reason { get; set; }

    [Column("prev_status")]
    [StringLength(20)]
    [Unicode(false)]
    public string? PrevStatus { get; set; }
}
