using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Depositctrlno", "Depositamendmentno")]
[Table("CFN_HSTDEPOSITINFO")]
public partial class CfnHstdepositinfo
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("DEPOSITCTRLNO")]
    [StringLength(5)]
    [Unicode(false)]
    public string Depositctrlno { get; set; } = null!;

    [Key]
    [Column("DEPOSITAMENDMENTNO")]
    [StringLength(5)]
    [Unicode(false)]
    public string Depositamendmentno { get; set; } = null!;

    [Column("AMENDMENTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Amendmenttype { get; set; }

    [Column("DEPOSITCATEGORY")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Depositcategory { get; set; }

    [Column("DEPOSITTYPE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Deposittype { get; set; }

    [Column("PAYABLETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Payabletype { get; set; }

    [Column("REQUIREDFOR")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Requiredfor { get; set; }

    [Column("DEPOSITINFOSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Depositinfostatus { get; set; }

    [Column("RECEIVEDDATE", TypeName = "datetime")]
    public DateTime? Receiveddate { get; set; }

    [Column("LETTERREFRNO")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Letterrefrno { get; set; }

    [Column("LETTERREFRDATE", TypeName = "datetime")]
    public DateTime? Letterrefrdate { get; set; }

    [Column("FAVOUROF")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Favourof { get; set; }

    [Column("PAYABLEAT")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Payableat { get; set; }

    [Column("PAYTOWARDS")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Paytowards { get; set; }

    [Column("DUEDATE", TypeName = "datetime")]
    public DateTime? Duedate { get; set; }

    [Column("DEPOSITTYPENO")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Deposittypeno { get; set; }

    [Column("OTHERDETAILS")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Otherdetails { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("DEPOSITTYPEDATE", TypeName = "datetime")]
    public DateTime? Deposittypedate { get; set; }

    [Column("DEPOSITAMOUNT", TypeName = "numeric(12, 4)")]
    public decimal? Depositamount { get; set; }

    [Column("BANKERNAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Bankername { get; set; }

    [Column("VALIDITYDATE", TypeName = "datetime")]
    public DateTime? Validitydate { get; set; }

    [Column("EMPPERC", TypeName = "numeric(2, 2)")]
    public decimal? Empperc { get; set; }

    [Column("DEPVALIDITYFROM", TypeName = "datetime")]
    public DateTime? Depvalidityfrom { get; set; }

    [Column("DEPVALIDITYTO", TypeName = "datetime")]
    public DateTime? Depvalidityto { get; set; }

    [Column("DEPREQUIREDDT", TypeName = "datetime")]
    public DateTime? Deprequireddt { get; set; }

    [Column("CLAIMPERIOD", TypeName = "datetime")]
    public DateTime? Claimperiod { get; set; }

    [Column("BANKCOMM", TypeName = "numeric(12, 4)")]
    public decimal? Bankcomm { get; set; }

    [Column("REMARKS")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Remarks { get; set; }
}
