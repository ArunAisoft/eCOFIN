using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_bank_cash")]
public partial class CfnBankCash
{
    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("bank_cash_account")]
    [StringLength(10)]
    [Unicode(false)]
    public string BankCashAccount { get; set; } = null!;

    [Column("bank_cash_description")]
    [StringLength(100)]
    [Unicode(false)]
    public string? BankCashDescription { get; set; }

    [Column("ctrl_onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlOnholdno { get; set; }

    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("rcpt_pmt")]
    [StringLength(1)]
    [Unicode(false)]
    public string? RcptPmt { get; set; }

    [Column("vchr_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("vchr_date", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("linedetails")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Linedetails { get; set; }

    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

    [Column("voucheramount", TypeName = "numeric(14, 2)")]
    public decimal? Voucheramount { get; set; }
}
