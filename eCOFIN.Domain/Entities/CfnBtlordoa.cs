using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_BTLORDOA")]
public partial class CfnBtlordoa
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("ORDACCUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ordaccumber { get; set; }

    [Column("ORDERNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ordernumber { get; set; }

    [Column("ORDRCTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? OrdrctrlOnholdno { get; set; }

    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime VchrDate { get; set; }

    [Column("INTERNALACC")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Internalacc { get; set; }

    [Column("CUSTOMERACC")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Customeracc { get; set; }

    [Column("CUSTOMERACCREMARK")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Customeraccremark { get; set; }

    [Column("INTERNALACCREMARK")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Internalaccremark { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("SUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("COSTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costtype { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("EXPENSETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Column("EMPLOYEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("SEGCODE2")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Segcode2 { get; set; }

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

    [Column("MODEOFDESPATCH")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Modeofdespatch { get; set; }

    [Column("ADVANCECOLLECTION")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Advancecollection { get; set; }

    [Column("ADVANCEAMT", TypeName = "numeric(12, 4)")]
    public decimal? Advanceamt { get; set; }

    [Column("CHEQUENO")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Chequeno { get; set; }

    [Column("CHEQUEDT", TypeName = "datetime")]
    public DateTime? Chequedt { get; set; }

    [Column("DRAWNON")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Drawnon { get; set; }

    [Column("NEGOTIATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Negotiate { get; set; }

    [Column("BANKNAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Bankname { get; set; }

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

    [Column("REMARK")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Remark { get; set; }

    [Column("DEALERCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Dealercode { get; set; }

    [Column("COMMISIONVAL", TypeName = "numeric(14, 2)")]
    public decimal? Commisionval { get; set; }

    [Column("COMMISIONPERC", TypeName = "numeric(6, 2)")]
    public decimal? Commisionperc { get; set; }

    [Column("AMCCHARGES", TypeName = "numeric(14, 2)")]
    public decimal? Amccharges { get; set; }

    [Column("AMCYEARS", TypeName = "numeric(2, 0)")]
    public decimal? Amcyears { get; set; }

    [Column("WARRANTYYRS", TypeName = "numeric(2, 0)")]
    public decimal? Warrantyyrs { get; set; }

    [Column("WARRANTYFROM", TypeName = "datetime")]
    public DateTime? Warrantyfrom { get; set; }

    [Column("WARRANTYTO", TypeName = "datetime")]
    public DateTime? Warrantyto { get; set; }

    [Column("TERMTYPE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Termtype { get; set; }

    [Column("ADVFORMAT")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Advformat { get; set; }

    [Column("STFORM")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Stform { get; set; }

    [Column("SALESTAX", TypeName = "numeric(6, 2)")]
    public decimal? Salestax { get; set; }
}
