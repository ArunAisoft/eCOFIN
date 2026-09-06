using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVBankpayment
{
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string Onholdno { get; set; } = null!;

    [Column("vouchernumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Vouchernumber { get; set; }

    [Column("voucherdate", TypeName = "datetime")]
    public DateTime Voucherdate { get; set; }

    [Column("vchrrefdate", TypeName = "datetime")]
    public DateTime? Vchrrefdate { get; set; }

    [Column("vchrrefnumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Vchrrefnumber { get; set; }

    [Column("vchrtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string Vchrtype { get; set; } = null!;

    [Column("narration")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Narration { get; set; }

    [Column("category")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Category { get; set; }

    [Column("syscategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Syscategory { get; set; }

    [Column("totalamount", TypeName = "numeric(14, 2)")]
    public decimal? Totalamount { get; set; }

    [Column("bcode")]
    [StringLength(20)]
    [Unicode(false)]
    public string Bcode { get; set; } = null!;

    [Column("bankaccount")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Bankaccount { get; set; }

    [Column("partycode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Partycode { get; set; }

    [Column("favourof")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Favourof { get; set; }

    [Column("gapcno")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Gapcno { get; set; }

    [Column("bankdocumentno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Bankdocumentno { get; set; }

    [Column("bankdocumentdate", TypeName = "datetime")]
    public DateTime? Bankdocumentdate { get; set; }

    [Column("lcnumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Lcnumber { get; set; }

    [Column("lcdate", TypeName = "datetime")]
    public DateTime? Lcdate { get; set; }

    [Column("currencycode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Currencycode { get; set; }

    [Column("exchangerate", TypeName = "numeric(10, 2)")]
    public decimal? Exchangerate { get; set; }

    [Column("foreigncurr", TypeName = "numeric(14, 4)")]
    public decimal? Foreigncurr { get; set; }

    [Column("chqauthorize")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Chqauthorize { get; set; }

    [Column("status")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Status { get; set; }

    [Column("cancelflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Cancelflag { get; set; }

    [Column("locationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Locationcode { get; set; }

    [Column("username")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Username { get; set; }

    [Column("createdon", TypeName = "datetime")]
    public DateTime? Createdon { get; set; }

    [Column("lastupdate", TypeName = "datetime")]
    public DateTime? Lastupdate { get; set; }

    [Column("logextract")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Logextract { get; set; }

    [Column("trglocationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Trglocationcode { get; set; }

    [Column("logextracttype")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Logextracttype { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("particulars")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Particulars { get; set; }

    [Column("referencenumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referencenumber { get; set; }

    [Column("referencedate", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("costcentrecode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("subaccountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("costtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costtype { get; set; }

    [Column("producttype")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Producttype { get; set; }

    [Column("expensetype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Column("employeecode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("segcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Segcode { get; set; }

    [Column("automated")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Automated { get; set; }

    [Column("instrumentcategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrumentcategory { get; set; }

    [Column("instrument")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrument { get; set; }

    [Column("instrumentbookno")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrumentbookno { get; set; }

    [Column("instrumentno")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("instrumentdate", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string Dbcrflag { get; set; } = null!;

    [Column("dbcramount", TypeName = "numeric(14, 2)")]
    public decimal Dbcramount { get; set; }

    [Column("linenumber", TypeName = "numeric(3, 0)")]
    public decimal Linenumber { get; set; }
}
