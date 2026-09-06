using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Auto_DATAPULL")]
public partial class AutoDatapull
{
    [Key]
    [Column("Row_ID")]
    public long RowId { get; set; }

    [Column("From_ServerName")]
    [StringLength(50)]
    [Unicode(false)]
    public string FromServerName { get; set; } = null!;

    [Column("From_DataBaseName")]
    [StringLength(50)]
    [Unicode(false)]
    public string FromDataBaseName { get; set; } = null!;

    [Column("From_TableName")]
    [StringLength(50)]
    [Unicode(false)]
    public string FromTableName { get; set; } = null!;

    [Column("From_IDFieldName")]
    [StringLength(50)]
    [Unicode(false)]
    public string FromIdfieldName { get; set; } = null!;

    [Column("To_ServerName")]
    [StringLength(50)]
    [Unicode(false)]
    public string ToServerName { get; set; } = null!;

    [Column("To_DataBaseName")]
    [StringLength(50)]
    [Unicode(false)]
    public string ToDataBaseName { get; set; } = null!;

    [Column("To_TableName")]
    [StringLength(50)]
    [Unicode(false)]
    public string ToTableName { get; set; } = null!;

    [Column("To_IDFieldName")]
    [StringLength(50)]
    [Unicode(false)]
    public string ToIdfieldName { get; set; } = null!;

    [Column("Table_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string TableName { get; set; } = null!;

    [Column("Synchronise_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string SynchroniseType { get; set; } = null!;

    [Column("Activity_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string ActivityName { get; set; } = null!;

    [Column("LastPulled_Date", TypeName = "datetime")]
    public DateTime LastPulledDate { get; set; }

    [Column("Execute_Order")]
    public int ExecuteOrder { get; set; }

    [Column("LastExecuted_Date", TypeName = "datetime")]
    public DateTime? LastExecutedDate { get; set; }

    [Column("Executed_Results")]
    [Unicode(false)]
    public string? ExecutedResults { get; set; }

    [Column("Report_ID")]
    [StringLength(25)]
    [Unicode(false)]
    public string ReportId { get; set; } = null!;

    [Column("Pull_Active")]
    [StringLength(1)]
    [Unicode(false)]
    public string PullActive { get; set; } = null!;

    [Column("Where_Condition")]
    [StringLength(250)]
    [Unicode(false)]
    public string? WhereCondition { get; set; }
}
