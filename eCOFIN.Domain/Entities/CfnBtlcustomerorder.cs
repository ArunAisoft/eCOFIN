using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_BTLCUSTOMERORDER")]
public partial class CfnBtlcustomerorder
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("ORDERNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ordernumber { get; set; }

    [Column("AMENDMENTNUMBER")]
    [StringLength(3)]
    [Unicode(false)]
    public string Amendmentnumber { get; set; } = null!;

    [Column("ORDERDATE", TypeName = "datetime")]
    public DateTime Orderdate { get; set; }

    [Column("ORDERTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Ordertype { get; set; }

    [Column("REFERENCEDATE", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("SALESCUSTOMERCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Salescustomercode { get; set; } = null!;

    [Column("TOTALORDERVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Totalordervalue { get; set; }

    [Column("AMENDMENTREASON")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Amendmentreason { get; set; }

    [Column("ORDERSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Orderstatus { get; set; }

    [Column("STATUSREMARK")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Statusremark { get; set; }

    [Column("ORDERBILLING")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Orderbilling { get; set; }

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

    [Column("REFERENCENO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referenceno { get; set; }

    [Column("OACTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? OactrlOnholdno { get; set; }

    [Column("OANUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Oanumber { get; set; }

    [Column("COMM_TELEPHONE1")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CommTelephone1 { get; set; }

    [Column("COMM_TELEPHONE2")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CommTelephone2 { get; set; }

    [Column("COMM_FAXNO")]
    [StringLength(80)]
    [Unicode(false)]
    public string? CommFaxno { get; set; }

    [Column("COMM_TELEXNO")]
    [StringLength(80)]
    [Unicode(false)]
    public string? CommTelexno { get; set; }

    [Column("COMM_EMAIL")]
    [StringLength(80)]
    [Unicode(false)]
    public string? CommEmail { get; set; }

    [Column("COMM_GRAMS")]
    [StringLength(80)]
    [Unicode(false)]
    public string? CommGrams { get; set; }

    [Column("COMM_CONTACTPERSON")]
    [StringLength(80)]
    [Unicode(false)]
    public string? CommContactperson { get; set; }

    [Column("EMPLOYEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("VCHR_SYSCATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrSyscategory { get; set; }

    [Column("VCHR_REFDATE", TypeName = "datetime")]
    public DateTime? VchrRefdate { get; set; }

    [Column("VCHR_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrType { get; set; }

    [Column("VCHR_CATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrCategory { get; set; }

    [Column("VCHR_REFNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrRefnumber { get; set; }

    [Column("VCHR_NARRATION")]
    [StringLength(400)]
    [Unicode(false)]
    public string? VchrNarration { get; set; }

    [Column("FACTORGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Factorgroup { get; set; }
}
