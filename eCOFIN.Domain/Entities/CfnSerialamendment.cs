using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_SERIALAMENDMENT")]
public partial class CfnSerialamendment
{
    [Key]
    [Column("SLNO", TypeName = "numeric(5, 0)")]
    public decimal Slno { get; set; }

    [Column("SERIALNUMBER")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Serialnumber { get; set; }

    [Column("AMENDSERIALNUMBER")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Amendserialnumber { get; set; }
}
