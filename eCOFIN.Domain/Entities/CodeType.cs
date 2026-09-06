using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Code_Type")]
//[Index("RowId", Name = "IX_Code_Type", IsUnique = true)]
public partial class CodeType
{
    [Column("Row_ID")]
    public long RowId { get; set; }

    [Key]
    [Column("Article_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string ArticleNo { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? Des { get; set; }

    [Column("Dra_No")]
    [StringLength(255)]
    [Unicode(false)]
    public string? DraNo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Type { get; set; }

    [Column("Sub_Type")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SubType { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? Code { get; set; }

    public double? Life { get; set; }

    [Column("ID")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Id { get; set; }

    [Column("Acc_Code")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AccCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateOfChange { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? EmpCode { get; set; }

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }
}
