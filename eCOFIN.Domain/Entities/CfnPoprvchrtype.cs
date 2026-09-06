using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Povchrtype", "Prvchrtype")]
[Table("CFN_POPRVCHRTYPE")]
public partial class CfnPoprvchrtype
{
    [Key]
    [Column("POVCHRTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Povchrtype { get; set; } = null!;

    [Key]
    [Column("PRVCHRTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Prvchrtype { get; set; } = null!;
}
