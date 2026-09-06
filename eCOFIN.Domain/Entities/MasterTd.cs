using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("Master_TDS")]
public partial class MasterTd
{
    [Column("TDS_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string TdsCode { get; set; } = null!;

    [Column("TDS_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string TdsNo { get; set; } = null!;

    [Column("TDS_ID")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TdsId { get; set; }

    [Column("TDS_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string TdsName { get; set; } = null!;

    [Column("TDS_Percentage")]
    public double TdsPercentage { get; set; }

    [Column("TDS_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string TdsType { get; set; } = null!;

    [Column("TDS_JINAccountCode")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TdsJinaccountCode { get; set; }

    [Column("TDS_GINAccountCode")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TdsGinaccountCode { get; set; }

    [Column("TDS_Des")]
    [StringLength(150)]
    [Unicode(false)]
    public string? TdsDes { get; set; }

    [Column("TDS_Active")]
    [StringLength(1)]
    [Unicode(false)]
    public string TdsActive { get; set; } = null!;

    [Column("Created_By")]
    [StringLength(25)]
    [Unicode(false)]
    public string CreatedBy { get; set; } = null!;

    [Column("Created_Date", TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column("Modified_By")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column("Modified_Date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }
}
