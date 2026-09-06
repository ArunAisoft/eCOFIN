using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Code_Entry_Account")]
//[Index("RowId", Name = "IX_Code_Entry_Account", IsUnique = true)]
public partial class CodeEntryAccount
{
    [Column("Row_ID")]
    public long RowId { get; set; }

    [Key]
    [Column("Acc_Code")]
    [StringLength(10)]
    public string AccCode { get; set; } = null!;

    [Column("Acc_Des")]
    [StringLength(100)]
    public string? AccDes { get; set; }

    [Column("Acc_Active")]
    [StringLength(1)]
    [Unicode(false)]
    public string? AccActive { get; set; }

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }
}
