using eCOFIN.Application.DTOs.Vouchers;
using eCOFIN.Application.Interfaces.Vouchers;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace eCOFIN.Infrastructure.Services.Vouchers
{
    public class BanksAndAccountsService : IBanksAndAccountsService
    {
        private readonly BilzFinDbContext _context;
        private readonly ILogger<BanksAndAccountsService> _logger;

        public BanksAndAccountsService(BilzFinDbContext context, ILogger<BanksAndAccountsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private static decimal ComputeBalance(decimal? closing, decimal? onholdDr, decimal? postedDr, decimal? onholdCr, decimal? postedCr) => (closing ?? 0m) + ((onholdDr ?? 0m) - (postedDr ?? 0m)) - ((onholdCr ?? 0m) - (postedCr ?? 0m));

        private static BankAccountDto MapToBankAccountDto(CfnAccount a) => new()
        {
            AccountCode = a.Accountcode,
            AccountName = a.Description,
            AccountStatus = a.Accountstatus,
            BankCode = a.Bankcode,
            AccountType = a.Accounttype,
            BillwiseAppl = a.Billwiseappl,
            CostAppl = a.Costappl,
            StockAppl = a.Stockappl,
            BudgetAppl = a.Budgetappl,
            SubledgerAppl = a.Subledgerappl,
            EmployeeAppl = a.Employeeappl,
            ProductAppl = a.Productappl,
            ExpenseAppl = a.Expenseappl,
            CostTypeAppl = a.Costtypeappl,
            BudgetType = a.Budgettype
        };


        private async Task<Dictionary<string, decimal>> LoadBalancesAsync(IQueryable<CfnAccount> accountScope, CancellationToken ct)
        {
            var rows = await (
                from g in _context.CfnGeneralledgers.AsNoTracking()
                join a in accountScope on g.Accountcode equals a.Accountcode
                join c in _context.CfnAccncalenders.AsNoTracking() on g.Accperiod equals c.Accperiod
                where c.Periodstate == "CLOSD" || c.Periodstate == "OPNPR"
                select new
                {
                    g.Accountcode,
                    c.Sequence,
                    g.Postedclosingbalance,
                    g.Postedopeningbalance,
                    g.Onholddebitamount,
                    g.Onholdcreditamount,
                    g.Posteddebitamount,
                    g.Postedcreditamount
                }
            ).ToListAsync(ct);

            return rows
                .Where(g => (g.Postedopeningbalance ?? 0m) != 0m
                         || (g.Onholddebitamount ?? 0m) != 0m
                         || (g.Onholdcreditamount ?? 0m) != 0m
                         || (g.Posteddebitamount ?? 0m) != 0m
                         || (g.Postedcreditamount ?? 0m) != 0m)
                .GroupBy(g => g.Accountcode!)
                .ToDictionary(
                    g => g.Key,
                    g =>
                    {
                        var latest = g.OrderByDescending(x => x.Sequence).First();
                        return ComputeBalance(
                            latest.Postedclosingbalance,
                            latest.Onholddebitamount, latest.Posteddebitamount,
                            latest.Onholdcreditamount, latest.Postedcreditamount);
                    });
        }

        public async Task<IEnumerable<BanksAndAccountsDto>> GetAllBanksWithAccountsAsync(CancellationToken ct = default)
        {
            try
            {
                var banks = await _context.CfnBanks.AsNoTracking().OrderBy(b => b.Name).ToListAsync(ct);
                if (!banks.Any()) return [];

                var accounts = await (
                    from a in _context.CfnAccounts.AsNoTracking()
                    join b in _context.CfnBanks.AsNoTracking() on a.Bankcode equals b.Bankcode
                    where a.Accountstatus == "ACTVE"
                    orderby a.Description
                    select a
                ).ToListAsync(ct);

                var accountsByBank = accounts.GroupBy(a => a.Bankcode!).ToDictionary(g => g.Key, g => g.ToList());

                return banks.Select(b => new BanksAndAccountsDto
                {
                    BankCode = b.Bankcode,
                    BankName = b.Name,
                    ObjectStatus = b.Objectstatus,
                    BankAccounts = accountsByBank.TryGetValue(b.Bankcode!, out var accts) ? accts.Select(MapToBankAccountDto).ToList() : []
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Banks with Accounts.");
                throw new ApplicationException("An error occurred while retrieving Banks with Accounts.", ex);
            }
        }

        public async Task<IEnumerable<BanksAndAccountsDto>> GetAllBanksWithAccountsVouchersBalancesAsync(string userName, string voucherGroup, CancellationToken ct = default)
        {
            try
            {
                var banks = await _context.CfnBanks.AsNoTracking().OrderBy(b => b.Name).ToListAsync(ct);
                if (!banks.Any()) return [];

                var accountScope = _context.CfnAccounts.AsNoTracking().Where(a => a.Accountstatus == "ACTVE").Join(_context.CfnBanks.AsNoTracking(), a => a.Bankcode, b => b.Bankcode, (a, b) => a);
                var accounts = await accountScope.OrderBy(a => a.Description).ToListAsync(ct);
                if (!accounts.Any())
                    return banks.Select(b => new BanksAndAccountsDto
                    {
                        BankCode = b.Bankcode,
                        BankName = b.Name,
                        ObjectStatus = b.Objectstatus,
                        BankAccounts = []
                    });

                var balances = await LoadBalancesAsync(accountScope, ct);

                var vtRows = (await (
                    from uv in _context.CfnUservchrs.AsNoTracking()
                    join vsd in _context.CfnVouchersysdata.AsNoTracking() on uv.Vouchertype equals vsd.Vouchertype
                    join vt in _context.CfnVchrtypes.AsNoTracking() on vsd.Vouchertype equals vt.Vouchertype
                    join a in accountScope on vsd.Code equals a.Accountcode
                    where uv.Username == userName
                       && vt.Vouchergroup == voucherGroup
                       && vt.Activestatus == "Y"
                    select new { a.Accountcode, vt.Vouchertype, vt.Vouchertypedescription, vt.Vouchergroup }
                ).ToListAsync(ct))
                .DistinctBy(x => new { x.Accountcode, x.Vouchertype })
                .ToList();

                var vtByAccount = vtRows
                    .GroupBy(x => x.Accountcode!)
                    .ToDictionary(g => g.Key, g => g.Select(x => new VoucherTypeDto
                    {
                        VoucherType = x.Vouchertype,
                        VoucherDescription = x.Vouchertypedescription,
                        VoucherGroup = x.Vouchergroup
                    }).ToList());

                var accountsByBank = accounts.GroupBy(a => a.Bankcode!).ToDictionary(g => g.Key, g => g.ToList());

                return banks.Select(b => new BanksAndAccountsDto
                {
                    BankCode = b.Bankcode,
                    BankName = b.Name,
                    ObjectStatus = b.Objectstatus,
                    BankAccounts = accountsByBank.TryGetValue(b.Bankcode!, out var accts)
                        ? accts.Select(a => new BankAccountDto
                        {
                            AccountCode = a.Accountcode,
                            AccountName = a.Description,
                            AccountStatus = a.Accountstatus,
                            BankCode = a.Bankcode,
                            AccountType = a.Accounttype,
                            BillwiseAppl = a.Billwiseappl,
                            CostAppl = a.Costappl,
                            StockAppl = a.Stockappl,
                            BudgetAppl = a.Budgetappl,
                            SubledgerAppl = a.Subledgerappl,
                            EmployeeAppl = a.Employeeappl,
                            ProductAppl = a.Productappl,
                            ExpenseAppl = a.Expenseappl,
                            CostTypeAppl = a.Costtypeappl,
                            BudgetType = a.Budgettype,
                            Balance = balances.TryGetValue(a.Accountcode!, out var bal) ? bal : 0m,
                            VoucherTypes = vtByAccount.TryGetValue(a.Accountcode!, out var vts) ? vts : []
                        }).ToList()
                        : []
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Banks with Accounts, Vouchers and Balances.");
                throw new ApplicationException("Error retrieving Banks with Accounts, Vouchers and Balances.", ex);
            }
        }

        public async Task<IEnumerable<BanksAndAccountsDto>> GetAllCreditAccountsVouchersAsync(string userName, string voucherGroup, CancellationToken ct = default)
        {
            try
            {
                var accountScope = _context.CfnAccounts.AsNoTracking().Where(a => a.Accounttype == "CRDT" && a.Accountstatus == "ACTVE" && a.CtrlNextrefrflag == "N");
                var accounts = await accountScope.OrderBy(a => a.Description).ToListAsync(ct);
                if (!accounts.Any()) return [];

                var vendorLinks = await (
                    from av in _context.CfnAccvendors.AsNoTracking()
                    join a in accountScope on av.Accountcode equals a.Accountcode
                    join v in _context.CfnVendors.AsNoTracking() on av.Vendorcode equals v.Vendorcode
                    where av.Vendorstatus == "ACTVE"
                    orderby v.Vendorname
                    select new { av.Accountcode, av.Vendorcode, v.Vendorname }
                ).ToListAsync(ct);

                var vendorsByAccount = vendorLinks
                    .GroupBy(x => x.Accountcode!)
                    .ToDictionary(g => g.Key, g => g
                        .Select(x => new BankAccountDto
                        {
                            BankCode = x.Accountcode,
                            AccountCode = x.Vendorcode,
                            AccountName = x.Vendorname,
                            VoucherTypes = null
                        }).ToList());

                return accounts.Select(a => new BanksAndAccountsDto
                {
                    BankCode = a.Accountcode,
                    BankName = a.Description,
                    AccountType = a.Accounttype,
                    BankAccounts = vendorsByAccount.TryGetValue(a.Accountcode!, out var vs) ? vs : []
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Accounts (with Vendors).");
                throw new ApplicationException("Error retrieving Accounts (with Vendors).", ex);
            }
        }

        public async Task<IEnumerable<VoucherTypeDto>> GetAllDebitCreditVoucherTypesAsync(string userName, string voucherGroup, CancellationToken ct = default)
        {
            try
            {
                var rows = await (
                    from uv in _context.CfnUservchrs.AsNoTracking()
                    join vt in _context.CfnVchrtypes.AsNoTracking() on uv.Vouchertype equals vt.Vouchertype
                    where uv.Username == userName
                       && vt.Vouchergroup == voucherGroup
                       && vt.Activestatus == "Y"
                    select new { vt.Vouchertype, vt.Vouchertypedescription, vt.Vouchergroup }
                ).ToListAsync(ct);

                return rows
                    .DistinctBy(x => x.Vouchertype)
                    .OrderBy(x => x.Vouchertype)
                    .Select(x => new VoucherTypeDto
                    {
                        VoucherType = x.Vouchertype,
                        VoucherDescription = x.Vouchertypedescription,
                        VoucherGroup = x.Vouchergroup
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Voucher Types.");
                throw new ApplicationException("Error retrieving Voucher Types.", ex);
            }
        }

        public async Task<IEnumerable<BanksAndAccountsDto>> GetAllDebitAccountsVouchersAsync(string userName, string voucherGroup, CancellationToken ct = default)
        {
            try
            {
                var accountScope = _context.CfnAccounts.AsNoTracking().Where(a => a.Accounttype == "DEBT" && a.Accountstatus == "ACTVE" && a.Billwiseappl == "Y");
                var accounts = await accountScope.OrderBy(a => a.Description).ToListAsync(ct);
                if (!accounts.Any()) return [];

                var customerLinks = await (
                    from ac in _context.CfnAcccustomers.AsNoTracking()
                    join a in accountScope on ac.Accountcode equals a.Accountcode
                    join c in _context.CfnCustomers.AsNoTracking() on ac.Customercode equals c.Customercode
                    where ac.Customerstatus == "ACTVE"
                    orderby c.Customername
                    select new { ac.Accountcode, ac.Customercode, c.Customername }
                ).ToListAsync(ct);

                var customersByAccount = customerLinks
                    .GroupBy(x => x.Accountcode!)
                    .ToDictionary(g => g.Key, g => g
                        .Select(x => new BankAccountDto
                        {
                            BankCode = x.Accountcode,
                            AccountCode = x.Customercode,
                            AccountName = x.Customername,
                            VoucherTypes = null
                        }).ToList());

                return accounts.Select(a => new BanksAndAccountsDto
                {
                    BankCode = a.Accountcode,
                    BankName = a.Description,
                    AccountType = a.Accounttype,
                    BankAccounts = customersByAccount.TryGetValue(a.Accountcode!, out var cs) ? cs : []
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Accounts (with Customers).");
                throw new ApplicationException("Error retrieving Accounts (with Customers).", ex);
            }
        }

        public async Task<IEnumerable<BanksAndAccountsDto>> GetAllCreditDebitAccountsVouchersAsync(string userName, string voucherGroup, CancellationToken ct = default)
        {
            try
            {
                var accounts = await _context.CfnAccounts.AsNoTracking().Where(a => (a.Accounttype == "DEBT" || a.Accounttype == "CRDT") && a.Accountstatus == "ACTVE").OrderBy(a => a.Description).ToListAsync(ct);
                if (!accounts.Any()) return [];

                var sharedVoucherTypes = (await (
                    from uv in _context.CfnUservchrs.AsNoTracking()
                    join vt in _context.CfnVchrtypes.AsNoTracking() on uv.Vouchertype equals vt.Vouchertype
                    where uv.Username == userName
                       && vt.Vouchergroup == voucherGroup
                       && vt.Activestatus == "Y"
                    select new VoucherTypeDto
                    {
                        VoucherType = vt.Vouchertype,
                        VoucherDescription = vt.Vouchertypedescription,
                        VoucherGroup = vt.Vouchergroup
                    }
                ).ToListAsync(ct))
                .DistinctBy(x => x.VoucherType)
                .ToList();

                var debtScope = _context.CfnAccounts.AsNoTracking().Where(a => a.Accounttype == "DEBT" && a.Accountstatus == "ACTVE");
                var crdtScope = _context.CfnAccounts.AsNoTracking().Where(a => a.Accounttype == "CRDT" && a.Accountstatus == "ACTVE");

                var customersByAccount = new Dictionary<string, List<BankAccountDto>>();
                if (accounts.Any(a => a.Accounttype == "DEBT"))
                {
                    var custLinks = await (
                        from ac in _context.CfnAcccustomers.AsNoTracking()
                        join a in debtScope on ac.Accountcode equals a.Accountcode
                        join c in _context.CfnCustomers.AsNoTracking() on ac.Customercode equals c.Customercode
                        where ac.Customerstatus == "ACTVE"
                        orderby c.Customername
                        select new { ac.Accountcode, ac.Customercode, c.Customername }
                    ).ToListAsync(ct);

                    customersByAccount = custLinks
                        .GroupBy(x => x.Accountcode!)
                        .ToDictionary(g => g.Key, g => g
                            .Select(x => new BankAccountDto
                            {
                                BankCode = x.Accountcode,
                                AccountCode = x.Customercode,
                                AccountName = x.Customername,
                                VoucherTypes = sharedVoucherTypes.ToList()
                            }).ToList());
                }

                var vendorsByAccount = new Dictionary<string, List<BankAccountDto>>();
                if (accounts.Any(a => a.Accounttype == "CRDT"))
                {
                    var vendLinks = await (
                        from av in _context.CfnAccvendors.AsNoTracking()
                        join a in crdtScope on av.Accountcode equals a.Accountcode
                        join v in _context.CfnVendors.AsNoTracking() on av.Vendorcode equals v.Vendorcode
                        where av.Vendorstatus == "ACTVE"
                        orderby v.Vendorname
                        select new { av.Accountcode, av.Vendorcode, v.Vendorname }
                    ).ToListAsync(ct);

                    vendorsByAccount = vendLinks
                        .GroupBy(x => x.Accountcode!)
                        .ToDictionary(g => g.Key, g => g
                            .Select(x => new BankAccountDto
                            {
                                BankCode = x.Accountcode,
                                AccountCode = x.Vendorcode,
                                AccountName = x.Vendorname,
                                VoucherTypes = sharedVoucherTypes.ToList()
                            }).ToList());
                }

                return accounts.Select(a => new BanksAndAccountsDto
                {
                    BankCode = a.Accountcode,
                    BankName = a.Description,
                    AccountType = a.Accounttype,
                    BankAccounts = a.Accounttype == "DEBT" ? (customersByAccount.TryGetValue(a.Accountcode!, out var cs) ? cs : []) : (vendorsByAccount.TryGetValue(a.Accountcode!, out var vs) ? vs : [])
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Accounts (with Vendors/Customers and Vouchers).");
                throw new ApplicationException("Error retrieving Accounts (with Vendors/Customers and Vouchers).", ex);
            }
        }

        public async Task<IEnumerable<BankAccountDto>> GetAllAccountsVouchersBalancesAsync(string userName, string accountType, string voucherGroup, CancellationToken ct = default)
        {
            try
            {
                var accountScope = _context.CfnAccounts.AsNoTracking().Where(a => a.Accounttype == accountType && a.Accountstatus == "ACTVE");
                var accounts = await accountScope.OrderBy(a => a.Description).ToListAsync(ct);
                if (!accounts.Any()) return [];

                var balances = await LoadBalancesAsync(accountScope, ct);

                var vtRows = (await (
                    from uv in _context.CfnUservchrs.AsNoTracking()
                    join vsd in _context.CfnVouchersysdata.AsNoTracking() on uv.Vouchertype equals vsd.Vouchertype
                    join vt in _context.CfnVchrtypes.AsNoTracking() on vsd.Vouchertype equals vt.Vouchertype
                    join a in accountScope on vsd.Code equals a.Accountcode
                    where uv.Username == userName
                       && vt.Vouchergroup == voucherGroup
                       && vt.Activestatus == "Y"
                    select new { a.Accountcode, vt.Vouchertype, vt.Vouchertypedescription, vt.Vouchergroup }
                ).ToListAsync(ct))
                .DistinctBy(x => new { x.Accountcode, x.Vouchertype })
                .ToList();

                var vtByAccount = vtRows
                    .GroupBy(x => x.Accountcode!)
                    .ToDictionary(g => g.Key, g => g.Select(x => new VoucherTypeDto
                    {
                        VoucherType = x.Vouchertype,
                        VoucherDescription = x.Vouchertypedescription,
                        VoucherGroup = x.Vouchergroup
                    }).ToList());

                return accounts.Select(a => new BankAccountDto
                {
                    AccountCode = a.Accountcode,
                    AccountName = a.Description,
                    AccountStatus = a.Accountstatus,
                    BankCode = a.Bankcode,
                    AccountType = a.Accounttype,
                    BillwiseAppl = a.Billwiseappl,
                    CostAppl = a.Costappl,
                    StockAppl = a.Stockappl,
                    BudgetAppl = a.Budgetappl,
                    SubledgerAppl = a.Subledgerappl,
                    EmployeeAppl = a.Employeeappl,
                    ProductAppl = a.Productappl,
                    ExpenseAppl = a.Expenseappl,
                    CostTypeAppl = a.Costtypeappl,
                    BudgetType = a.Budgettype,
                    Balance = balances.TryGetValue(a.Accountcode!, out var bal) ? bal : 0m,
                    VoucherTypes = vtByAccount.TryGetValue(a.Accountcode!, out var vts) ? vts : []
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Accounts, Vouchers and Balances.");
                throw new ApplicationException("Error retrieving Accounts, Vouchers and Balances.", ex);
            }
        }

        public async Task<IEnumerable<GroupAccountDto>> GetAllGroupAccountsByVoucherTypeAsync(string voucherType, CancellationToken ct = default)
        {
            try
            {
                return await (
                    from va in _context.CfnVoucheraccounts.AsNoTracking()
                    join acc in _context.CfnAccounts.AsNoTracking() on va.Accountcode equals acc.Accountcode
                    where va.Vouchertype == voucherType && acc.Accountstatus == "ACTVE"
                    orderby acc.Accountcode, acc.Description
                    select new GroupAccountDto
                    {
                        AccountCode = acc.Accountcode,
                        AccountName = acc.Description,
                        AccountType = acc.Accounttype,
                        BillwiseAppl = acc.Billwiseappl ?? "N",
                        CostAppl = acc.Costappl ?? "N",
                        StockAppl = acc.Stockappl ?? "N",
                        BudgetAppl = acc.Budgetappl ?? "N",
                        SubledgerAppl = acc.Subledgerappl ?? "N",
                        EmployeeAppl = acc.Employeeappl ?? "N",
                        ProductAppl = acc.Productappl ?? "N",
                        ExpenseAppl = acc.Expenseappl ?? "N",
                        CostTypeAppl = acc.Costtypeappl ?? "N",
                        BudgetType = acc.Budgettype ?? "N"
                    }
                ).ToListAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Group Accounts.");
                throw new ApplicationException("Error retrieving Group Accounts.", ex);
            }
        }

        public async Task<List<GroupSubAccountDto>> GetAllSubAccountsByCodeAndTypeAsync(string accountCode, string accountType, bool includeCostCentres = false, CancellationToken ct = default)
        {
            try
            {
                if (includeCostCentres)
                {
                    var costAppl = await _context.CfnAccounts.AsNoTracking().Where(a => a.Accountcode == accountCode).Select(a => a.Costappl).FirstOrDefaultAsync(ct);

                    if (!string.Equals(costAppl, "Y", StringComparison.OrdinalIgnoreCase))
                        return [];

                    return await _context.CfnCostcentres.AsNoTracking()
                        .Where(c => c.Objectstatus == "ACTVE")
                        .OrderBy(c => c.Costcentrecode)
                        .Select(c => new GroupSubAccountDto
                        {
                            Code = c.Costcentrecode,
                            Name = c.Description ?? ""
                        })
                        .ToListAsync(ct);
                }

                switch (accountType?.ToUpperInvariant())
                {
                    case "DEBT":
                        return (await (
                            from ac in _context.CfnAcccustomers.AsNoTracking()
                            join c in _context.CfnCustomers.AsNoTracking() on ac.Customercode equals c.Customercode
                            where ac.Accountcode == accountCode
                               && ac.Customerstatus == "ACTVE"
                            orderby c.Customercode
                            select new GroupSubAccountDto { Code = c.Customercode, Name = c.Customername ?? "" }
                        ).ToListAsync(ct))
                        .DistinctBy(x => x.Code)
                        .ToList();

                    case "CRDT":
                        return (await (
                            from av in _context.CfnAccvendors.AsNoTracking()
                            join v in _context.CfnVendors.AsNoTracking() on av.Vendorcode equals v.Vendorcode
                            where av.Accountcode == accountCode
                               && (v.Objectstatus == "ACTVE" || av.Vendorstatus == "ACTVE")
                            orderby v.Vendorcode
                            select new GroupSubAccountDto { Code = v.Vendorcode, Name = v.Vendorname ?? "" }
                        ).ToListAsync(ct))
                        .DistinctBy(x => x.Code)
                        .ToList();

                    case "STADV":
                        return (await (
                            from ae in _context.CfnAccemployees.AsNoTracking()
                            join e in _context.CfnEmployees.AsNoTracking() on ae.Employeecode equals e.Employeecode
                            where ae.Accountcode == accountCode
                               && (ae.Employeestatus == "ACTVE" || e.Objectstatus == "ACTVE")
                            orderby e.Employeecode
                            select new GroupSubAccountDto { Code = e.Employeecode, Name = e.Employeename ?? "" }
                        ).ToListAsync(ct))
                        .DistinctBy(x => x.Code)
                        .ToList();

                    default:
                        return [];
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving SubAccounts.");
                throw new ApplicationException("Error retrieving SubAccounts.", ex);
            }
        }

        public async Task<IEnumerable<ExportVendorInvoiceDto>> GetVendorInvoicesAsync(string invoiceAccount, string invoiceVendor, CancellationToken ct = default)
        {
            try
            {
                return await _context.CfnBills.AsNoTracking()
                    .Where(b => b.Billbalance > 0
                             && b.CtrlStatus == "Post"
                             && b.Accountcode == invoiceAccount
                             && b.Subaccountcode == invoiceVendor)
                    .OrderBy(b => b.VchrDate)
                    .Select(b => new ExportVendorInvoiceDto
                    {
                        AccountNo = b.Accountcode,
                        SubAccountNo = b.Subaccountcode,
                        CtrlOnHoldNo = b.CtrlOnholdno,
                        CtrlSequenceNo = (int?)b.CtrlSequenceno,
                        VoucherNo = b.VchrNumber,
                        VoucherDate = b.VchrDate,
                        BillNo = b.Billno,
                        BillDate = b.Billdate,
                        DueDate = b.Billduedate,
                        BillAmount = b.Billamount ?? 0m,
                        BillBalance = b.Billbalance ?? 0m,
                        AmountAdjusted = b.Billamountadjusted ?? 0m,
                        OrginalBillBalance = b.Billbalance ?? 0m,
                        OrginalAmountAdjusted = b.Billamountadjusted ?? 0m,
                        AcceptedAmount = null,
                        GINJINNo = string.Empty
                    })
                    .ToListAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vendor invoices.");
                throw new ApplicationException("Error retrieving vendor invoices.", ex);
            }
        }

        public async Task<BillAndPaymentDto> GetBillAndPaymentDetailsAsync(string accountCode, string subAccountCode, CancellationToken ct = default)
        {
            try
            {
                var cutoff = DateTime.UtcNow;
                var bills = await _context.CfnBills.AsNoTracking()
                    .Where(b => b.Billbalance > 0
                             && b.CtrlStatus == "Post"
                             && b.Accountcode == accountCode
                             && b.Subaccountcode == subAccountCode
                             && b.VchrDate <= cutoff)
                    .OrderBy(b => b.VchrDate)
                    .Select(b => new BillDetailsDto
                    {
                        CtrlOnHoldNo = b.CtrlOnholdno,
                        CtrlSequenceNo = b.CtrlSequenceno != null ? b.CtrlSequenceno.ToString() : null,
                        VoucherNo = b.VchrNumber,
                        BillNo = b.Billno,
                        BillDate = b.Billdate,
                        BillAmount = b.Billamount ?? 0m,
                        AmountAdjusted = b.Billamountadjusted ?? 0m,
                        BalanceAmount = b.Billbalance ?? 0m,
                        AcceptedAmount = null,
                        Particulars = b.VchrNarration
                    })
                    .ToListAsync(ct);

                var payments = await _context.CfnPayments.AsNoTracking()
                    .Where(p => p.Paymentamountbalance > 0
                             && p.CtrlStatus == "Post"
                             && p.Accountcode == accountCode
                             && p.Subaccountcode == subAccountCode
                             && p.VchrDate <= cutoff)
                    .OrderBy(p => p.VchrDate)
                    .Select(p => new PaymentDetailsDto
                    {
                        CtrlOnHoldNo = p.CtrlOnholdno,
                        CtrlSequenceNo = p.CtrlSequenceno != null ? p.CtrlSequenceno.ToString() : null,
                        VoucherNo = p.VchrNumber,
                        VoucherDate = p.VchrDate,
                        InstrumentNo = p.Instrumentno,
                        InstrumentDate = p.Instrumentdate,
                        PaymentAmount = p.Paymentamount ?? 0m,
                        AmountAdjusted = p.Paymentamountadjusted ?? 0m,
                        BalanceAmount = p.Paymentamountbalance ?? 0m,
                        AcceptedAmount = null,
                        Particulars = p.VchrNarration
                    })
                    .ToListAsync(ct);

                return new BillAndPaymentDto { BillDetails = bills, PaymentDetails = payments };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bill and payment details.");
                throw new ApplicationException("Error retrieving bill and payment details.", ex);
            }
        }

        public async Task SaveBillAndPaymentAdjustmentAsync(BillPaymentAdjustmentRequestDto request, CancellationToken ct = default)
        {
            if (request?.Bill == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Payments == null || !request.Payments.Any())
                throw new ArgumentException("At least one payment adjustment is required.");

            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct);
            try
            {
                var now = DateTime.UtcNow;
                var bill = await _context.CfnBills.FirstOrDefaultAsync(b => b.CtrlOnholdno == request.Bill.OnHoldNo && b.CtrlSequenceno == request.Bill.SequenceNo, ct);

                if (bill == null)
                    throw new ApplicationException("Bill not found for adjustment.");

                bill.Billbalance = request.Bill.BalanceAmount;
                bill.Billamountadjusted = request.Bill.AmountAdjusted;
                bill.CtrlLastupdate = now;

                foreach (var p in request.Payments)
                {
                    var payment = await _context.CfnPayments.FirstOrDefaultAsync(x => x.CtrlOnholdno == p.OnHoldNo && x.CtrlSequenceno == (decimal)p.SequenceNo, ct);

                    if (payment == null)
                        throw new ApplicationException($"Payment not found for OnHold No {p.OnHoldNo}.");

                    payment.Paymentamountadjusted = p.AmountAdjusted;
                    payment.Paymentamountbalance = p.BalanceAmount;
                    payment.CtrlLastupdate = now;
                }

                await _context.SaveChangesAsync(ct);
                await tx.CommitAsync(ct);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync(ct);
                _logger.LogError(ex, "Error saving bill and payment adjustment.");
                throw new ApplicationException("Error saving bill and payment adjustment.", ex);
            }
        }

        public async Task<bool> IsBillAlreadyExistsAsync(string bankCode, string bankAccount, string billNo, DateTime billDate, string? excludeOnHoldNo = null, CancellationToken ct = default)
        {
            try
            {
                var startDate = billDate.Date;
                var endDate = startDate.AddDays(1);

                var query = _context.CfnBills.AsNoTracking()
                    .Where(x => x.Accountcode == bankCode
                             && x.Subaccountcode == bankAccount
                             && x.Billno == billNo
                             && x.Billdate >= startDate
                             && x.Billdate < endDate);

                if (!string.IsNullOrWhiteSpace(excludeOnHoldNo))
                    query = query.Where(x => x.CtrlOnholdno != excludeOnHoldNo);

                return await query.AnyAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existing bill.");
                throw new ApplicationException("Error while checking existing bill.", ex);
            }
        }
    }
}