using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "Amendmentnumber")]
[Table("CFN_CUSTOMERORDER")]
public partial class CfnCustomerorder
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

    [Key]
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

    [Column("REFERENCENUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referencenumber { get; set; }

    [Column("REFERENCEDATE", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("CUSTOMERCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Customercode { get; set; } = null!;

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
}
