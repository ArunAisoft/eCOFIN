using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accperiod", "VchrNumber", "JvCtrlOnholdno")]
[Table("CFN_TRANSLATION_BILL")]
public partial class CfnTranslationBill
{
    [Key]
    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Key]
    [Column("VCHR_NUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string VchrNumber { get; set; } = null!;

    [Key]
    [Column("JV_CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string JvCtrlOnholdno { get; set; } = null!;

    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("BILLNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Billno { get; set; }

    [Column("BILLDATE", TypeName = "datetime")]
    public DateTime? Billdate { get; set; }

    [Column("BILLAMOUNT", TypeName = "decimal(14, 2)")]
    public decimal? Billamount { get; set; }

    [Column("BILLBALANCE", TypeName = "decimal(14, 2)")]
    public decimal? Billbalance { get; set; }

    [Column("TRANSLATION_AMT", TypeName = "decimal(14, 2)")]
    public decimal? TranslationAmt { get; set; }

    [Column("AP_AR_TYPE")]
    [StringLength(2)]
    [Unicode(false)]
    public string? ApArType { get; set; }

    [Column("SUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }
}
