namespace eCOFIN.Application.DTOs.Masters
{
    public class CustomerDto
    {
        public string CustomerCode { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string? CustomerType { get; set; }
        public string? BusinessNature { get; set; }
        public string? GeographyCode { get; set; }
        public string? LstNoDate { get; set; }
        public string? CstNoDate { get; set; }
        public string? ApplCustomerCode { get; set; }
        public string? ObjectStatus { get; set; }

        public string? AddrLine1 { get; set; }
        public string? AddrLine2 { get; set; }
        public string? AddrLine3 { get; set; }
        public string? AddrLine4 { get; set; }
        public string? AddrCity { get; set; }
        public string? AddrPin { get; set; }
        public string? AddrState { get; set; }
        public string? AddrCountry { get; set; }

        public string? CommTelephone1 { get; set; }
        public string? CommTelephone2 { get; set; }
        public string? CommFaxno { get; set; }
        public string? CommTelexno { get; set; }
        public string? CommEmail { get; set; }
        public string? CommGrams { get; set; }
        public string? CommContactperson { get; set; }

        public string? AccountCode { get; set; }
    }

    public class CustomerCreateModel
    {
        public string CustomerCode { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string? CustomerType { get; set; }
        public string? BusinessNature { get; set; }
        public string? GeographyCode { get; set; }
        public string? LstNoDate { get; set; }
        public string? CstNoDate { get; set; }
        public string? ApplCustomerCode { get; set; }
        public string? ObjectStatus { get; set; }

        public string? AddrLine1 { get; set; }
        public string? AddrLine2 { get; set; }
        public string? AddrLine3 { get; set; }
        public string? AddrLine4 { get; set; }
        public string? AddrCity { get; set; }
        public string? AddrPin { get; set; }
        public string? AddrState { get; set; }
        public string? AddrCountry { get; set; }

        public string? CommTelephone1 { get; set; }
        public string? CommTelephone2 { get; set; }
        public string? CommFaxno { get; set; }
        public string? CommTelexno { get; set; }
        public string? CommEmail { get; set; }
        public string? CommGrams { get; set; }
        public string? CommContactperson { get; set; }

        public string? AccountCode { get; set; }

        public string? Username { get; set; }
        public string? Location { get; set; }
        public string? AccPeriod { get; set; }
    }

    public class ImportCustomerModel
    {
        public string CustomerCode { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string? CustomerType { get; set; }

        public string? AddrLine1 { get; set; }
        public string? AddrLine2 { get; set; }
        public string? AddrCity { get; set; }
        public string? AddrPin { get; set; }
        public string? AddrState { get; set; }
        public string? AddrCountry { get; set; }

        public string? CommTelephone1 { get; set; }
        public string? CommFaxno { get; set; }
        public string? CommEmail { get; set; }
        public string? CstNoDate { get; set; }

        public string ObjectStatus { get; set; } = "ACTVE";

        public string AccountCode { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string? Location { get; set; }
    }

    public class AccountDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class AccountLinkModel
    {
        public string CustomerCode { get; set; } = string.Empty;
        public string AccountCode { get; set; } = string.Empty;
        public string CustomerStatus { get; set; } = "ACTVE";
        public string? Username { get; set; }
    }

    public class AccCustomerDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public string? CustomerStatus { get; set; }
    }

    public class ImportableCustomerDto
    {
        public string Custcode { get; set; } = string.Empty;
        public string? Coname { get; set; }
        public string? Add1 { get; set; }
        public string? Add2 { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? State { get; set; }
        public string? Type { get; set; }
        public string? Country { get; set; }
        public string? Phoneno0 { get; set; }
        public string? Faxno0 { get; set; }
        public string? Mail0 { get; set; }
        public string? Cstdate { get; set; }
    }
}