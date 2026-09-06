using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_BNKRDETAIL")]
public partial class CfnBnkrdetail
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string Dbcrflag { get; set; } = null!;

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

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

    [Column("INSTRUMENTCATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrumentcategory { get; set; }

    [Column("INSTRUMENT")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Instrument { get; set; }

    [Column("INSTRUMENTNO")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("INSTRUMENTDATE", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("DRCRAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal Drcramount { get; set; }

    [Column("LINEPARTICULARS")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Lineparticulars { get; set; }

    [Column("REFERENCENUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referencenumber { get; set; }

    [Column("REFERENCEDATE", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("AUTOMATED")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Automated { get; set; }

    [Column("DEPOSITSLIPNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Depositslipno { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }
}
