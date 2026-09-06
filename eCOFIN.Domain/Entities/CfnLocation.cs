using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_LOCATION")]
public partial class CfnLocation
{
    [Key]
    [Column("LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Locationcode { get; set; } = null!;

    [Column("LOCATIONCATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string Locationcategory { get; set; } = null!;

    [Column("LOCATIONTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Locationtype { get; set; } = null!;

    [Column("NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("COMPANYCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Companycode { get; set; } = null!;

    [Column("NATUREOFBUSINESS")]
    [StringLength(5)]
    [Unicode(false)]
    public string Natureofbusiness { get; set; } = null!;

    [Column("INWARDDIRECTORY")]
    [StringLength(100)]
    [Unicode(false)]
    public string Inwarddirectory { get; set; } = null!;

    [Column("OUTWARDDIRECTORY")]
    [StringLength(100)]
    [Unicode(false)]
    public string Outwarddirectory { get; set; } = null!;

    [Column("TDS_REMITANCE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? TdsRemitance { get; set; }

    [Column("LSTNO")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Lstno { get; set; }

    [Column("CSTNO")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Cstno { get; set; }

    [Column("PANNO")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Panno { get; set; }

    [Column("ROCNO")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Rocno { get; set; }

    [Column("TANNO")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Tanno { get; set; }

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
    [StringLength(5)]
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

    [Column("TDS_CIRCLE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TdsCircle { get; set; }

    [Column("AUTHORIZED_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AuthorizedName { get; set; }

    [Column("STATION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Station { get; set; }

    [Column("DESIGNATION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Designation { get; set; }
}
