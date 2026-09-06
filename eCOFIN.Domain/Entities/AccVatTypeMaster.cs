using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Acc_VatType_Master")]
//[Index("VatType", Name = "IX_Acc_VatType_Master", IsUnique = true)]
public partial class AccVatTypeMaster
{
    [Key]
    [Column("Sl_No")]
    public int SlNo { get; set; }

    [Column("Vat_Type")]
    [StringLength(50)]
    public string? VatType { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Active { get; set; }
}
