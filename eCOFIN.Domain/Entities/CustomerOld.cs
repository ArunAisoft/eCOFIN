using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Keyless]
[Table("Customer_OLD")]
public partial class CustomerOld
{
    [Column("Row_No")]
    public long RowNo { get; set; }

    [Column("Group_Type")]
    [StringLength(50)]
    [Unicode(false)]
    public string? GroupType { get; set; }

    [Column("Family_Type")]
    [StringLength(50)]
    [Unicode(false)]
    public string? FamilyType { get; set; }

    [Column("Category_Type")]
    [StringLength(50)]
    [Unicode(false)]
    public string CategoryType { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    [StringLength(25)]
    [Unicode(false)]
    public string Custcode { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Custname { get; set; } = null!;

    [StringLength(100)]
    public string? Coname { get; set; }

    [Column("Display_Name")]
    [StringLength(100)]
    public string? DisplayName { get; set; }

    [Column("Group_Name")]
    [StringLength(100)]
    public string? GroupName { get; set; }

    [StringLength(100)]
    public string? Add1 { get; set; }

    [StringLength(100)]
    public string? Add2 { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Pincode { get; set; }

    [StringLength(50)]
    public string? ContactPerson { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Phoneno { get; set; }

    [Column("EMail")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Faxno { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? State { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Ecc { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Kst { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Kstdate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Cst { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Cstdate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Vencode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Country { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Modeship { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Couriername { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Packaging { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Typeindu { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Pricetype { get; set; }

    [Column("PriceType_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PriceTypeCode { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? Currency { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Zone { get; set; }

    [StringLength(50)]
    public string? Refno { get; set; }

    [StringLength(250)]
    public string? Remarks { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Cess { get; set; }

    [StringLength(50)]
    public string? Bkname { get; set; }

    [StringLength(250)]
    public string? Madd1 { get; set; }

    [StringLength(250)]
    public string? Madd2 { get; set; }

    [Column("MTown")]
    [StringLength(100)]
    public string? Mtown { get; set; }

    [StringLength(100)]
    public string? Mcity { get; set; }

    [StringLength(50)]
    public string? Mpin { get; set; }

    [StringLength(50)]
    public string? Mstate { get; set; }

    [Column("MContactPerson")]
    [StringLength(50)]
    public string? McontactPerson { get; set; }

    [Column("MPhoneno")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Mphoneno { get; set; }

    [Column("MEMail")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Memail { get; set; }

    [Column("MFaxno")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Mfaxno { get; set; }

    public double? KstVal { get; set; }

    public double? CstVal { get; set; }

    [Column("cessval")]
    public double? Cessval { get; set; }

    [Column("ed")]
    public double? Ed { get; set; }

    [Column("mzone")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Mzone { get; set; }

    [Column("mcountry")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Mcountry { get; set; }

    [Column("cust_type")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CustType { get; set; }

    public int? Days { get; set; }

    [Column("Air_Pre_Carr")]
    [StringLength(50)]
    public string? AirPreCarr { get; set; }

    [Column("Air_PlaRec_PreCarr")]
    [StringLength(50)]
    public string? AirPlaRecPreCarr { get; set; }

    [Column("Air_PortLoad")]
    [StringLength(50)]
    public string? AirPortLoad { get; set; }

    [Column("Air_PortDis")]
    [StringLength(50)]
    public string? AirPortDis { get; set; }

    [Column("Air_PlaDel")]
    [StringLength(50)]
    public string? AirPlaDel { get; set; }

    [Column("Air_Insurence")]
    [StringLength(100)]
    public string? AirInsurence { get; set; }

    [Column("Sea_Pre_Carr")]
    [StringLength(50)]
    public string? SeaPreCarr { get; set; }

    [Column("Sea_PlaRec_PreCarr")]
    [StringLength(50)]
    public string? SeaPlaRecPreCarr { get; set; }

    [Column("Sea_PortLoad")]
    [StringLength(50)]
    public string? SeaPortLoad { get; set; }

    [Column("Sea_PortDis")]
    [StringLength(50)]
    public string? SeaPortDis { get; set; }

    [Column("Sea_PlaDel")]
    [StringLength(50)]
    public string? SeaPlaDel { get; set; }

    [Column("Sea_Insurence")]
    [StringLength(100)]
    public string? SeaInsurence { get; set; }

    [Column("Permanent_Customer")]
    [StringLength(1)]
    [Unicode(false)]
    public string? PermanentCustomer { get; set; }

    [Column("CT3_RegNo")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Ct3RegNo { get; set; }

    [Column("WH_LicNo")]
    [StringLength(50)]
    [Unicode(false)]
    public string? WhLicNo { get; set; }

    [Column("WH_LicDate", TypeName = "datetime")]
    public DateTime? WhLicDate { get; set; }

    [Column("WH_ValidTo", TypeName = "datetime")]
    public DateTime? WhValidTo { get; set; }

    [Column("GS_BondNo")]
    [StringLength(50)]
    [Unicode(false)]
    public string? GsBondNo { get; set; }

    [Column("GS_BondDate", TypeName = "datetime")]
    public DateTime? GsBondDate { get; set; }

    [Column("GS_ValidTo", TypeName = "datetime")]
    public DateTime? GsValidTo { get; set; }

    [Column("CT3_Amount")]
    public double? Ct3Amount { get; set; }

    [Column("CT3_Fromname")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Ct3Fromname { get; set; }

    [Column("CT3_Add1")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Ct3Add1 { get; set; }

    [Column("CT3_Add2")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Ct3Add2 { get; set; }

    [Column("Old_Cust_Code")]
    [StringLength(50)]
    [Unicode(false)]
    public string? OldCustCode { get; set; }

    [Column("Cust_Update")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CustUpdate { get; set; }

    [Column("Fluc_Factor")]
    public double? FlucFactor { get; set; }

    [Column("SEZ")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Sez { get; set; }

    [Column("Zone_Category")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ZoneCategory { get; set; }

    [Column("Sales_Eng")]
    [StringLength(150)]
    [Unicode(false)]
    public string? SalesEng { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Active { get; set; }

    [Column("Del_Cond")]
    [StringLength(200)]
    [Unicode(false)]
    public string? DelCond { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Segment { get; set; }

    [StringLength(120)]
    [Unicode(false)]
    public string Payterm { get; set; } = null!;

    [Column("Payment_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string PaymentType { get; set; } = null!;

    [Column("Credit_Days")]
    public int? CreditDays { get; set; }

    [Column("Credit_Amount")]
    public double? CreditAmount { get; set; }

    [Column("AutoMail_OS")]
    [StringLength(1)]
    [Unicode(false)]
    public string? AutoMailOs { get; set; }

    [Column("AutoMail_OS_LastSent", TypeName = "datetime")]
    public DateTime? AutoMailOsLastSent { get; set; }

    [Column("CForm")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Cform { get; set; }

    [Column("Currency_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CurrencyCode { get; set; }

    [Column("Created_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column("Created_Date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

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

    [Column("Road_Permit")]
    [StringLength(50)]
    [Unicode(false)]
    public string? RoadPermit { get; set; }

    [Column("GST_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? GstNo { get; set; }

    [Column("URL")]
    [StringLength(99)]
    [Unicode(false)]
    public string? Url { get; set; }

    [Column("CIN_NO")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CinNo { get; set; }

    [Column("Packing_Inst")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PackingInst { get; set; }

    [Column("Labeling_Inst")]
    [StringLength(100)]
    [Unicode(false)]
    public string? LabelingInst { get; set; }

    [Column("Marking_Inst")]
    [StringLength(100)]
    [Unicode(false)]
    public string? MarkingInst { get; set; }

    public int? CallOffReminderInDays { get; set; }

    [Column("Inspection_Terms")]
    [StringLength(150)]
    [Unicode(false)]
    public string? InspectionTerms { get; set; }

    [Column("AllowMultiplePO_ToInvoice")]
    [StringLength(1)]
    [Unicode(false)]
    public string? AllowMultiplePoToInvoice { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PhoneNo0 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? FaxNo0 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Mail0 { get; set; }

    [StringLength(50)]
    public string? Conper0 { get; set; }

    [Column("CUST_AdvanceResDays")]
    public int CustAdvanceResDays { get; set; }

    [Column("CUST_ProformaResDays")]
    public int CustProformaResDays { get; set; }

    [Column("Promotional_ItemClass")]
    [StringLength(150)]
    [Unicode(false)]
    public string? PromotionalItemClass { get; set; }

    [Column("PFInv_ExpiryDays")]
    public int PfinvExpiryDays { get; set; }

    [Column("Delivery_Terms")]
    [StringLength(120)]
    [Unicode(false)]
    public string DeliveryTerms { get; set; } = null!;

    [Column("BackOffice_Person")]
    [StringLength(150)]
    [Unicode(false)]
    public string? BackOfficePerson { get; set; }

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }

    [Column("LOI_Allowed")]
    [StringLength(1)]
    [Unicode(false)]
    public string LoiAllowed { get; set; } = null!;

    [Column("Inv_MultiPO")]
    [StringLength(1)]
    [Unicode(false)]
    public string InvMultiPo { get; set; } = null!;

    [Column("MaxLineItems_InInvoice")]
    public int MaxLineItemsInInvoice { get; set; }

    [Column("REX_Apply")]
    [StringLength(1)]
    [Unicode(false)]
    public string RexApply { get; set; } = null!;

    [Column("TIN_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TinNo { get; set; }

    [Column("Email_InvCopy")]
    [StringLength(1)]
    [Unicode(false)]
    public string? EmailInvCopy { get; set; }

    [Column("PY_TurnOver")]
    public double? PyTurnOver { get; set; }

    [Column("CY_TurnOver")]
    public double? CyTurnOver { get; set; }

    public int? Distance { get; set; }

    [Column("Country_Code")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CountryCode { get; set; }

    [Column("PY_POTurnOver")]
    public double PyPoturnOver { get; set; }

    [Column("CY_POTurnOver")]
    public double CyPoturnOver { get; set; }

    [Column("Stock_Name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? StockName { get; set; }

    [Column("PAN_No")]
    [StringLength(10)]
    [Unicode(false)]
    public string? PanNo { get; set; }
}
