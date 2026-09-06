using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Accountcode", "Accperiod")]
[Table("CFN_REPGENERALLEDGER")]
public partial class CfnRepgeneralledger
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("OB_DEBIT", TypeName = "numeric(14, 2)")]
    public decimal? ObDebit { get; set; }

    [Column("OB_CREDIT", TypeName = "numeric(14, 2)")]
    public decimal? ObCredit { get; set; }

    [Column("CB_DEBIT", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit { get; set; }

    [Column("CB_CREDIT", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit { get; set; }

    [Key]
    [Column("ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("DEBIT_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? DebitAmnt { get; set; }

    [Column("CREDIT_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? CreditAmnt { get; set; }

    [Column("YEAR_CREDIT_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? YearCreditAmnt { get; set; }

    [Column("YEAR_DEBIT_AMNT", TypeName = "numeric(14, 2)")]
    public decimal? YearDebitAmnt { get; set; }
}
