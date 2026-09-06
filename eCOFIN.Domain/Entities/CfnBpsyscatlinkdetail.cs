using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CrVchrNumber", "BpVchrNumber", "CrSequenceno", "BpSequenceno")]
[Table("CFN_BPSYSCATLINKDETAIL")]
public partial class CfnBpsyscatlinkdetail
{
    [Key]
    [Column("CR_VCHR_NUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string CrVchrNumber { get; set; } = null!;

    [Key]
    [Column("BP_VCHR_NUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string BpVchrNumber { get; set; } = null!;

    [Key]
    [Column("CR_SEQUENCENO", TypeName = "numeric(5, 0)")]
    public decimal CrSequenceno { get; set; }

    [Key]
    [Column("BP_SEQUENCENO", TypeName = "numeric(5, 0)")]
    public decimal BpSequenceno { get; set; }

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

    [Column("TOTALAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Totalamount { get; set; }

    [Column("BALANCEAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Balanceamount { get; set; }

    [Column("BPAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Bpamount { get; set; }

    [Column("CTRL_SEQUENCENO", TypeName = "numeric(14, 2)")]
    public decimal? CtrlSequenceno { get; set; }

    [Column("REFERENCENUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referencenumber { get; set; }

    [Column("REFERENCEDATE", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }
}
