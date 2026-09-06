using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Inv_main")]
//[Index("RowNo", Name = "IX_Inv_main", IsUnique = true)]
//[Index("CustCode", "Slno", Name = "IX_Inv_main_Cust", IsUnique = true)]
public partial class InvMain
{
    [Column("Row_No")]
    public long RowNo { get; set; }

    [Key]
    [Column("slno")]
    [StringLength(25)]
    [Unicode(false)]
    public string Slno { get; set; } = null!;

    [Column("V_SlNo")]
    [StringLength(25)]
    [Unicode(false)]
    public string? VSlNo { get; set; }

    [Column("start_date", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("start_time")]
    [StringLength(25)]
    [Unicode(false)]
    public string? StartTime { get; set; }

    [Column("cust_code")]
    [StringLength(25)]
    [Unicode(false)]
    public string CustCode { get; set; } = null!;

    [Column("Company_Name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? CompanyName { get; set; }

    [Column("mode")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Mode { get; set; }

    [Column("Cess_ED")]
    public double? CessEd { get; set; }

    [Column("cst")]
    public double? Cst { get; set; }

    [Column("kst")]
    public double? Kst { get; set; }

    [Column("cess")]
    public double? Cess { get; set; }

    [Column("insurance")]
    public double? Insurance { get; set; }

    [Column("freight")]
    public double? Freight { get; set; }

    [Column("regn")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Regn { get; set; }

    [Column("book")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Book { get; set; }

    [Column("receiving")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Receiving { get; set; }

    [Column("removal_date", TypeName = "datetime")]
    public DateTime? RemovalDate { get; set; }

    [Column("removal_time")]
    [StringLength(25)]
    [Unicode(false)]
    public string? RemovalTime { get; set; }

    [Column("type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Type { get; set; }

    [Column("debit")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Debit { get; set; }

    [Column("debit_no")]
    public int? DebitNo { get; set; }

    [Column("debit_date", TypeName = "datetime")]
    public DateTime? DebitDate { get; set; }

    [Column("ftype")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Ftype { get; set; }

    [Column("ar4")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Ar4 { get; set; }

    [Column("commercial")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Commercial { get; set; }

    [Column("tarrif")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Tarrif { get; set; }

    [Column("Cust_Rec")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CustRec { get; set; }

    [Column("Cust_Rec_date", TypeName = "datetime")]
    public DateTime? CustRecDate { get; set; }

    [Column("Cust_Rec_No")]
    [StringLength(50)]
    public string? CustRecNo { get; set; }

    [Column("CT3_No")]
    [StringLength(25)]
    public string? Ct3No { get; set; }

    [Column("EDCess_Perc")]
    public double? EdcessPerc { get; set; }

    [Column("EDCess_Value")]
    public double? EdcessValue { get; set; }

    [Column("EDCess2_Perc")]
    public double? Edcess2Perc { get; set; }

    [Column("EDCess2_Value")]
    public double? Edcess2Value { get; set; }

    [Column("Fette_SlNo")]
    [StringLength(25)]
    public string? FetteSlNo { get; set; }

    [Column("Loc_FetteInvoice")]
    [StringLength(100)]
    public string? LocFetteInvoice { get; set; }

    [Column("Origin_Charge")]
    public double? OriginCharge { get; set; }

    [Column("Consignee_Code")]
    [StringLength(25)]
    public string? ConsigneeCode { get; set; }

    [Column("Currency_Code")]
    [StringLength(25)]
    public string? CurrencyCode { get; set; }

    [Column("Currency_Rate")]
    public double? CurrencyRate { get; set; }

    [Column("Group_Invoice")]
    [StringLength(50)]
    public string? GroupInvoice { get; set; }

    [Column("Sales_Engineer")]
    [StringLength(150)]
    [Unicode(false)]
    public string? SalesEngineer { get; set; }

    [Column("Item_Segment")]
    [StringLength(50)]
    public string? ItemSegment { get; set; }

    [Column("Cust_Type")]
    [StringLength(50)]
    public string? CustType { get; set; }

    [StringLength(300)]
    public string? Remarks { get; set; }

    [Column("BTI_GINNo")]
    [StringLength(25)]
    public string? BtiGinno { get; set; }

    [Column("Service_Tax")]
    public double? ServiceTax { get; set; }

    [Column("SBharat_CessP")]
    public double? SbharatCessP { get; set; }

    [Column("KrishiK_CessP")]
    public double? KrishiKCessP { get; set; }

    [Column("Display_Discount")]
    [StringLength(1)]
    [Unicode(false)]
    public string? DisplayDiscount { get; set; }

    [Column("PayReceipt_ID")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PayReceiptId { get; set; }

    [Column("PayReceipt_Amt")]
    public double? PayReceiptAmt { get; set; }

    [Column("PackForward_Charges")]
    public double PackForwardCharges { get; set; }

    [Column("HSN_CodeM")]
    [StringLength(25)]
    [Unicode(false)]
    public string? HsnCodeM { get; set; }

    [Column("Freight_Charges")]
    public double FreightCharges { get; set; }

    [Column("Origin_Charges")]
    public double OriginCharges { get; set; }

    [Column("PF_Charges")]
    public double PfCharges { get; set; }

    [Column("Insurance_Charges")]
    public double InsuranceCharges { get; set; }

    [Column("GST_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? GstType { get; set; }

    [Column("GST_PerM")]
    public double? GstPerM { get; set; }

    [Column("SGST_PerM")]
    public double? SgstPerM { get; set; }

    [Column("CGST_PerM")]
    public double? CgstPerM { get; set; }

    [Column("IGST_PerM")]
    public double? IgstPerM { get; set; }

    [Column("Other_Charges_OutSideGST")]
    public double OtherChargesOutSideGst { get; set; }

    [Column("MAC_Address")]
    [StringLength(15)]
    [Unicode(false)]
    public string? MacAddress { get; set; }

    [Column("Client_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ClientName { get; set; }

    [Column("Created_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column("PONo_List")]
    [Unicode(false)]
    public string? PonoList { get; set; }

    [Column("Special_Order")]
    [StringLength(50)]
    [Unicode(false)]
    public string SpecialOrder { get; set; } = null!;

    [Column("Order_Value")]
    public double OrderValue { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string IsPromoOrder { get; set; } = null!;

    [Column("Discount_Per")]
    public double DiscountPer { get; set; }

    [Column("Promo_Description")]
    [StringLength(150)]
    [Unicode(false)]
    public string? PromoDescription { get; set; }

    [Column("Sales_Eng")]
    [StringLength(150)]
    [Unicode(false)]
    public string? SalesEng { get; set; }

    [Column("Inv_Active")]
    [StringLength(1)]
    [Unicode(false)]
    public string InvActive { get; set; } = null!;

    [Column("Inv_MultiPO")]
    [StringLength(1)]
    [Unicode(false)]
    public string InvMultiPo { get; set; } = null!;

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }

    [Column("GP_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? GpNo { get; set; }

    [Column("INV_SwipeNo")]
    [StringLength(25)]
    [Unicode(false)]
    public string? InvSwipeNo { get; set; }

    [Column("Delivery_Terms")]
    [StringLength(250)]
    [Unicode(false)]
    public string? DeliveryTerms { get; set; }

    [Column("Payment_Terms")]
    [StringLength(250)]
    [Unicode(false)]
    public string? PaymentTerms { get; set; }

    [Column("IRN_No")]
    [StringLength(100)]
    [Unicode(false)]
    public string? IrnNo { get; set; }

    [Column("IRN_QRCode")]
    [StringLength(2500)]
    [Unicode(false)]
    public string? IrnQrcode { get; set; }

    [Column("EWB_No")]
    [StringLength(50)]
    [Unicode(false)]
    public string? EwbNo { get; set; }

    [Column("ACK_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string? AckNo { get; set; }

    [Column("ACK_Date", TypeName = "datetime")]
    public DateTime? AckDate { get; set; }

    public double Cusduty { get; set; }

    [Column("Cus_Cess")]
    public double CusCess { get; set; }

    public double VatonDuty { get; set; }

    [Column("Assess_Value")]
    public double AssessValue { get; set; }

    [Column("Exempted_Duty")]
    public double ExemptedDuty { get; set; }

    [Column("CVD_Value")]
    public double CvdValue { get; set; }

    [Column("CVD_CessValue")]
    public double CvdCessValue { get; set; }

    [Column("CVD_CessValue2")]
    public double CvdCessValue2 { get; set; }

    [Column("Payable_Duty")]
    public double PayableDuty { get; set; }

    [Column("PayDuty_CessValue")]
    public double PayDutyCessValue { get; set; }

    [Column("PayDuty_CessValue2")]
    public double PayDutyCessValue2 { get; set; }

    [Column("Total_PayableDuty")]
    public double TotalPayableDuty { get; set; }

    [Column("VAT_Value")]
    public double VatValue { get; set; }

    [Column("Total_Value")]
    public double TotalValue { get; set; }

    [Column("Indigenous_Inv")]
    [StringLength(1)]
    [Unicode(false)]
    public string? IndigenousInv { get; set; }

    [Column("Sub_Type")]
    [StringLength(25)]
    [Unicode(false)]
    public string? SubType { get; set; }

    [Column("Sub_SubType")]
    [StringLength(25)]
    [Unicode(false)]
    public string? SubSubType { get; set; }

    [Column("PC_No")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PcNo { get; set; }

    [Column("PC_Date", TypeName = "datetime")]
    public DateTime? PcDate { get; set; }

    [Column("Currency_name")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CurrencyName { get; set; }

    [Column("Packing_Charges")]
    public double PackingCharges { get; set; }

    [Column("Cess_Per")]
    public double CessPer { get; set; }

    [Column("HECess_Per")]
    public double HecessPer { get; set; }

    [Column("BCD_Per")]
    public double BcdPer { get; set; }

    [Column("VCD_Per")]
    public double VcdPer { get; set; }

    [Column("TCS_Per")]
    public double TcsPer { get; set; }
}
