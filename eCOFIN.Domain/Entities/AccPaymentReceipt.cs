using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Acc_PaymentReceipt")]
public partial class AccPaymentReceipt
{
    [Key]
    [Column("PayReceipt_ID")]
    [StringLength(25)]
    [Unicode(false)]
    public string PayReceiptId { get; set; } = null!;

    [Column("PayReceipt_Date", TypeName = "datetime")]
    public DateTime? PayReceiptDate { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Advance { get; set; }

    [Column("Cust_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CustCode { get; set; }

    [Column("Pay_Mode")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PayMode { get; set; }

    [Column("Cheque_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ChequeNo { get; set; }

    [Column("Cheque_Date", TypeName = "datetime")]
    public DateTime? ChequeDate { get; set; }

    [Column("Cheque_Amount")]
    public double? ChequeAmount { get; set; }

    [Column("Act_AdvanceAmount")]
    public double? ActAdvanceAmount { get; set; }

    [Column("Advance_Amount")]
    public double? AdvanceAmount { get; set; }

    [Column("Bank_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? BankName { get; set; }

    [Column("Created_By")]
    [StringLength(10)]
    public string? CreatedBy { get; set; }

    [Column("Created_Date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("Modified_By")]
    [StringLength(10)]
    public string? ModifiedBy { get; set; }

    [Column("Modified_Date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }
}
