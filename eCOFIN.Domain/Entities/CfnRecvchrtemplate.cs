using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("CFN_RECVCHRTEMPLATE")]
public partial class CfnRecvchrtemplate
{
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

    [Column("VCHR_REFNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrRefnumber { get; set; }

    [Column("VCHR_REFDATE", TypeName = "datetime")]
    public DateTime? VchrRefdate { get; set; }

    [Column("VCHR_NARRATION")]
    [StringLength(400)]
    [Unicode(false)]
    public string? VchrNarration { get; set; }

    [Column("VCHR_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string VchrType { get; set; } = null!;

    [Column("VCHR_CATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrCategory { get; set; }

    [Column("VCHR_SYSCATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrSyscategory { get; set; }

    [Column("VCHR_TOTALAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? VchrTotalamount { get; set; }

    [Column("BANKCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Bankcode { get; set; } = null!;

    [Column("FAVOUROF")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Favourof { get; set; }

    [Column("BANKACCOUNT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Bankaccount { get; set; }

    [Column("INSTRUMENTBOOKNO", TypeName = "numeric(5, 0)")]
    public decimal? Instrumentbookno { get; set; }

    [Column("TAGNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Tagnumber { get; set; }

    [Column("TAGVOUCHERGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Tagvouchergroup { get; set; }

    [Column("BILLNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Billno { get; set; }

    [Column("BILLDATE", TypeName = "datetime")]
    public DateTime? Billdate { get; set; }

    [Column("BILLDUEDATE", TypeName = "datetime")]
    public DateTime? Billduedate { get; set; }

    [Column("DEBITACCOUNT")]
    [StringLength(10)]
    [Unicode(false)]
    public string Debitaccount { get; set; } = null!;

    [Column("DEBITCOSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Debitcostcentrecode { get; set; }

    [Column("DEBITSUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Debitsubaccountcode { get; set; }

    [Column("DEBITCOSTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Debitcosttype { get; set; }

    [Column("DEBITPRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Debitproductcode { get; set; }

    [Column("DEBITEXPENSETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Debitexpensetype { get; set; }

    [Column("DEBITEMPLOYEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Debitemployeecode { get; set; }

    [Column("DEBITSEGCODE2")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Debitsegcode2 { get; set; }

    [Column("CREDITACCOUNT")]
    [StringLength(10)]
    [Unicode(false)]
    public string Creditaccount { get; set; } = null!;

    [Column("CREDITCOSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Creditcostcentrecode { get; set; }

    [Column("CREDITSUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Creditsubaccountcode { get; set; }

    [Column("CREDITCOSTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Creditcosttype { get; set; }

    [Column("CREDITPRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Creditproductcode { get; set; }

    [Column("CREDITEXPENSETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Creditexpensetype { get; set; }

    [Column("CREDITEMPLOYEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Creditemployeecode { get; set; }

    [Column("CREDITSEGCODE2")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Creditsegcode2 { get; set; }

    [Column("DBCRAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Dbcramount { get; set; }

    [Column("TDSCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Tdscode { get; set; }

    [Column("TDSDEDAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Tdsdedamount { get; set; }

    [Column("TDSAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Tdsamount { get; set; }

    [Column("FIXEDORVARIABLE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Fixedorvariable { get; set; }

    [Column("OBSOLETEFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Obsoleteflag { get; set; }

    [Column("CTRL_STATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("CTRL_CANCELFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlCancelflag { get; set; }

    [Column("CTRL_LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlLocationcode { get; set; }

    [Column("CTRL_ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlAccperiod { get; set; }

    [Column("CTRL_USERNAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CtrlUsername { get; set; }

    [Column("CTRL_CREATEDON", TypeName = "datetime")]
    public DateTime? CtrlCreatedon { get; set; }

    [Column("CTRL_LASTUPDATE", TypeName = "datetime")]
    public DateTime? CtrlLastupdate { get; set; }

    [Column("CTRL_LOGEXTRACT")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextract { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("CTRL_LOGEXTRACTTYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextracttype { get; set; }
}
