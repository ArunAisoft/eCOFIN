using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Sto_Annex_Master")]
//[Index("RowNo", Name = "IX_Sto_Annex_Master", IsUnique = true)]
public partial class StoAnnexMaster
{
    [Column("Row_No")]
    public long RowNo { get; set; }

    [Column("Ann_Type")]
    [StringLength(10)]
    [Unicode(false)]
    public string? AnnType { get; set; }

    [Key]
    [Column("Ann_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string AnnNo { get; set; } = null!;

    [Column("Ann_Date", TypeName = "datetime")]
    public DateTime? AnnDate { get; set; }

    [Column("Ann_D_Date", TypeName = "datetime")]
    public DateTime? AnnDDate { get; set; }

    [Column("Ann_R_Date", TypeName = "datetime")]
    public DateTime? AnnRDate { get; set; }

    [Column("LeadTime_ExSunday", TypeName = "datetime")]
    public DateTime? LeadTimeExSunday { get; set; }

    [Column("Ann_Value")]
    public double? AnnValue { get; set; }

    [Column("Ann_Return")]
    [StringLength(1)]
    [Unicode(false)]
    public string? AnnReturn { get; set; }

    [Column("Ann_ReturnQty")]
    public double? AnnReturnQty { get; set; }

    [Column("Emp_Code")]
    [StringLength(150)]
    [Unicode(false)]
    public string? EmpCode { get; set; }

    [Column("Ret_Emp_Code")]
    [StringLength(150)]
    [Unicode(false)]
    public string? RetEmpCode { get; set; }

    [Column("Ann_Remarks")]
    [StringLength(300)]
    public string? AnnRemarks { get; set; }

    [Column("Ret_Remarks")]
    [StringLength(300)]
    public string? RetRemarks { get; set; }

    [Column("Opn_List")]
    [StringLength(500)]
    [Unicode(false)]
    public string? OpnList { get; set; }

    [Column("Rate_List")]
    [StringLength(500)]
    [Unicode(false)]
    public string? RateList { get; set; }

    [Column("Ann_SwipeNo")]
    [StringLength(25)]
    [Unicode(false)]
    public string? AnnSwipeNo { get; set; }

    [Column("Amount_Debit")]
    public double? AmountDebit { get; set; }

    [Column("Amount_Credit")]
    public double? AmountCredit { get; set; }

    [Column("AnnNo_Swipe")]
    [StringLength(25)]
    [Unicode(false)]
    public string? AnnNoSwipe { get; set; }

    [Column("Issue_Cost")]
    public double? IssueCost { get; set; }

    [Column("Process_Cost")]
    public double? ProcessCost { get; set; }

    [Column("Tray_Details")]
    [StringLength(150)]
    [Unicode(false)]
    public string? TrayDetails { get; set; }

    [Column("Trays_Sent")]
    public int TraysSent { get; set; }

    [Column("Trays_Retun")]
    public int TraysRetun { get; set; }

    [Column("MAC_Address")]
    [StringLength(15)]
    [Unicode(false)]
    public string MacAddress { get; set; } = null!;

    [Column("Client_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string ClientName { get; set; } = null!;

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }

    [Column("Ext_RC_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ExtRcNo { get; set; }

    [Column("Tray_TokenNo")]
    public int? TrayTokenNo { get; set; }
}
