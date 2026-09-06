using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVStockRegister
{
    [Column("f_year")]
    [StringLength(10)]
    [Unicode(false)]
    public string FYear { get; set; } = null!;

    [Column("month")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Month { get; set; }

    [Column("rcpt_issue")]
    [StringLength(7)]
    [Unicode(false)]
    public string RcptIssue { get; set; } = null!;

    [Column("vchr_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("cntrl_onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string CntrlOnholdno { get; set; } = null!;

    [Column("vchr_date", TypeName = "datetime")]
    public DateTime VchrDate { get; set; }

    [Column("particulars")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Particulars { get; set; }

    [Column("ref_no")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RefNo { get; set; }

    [Column("warehousecode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Warehousecode { get; set; }

    [Column("productcode")]
    [StringLength(20)]
    [Unicode(false)]
    public string Productcode { get; set; } = null!;

    [Column("parameterdescription")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Parameterdescription { get; set; }

    [Column("qty", TypeName = "numeric(38, 0)")]
    public decimal? Qty { get; set; }

    [Column("customername")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Customername { get; set; }
}
