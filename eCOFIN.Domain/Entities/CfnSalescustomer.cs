using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_SALESCUSTOMER")]
public partial class CfnSalescustomer
{
    [Key]
    [Column("SALESCUSTOMERCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Salescustomercode { get; set; } = null!;

    [Column("SALESCUSTOMERNAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Salescustomername { get; set; }

    [Column("LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Locationcode { get; set; }

    [Column("BUSINESSNATURE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Businessnature { get; set; }

    [Column("CUSTOMERTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Customertype { get; set; }

    [Column("SUBCUSTOMERCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subcustomercode { get; set; }

    [Column("GEOGRAPHYCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Geographycode { get; set; }

    [Column("LSTNODATE")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Lstnodate { get; set; }

    [Column("CSTNODATE")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Cstnodate { get; set; }

    [Column("ADDR_LINE1")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AddrLine1 { get; set; }

    [Column("ADDR_LINE2")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AddrLine2 { get; set; }

    [Column("ADDR_LINE3")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AddrLine3 { get; set; }

    [Column("ADDR_LINE4")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AddrLine4 { get; set; }

    [Column("ADDR_CITY")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AddrCity { get; set; }

    [Column("ADDR_PIN")]
    [StringLength(10)]
    [Unicode(false)]
    public string? AddrPin { get; set; }

    [Column("ADDR_STATE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AddrState { get; set; }

    [Column("ADDR_COUNTRY")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AddrCountry { get; set; }

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

    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlOnholdno { get; set; }

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

    [Column("CTRL_PREVREFR")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlPrevrefr { get; set; }

    [Column("CTRL_NEXTREFRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlNextrefrflag { get; set; }

    [Column("OBJECTSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Objectstatus { get; set; }
}
