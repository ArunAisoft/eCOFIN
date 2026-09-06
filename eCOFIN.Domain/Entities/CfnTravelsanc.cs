using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_TRAVELSANC")]
public partial class CfnTravelsanc
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("SANCTIONNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Sanctionnumber { get; set; }

    [Column("SANCTIONDATE", TypeName = "datetime")]
    public DateTime Sanctiondate { get; set; }

    [Column("EMPLOYEE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Employee { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

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

    [Column("NARRATION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Narration { get; set; }

    [Column("DEPARTMENTCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Departmentcode { get; set; }

    [Column("STARTINGDATE", TypeName = "datetime")]
    public DateTime Startingdate { get; set; }

    [Column("ENDINGDATE", TypeName = "datetime")]
    public DateTime Endingdate { get; set; }

    [Column("DURATION", TypeName = "numeric(3, 0)")]
    public decimal? Duration { get; set; }

    [Column("MODEOFTRAVEL")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Modeoftravel { get; set; }

    [Column("FARE", TypeName = "numeric(14, 2)")]
    public decimal? Fare { get; set; }

    [Column("HOTELEXPENSES", TypeName = "numeric(14, 2)")]
    public decimal? Hotelexpenses { get; set; }

    [Column("LOCALCONVEYANCE", TypeName = "numeric(14, 2)")]
    public decimal? Localconveyance { get; set; }

    [Column("HOEXPENSES", TypeName = "numeric(14, 2)")]
    public decimal? Hoexpenses { get; set; }

    [Column("SUNDRY", TypeName = "numeric(14, 2)")]
    public decimal? Sundry { get; set; }

    [Column("TOTALAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Totalamount { get; set; }

    [Column("DEBITAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Debitamount { get; set; }

    [Column("CREDITAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Creditamount { get; set; }

    [Column("SANCTIONEDAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Sanctionedamount { get; set; }

    [Column("BALANCEAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Balanceamount { get; set; }

    [Column("SANCTIONSETTLED", TypeName = "numeric(14, 2)")]
    public decimal? Sanctionsettled { get; set; }

    [Column("SETTLEMENTFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Settlementflag { get; set; }

    [Column("TRAVELSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Travelstatus { get; set; }

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

    [Column("BPVCHR_NUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BpvchrNumber { get; set; }

    [Column("BUDGETACCOUNT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Budgetaccount { get; set; }

    [Column("BUDGETCOSTCENTRE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Budgetcostcentre { get; set; }

    [Column("CUMULATIVE_DR", TypeName = "numeric(14, 2)")]
    public decimal? CumulativeDr { get; set; }

    [Column("CUMULATIVE_CR", TypeName = "numeric(14, 2)")]
    public decimal? CumulativeCr { get; set; }
}
