using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_BANKDOCUMENT")]
public partial class CfnBankdocument
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("BANKDOCUMENTNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Bankdocumentno { get; set; }

    [Column("BANKDOCUMENTDATE", TypeName = "datetime")]
    public DateTime? Bankdocumentdate { get; set; }

    [Column("BANKDOCUMENTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Bankdocumenttype { get; set; } = null!;

    [Column("BANKTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Banktype { get; set; } = null!;

    [Column("BANKCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Bankcode { get; set; } = null!;

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

    [Column("VENDORCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Vendorcode { get; set; }

    [Column("RETIRED")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Retired { get; set; }

    [Column("BILLAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Billamount { get; set; }

    [Column("BANKCHARGES", TypeName = "numeric(10, 2)")]
    public decimal? Bankcharges { get; set; }

    [Column("INTERESTCHARGES", TypeName = "numeric(10, 2)")]
    public decimal? Interestcharges { get; set; }

    [Column("OTHERCHARGES", TypeName = "numeric(10, 2)")]
    public decimal? Othercharges { get; set; }

    [Column("OVERHEADCHARGES", TypeName = "numeric(10, 2)")]
    public decimal? Overheadcharges { get; set; }

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

    [Column("DESCRIPTION")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("CTRL_LOGEXTRACTTYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextracttype { get; set; }

    [Column("RETIREMENTNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Retirementnumber { get; set; }

    [Column("BPVCHR_NUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BpvchrNumber { get; set; }
}
