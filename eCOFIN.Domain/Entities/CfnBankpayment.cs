using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("cfn_bankpayment")]
public partial class CfnBankpayment
{
    [Key]
    [Column("ctrl_onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("vchr_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("vchr_date", TypeName = "datetime")]
    public DateTime VchrDate { get; set; }

    [Column("vchr_refnumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrRefnumber { get; set; }

    [Column("vchr_refdate", TypeName = "datetime")]
    public DateTime? VchrRefdate { get; set; }

    [Column("vchr_narration")]
    [StringLength(400)]
    [Unicode(false)]
    public string? VchrNarration { get; set; }

    [Column("vchr_type")]
    [StringLength(5)]
    [Unicode(false)]
    public string VchrType { get; set; } = null!;

    [Column("vchr_category")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrCategory { get; set; }

    [Column("vchr_syscategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrSyscategory { get; set; }

    [Column("vchr_totalamount", TypeName = "numeric(14, 2)")]
    public decimal? VchrTotalamount { get; set; }

    [Column("bankcode")]
    [StringLength(20)]
    [Unicode(false)]
    public string Bankcode { get; set; } = null!;

    [Column("partycode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Partycode { get; set; }

    [Column("favourof")]
    [StringLength(125)]
    [Unicode(false)]
    public string? Favourof { get; set; }

    [Column("bankaccount")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Bankaccount { get; set; }

    [Column("instrumentcategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrumentcategory { get; set; }

    [Column("instrument")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrument { get; set; }

    [Column("instrumentno")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("instrumentdate", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("instrumentbookno", TypeName = "numeric(5, 0)")]
    public decimal? Instrumentbookno { get; set; }

    [Column("gapcno")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Gapcno { get; set; }

    [Column("bankdocumentno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Bankdocumentno { get; set; }

    [Column("bankdocumentdate", TypeName = "datetime")]
    public DateTime? Bankdocumentdate { get; set; }

    [Column("lcnumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Lcnumber { get; set; }

    [Column("lcdate", TypeName = "datetime")]
    public DateTime? Lcdate { get; set; }

    [Column("currencycode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Currencycode { get; set; }

    [Column("exchangerate", TypeName = "numeric(10, 2)")]
    public decimal? Exchangerate { get; set; }

    [Column("foreigncurr", TypeName = "numeric(14, 4)")]
    public decimal? Foreigncurr { get; set; }

    [Column("chqauthorize")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Chqauthorize { get; set; }

    [Column("ctrl_status")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("ctrl_cancelflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlCancelflag { get; set; }

    [Column("ctrl_locationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlLocationcode { get; set; }

    [Column("ctrl_accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlAccperiod { get; set; }

    [Column("ctrl_username")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CtrlUsername { get; set; }

    [Column("ctrl_createdon", TypeName = "datetime")]
    public DateTime? CtrlCreatedon { get; set; }

    [Column("ctrl_lastupdate", TypeName = "datetime")]
    public DateTime? CtrlLastupdate { get; set; }

    [Column("ctrl_logextract")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextract { get; set; }

    [Column("ctrl_trglocationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("ctrl_logextracttype")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextracttype { get; set; }

    [Column("chqgenerate")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Chqgenerate { get; set; }

    [Column("bank_rate", TypeName = "numeric(14, 2)")]
    public decimal? BankRate { get; set; }
}
