using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_BILLPASSINGHDR")]
public partial class CfnBillpassinghdr
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("VCHR_NUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNumber { get; set; }

    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("VCHR_REFNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrRefnumber { get; set; }

    [Column("VCHR_REFDATE", TypeName = "datetime")]
    public DateTime? VchrRefdate { get; set; }

    [Column("VCHR_NARRATION")]
    [StringLength(400)]
    [Unicode(false)]
    public string? VchrNarration { get; set; }

    [Column("VCHR_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string VchrType { get; set; } = null!;

    [Column("VCHR_CATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrCategory { get; set; }

    [Column("VCHR_SYSCATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrSyscategory { get; set; }

    [Column("VCHR_TOTALAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? VchrTotalamount { get; set; }

    [Column("CURRENCYCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Currencycode { get; set; }

    [Column("ACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Accountcode { get; set; }

    [Column("SUBACCOUNTCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Subaccountcode { get; set; }

    [Column("CREDITPERIOD", TypeName = "datetime")]
    public DateTime? Creditperiod { get; set; }

    [Column("CTRL_STATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlStatus { get; set; }

    [Column("CTRL_CANCELFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlCancelflag { get; set; }

    [Column("CTRL_LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlLocationcode { get; set; }

    [Column("CTRL_ACCPERIOD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CtrlAccperiod { get; set; }

    [Column("CTRL_USERNAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string? CtrlUsername { get; set; }

    [Column("CTRL_CREATEDON", TypeName = "datetime")]
    public DateTime? CtrlCreatedon { get; set; }

    [Column("CTRL_LASTUPDATE", TypeName = "datetime")]
    public DateTime? CtrlLastupdate { get; set; }

    [Column("CTRL_LOGEXTRACT")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextract { get; set; }

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("CTRL_LOGEXTRACTTYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CtrlLogextracttype { get; set; }

    [Column("COSTCENTRECODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("COSTTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costtype { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("EXPENSETYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Column("EMPLOYEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("SEGCODE2")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Segcode2 { get; set; }

    [Column("TDSCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Tdscode { get; set; }

    [Column("PONUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ponumber { get; set; }

    [Column("POVCHR_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? PovchrType { get; set; }

    [Column("POVCHR_CATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? PovchrCategory { get; set; }

    [Column("POVCHR_SYSCATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? PovchrSyscategory { get; set; }

    [Column("RELEASEAMT", TypeName = "numeric(14, 2)")]
    public decimal? Releaseamt { get; set; }

    [Column("BILLNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Billno { get; set; }

    [Column("BILLDATE", TypeName = "datetime")]
    public DateTime? Billdate { get; set; }

    [Column("BILLDUEDATE", TypeName = "datetime")]
    public DateTime? Billduedate { get; set; }

    [Column("BILLAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Billamount { get; set; }

    [Column("TDSDEDAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Tdsdedamount { get; set; }

    [Column("TDSAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Tdsamount { get; set; }

    [Column("LST_CATEGORY")]
    [StringLength(10)]
    [Unicode(false)]
    public string? LstCategory { get; set; }

    [Column("LST", TypeName = "numeric(14, 2)")]
    public decimal? Lst { get; set; }

    [Column("CST", TypeName = "numeric(14, 2)")]
    public decimal? Cst { get; set; }

    [Column("EXCISE", TypeName = "numeric(14, 2)")]
    public decimal? Excise { get; set; }

    [Column("SERVICETAX", TypeName = "numeric(14, 2)")]
    public decimal? Servicetax { get; set; }
}
