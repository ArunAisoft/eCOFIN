using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVInvoice
{
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string Onholdno { get; set; } = null!;

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("invoicenumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Invoicenumber { get; set; }

    [Column("invoicedate", TypeName = "datetime")]
    public DateTime Invoicedate { get; set; }

    [Column("invoicecategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string Invoicecategory { get; set; } = null!;

    [Column("invoicetype")]
    [StringLength(5)]
    [Unicode(false)]
    public string Invoicetype { get; set; } = null!;

    [Column("orderreference")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Orderreference { get; set; }

    [Column("customercode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Customercode { get; set; } = null!;

    [Column("consineecode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Consineecode { get; set; }

    [Column("consigneename")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Consigneename { get; set; }

    [Column("narration")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Narration { get; set; }

    [Column("creditperiod", TypeName = "numeric(3, 0)")]
    public decimal? Creditperiod { get; set; }

    [Column("totalinvoicevalue", TypeName = "numeric(14, 2)")]
    public decimal? Totalinvoicevalue { get; set; }

    [Column("transportercode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Transportercode { get; set; }

    [Column("invsyscategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Invsyscategory { get; set; }

    [Column("status")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Status { get; set; }

    [Column("subaccountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("costcentrecode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("expensetype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Column("productcode")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("costtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costtype { get; set; }

    [Column("particulars")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Particulars { get; set; }

    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string Dbcrflag { get; set; } = null!;

    [Column("dbcramount", TypeName = "numeric(14, 2)")]
    public decimal? Dbcramount { get; set; }

    [Column("lineno1", TypeName = "numeric(3, 0)")]
    public decimal Lineno1 { get; set; }
}
