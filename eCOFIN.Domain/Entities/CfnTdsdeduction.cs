using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Vendorcode", "Tdscode", "CtrlSequenceno", "CtrlAccperiod")]
[Table("cfn_tdsdeduction")]
public partial class CfnTdsdeduction
{
    [Key]
    [Column("vendorcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Vendorcode { get; set; } = null!;

    [Key]
    [Column("tdscode")]
    [StringLength(5)]
    [Unicode(false)]
    public string Tdscode { get; set; } = null!;

    [Column("billamount", TypeName = "numeric(14, 2)")]
    public decimal? Billamount { get; set; }

    [Column("tdsamount", TypeName = "numeric(14, 2)")]
    public decimal? Tdsamount { get; set; }

    [Column("tdsdedamount", TypeName = "numeric(14, 2)")]
    public decimal? Tdsdedamount { get; set; }

    [Column("paymentamount", TypeName = "numeric(14, 2)")]
    public decimal? Paymentamount { get; set; }

    [Column("paymenttdsamount", TypeName = "numeric(14, 2)")]
    public decimal? Paymenttdsamount { get; set; }

    [Column("paymenttdsdedamount", TypeName = "numeric(14, 2)")]
    public decimal? Paymenttdsdedamount { get; set; }

    [Column("challanno")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Challanno { get; set; }

    [Column("challandt", TypeName = "datetime")]
    public DateTime? Challandt { get; set; }

    [Column("locationcode")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Locationcode { get; set; }

    [Column("fromdt", TypeName = "datetime")]
    public DateTime Fromdt { get; set; }

    [Column("todate", TypeName = "datetime")]
    public DateTime Todate { get; set; }

    [Column("bankcode")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Bankcode { get; set; }

    [Column("natureofpayment")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Natureofpayment { get; set; }

    [Column("challanamt", TypeName = "numeric(14, 2)")]
    public decimal? Challanamt { get; set; }

    [Column("certificateno")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Certificateno { get; set; }

    [Column("ctrl_status")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("ctrl_cancelflag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlCancelflag { get; set; }

    [Column("ctrl_locationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlLocationcode { get; set; }

    [Key]
    [Column("ctrl_accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string CtrlAccperiod { get; set; } = null!;

    [Column("ctrl_username")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CtrlUsername { get; set; }

    [Column("ctrl_createdon", TypeName = "datetime")]
    public DateTime? CtrlCreatedon { get; set; }

    [Column("ctrl_lastupdate", TypeName = "datetime")]
    public DateTime? CtrlLastupdate { get; set; }

    [Column("ctrl_logextract")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextract { get; set; }

    [Column("ctrl_trglocationcode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("ctrl_logextracttype")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextracttype { get; set; }

    [Column("ctrl_onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CtrlOnholdno { get; set; }

    [Key]
    [Column("ctrl_sequenceno", TypeName = "numeric(5, 0)")]
    public decimal CtrlSequenceno { get; set; }

    [Column("certificatedate", TypeName = "datetime")]
    public DateTime? Certificatedate { get; set; }
}
