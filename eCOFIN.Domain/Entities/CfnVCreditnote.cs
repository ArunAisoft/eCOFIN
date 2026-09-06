using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVCreditnote
{
    [Column("accountcode_hdr")]
    [StringLength(10)]
    [Unicode(false)]
    public string? AccountcodeHdr { get; set; }

    [Column("subaccountcode_hdr")]
    [StringLength(10)]
    [Unicode(false)]
    public string? SubaccountcodeHdr { get; set; }

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

    [Column("voucherrefnumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Voucherrefnumber { get; set; }

    [Column("voucherrefdate", TypeName = "datetime")]
    public DateTime? Voucherrefdate { get; set; }

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

    [Column("subaccountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("costcentrecode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

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

    [Column("subaccountdescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Subaccountdescription { get; set; }

    [Column("referenceno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referenceno { get; set; }

    [Column("referencedate", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("amount", TypeName = "numeric(14, 2)")]
    public decimal Amount { get; set; }

    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string Dbcrflag { get; set; } = null!;

    [Column("automated")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Automated { get; set; }

    [Column("lineno1", TypeName = "numeric(3, 0)")]
    public decimal Lineno1 { get; set; }
}
