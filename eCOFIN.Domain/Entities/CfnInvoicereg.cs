using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Invoiceno", "Sequenceno")]
[Table("CFN_INVOICEREG")]
public partial class CfnInvoicereg
{
    [Key]
    [Column("INVOICENO")]
    [StringLength(10)]
    [Unicode(false)]
    public string Invoiceno { get; set; } = null!;

    [Column("INVOICEDATE", TypeName = "datetime")]
    public DateTime? Invoicedate { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("SUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("LCSTNODATE")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Lcstnodate { get; set; }

    [Column("PARTICULARS")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Particulars { get; set; }

    [Column("AMOUNT1", TypeName = "numeric(14, 2)")]
    public decimal? Amount1 { get; set; }

    [Column("AMOUNT2", TypeName = "numeric(14, 2)")]
    public decimal? Amount2 { get; set; }

    [Column("AMOUNT3", TypeName = "numeric(14, 2)")]
    public decimal? Amount3 { get; set; }

    [Column("AMOUNT4", TypeName = "numeric(14, 2)")]
    public decimal? Amount4 { get; set; }

    [Column("AMOUNT5", TypeName = "numeric(14, 2)")]
    public decimal? Amount5 { get; set; }

    [Column("AMOUNT6", TypeName = "numeric(14, 2)")]
    public decimal? Amount6 { get; set; }

    [Column("AMOUNT7", TypeName = "numeric(14, 2)")]
    public decimal? Amount7 { get; set; }

    [Column("AMOUNT8", TypeName = "numeric(14, 2)")]
    public decimal? Amount8 { get; set; }

    [Column("AMOUNT9", TypeName = "numeric(14, 2)")]
    public decimal? Amount9 { get; set; }

    [Column("AMOUNT10", TypeName = "numeric(14, 2)")]
    public decimal? Amount10 { get; set; }

    [Column("AMOUNT11", TypeName = "numeric(14, 2)")]
    public decimal? Amount11 { get; set; }

    [Column("AMOUNT12", TypeName = "numeric(14, 2)")]
    public decimal? Amount12 { get; set; }

    [Column("AMOUNT13", TypeName = "numeric(14, 2)")]
    public decimal? Amount13 { get; set; }

    [Column("AMOUNT14", TypeName = "numeric(14, 2)")]
    public decimal? Amount14 { get; set; }

    [Column("AMOUNT15", TypeName = "numeric(14, 2)")]
    public decimal? Amount15 { get; set; }

    [Column("CTRL_ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlAccperiod { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Key]
    [Column("SEQUENCENO", TypeName = "numeric(5, 0)")]
    public decimal Sequenceno { get; set; }

    [Column("ORDERREFERENCENO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Orderreferenceno { get; set; }

    [Column("QUANTITY", TypeName = "numeric(10, 0)")]
    public decimal? Quantity { get; set; }
}
