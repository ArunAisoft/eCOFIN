using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("SupplierDetail")]
//[Index("SuppName", Name = "IX_SupplierDetail", IsUnique = true)]
//[Index("RowId", Name = "IX_SupplierDetail_Row_No", IsUnique = true)]
public partial class SupplierDetail
{
    [Column("Row_ID")]
    public long RowId { get; set; }

    [Column("Supp_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string SuppCode { get; set; } = null!;

    [Key]
    [Column("SuppCode")]
    [StringLength(25)]
    [Unicode(false)]
    public string SuppCode1 { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string SuppName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? SuppNameShort { get; set; }

    [Column("add1")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Add1 { get; set; }

    [Column("add2")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Add2 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? State { get; set; }

    [Column("pincode")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Pincode { get; set; }

    [Column("phoneno")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Phoneno { get; set; }

    [Column("faxno")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Faxno { get; set; }

    [Column("Sup_ServiceType")]
    [StringLength(25)]
    [Unicode(false)]
    public string? SupServiceType { get; set; }

    [Column("Duty_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? DutyType { get; set; }

    [Column("Sup_Type")]
    [StringLength(1)]
    [Unicode(false)]
    public string? SupType { get; set; }

    [Column("Price_Type")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PriceType { get; set; }

    [Column("Group_Name")]
    [StringLength(25)]
    [Unicode(false)]
    public string? GroupName { get; set; }

    [Column("Std_Price")]
    [StringLength(1)]
    [Unicode(false)]
    public string? StdPrice { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SuppName1 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Country { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? Currency { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? MobileNo { get; set; }

    [Column("TINNo")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Tinno { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? EccNo { get; set; }

    [Column("EMail")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("Supplier_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? SupplierType { get; set; }

    [Column("PAN_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PanNo { get; set; }

    [Column("Supp_Active")]
    [StringLength(1)]
    [Unicode(false)]
    public string? SuppActive { get; set; }

    [Column("Skip_StoreValuation")]
    [StringLength(1)]
    [Unicode(false)]
    public string? SkipStoreValuation { get; set; }

    [Column("ESI_NO")]
    [StringLength(25)]
    [Unicode(false)]
    public string? EsiNo { get; set; }

    [Column("STR_NO")]
    [StringLength(25)]
    [Unicode(false)]
    public string? StrNo { get; set; }

    [Column("GST_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? GstNo { get; set; }

    [Column("URL")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Url { get; set; }

    [Column("CIN_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CinNo { get; set; }

    [Column("TDS_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? TdsCode { get; set; }

    [Column("Created_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column("Created_date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("Modified_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column("Modified_Date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("Mandatoty_SupplierItemDetails")]
    [StringLength(1)]
    [Unicode(false)]
    public string MandatotySupplierItemDetails { get; set; } = null!;

    [Column("Currency_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CurrencyCode { get; set; }

    [Column("PriceType_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string PriceTypeCode { get; set; } = null!;

    [Column("Supp_AutoPO")]
    [StringLength(1)]
    [Unicode(false)]
    public string SuppAutoPo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? TypeIndu { get; set; }

    [Column("Delivery_Terms")]
    [StringLength(250)]
    [Unicode(false)]
    public string? DeliveryTerms { get; set; }

    [Column("Payment_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PaymentType { get; set; }

    [Column("Credit_Days")]
    public int CreditDays { get; set; }

    [Column("Payment_Terms")]
    [StringLength(250)]
    [Unicode(false)]
    public string? PaymentTerms { get; set; }

    [Column("PY_POTurnOver")]
    public double PyPoturnOver { get; set; }

    [Column("CY_POTurnOver")]
    public double CyPoturnOver { get; set; }

    [Column("Contact_Person")]
    [StringLength(250)]
    [Unicode(false)]
    public string? ContactPerson { get; set; }

    [Column("Company_ContactPerson")]
    [StringLength(150)]
    [Unicode(false)]
    public string? CompanyContactPerson { get; set; }

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
    public string? Type { get; set; }
}
