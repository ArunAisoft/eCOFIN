using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_INVOICE")]
public partial class CfnInvoice
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("INVOICENUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Invoicenumber { get; set; }

    [Column("INVOICEDATE", TypeName = "datetime")]
    public DateTime Invoicedate { get; set; }

    [Column("INVOICECATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string Invoicecategory { get; set; } = null!;

    [Column("INVOICETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Invoicetype { get; set; } = null!;

    [Column("ORDERREFERENCE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Orderreference { get; set; }

    [Column("CUSTOMERCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Customercode { get; set; } = null!;

    [Column("CONSIGNEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Consigneecode { get; set; }

    [Column("CONSIGNEENAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Consigneename { get; set; }

    [Column("NARRATION")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Narration { get; set; }

    [Column("CREDITPERIOD", TypeName = "numeric(3, 0)")]
    public decimal? Creditperiod { get; set; }

    [Column("TOTALINVOICEVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Totalinvoicevalue { get; set; }

    [Column("INVOICECANCELFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Invoicecancelflag { get; set; }

    [Column("TRANSPORTERCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Transportercode { get; set; }

    [Column("VEHICLENO")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Vehicleno { get; set; }

    [Column("LORRYRECPNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Lorryrecpno { get; set; }

    [Column("EXCISEFORMNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Exciseformno { get; set; }

    [Column("INVSYS_CATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? InvsysCategory { get; set; }

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

    [Column("AUTOGOVVCHRNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Autogovvchrnumber { get; set; }

    [Column("AUTOVCHRTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Autovchrtype { get; set; }

    [Column("FACTORGROUP")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Factorgroup { get; set; }

    [Column("GOVVCHRNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Govvchrnumber { get; set; }

    [Column("GOVVCHRTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Govvchrtype { get; set; }

    [Column("WAREHOUSECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Warehousecode { get; set; }

    [Column("AMCFROMDATE", TypeName = "datetime")]
    public DateTime? Amcfromdate { get; set; }

    [Column("AMCTODATE", TypeName = "datetime")]
    public DateTime? Amctodate { get; set; }

    [Column("VCHR_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrType { get; set; }

    [Column("CONSIGNEE_FLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? ConsigneeFlag { get; set; }
}
