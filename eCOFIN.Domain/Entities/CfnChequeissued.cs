using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_CHEQUEISSUED")]
public partial class CfnChequeissued
{
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("VCHR_NUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("BANKCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Bankcode { get; set; } = null!;

    [Column("PARTYCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Partycode { get; set; }

    [Column("FAVOUROF")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Favourof { get; set; }

    [Column("BANKACCOUNT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Bankaccount { get; set; }

    [Column("INSTRUMENTBOOKNO")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrumentbookno { get; set; }

    [Column("INSTRUMENTNO")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("INSTRUMENTDATE", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("AMOUNT", TypeName = "numeric(14, 2)")]
    public decimal Amount { get; set; }

    [Column("CHEQUESTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Chequestatus { get; set; }
}
