using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_REPCONTB")]
public partial class CfnRepcontb
{
    [Key]
    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("DESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("CB_CREDIT1", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit1 { get; set; }

    [Column("CB_DEBIT1", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit1 { get; set; }

    [Column("CB_DBCR1")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr1 { get; set; }

    [Column("CB_CREDIT2", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit2 { get; set; }

    [Column("CB_DEBIT2", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit2 { get; set; }

    [Column("CB_DBCR2")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr2 { get; set; }

    [Column("CB_CREDIT3", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit3 { get; set; }

    [Column("CB_DEBIT3", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit3 { get; set; }

    [Column("CB_DBCR3")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr3 { get; set; }

    [Column("CB_CREDIT4", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit4 { get; set; }

    [Column("CB_DEBIT4", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit4 { get; set; }

    [Column("CB_DBCR4")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr4 { get; set; }

    [Column("CB_CREDIT5", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit5 { get; set; }

    [Column("CB_DEBIT5", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit5 { get; set; }

    [Column("CB_DBCR5")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr5 { get; set; }

    [Column("CB_CREDIT6", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit6 { get; set; }

    [Column("CB_DEBIT6", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit6 { get; set; }

    [Column("CB_DBCR6")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr6 { get; set; }

    [Column("CB_CREDIT7", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit7 { get; set; }

    [Column("CB_DEBIT7", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit7 { get; set; }

    [Column("CB_DBCR7")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr7 { get; set; }

    [Column("CB_CREDIT8", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit8 { get; set; }

    [Column("CB_DEBIT8", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit8 { get; set; }

    [Column("CB_DBCR8")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr8 { get; set; }

    [Column("CB_CREDIT9", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit9 { get; set; }

    [Column("CB_DEBIT9", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit9 { get; set; }

    [Column("CB_DBCR9")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr9 { get; set; }

    [Column("CB_CREDIT10", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit10 { get; set; }

    [Column("CB_DEBIT10", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit10 { get; set; }

    [Column("CB_DBCR10")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr10 { get; set; }

    [Column("CB_CREDIT11", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit11 { get; set; }

    [Column("CB_DEBIT11", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit11 { get; set; }

    [Column("CB_DBCR11")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr11 { get; set; }

    [Column("CB_CREDIT12", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit12 { get; set; }

    [Column("CB_DEBIT12", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit12 { get; set; }

    [Column("CB_DBCR12")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr12 { get; set; }

    [Column("CB_CREDIT13", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit13 { get; set; }

    [Column("CB_DEBIT13", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit13 { get; set; }

    [Column("CB_DBCR13")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr13 { get; set; }

    [Column("CB_CREDIT14", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit14 { get; set; }

    [Column("CB_DEBIT14", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit14 { get; set; }

    [Column("CB_DBCR14")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr14 { get; set; }

    [Column("CB_CREDIT15", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit15 { get; set; }

    [Column("CB_DEBIT15", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit15 { get; set; }

    [Column("CB_DBCR15")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr15 { get; set; }

    [Column("CB_CREDIT16", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit16 { get; set; }

    [Column("CB_DEBIT16", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit16 { get; set; }

    [Column("CB_DBCR16")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr16 { get; set; }

    [Column("CB_CREDIT17", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit17 { get; set; }

    [Column("CB_DEBIT17", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit17 { get; set; }

    [Column("CB_DBCR17")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr17 { get; set; }

    [Column("CB_CREDIT18", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit18 { get; set; }

    [Column("CB_DEBIT18", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit18 { get; set; }

    [Column("CB_DBCR18")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr18 { get; set; }

    [Column("CB_CREDIT19", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit19 { get; set; }

    [Column("CB_DEBIT19", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit19 { get; set; }

    [Column("CB_DBCR19")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr19 { get; set; }

    [Column("CB_CREDIT20", TypeName = "numeric(14, 2)")]
    public decimal? CbCredit20 { get; set; }

    [Column("CB_DEBIT20", TypeName = "numeric(14, 2)")]
    public decimal? CbDebit20 { get; set; }

    [Column("CB_DBCR20")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbDbcr20 { get; set; }

    [Column("TOT_CREDIT", TypeName = "numeric(14, 2)")]
    public decimal? TotCredit { get; set; }

    [Column("TOT_DEBIT", TypeName = "numeric(14, 2)")]
    public decimal? TotDebit { get; set; }

    [Column("TOT_DBCR")]
    [StringLength(1)]
    [Unicode(false)]
    public string? TotDbcr { get; set; }
}
