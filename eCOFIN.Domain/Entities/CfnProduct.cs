using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_PRODUCT")]
public partial class CfnProduct
{
    [Key]
    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Column("PRODUCTDESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Productdescription { get; set; }

    [Column("PRODUCTSTATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Productstatus { get; set; }

    [Column("PRODUCTABC")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Productabc { get; set; }

    [Column("UOM")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Uom { get; set; }

    [Column("BASICPRICE", TypeName = "numeric(12, 4)")]
    public decimal? Basicprice { get; set; }

    [Column("STOCK")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Stock { get; set; }

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
    [StringLength(20)]
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

    [Column("SERIALNOAPPL")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Serialnoappl { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("SUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("COSTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costtype { get; set; }

    [Column("EXPENSETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Column("EMPLOYEECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("SEGCODE2")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Segcode2 { get; set; }

    [Column("POLANDEDCOST", TypeName = "numeric(14, 2)")]
    public decimal? Polandedcost { get; set; }

    [Column("BUDGETACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Budgetaccountcode { get; set; }

    [Column("PURCHASEACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Purchaseaccountcode { get; set; }

    [Column("QUOTEBASEDRATECONTRACT")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Quotebasedratecontract { get; set; }

    [Column("QTYINDICATOR")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Qtyindicator { get; set; }

    [Column("SERVICENONSERVICE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Servicenonservice { get; set; }

    [Column("PURCHASEEXPENSETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Purchaseexpensetype { get; set; }
}
