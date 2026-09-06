namespace eCOFIN.Application.DTOs.Dashboard
{
    public class DashboardSummaryDto
    {
        /// <summary>
        /// Financial years that actually have active periods, newest first,
        /// e.g. "2026-27". Populates the year dropdown so it never offers an
        /// empty year. Indian FY: April through March.
        /// </summary>
        public List<string> FinancialYears { get; set; } = [];

        /// <summary>The year this payload was built for, null when unfiltered.</summary>
        public string? SelectedFinYear { get; set; }

        public DashboardKpiDto Kpi { get; set; } = new();
        public List<VoucherTypeMetricDto> VoucherMetrics { get; set; } = [];
        public List<BankSummaryDto> BankSummary { get; set; } = [];
        public List<MonthlyTrendDto> MonthlyTrend { get; set; } = [];
        public List<RecentVoucherDto> RecentVouchers { get; set; } = [];
    }

    public class DashboardKpiDto
    {
        public int TotalVouchers { get; set; }
        public decimal TotalPostedAmount { get; set; }
        public decimal TotalOnHoldAmount { get; set; }
        public int TotalBanks { get; set; }
        public int TotalAccounts { get; set; }
        public int TotalVendors { get; set; }
        public int TotalCustomers { get; set; }
        public int PostedCount { get; set; }
        public int OnHoldCount { get; set; }
        public int DraftCount { get; set; }
    }

    public class VoucherTypeMetricDto
    {
        public string VoucherType { get; set; } = string.Empty;
        public string VoucherSysCategory { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VoucherGroup { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public int TotalCount { get; set; }
        public int PostedCount { get; set; }
        public int OnHoldCount { get; set; }
        public int DraftCount { get; set; }
        public decimal PostedAmount { get; set; }
        public decimal OnHoldAmount { get; set; }
        public decimal PostedPercent { get; set; }
        public decimal OnHoldPercent { get; set; }
        public List<LinkedBankAccountDto> LinkedAccounts { get; set; } = [];
        public List<MonthlyVolumeDto> MonthlyVolume { get; set; } = [];
    }

    public class LinkedBankAccountDto
    {
        public string BankCode { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string AccountCode { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }

    public class MonthlyVolumeDto
    {
        public string AccPeriod { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal Amount { get; set; }
    }

    public class BankSummaryDto
    {
        public string BankCode { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string ObjectStatus { get; set; } = string.Empty;
        public int AccountCount { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal PostedDebit { get; set; }
        public decimal PostedCredit { get; set; }
        public decimal OnHoldDebit { get; set; }
        public decimal OnHoldCredit { get; set; }
        public List<BankAccountSummaryDto> Accounts { get; set; } = [];
    }

    public class BankAccountSummaryDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public decimal PostedDebit { get; set; }
        public decimal PostedCredit { get; set; }
        public decimal OnHoldDebit { get; set; }
        public decimal OnHoldCredit { get; set; }
        public List<string> VoucherTypes { get; set; } = [];
    }

    public class MonthlyTrendDto
    {
        public string AccPeriod { get; set; } = string.Empty;
        public int Sequence { get; set; }
        public string PeriodState { get; set; } = string.Empty;
        public List<GroupTrendDto> Groups { get; set; } = [];
    }

    public class GroupTrendDto
    {
        public string VoucherGroup { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal PostedAmount { get; set; }
        public decimal OnHoldAmount { get; set; }
    }

    public class RecentVoucherDto
    {
        public string VoucherNo { get; set; } = string.Empty;
        public string OnHoldNo { get; set; } = string.Empty;
        public string VoucherType { get; set; } = string.Empty;
        public string VoucherSysCat { get; set; } = string.Empty;
        public string VoucherGroup { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AccountCode { get; set; } = string.Empty;
        public string SubAccountCode { get; set; } = string.Empty;
        public string BankCode { get; set; } = string.Empty;
        public string AccPeriod { get; set; } = string.Empty;
        public DateTime VoucherDate { get; set; }
        public decimal Amount { get; set; }
        public string DbCrFlag { get; set; } = string.Empty;
        public string CtrlStatus { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class VoucherTypeDrillDto
    {
        public string VoucherSysCategory { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VoucherGroup { get; set; } = string.Empty;
        public DashboardKpiDto Kpi { get; set; } = new();
        public List<LinkedBankAccountDto> LinkedAccounts { get; set; } = [];
        public List<MonthlyVolumeDto> MonthlyVolume { get; set; } = [];
        public List<RecentVoucherDto> RecentVouchers { get; set; } = [];
        public StatusBreakdownDto StatusBreakdown { get; set; } = new();
    }

    public class StatusBreakdownDto
    {
        public int PostedCount { get; set; }
        public int OnHoldCount { get; set; }
        public int DraftCount { get; set; }
        public decimal PostedAmount { get; set; }
        public decimal OnHoldAmount { get; set; }
        public decimal PostedPct { get; set; }
        public decimal OnHoldPct { get; set; }
        public decimal DraftPct { get; set; }
    }
}