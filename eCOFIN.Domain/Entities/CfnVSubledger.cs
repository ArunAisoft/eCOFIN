using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
public partial class CfnVSubledger
{
    [Column("accperiod")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accperiod { get; set; } = null!;

    [Column("accountcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Accountcode { get; set; } = null!;

    [Column("subcode")]
    [StringLength(10)]
    [Unicode(false)]
    public string Subcode { get; set; } = null!;

    [Column("onholdno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Onholdno { get; set; }

    [Column("lineparticulars")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Lineparticulars { get; set; }

    [Column("accountstatus")]
    [StringLength(5)]
    [Unicode(false)]
    public string Accountstatus { get; set; } = null!;

    [Column("ob_amnt", TypeName = "numeric(14, 2)")]
    public decimal? ObAmnt { get; set; }

    [Column("ob_flag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? ObFlag { get; set; }

    [Column("cb_amnt", TypeName = "numeric(14, 2)")]
    public decimal? CbAmnt { get; set; }

    [Column("cb_flag")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CbFlag { get; set; }

    [Column("vchr_no")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrNo { get; set; }

    [Column("vchr_date", TypeName = "datetime")]
    public DateTime? VchrDate { get; set; }

    [Column("vchr_type")]
    [StringLength(5)]
    [Unicode(false)]
    public string? VchrType { get; set; }

    [Column("vchr_refno")]
    [StringLength(20)]
    [Unicode(false)]
    public string? VchrRefno { get; set; }

    [Column("vchr_refdate", TypeName = "datetime")]
    public DateTime? VchrRefdate { get; set; }

    [Column("lastupdate", TypeName = "datetime")]
    public DateTime? Lastupdate { get; set; }

    [Column("DBCRFLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dbcrflag { get; set; }

    [Column("VOUCHERAMOUNT", TypeName = "numeric(14, 2)")]
    public decimal? Voucheramount { get; set; }

    [Column("costtypedescription")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Costtypedescription { get; set; }

    [Column("epensetypedescription")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Epensetypedescription { get; set; }

    [Column("productdescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Productdescription { get; set; }

    [Column("costcentredescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Costcentredescription { get; set; }

    [Column("subacccodedescription")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Subacccodedescription { get; set; }

    [Column("costtype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costtype { get; set; }

    [Column("productcode")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Productcode { get; set; }

    [Column("expensetype")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Expensetype { get; set; }

    [Column("employeecode")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Employeecode { get; set; }

    [Column("costcentrecode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Costcentrecode { get; set; }

    [Column("instrumentno")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Instrumentno { get; set; }

    [Column("instrumentdate", TypeName = "datetime")]
    public DateTime? Instrumentdate { get; set; }

    [Column("referencenumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Referencenumber { get; set; }

    [Column("referencedate", TypeName = "datetime")]
    public DateTime? Referencedate { get; set; }

    [Column("tdscode")]
    [StringLength(5)]
    [Unicode(false)]
    public string? Tdscode { get; set; }

    [Column("tdsamount", TypeName = "numeric(14, 2)")]
    public decimal? Tdsamount { get; set; }

    [Column("tdsperc", TypeName = "numeric(5, 3)")]
    public decimal? Tdsperc { get; set; }

    [Column("narration")]
    [StringLength(400)]
    [Unicode(false)]
    public string? Narration { get; set; }
}
