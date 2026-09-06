using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Companycode", "Locationcode")]
[Table("CFN_INSTALLPARAM")]
public partial class CfnInstallparam
{
    [Key]
    [Column("COMPANYCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Companycode { get; set; } = null!;

    [Key]
    [Column("LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Locationcode { get; set; } = null!;

    [Column("STOCK_ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? StockAccperiod { get; set; }

    [Column("PRODUCTSTOCKLOCK")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Productstocklock { get; set; }

    [Column("CURRPRODUCTACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Currproductaccperiod { get; set; }

    [Column("STALECHEQUEDAYS", TypeName = "numeric(5, 0)")]
    public decimal? Stalechequedays { get; set; }

    [Column("POSTEDCHEQUEDATES", TypeName = "numeric(5, 0)")]
    public decimal? Postedchequedates { get; set; }

    [Column("NETOFFBP")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Netoffbp { get; set; }

    [Column("NETOFFBR")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Netoffbr { get; set; }

    [Column("NETOFFCP")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Netoffcp { get; set; }

    [Column("NETOFFCR")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Netoffcr { get; set; }

    [Column("TDSPREFIX_ACCOUNTCODE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? TdsprefixAccountcode { get; set; }

    [Column("NETOFFCN")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Netoffcn { get; set; }

    [Column("NETOFFDN")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Netoffdn { get; set; }

    [Column("ROUNDOFACCOUNT")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Roundofaccount { get; set; }

    [Column("EDITINVOICEDATE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Editinvoicedate { get; set; }

    [Column("INVSALEACCTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Invsaleacctype { get; set; }

    [Column("PLACCOUNT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Placcount { get; set; }

    [Column("POVARIANCE", TypeName = "numeric(2, 0)")]
    public decimal? Povariance { get; set; }

    [Column("POQTYVARIANCE", TypeName = "numeric(2, 0)")]
    public decimal? Poqtyvariance { get; set; }

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

    [Column("CTRL_PREVERERF")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlPrevererf { get; set; }

    [Column("CTRL_NEXTREFRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlNextrefrflag { get; set; }

    [Column("PURCHASE_BILL_PERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? PurchaseBillPeriod { get; set; }

    [Column("currency_fluct_account")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CurrencyFluctAccount { get; set; }
}
