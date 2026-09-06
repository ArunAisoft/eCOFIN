using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("CFN_TENDER")]
public partial class CfnTender
{
    [Key]
    [Column("CTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string CtrlOnholdno { get; set; } = null!;

    [Column("REFERENCENO")]
    [StringLength(20)]
    [Unicode(false)]
    public string Referenceno { get; set; } = null!;

    [Column("REFR_DATE", TypeName = "datetime")]
    public DateTime? RefrDate { get; set; }

    [Column("TENDERNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Tenderno { get; set; }

    [Column("SALESCUSTOMERCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Salescustomercode { get; set; }

    [Column("SALESCUSTOMERNAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Salescustomername { get; set; }

    [Column("PRODUCTCODE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("DESCRIPTION")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("DUEDATE", TypeName = "datetime")]
    public DateTime? Duedate { get; set; }

    [Column("PAYMENTMODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Paymentmode { get; set; }

    [Column("TENDERFEE", TypeName = "numeric(13, 2)")]
    public decimal? Tenderfee { get; set; }

    [Column("FAVOUROF")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Favourof { get; set; }

    [Column("PAYABLEAT")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Payableat { get; set; }

    [Column("TENDER_SPECNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TenderSpecno { get; set; }

    [Column("SPEC_LASTDATE", TypeName = "datetime")]
    public DateTime? SpecLastdate { get; set; }

    [Column("SUBMISSIONDATE", TypeName = "datetime")]
    public DateTime? Submissiondate { get; set; }

    [Column("OPENINGDATE", TypeName = "datetime")]
    public DateTime? Openingdate { get; set; }

    [Column("LOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Locationcode { get; set; }

    [Column("REQUEST_DATE", TypeName = "datetime")]
    public DateTime? RequestDate { get; set; }

    [Column("REMINDERSENT")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Remindersent { get; set; }

    [Column("SPEC_RECEIVEDON", TypeName = "datetime")]
    public DateTime? SpecReceivedon { get; set; }

    [Column("OFFER_DATE", TypeName = "datetime")]
    public DateTime? OfferDate { get; set; }

    [Column("OFFER_SENTTO")]
    [StringLength(10)]
    [Unicode(false)]
    public string? OfferSentto { get; set; }

    [Column("DESPATCHMODE")]
    [StringLength(4)]
    [Unicode(false)]
    public string? Despatchmode { get; set; }

    [Column("OFFER_REFRNO")]
    [StringLength(15)]
    [Unicode(false)]
    public string? OfferRefrno { get; set; }

    [Column("REMARKS")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Remarks { get; set; }

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

    [Column("TENDERCATEGORY")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Tendercategory { get; set; }

    [Column("STARTON_DATE", TypeName = "datetime")]
    public DateTime? StartonDate { get; set; }

    [Column("NO_OF_COPIES", TypeName = "numeric(10, 0)")]
    public decimal? NoOfCopies { get; set; }

    [Column("TENDER_VALUE", TypeName = "numeric(10, 4)")]
    public decimal? TenderValue { get; set; }

    [Column("PRICE_BASIS")]
    [StringLength(10)]
    [Unicode(false)]
    public string? PriceBasis { get; set; }

    [Column("DELIVERY_DT", TypeName = "datetime")]
    public DateTime? DeliveryDt { get; set; }

    [Column("SCRUTINY_TYPE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? ScrutinyType { get; set; }

    [Column("SECURITY_DEPOSIT")]
    [StringLength(1)]
    [Unicode(false)]
    public string? SecurityDeposit { get; set; }

    [Column("SECURITY_PERC", TypeName = "numeric(4, 2)")]
    public decimal? SecurityPerc { get; set; }

    [Column("SECURITY_TYPE")]
    [StringLength(3)]
    [Unicode(false)]
    public string? SecurityType { get; set; }

    [Column("PERF_GUARANTEE")]
    [StringLength(1)]
    [Unicode(false)]
    public string? PerfGuarantee { get; set; }

    [Column("PERF_TYPE")]
    [StringLength(3)]
    [Unicode(false)]
    public string? PerfType { get; set; }

    [Column("BID_CURRENCY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? BidCurrency { get; set; }

    [Column("TENDER_SENTON_DATE", TypeName = "datetime")]
    public DateTime? TenderSentonDate { get; set; }

    [Column("OPENING_RESULTDATE", TypeName = "datetime")]
    public DateTime? OpeningResultdate { get; set; }

    [Column("VALIDITY_DATE", TypeName = "datetime")]
    public DateTime? ValidityDate { get; set; }

    [Column("TENDER_STATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? TenderStatus { get; set; }

    [Column("COMM_TELEPHONE1")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CommTelephone1 { get; set; }

    [Column("COMM_TELEPHONE2")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CommTelephone2 { get; set; }

    [Column("COMM_FAXNO")]
    [StringLength(80)]
    [Unicode(false)]
    public string? CommFaxno { get; set; }

    [Column("COMM_TELEXNO")]
    [StringLength(80)]
    [Unicode(false)]
    public string? CommTelexno { get; set; }

    [Column("COMM_EMAIL")]
    [StringLength(80)]
    [Unicode(false)]
    public string? CommEmail { get; set; }

    [Column("COMM_GRAMS")]
    [StringLength(80)]
    [Unicode(false)]
    public string? CommGrams { get; set; }

    [Column("COMM_CONTACTPERSON")]
    [StringLength(80)]
    [Unicode(false)]
    public string? CommContactperson { get; set; }

    [Column("EMPLOYEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("VCHR_DATE", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("VCHR_SYSCATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrSyscategory { get; set; }

    [Column("VCHR_REFDATE", TypeName = "datetime")]
    public DateTime? VchrRefdate { get; set; }

    [Column("VCHR_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrType { get; set; }

    [Column("VCHR_CATEGORY")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrCategory { get; set; }

    [Column("VCHR_REFNUMBER")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrRefnumber { get; set; }

    [Column("VCHR_NARRATION")]
    [StringLength(400)]
    [Unicode(false)]
    public string? VchrNarration { get; set; }

    [Column("ORDRCTRL_ONHOLDNO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? OrdrctrlOnholdno { get; set; }
}
