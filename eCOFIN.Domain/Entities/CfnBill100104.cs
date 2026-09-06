using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_BILL_100104")]
public partial class CfnBill100104
{
    [Column("ctrl_onholdno")]
    [StringLength(50)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("ctrl_sequenceno", TypeName = "numeric(16, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("accountcode")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("costcentrecode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("subaccountcode")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("costtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costtype { get; set; }

    [Column("productcode")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("expensetype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Column("employeecode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("segcode2")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Segcode2 { get; set; }

    [Column("vchr_number")]
    [StringLength(50)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("vchr_date", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("vchr_refnumber")]
    [StringLength(35)]
    [Unicode(false)]
    public string? VchrRefnumber { get; set; }

    [Column("vchr_refdate", TypeName = "datetime")]
    public DateTime? VchrRefdate { get; set; }

    [Column("vchr_narration")]
    [StringLength(255)]
    [Unicode(false)]
    public string? VchrNarration { get; set; }

    [Column("vchr_type")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrType { get; set; }

    [Column("vchr_category")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrCategory { get; set; }

    [Column("vchr_syscategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrSyscategory { get; set; }

    [Column("vchr_totalamount", TypeName = "numeric(16, 2)")]
    public decimal? VchrTotalamount { get; set; }

    [Column("bankdocumentno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Bankdocumentno { get; set; }

    [Column("lcnumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Lcnumber { get; set; }

    [Column("billno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Billno { get; set; }

    [Column("billdate", TypeName = "datetime")]
    public DateTime? Billdate { get; set; }

    [Column("billduedate", TypeName = "datetime")]
    public DateTime? Billduedate { get; set; }

    [Column("billamount", TypeName = "numeric(16, 2)")]
    public decimal? Billamount { get; set; }

    [Column("billbalance", TypeName = "numeric(16, 2)")]
    public decimal? Billbalance { get; set; }

    [Column("billamountadjusted", TypeName = "numeric(16, 2)")]
    public decimal? Billamountadjusted { get; set; }

    [Column("onholdamountadjusted", TypeName = "numeric(16, 2)")]
    public decimal? Onholdamountadjusted { get; set; }

    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

    [Column("ponumber")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Ponumber { get; set; }

    [Column("podate", TypeName = "datetime")]
    public DateTime? Podate { get; set; }

    [Column("tdsamount", TypeName = "numeric(16, 2)")]
    public decimal? Tdsamount { get; set; }

    [Column("tdscode")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Tdscode { get; set; }

    [Column("tdscertificateno")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Tdscertificateno { get; set; }

    [Column("acceptamount", TypeName = "numeric(16, 2)")]
    public decimal? Acceptamount { get; set; }

    [Column("tdsdedamount", TypeName = "numeric(16, 2)")]
    public decimal? Tdsdedamount { get; set; }

    [Column("fromdate", TypeName = "datetime")]
    public DateTime? Fromdate { get; set; }

    [Column("todate", TypeName = "datetime")]
    public DateTime? Todate { get; set; }

    [Column("ctrl_status")]
    [StringLength(50)]
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

    [Column("releaseamt", TypeName = "numeric(16, 2)")]
    public decimal? Releaseamt { get; set; }

    [Column("lst_category")]
    [StringLength(10)]
    [Unicode(false)]
    public string? LstCategory { get; set; }

    [Column("lst", TypeName = "numeric(16, 2)")]
    public decimal? Lst { get; set; }

    [Column("cst", TypeName = "numeric(16, 2)")]
    public decimal? Cst { get; set; }

    [Column("excise", TypeName = "numeric(16, 2)")]
    public decimal? Excise { get; set; }

    [Column("servicetax", TypeName = "numeric(16, 2)")]
    public decimal? Servicetax { get; set; }

    [Column("currencyamount", TypeName = "numeric(14, 2)")]
    public decimal? Currencyamount { get; set; }

    [Column("currencybalance", TypeName = "numeric(14, 2)")]
    public decimal? Currencybalance { get; set; }

    [Column("currencyamountadjusted", TypeName = "numeric(14, 2)")]
    public decimal? Currencyamountadjusted { get; set; }

    [Column("currencycode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Currencycode { get; set; }

    [Column("currencyacceptamount", TypeName = "numeric(14, 2)")]
    public decimal? Currencyacceptamount { get; set; }

    [Column("currencyrate", TypeName = "numeric(14, 2)")]
    public decimal? Currencyrate { get; set; }
}
