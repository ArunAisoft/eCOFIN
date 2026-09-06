namespace eCOFIN.Application.DTOs.Masters
{
    public class LedgerAccountDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public string NatureOfAccount { get; set; } = string.Empty;
        public string AccountStatus { get; set; } = "ACTVE";
        public string? ActivatePeriod { get; set; }
        public bool ZeroLevelCheck { get; set; }
        public string ControlAccount { get; set; } = "N";
        public string? ParentRefr { get; set; }
        public string? Banker { get; set; }
        public string? EfcAccount { get; set; }
        public string? BillwiseAppl { get; set; }
        public string? BudgetAppl { get; set; }
        public string? CostAppl { get; set; }
        public string? SubledgerAppl { get; set; }
        public string? EmployeeAppl { get; set; }
        public string? CostTypeAppl { get; set; }
        public string? ExpenseAppl { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public class ParameterDto
    {
        public string ParameterGroup { get; set; } = string.Empty;
        public string ParameterCode { get; set; } = string.Empty;
        public string ParameterDescription { get; set; } = string.Empty;
        public string? ActiveStatus { get; set; }
    }

    public class BankListDto
    {
        public string BankCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class EfcAccountDto
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class SaveAccountRequest
    {
        public string AccountCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public string NatureOfAccount { get; set; } = string.Empty;
        public string AccountStatus { get; set; } = "ACTVE";
        public string? ActivatePeriod { get; set; }
        public string ZeroLevelCheck { get; set; } = "N";
        public string ControlAccount { get; set; } = "N";
        public string? ParentRefr { get; set; }
        public string? Banker { get; set; }
        public string? EfcAccount { get; set; }
        public string BillwiseAppl { get; set; } = "N";
        public string BudgetAppl { get; set; } = "N";
        public string CostAppl { get; set; } = "N";
        public string SubledgerAppl { get; set; } = "N";
        public string EmployeeAppl { get; set; } = "N";
        public string CostTypeAppl { get; set; } = "N";
        public string ExpenseAppl { get; set; } = "N";
        public string? Username { get; set; }
        public string? Location { get; set; }
    }
}
