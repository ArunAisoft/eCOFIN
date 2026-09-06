using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("cfn_ext_template")]
public partial class CfnExtTemplate
{
    [Column("ctrl_onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("ctrl_sequenceno", TypeName = "numeric(16, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

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
    [StringLength(5)]
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
    [StringLength(10)]
    [Unicode(false)]
    public string? Segcode2 { get; set; }

    [Column("vchr_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("vchr_date", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("vchr_refnumber")]
    [StringLength(35)]
    [Unicode(false)]
    public string? VchrRefnumber { get; set; }

    [Column("vchr_refdate", TypeName = "datetime")]
    public DateTime? VchrRefdate { get; set; }

    [Column("vchr_narration")]
    [StringLength(400)]
    [Unicode(false)]
    public string? VchrNarration { get; set; }

    [Column("vchr_type")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrType { get; set; }

    [Column("vchr_category")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrCategory { get; set; }

    [Column("vchr_syscategory")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrSyscategory { get; set; }

    [Column("vchr_totalamount", TypeName = "numeric(16, 2)")]
    public decimal? VchrTotalamount { get; set; }

    [Column("instrumentno", TypeName = "numeric(16, 2)")]
    public decimal? Instrumentno { get; set; }

    [Column("instrumentdate", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("billno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Billno { get; set; }

    [Column("billdate", TypeName = "datetime")]
    public DateTime? Billdate { get; set; }

    [Column("amount", TypeName = "numeric(16, 2)")]
    public decimal? Amount { get; set; }

    [Column("amountadjusted", TypeName = "numeric(16, 2)")]
    public decimal? Amountadjusted { get; set; }

    [Column("balanceamount", TypeName = "numeric(16, 2)")]
    public decimal? Balanceamount { get; set; }

    [Column("acceptamount", TypeName = "numeric(16, 2)")]
    public decimal? Acceptamount { get; set; }

    [Column("tdsdedamount", TypeName = "numeric(16, 2)")]
    public decimal? Tdsdedamount { get; set; }

    [Column("tdsamount", TypeName = "numeric(16, 2)")]
    public decimal? Tdsamount { get; set; }

    [Column("dbcrflag")]
    [StringLength(2)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

    [Column("bp_flag")]
    [StringLength(2)]
    [Unicode(false)]
    public string? BpFlag { get; set; }
}
