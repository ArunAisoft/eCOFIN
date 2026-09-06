using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("vendcode")]
//[Index("RowNo", Name = "IX_vendcode", IsUnique = true)]
public partial class Vendcode
{
    [Column("Row_No")]
    public long RowNo { get; set; }

    [Key]
    [Column("code")]
    [StringLength(25)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string? Add1 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Add2 { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Add3 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? State { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? PinCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Country { get; set; }

    [Column("Phone_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PhoneNo { get; set; }

    [Column("tarrif")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Tarrif { get; set; }

    [Column("ECC_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? EccNo { get; set; }

    [Column("division")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Division { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string Active { get; set; } = null!;

    [Column("TDS_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TdsCode { get; set; }

    [Column("Group_Name")]
    [StringLength(25)]
    [Unicode(false)]
    public string? GroupName { get; set; }

    [Column("PAN_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PanNo { get; set; }

    [Column("TIN_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TinNo { get; set; }

    [Column("ServiceTax_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ServiceTaxNo { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Authorised { get; set; }

    [Column("Email_ID")]
    [StringLength(200)]
    [Unicode(false)]
    public string? EmailId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Add4 { get; set; }

    [Column("Fax_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? FaxNo { get; set; }

    [Column("Contact_Person")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ContactPerson { get; set; }

    [Column("Credit_Days")]
    public double? CreditDays { get; set; }

    [Column("GST_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? GstNo { get; set; }

    [Column("URL")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Url { get; set; }

    [Column("CIN_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CinNo { get; set; }

    [Column("Entered_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? EnteredBy { get; set; }

    [Column("Entered_Date", TypeName = "datetime")]
    public DateTime? EnteredDate { get; set; }

    [Column("Modified_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column("Modified_Date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("MAC_Address")]
    [StringLength(15)]
    [Unicode(false)]
    public string? MacAddress { get; set; }

    [Column("Client_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ClientName { get; set; }

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? TypeIndu { get; set; }

    [Column("Company_ContactPerson")]
    [StringLength(150)]
    [Unicode(false)]
    public string? CompanyContactPerson { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Town { get; set; }

    [Column("Currency_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string CurrencyCode { get; set; } = null!;

    [Column("PriceType_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PriceTypeCode { get; set; }
}
