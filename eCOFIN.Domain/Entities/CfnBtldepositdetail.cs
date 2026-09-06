using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "DepositCtrlno", "Amendmentnumber")]
[Table("CFN_BTLDEPOSITDETAIL")]
public partial class CfnBtldepositdetail
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("DEPOSIT_CTRLNO")]
    [StringLength(5)]
    [Unicode(false)]
    public string DepositCtrlno { get; set; } = null!;

    [Column("REQUIRED_FOR")]
    [StringLength(5)]
    [Unicode(false)]
    public string? RequiredFor { get; set; }

    [Column("LETTERREFRNO", TypeName = "numeric(12, 0)")]
    public decimal? Letterrefrno { get; set; }

    [Column("LETTERREFRDATE", TypeName = "datetime")]
    public DateTime? Letterrefrdate { get; set; }

    [Column("GUARANTEE_AMT", TypeName = "numeric(12, 4)")]
    public decimal? GuaranteeAmt { get; set; }

    [Column("FAVOUROF")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Favourof { get; set; }

    [Column("PAYABLEAT")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Payableat { get; set; }

    [Column("PAY_TOWARDS")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PayTowards { get; set; }

    [Column("DUEDATE", TypeName = "datetime")]
    public DateTime? Duedate { get; set; }

    [Column("DEPOSITTYPENO", TypeName = "numeric(12, 0)")]
    public decimal? Deposittypeno { get; set; }

    [Column("OTHER_DETAILS")]
    [StringLength(200)]
    [Unicode(false)]
    public string? OtherDetails { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("DEPOSITTYPEDATE", TypeName = "datetime")]
    public DateTime? Deposittypedate { get; set; }

    [Column("DEPOSITAMOUNT", TypeName = "numeric(12, 4)")]
    public decimal? Depositamount { get; set; }

    [Column("BANKER_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? BankerName { get; set; }

    [Column("VALIDITY_DATE", TypeName = "datetime")]
    public DateTime? ValidityDate { get; set; }

    [Column("EMP_PERC", TypeName = "numeric(2, 2)")]
    public decimal? EmpPerc { get; set; }

    [Column("DEP_VALIDITYFROM", TypeName = "datetime")]
    public DateTime? DepValidityfrom { get; set; }

    [Column("DEP_VALIDITYTO", TypeName = "datetime")]
    public DateTime? DepValidityto { get; set; }

    [Column("DEP_REQUIREDDT", TypeName = "datetime")]
    public DateTime? DepRequireddt { get; set; }

    [Column("CLAIM_PERIOD", TypeName = "datetime")]
    public DateTime? ClaimPeriod { get; set; }

    [Column("BANK_COMM", TypeName = "numeric(12, 4)")]
    public decimal? BankComm { get; set; }

    [Key]
    [Column("AMENDMENTNUMBER")]
    [StringLength(3)]
    [Unicode(false)]
    public string Amendmentnumber { get; set; } = null!;
}
