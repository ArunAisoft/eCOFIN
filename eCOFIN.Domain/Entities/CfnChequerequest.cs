using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_CHEQUEREQUEST")]
public partial class CfnChequerequest
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("VCHR_GROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string VchrGroup { get; set; } = null!;

    [Column("VCHR_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string VchrType { get; set; } = null!;

    [Column("REQUESTNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Requestnumber { get; set; }

    [Column("REQUESTDATE", TypeName = "datetime")]
    public DateTime? Requestdate { get; set; }

    [Column("REQUESTFROMDEPT")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Requestfromdept { get; set; }

    [Column("FAVOURTO")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Favourto { get; set; }

    [Column("REQUESTSOURCE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Requestsource { get; set; }

    [Column("PAIDSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Paidstatus { get; set; }

    [Column("PARTYCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Partycode { get; set; }

    [Column("REQUESTAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Requestamount { get; set; }

    [Column("AUTHORIZEDAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Authorizedamount { get; set; }

    [Column("BALANCEAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Balanceamount { get; set; }

    [Column("NARRATION")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Narration { get; set; }

    [Column("AMTPAID", TypeName = "numeric(14, 2)")]
    public decimal? Amtpaid { get; set; }

    [Column("ACCCLEARANCE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Accclearance { get; set; }

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

    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("EMPLOYEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }
}
