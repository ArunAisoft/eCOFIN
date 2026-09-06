namespace eCOFIN.Application.DTOs.Masters
{
    /* ================= GRID ================= */

    public class PrefixLinkDto
    {
        public string ProdPrefix { get; set; } = string.Empty;
        public string AccountCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VchrType { get; set; } = string.Empty;
        public string ProdPrefixType { get; set; } = string.Empty;
        public string ProdLevyType { get; set; } = string.Empty;   // 'L' or 'P'
        public string DbcrFlag { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string? ObjectStatus { get; set; }
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages =>
            PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
    }

    /* ================= LOOKUPS ================= */

    public class AccountLookupDto
    {
        public string AccountCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CustomerLookupDto
    {
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
    }

    public class PrefixLookupsDto
    {
        public List<AccountLookupDto> Accounts { get; set; } = new();
        public List<CustomerLookupDto> Customers { get; set; } = new();
    }

    /* ================= ADD CONTEXT ================= */

    /// <summary>
    /// One row of cfn_genhelp. The legacy application used this table as a
    /// metadata registry: for a given editable column (helptopic) it says which
    /// lookup window to open, which table backs it, and which column receives
    /// the selected value.
    /// </summary>
    public class HelpTopicDto
    {
        public string HelpId { get; set; } = string.Empty;
        public string HelpTopic { get; set; } = string.Empty;
        public string RegFlag { get; set; } = string.Empty;
        public string? HelpObjectName { get; set; }
        public string? HelpUpdateableColumn { get; set; }
        public string? TableName { get; set; }
        public string? DestColumn { get; set; }
    }

    /// <summary>
    /// Everything the Add button needs, in one round trip. The legacy screen
    /// issued about twelve queries for this.
    /// </summary>
    public class PrefixAddContextDto
    {
        /// <summary>Resolved from cfn_task.taskinterfacerefr = 'w_accountottolink'.</summary>
        public string TaskId { get; set; } = string.Empty;

        /// <summary>cfn_userpermission.levelnumber. -1 when the user has no row.</summary>
        public int LevelNumber { get; set; } = -1;

        public string LevelLabel { get; set; } = "None";

        public bool CanBrowse { get; set; }
        public bool CanAdd { get; set; }
        public bool CanHold { get; set; }
        public bool CanPost { get; set; }
        public bool CanObsolete { get; set; }

        /// <summary>Keyed by help topic: "customercode", "accountcode".</summary>
        public Dictionary<string, HelpTopicDto> HelpTopics { get; set; } = new();

        /// <summary>
        /// True when cfn_voucheraccounts has rows for the reference type, which
        /// narrows the account list. Verified empty for VENDR on this database,
        /// so this is normally false — same as the legacy app.
        /// </summary>
        public bool AccountsRestricted { get; set; }

        public List<AccountLookupDto> Accounts { get; set; } = new();
        public List<CustomerLookupDto> Customers { get; set; } = new();
    }

    /* ================= WRITE ================= */

    public class PrefixRowPayload
    {
        public string ProdPrefix { get; set; } = string.Empty;
        public string AccountCode { get; set; } = string.Empty;
        public string ProdLevyType { get; set; } = string.Empty;
        public string DbcrFlag { get; set; } = string.Empty;
    }

    public class SavePrefixLinksRequest
    {
        public string VchrType { get; set; } = string.Empty;
        public string PrefixType { get; set; } = string.Empty;
        public List<PrefixRowPayload> Rows { get; set; } = new();
        public string? Username { get; set; }
        public string? Location { get; set; }
    }

    public class DeletePrefixLinkRequest
    {
        public string ProdPrefix { get; set; } = string.Empty;
        public string AccountCode { get; set; } = string.Empty;
        public string VchrType { get; set; } = string.Empty;
        public string PrefixType { get; set; } = string.Empty;
    }
}