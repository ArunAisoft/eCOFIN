using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("Acc_STD_MHR_Master")]
public partial class AccStdMhrMaster
{
    [Column("CC_No")]
    [StringLength(6)]
    [Unicode(false)]
    public string? CcNo { get; set; }

    [Column("dcl")]
    public double? Dcl { get; set; }

    [Column("wcl")]
    public double? Wcl { get; set; }

    [Column("Emp_Code")]
    [StringLength(10)]
    public string? EmpCode { get; set; }

    [Column("Ent_Date", TypeName = "datetime")]
    public DateTime? EntDate { get; set; }
}
