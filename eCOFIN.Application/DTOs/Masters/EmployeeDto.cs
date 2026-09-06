namespace eCOFIN.Application.DTOs.Masters
{
    public class EmployeeDto
    {
        public string EmployeeCode { get; set; } = "";
        public string? EmployeeName { get; set; }
        public string? EmployeeType { get; set; }
        public string? BankAccount { get; set; }
        //public string? ReportTo { get; set; }
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

    public class EmployeeCreateModel
    {
        public string EmployeeCode { get; set; } = "";
        public string EmployeeName { get; set; } = "";
        public string? EmployeeType { get; set; }
        public string? BankAccount { get; set; }
        //public string? ReportTo { get; set; }
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
        public string? Username { get; set; }
        public string? Location { get; set; }
        public string? AccPeriod { get; set; }
    }

    public class AccEmployeeDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string? EmployeeCode { get; set; }
        public string? EmployeeStatus { get; set; }
    }

    public class AccEmployeeCreateModel
    {
        public string EmployeeCode { get; set; } = string.Empty;
        public List<AccEmployeeRowModel> Rows { get; set; } = new();
    }

    public class AccEmployeeRowModel
    {
        public string AccountCode { get; set; } = string.Empty;
        public string? EmployeeStatus { get; set; }
    }

    public class PendingPersonnelDto
    {
        public string? EmpNo { get; set; }
        public string? Name { get; set; }
        public string? Padd1 { get; set; }
        public string? Padd2 { get; set; }
        public string? PCity { get; set; }
        public string? PState { get; set; }
        public string? Pincode { get; set; }
        public string? Pcountry { get; set; }
        public string? Tel { get; set; }
        public string? Mobile { get; set; }
        //public string? Reportto { get; set; }
        public string? Bankaccno { get; set; }
    }

    public class AccountDropdownDto
    {
        public string? AccountCode { get; set; }
        public string? Description { get; set; }
    }

    public class ImportEmployeeModel
    {
        public string EmployeeCode { get; set; } = "";
        public string EmployeeName { get; set; } = "";
        public string? BankAccount { get; set; }
        //public string? ReportTo { get; set; }
        public string? AddrLine1 { get; set; }
        public string? AddrLine2 { get; set; }
        public string? AddrCity { get; set; }
        public string? AddrState { get; set; }
        public string? AddrPin { get; set; }
        public string? AddrCountry { get; set; }
        public string? CommTelephone1 { get; set; }
        public string? CommTelephone2 { get; set; }
        public string AccountCode { get; set; } = "";
        public string? ObjectStatus { get; set; }
        public string? Username { get; set; }
        public string? Location { get; set; }
    }
}