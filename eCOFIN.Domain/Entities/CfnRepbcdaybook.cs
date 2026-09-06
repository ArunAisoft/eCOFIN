using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("VchrDate", "BankCashAc")]
[Table("CFN_REPBCDAYBOOK")]
public partial class CfnRepbcdaybook
{
    [Key]
    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime VchrDate { get; set; }

    [Key]
    [Column("BANK_CASH_AC")]
    [StringLength(10)]
    [Unicode(false)]
    public string BankCashAc { get; set; } = null!;

    [Column("BANK_CASH_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? BankCashType { get; set; }

    [Column("OB_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? ObAmnt { get; set; }

    [Column("OB_DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? ObDbcrflag { get; set; }

    [Column("CB_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? CbAmnt { get; set; }

    [Column("CB_DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcrflag { get; set; }

    [Column("DEBIT_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? DebitAmnt { get; set; }

    [Column("CREDIT_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? CreditAmnt { get; set; }
}
