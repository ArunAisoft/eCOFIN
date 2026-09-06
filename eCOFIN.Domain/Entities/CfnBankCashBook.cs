using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_bank_cash_book")]
public partial class CfnBankCashBook
{
    [Column("bank_cash_type")]
    [StringLength(4)]
    [Unicode(false)]
    public string? BankCashType { get; set; }

    [Column("bank_cash_account")]
    [StringLength(10)]
    [Unicode(false)]
    public string? BankCashAccount { get; set; }

    [Column("slno", TypeName = "numeric(8, 0)")]
    public decimal Slno { get; set; }

    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("vchr_narration")]
    [StringLength(400)]
    [Unicode(false)]
    public string? VchrNarration { get; set; }

    [Column("vchr_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("vchr_date", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("instrumentno")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("instrumentdate", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("subaccountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("receipt", TypeName = "numeric(14, 2)")]
    public decimal? Receipt { get; set; }

    [Column("payment", TypeName = "numeric(14, 2)")]
    public decimal? Payment { get; set; }

    [Column("balance", TypeName = "numeric(14, 2)")]
    public decimal? Balance { get; set; }
}
