using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Personel")]
//[Index("RowNo", Name = "IX_Personel", IsUnique = true)]
//[Index("RowNo", Name = "IX_Personel_Emp_Code", IsUnique = true)]
public partial class Personel
{
    [Column("Row_No")]
    public long RowNo { get; set; }

    [Column("Company_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string CompanyCode { get; set; } = null!;

    [Column("Emp_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string EmpCode { get; set; } = null!;

    [Key]
    [StringLength(25)]
    [Unicode(false)]
    public string EmpNo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Fname { get; set; }

    [Column("Dept_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? DeptCode { get; set; }

    [Column("Cell_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CellCode { get; set; }

    [Column("Section_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? SectionCode { get; set; }

    [Column("DOJ", TypeName = "datetime")]
    public DateTime? Doj { get; set; }

    [Column("DOB", TypeName = "datetime")]
    public DateTime? Dob { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Padd1 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Padd2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Pcity { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Pstate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Pcountry { get; set; }

    [Column("PINcode", TypeName = "numeric(18, 0)")]
    public decimal? Pincode { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Tadd1 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Tadd2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Tcity { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Tstate { get; set; }

    [Column("TPincode")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Tpincode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Tel { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [Column("pager")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Pager { get; set; }

    [Column("martialstatus")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Martialstatus { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Sex { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Salgrade { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Position { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Designation { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? Jobdes { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? ReportTo { get; set; }

    public int? Subnos { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? Jobstat { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BloodGroup { get; set; }

    [Column("DOC", TypeName = "datetime")]
    public DateTime? Doc { get; set; }

    [Column("costcentre")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Costcentre { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Bankcode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Bankaccno { get; set; }

    [Column("ESIDispensary")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Esidispensary { get; set; }

    [Column("ESINO")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Esino { get; set; }

    [Column("PFno")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Pfno { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? Saltype { get; set; }

    [Column("DOI", TypeName = "datetime")]
    public DateTime? Doi { get; set; }

    [StringLength(12)]
    [Unicode(false)]
    public string? Paymode { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? Insurenceno { get; set; }

    [Column("ITaxNo")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ItaxNo { get; set; }

    [Column("caste")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Caste { get; set; }

    [Column("nationality")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Nationality { get; set; }

    [Column("religion")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Religion { get; set; }

    [Column("passportno")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Passportno { get; set; }

    [Column("ppdoi", TypeName = "datetime")]
    public DateTime? Ppdoi { get; set; }

    [Column("ppvalid", TypeName = "datetime")]
    public DateTime? Ppvalid { get; set; }

    public int? Resignation { get; set; }

    [Column("resgdate", TypeName = "datetime")]
    public DateTime? Resgdate { get; set; }

    [Column("resreason")]
    [StringLength(150)]
    public string? Resreason { get; set; }

    [StringLength(50)]
    public string? Emptype { get; set; }

    [Column("yearsofexperience")]
    [StringLength(10)]
    public string? Yearsofexperience { get; set; }

    [Column("Emp_Bar_Code_OLD")]
    [StringLength(12)]
    public string? EmpBarCodeOld { get; set; }

    [Column("Emp_Bar_Code")]
    [StringLength(12)]
    [Unicode(false)]
    public string? EmpBarCode { get; set; }

    [Column("HPosition")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Hposition { get; set; }

    [Column("Emp_Active")]
    [StringLength(1)]
    [Unicode(false)]
    public string? EmpActive { get; set; }

    [Column("System_LoginID")]
    [StringLength(25)]
    public string? SystemLoginId { get; set; }

    [Column("Email_ID")]
    [StringLength(50)]
    public string? EmailId { get; set; }

    [Column("H_Level")]
    public int? HLevel { get; set; }

    [Column("Alt_Email_ID")]
    [StringLength(50)]
    public string? AltEmailId { get; set; }

    [Column("Alt_Mobile")]
    [StringLength(15)]
    public string? AltMobile { get; set; }

    [Column("Alt_Phone")]
    [StringLength(15)]
    public string? AltPhone { get; set; }

    [Column("HR_Rights")]
    [StringLength(1)]
    [Unicode(false)]
    public string? HrRights { get; set; }

    [Column("AutoMail_QR_LastSent", TypeName = "datetime")]
    public DateTime? AutoMailQrLastSent { get; set; }

    [Column("CompensatoryLeave_Allowed")]
    [StringLength(1)]
    [Unicode(false)]
    public string CompensatoryLeaveAllowed { get; set; } = null!;

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }

    [Column("AutoMail_OP_LastSent", TypeName = "datetime")]
    public DateTime? AutoMailOpLastSent { get; set; }

    [Column("ReportTo_Manage")]
    [StringLength(150)]
    [Unicode(false)]
    public string? ReportToManage { get; set; }

    [Column("Shift_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string ShiftType { get; set; } = null!;

    [Column("EmailID_Per")]
    [StringLength(150)]
    [Unicode(false)]
    public string? EmailIdPer { get; set; }

    [Column("Mobile_Per")]
    [StringLength(50)]
    [Unicode(false)]
    public string? MobilePer { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? Dept { get; set; }

    [Column("Cell_SN")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CellSn { get; set; }

    [Column("section")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Section { get; set; }

    [Column("Frequent_Traveller")]
    [StringLength(1)]
    [Unicode(false)]
    public string? FrequentTraveller { get; set; }
}
