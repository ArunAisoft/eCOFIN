namespace eCOFIN.Application.DTOs.Masters
{
    public class VendorDto
    {
        public string VendorCode { get; set; } = string.Empty;
        public string? VendorName { get; set; }
        public string? VendorType { get; set; }
        public string? VendorCategory { get; set; }
        public string? PanNumber { get; set; }
        public string? LstNumber { get; set; }
        public string? CstNumber { get; set; }
        public string? TinNumber { get; set; }
        public string? ServiceTax { get; set; }
        public string? EccNumber { get; set; }
        public string? VendorStatus { get; set; }
        public string? ObjectStatus { get; set; }
        public string? AddrLine1 { get; set; }
        public string? AddrLine2 { get; set; }
        public string? AddrLine3 { get; set; }
        public string? AddrLine4 { get; set; }
        public string? AddrCity { get; set; }
        public string? AddrPin { get; set; }
        public string? AddrState { get; set; }
        public string? AddrCountry { get; set; }
    }

    public class VendorCreateModel
    {
        public string VendorCode { get; set; } = string.Empty;
        public string? VendorName { get; set; }
        public string? VendorType { get; set; }
        public string? VendorCategory { get; set; }
        public string? PanNumber { get; set; }
        public string? LstNumber { get; set; }
        public string? CstNumber { get; set; }
        public string? TinNumber { get; set; }
        public string? ServiceTax { get; set; }
        public string? EccNumber { get; set; }
        public string? VendorStatus { get; set; }
        public string? ObjectStatus { get; set; }
        public string? AddrLine1 { get; set; }
        public string? AddrLine2 { get; set; }
        public string? AddrLine3 { get; set; }
        public string? AddrLine4 { get; set; }
        public string? AddrCity { get; set; }
        public string? AddrPin { get; set; }
        public string? AddrState { get; set; }
        public string? AddrCountry { get; set; }
        public string? Username { get; set; }
        public string? Location { get; set; }
        public string? AccPeriod { get; set; }
    }

    public class ImportVendorModel
    {
        public string VendorCode { get; set; } = string.Empty;
        public string? VendorName { get; set; }
        public string? VendorType { get; set; }
        public string? AddrLine1 { get; set; }
        public string? AddrLine2 { get; set; }
        public string? AddrCity { get; set; }
        public string? AddrPin { get; set; }
        public string? AddrState { get; set; }
        public string? AddrCountry { get; set; }
        public string? PanNumber { get; set; }
        public string? TinNumber { get; set; }
        public string? EccNumber { get; set; }
        public string ObjectStatus { get; set; } = "ACTVE";
        public string AccountCode { get; set; } = string.Empty;
        public string Source { get; set; } = "supplier";
        public string? Username { get; set; }
        public string? Location { get; set; }
        public string? VendorStatus { get; set; }
    }

    public class ImportableSupplierDto
    {
        public string Code { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Add1 { get; set; }
        public string? Add2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; }
        public string? Country { get; set; }
        public string? PhoneNo { get; set; }
        public string? FaxNo { get; set; }
        public string? EmailId { get; set; }
        public string? PanNo { get; set; }
        public string? TinNo { get; set; }
        public string? EccNo { get; set; }
    }

    public class ImportableVendorDto
    {
        public string Code { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Add1 { get; set; }
        public string? Add2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; }
        public string? Country { get; set; }
        public string? PhoneNo { get; set; }
        public string? FaxNo { get; set; }
        public string? EmailId { get; set; }
        public string? PanNo { get; set; }
        public string? TinNo { get; set; }
        public string? EccNo { get; set; }
    }

    public class AccVendorDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string? VendorCode { get; set; }
        public string? VendorStatus { get; set; }
    }

    public class AccVendorAccountDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class AccVendorCreateModel
    {
        public string VendorCode { get; set; } = string.Empty;
        public List<AccVendorRowModel> Rows { get; set; } = new();
    }

    public class AccVendorRowModel
    {
        public string AccountCode { get; set; } = string.Empty;
        public string? VendorStatus { get; set; }
    }
}