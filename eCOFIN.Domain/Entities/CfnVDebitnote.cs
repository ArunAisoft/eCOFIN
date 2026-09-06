using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVDebitnote
{
    [Column("onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string Onholdno { get; set; } = null!;

    [Column("accountcode_hdr")]
    [StringLength(10)]
    [Unicode(false)]
    public string AccountcodeHdr { get; set; } = null!;

    [Column("subaccountcode_hdr")]
    [StringLength(10)]
    [Unicode(false)]
    public string? SubaccountcodeHdr { get; set; }

    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accperiod { get; set; }

    [Column("vouchernumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Vouchernumber { get; set; }

    [Column("voucherdate", TypeName = "datetime")]
    public DateTime Voucherdate { get; set; }

    [Column("lineno1", TypeName = "numeric(3, 0)")]
    public decimal Lineno1 { get; set; }

    [Column("particulars")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Particulars { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("subaccountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("accountdescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string Accountdescription { get; set; } = null!;

    [Column("subaccountdescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Subaccountdescription { get; set; }

    [Column("referenceno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referenceno { get; set; }

    [Column("automated")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Automated { get; set; }

    [Column("referencedate", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("amount", TypeName = "numeric(14, 2)")]
    public decimal Amount { get; set; }

    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string Dbcrflag { get; set; } = null!;

    [Column("costtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costtype { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ProductCode { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? ExpenseType { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? EmployeeCode { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? CostCentreCode { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("costtypedescription")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Costtypedescription { get; set; }

    [Column("expensetypedescritpion")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Expensetypedescritpion { get; set; }

    [Column("locationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Locationcode { get; set; }

    [Column("narration")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Narration { get; set; }

    [Column("voucherrefnumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Voucherrefnumber { get; set; }

    [Column("voucherrefdate", TypeName = "datetime")]
    public DateTime? Voucherrefdate { get; set; }

    [Column("ctrl_status")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }
}
