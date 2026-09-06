using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("CtrlOnholdno", "CtrlSequenceno")]
[Table("CFN_ORDERGENFACTOR")]
public partial class CfnOrdergenfactor
{
    [Column("FACTORLEVELNO")]
    [StringLength(1)]
    [Unicode(false)]
    public string Factorlevelno { get; set; } = null!;

    [Column("FACTORLEVELCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string Factorlevelcode { get; set; } = null!;

    [Column("FACTORCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Factorcode { get; set; } = null!;

    [Column("FORMULAID")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Formulaid { get; set; }

    [Column("PERCENTAGE", TypeName = "numeric(5, 2)")]
    public decimal? Percentage { get; set; }

    [Column("FACTORVALUE", TypeName = "numeric(16, 4)")]
    public decimal? Factorvalue { get; set; }

    [Column("COMPUTEDVALUE", TypeName = "numeric(16, 4)")]
    public decimal? Computedvalue { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Key]
    [Column("CTRL_SEQUENCENO", TypeName = "numeric(3, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("SYSTEMFACTOR")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Systemfactor { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("PERCENTAGEORVALUE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Percentageorvalue { get; set; }

    [Column("AMENDMENTNUMBER")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Amendmentnumber { get; set; }

    [Column("FORMULADESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Formuladescription { get; set; }
}
