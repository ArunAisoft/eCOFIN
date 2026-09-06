using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_POQUOTGENFACTOR")]
public partial class CfnPoquotgenfactor
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("PRICINGFACTORCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Pricingfactorcode { get; set; }

    [Column("FACTORDESCRIPTION")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Factordescription { get; set; }

    [Column("FACTORTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Factortype { get; set; }

    [Column("COMPUTEORDER", TypeName = "numeric(5, 0)")]
    public decimal? Computeorder { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

    [Column("LEVELNO")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Levelno { get; set; }

    [Column("FORMULAID")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Formulaid { get; set; }

    [Column("FACTORLEVELCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Factorlevelcode { get; set; }

    [Column("FIXEDORVARIABLE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Fixedorvariable { get; set; }

    [Column("PERCENTAGEORVALUE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Percentageorvalue { get; set; }

    [Column("PERCENTAGE", TypeName = "numeric(5, 2)")]
    public decimal? Percentage { get; set; }

    [Column("FACTORVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Factorvalue { get; set; }

    [Column("FORMULADESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Formuladescription { get; set; }

    [Column("COMPUTEDVALUE", TypeName = "numeric(16, 4)")]
    public decimal? Computedvalue { get; set; }

    [Column("PRODUCTID", TypeName = "numeric(5, 0)")]
    public decimal? Productid { get; set; }

    [Column("BILLEDFACTORVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Billedfactorvalue { get; set; }

    [Column("EXCHANGERATE", TypeName = "numeric(14, 2)")]
    public decimal? Exchangerate { get; set; }

    [Column("EQUILENTVALUE", TypeName = "numeric(14, 2)")]
    public decimal? Equilentvalue { get; set; }

    [Column("AMENDMENTNUMBER")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Amendmentnumber { get; set; }
}
