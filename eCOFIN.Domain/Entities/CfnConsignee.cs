using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[PrimaryKey("Customercode", "Consigneecode")]
[Table("CFN_CONSIGNEE")]
public partial class CfnConsignee
{
    [Key]
    [Column("CUSTOMERCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Customercode { get; set; } = null!;

    [Key]
    [Column("CONSIGNEECODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Consigneecode { get; set; } = null!;

    [Column("BUSINESSNATURE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Businessnature { get; set; }

    [Column("CUSTOMERTYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Customertype { get; set; }

    [Column("CONSIGNEENAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Consigneename { get; set; }

    [Column("LSTNODATE")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Lstnodate { get; set; }

    [Column("CSTNODATE")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Cstnodate { get; set; }

    [Column("GEOGRAPHYCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Geographycode { get; set; }

    [Column("ADDR_LINE1")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AddrLine1 { get; set; }

    [Column("ADDR_LINE2")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AddrLine2 { get; set; }

    [Column("ADDR_LINE3")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AddrLine3 { get; set; }

    [Column("ADDR_LINE4")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AddrLine4 { get; set; }

    [Column("ADDR_CITY")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AddrCity { get; set; }

    [Column("ADDR_PIN")]
    [StringLength(10)]
    [Unicode(false)]
    public string? AddrPin { get; set; }

    [Column("ADDR_STATE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AddrState { get; set; }

    [Column("ADDR_COUNTRY")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AddrCountry { get; set; }

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

    [Column("CTRL_TRGLOCATIONCODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CtrlTrglocationcode { get; set; }

    [Column("OBJECT_STATUS")]
    [StringLength(5)]
    [Unicode(false)]
    public string? ObjectStatus { get; set; }
}
