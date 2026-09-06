using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCOFIN.Application.DTOs.Reports
{
    public class TdsReportFilterModel
    {
        public string? AccountCode { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }

    public class TdsReportRowDto
    {
        public string? TdsAccount { get; set; }
        public string? VchrNumber { get; set; }
        public string? VchrDate { get; set; }
        public string? SubAccountCode { get; set; }
        public string? SubAccountCodeDesc { get; set; }
        public string? DbCrFlag { get; set; }
        public decimal? TdsDedAmount { get; set; }
        public decimal? TdsAmount { get; set; }
        public decimal? BillAmount { get; set; }
        public string? TdsCode { get; set; }
        public decimal? Amount { get; set; }
        public string? TdsDescription { get; set; }
    }

    public class TdsAccountDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class VoucherDetailDto
    {
        public string? VchrNumber { get; set; }
        public string? VchrDate { get; set; }
        public string? AccountCode { get; set; }
        public string? AccountDesc { get; set; }
        public string? SubAccountCode { get; set; }
        public string? SubAccountDesc { get; set; }
        public string? DbCrFlag { get; set; }
        public decimal? VoucherAmount { get; set; }
        public string? Particulars { get; set; }
        public string? OnHoldNo { get; set; }
    }
}