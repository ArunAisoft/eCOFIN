using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("VchrNumber", "Sequenceno")]
[Table("CFN_PURCHASEPRINT")]
public partial class CfnPurchaseprint
{
    [Key]
    [Column("VCHR_NUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string VchrNumber { get; set; } = null!;

    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("VCHR_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrType { get; set; }

    [Column("BILLNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Billno { get; set; }

    [Column("BILLDATE", TypeName = "datetime")]
    public DateTime? Billdate { get; set; }

    [Column("GRINNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Grinno { get; set; }

    [Column("GRINDATE", TypeName = "datetime")]
    public DateTime? Grindate { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("SUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("SUBDESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Subdescription { get; set; }

    [Column("EXPENSEACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Expenseaccountcode { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("VCHR_NARRATION")]
    [StringLength(400)]
    [Unicode(false)]
    public string? VchrNarration { get; set; }

    [Column("BILLAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Billamount { get; set; }

    [Column("ED", TypeName = "numeric(14, 2)")]
    public decimal? Ed { get; set; }

    [Column("TDS", TypeName = "numeric(14, 2)")]
    public decimal? Tds { get; set; }

    [Column("TOTAL", TypeName = "numeric(14, 2)")]
    public decimal? Total { get; set; }

    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("EXPENSETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Key]
    [Column("SEQUENCENO", TypeName = "numeric(5, 0)")]
    public decimal Sequenceno { get; set; }

    [Column("TOTALAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Totalamount { get; set; }
}
