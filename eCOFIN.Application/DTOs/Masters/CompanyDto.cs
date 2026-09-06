namespace eCOFIN.Application.DTOs.Masters
{
    public class CompanyDto
    {
        public string CompanyCode { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
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

    public class CompanyCreateModel
    {
        public string CompanyCode { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
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
}
