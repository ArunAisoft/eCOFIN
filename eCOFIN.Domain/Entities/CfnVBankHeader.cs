using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVBankHeader
{
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("rcpt_pmt")]
    [StringLength(1)]
    [Unicode(false)]
    public string? RcptPmt { get; set; }

    [Column("bank_cash_account")]
    [StringLength(10)]
    [Unicode(false)]
    public string BankCashAccount { get; set; } = null!;

    [Column("bank_cash_description")]
    [StringLength(100)]
    [Unicode(false)]
    public string BankCashDescription { get; set; } = null!;

    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("VCHR_NUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime VchrDate { get; set; }

    [Column("instrumentno")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("instrumentdate", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("linedetails")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Linedetails { get; set; }

    [Column("VOUCHERAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Voucheramount { get; set; }
}
