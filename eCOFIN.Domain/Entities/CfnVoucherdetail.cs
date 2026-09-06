using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno", "CostSequenceno")]
[Table("cfn_voucherdetail")]
public partial class CfnVoucherdetail
{
    [Key]
    [Column("ctrl_onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("sl_number", TypeName = "numeric(3, 0)")]
    public decimal SlNumber { get; set; }

    [Key]
    [Column("ctrl_sequenceno", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Key]
    [Column("cost_sequenceno", TypeName = "numeric(3, 0)")]
    public decimal CostSequenceno { get; set; }

    [Column("dbcrflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("costcentrecode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("costcentrecodedescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Costcentrecodedescription { get; set; }

    [Column("subaccountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("subaccountcodedescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Subaccountcodedescription { get; set; }

    [Column("costtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costtype { get; set; }

    [Column("costtypedescription")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Costtypedescription { get; set; }

    [Column("productcode")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("productdescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Productdescription { get; set; }

    [Column("expensetype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Column("expensetypedescription")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Expensetypedescription { get; set; }

    [Column("employeecode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("employeename")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Employeename { get; set; }

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

    [Column("dbcramount", TypeName = "numeric(14, 2)")]
    public decimal? Dbcramount { get; set; }

    [Column("cost_amount", TypeName = "numeric(14, 2)")]
    public decimal? CostAmount { get; set; }

    [Column("lineparticulars")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Lineparticulars { get; set; }

    [Column("referencenumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referencenumber { get; set; }

    [Column("referencedate", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("automated")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Automated { get; set; }

    [Column("tdsamount", TypeName = "numeric(14, 2)")]
    public decimal? Tdsamount { get; set; }

    [Column("tdscode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Tdscode { get; set; }

    [Column("ctrl_trglocationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("depositslipno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Depositslipno { get; set; }

    [Column("vouchernumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Vouchernumber { get; set; }

    [Column("tdsperc", TypeName = "numeric(5, 3)")]
    public decimal? Tdsperc { get; set; }
}
