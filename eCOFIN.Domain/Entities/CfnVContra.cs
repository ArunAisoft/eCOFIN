using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVContra
{
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string Onholdno { get; set; } = null!;

    [Column("voucherdate", TypeName = "datetime")]
    public DateTime Voucherdate { get; set; }

    [Column("vouchernumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Vouchernumber { get; set; }

    [Column("narration")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Narration { get; set; }

    [Column("vchrrefnumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Vchrrefnumber { get; set; }

    [Column("vchrrefdate", TypeName = "datetime")]
    public DateTime? Vchrrefdate { get; set; }

    [Column("vchrtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string Vchrtype { get; set; } = null!;

    [Column("vchrcatagory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Vchrcatagory { get; set; }

    [Column("syscatagory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Syscatagory { get; set; }

    [Column("totalamount", TypeName = "numeric(14, 2)")]
    public decimal? Totalamount { get; set; }

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

    [Column("lineparticulars")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Lineparticulars { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("accountdesc")]
    [StringLength(100)]
    [Unicode(false)]
    public string Accountdesc { get; set; } = null!;

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

    [Column("productcode")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("expensetype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Column("employeecode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("segcode2")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Segcode2 { get; set; }

    [Column("instrumentcategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrumentcategory { get; set; }

    [Column("instrument")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrument { get; set; }

    [Column("instrumentno")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("instrumentdate", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("instrumentbookno", TypeName = "numeric(5, 0)")]
    public decimal? Instrumentbookno { get; set; }

    [Column("referencenumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referencenumber { get; set; }

    [Column("referencedate", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("amount", TypeName = "numeric(14, 2)")]
    public decimal Amount { get; set; }

    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string Dbcrflag { get; set; } = null!;

    [Column("linenumber", TypeName = "numeric(3, 0)")]
    public decimal Linenumber { get; set; }

    [Column("referenceno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referenceno { get; set; }

    [Column("instrumentcatagory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrumentcatagory { get; set; }
}
