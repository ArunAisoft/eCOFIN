using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class LedgerAccountService : ILedgerAccountService
    {
        private readonly BilzFinDbContext _context;

        public LedgerAccountService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<List<LedgerAccountDto>> GetAllAccountsAsync()
        {
            try
            {
                return await _context.CfnAccounts
                    .AsNoTracking()
                    .Where(x => x.Accountstatus != "OBSLT")
                    .OrderBy(x => x.Accountcode)
                    .Select(x => new LedgerAccountDto
                    {
                        AccountCode = x.Accountcode,
                        Description = x.Description ?? "",
                        AccountType = x.Accounttype ?? "",
                        NatureOfAccount = x.Natureofaccount ?? "",
                        AccountStatus = x.Accountstatus ?? "ACTVE",
                        ControlAccount = x.Controlaccount ?? "N",
                        Banker = x.Bankcode,
                        BillwiseAppl = x.Billwiseappl,
                        BudgetAppl = x.Budgetappl,
                        CostAppl = x.Costappl,
                        SubledgerAppl = x.Subledgerappl,
                        EmployeeAppl = x.Employeeappl,
                        CostTypeAppl = x.Costtypeappl,
                        ExpenseAppl = x.Expenseappl,
                        CreatedOn = x.Createdon
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving accounts: " + ex.Message);
            }
        }

        public async Task<List<ParameterDto>> GetAccountTypesAsync()
        {
            try
            {
                return await _context.CfnCfparvalues
                    .AsNoTracking()
                    .Where(x => x.Parametergroup == "ACCTY")
                    .OrderBy(x => x.Parameterdescription)
                    .Select(x => new ParameterDto
                    {
                        ParameterGroup = x.Parametergroup ?? "",
                        ParameterCode = x.Parametercode ?? "",
                        ParameterDescription = x.Parameterdescription ?? "",
                        ActiveStatus = x.Activestatus
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving account types: " + ex.Message);
            }
        }

        public async Task<List<ParameterDto>> GetAccountNaturesAsync()
        {
            try
            {
                return await _context.CfnCfparvalues
                    .AsNoTracking()
                    .Where(x => x.Parametergroup == "NOACT")
                    .OrderBy(x => x.Parameterdescription)
                    .Select(x => new ParameterDto
                    {
                        ParameterGroup = x.Parametergroup ?? "",
                        ParameterCode = x.Parametercode ?? "",
                        ParameterDescription = x.Parameterdescription ?? "",
                        ActiveStatus = x.Activestatus
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving account natures: " + ex.Message);
            }
        }

        public async Task<List<BankListDto>> GetBanksAsync()
        {
            try
            {
                return await _context.CfnBanks
                    .AsNoTracking()
                    .Where(x => x.Objectstatus != "OBSLT")
                    .OrderBy(x => x.Bankcode)
                    .Select(x => new BankListDto
                    {
                        BankCode = x.Bankcode,
                        Name = x.Name ?? ""
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving banks: " + ex.Message);
            }
        }

        public async Task<List<EfcAccountDto>> GetEfcAccountsAsync()
        {
            // From your spec: EFC Account dropdown is Yes / No
            await Task.CompletedTask;
            return new List<EfcAccountDto>
            {
                new() { Code = "Y", Description = "Yes" },
                new() { Code = "N", Description = "No" }
            };
        }

        public async Task<(bool Success, string Message)> SaveOrUpdateAccountAsync(SaveAccountRequest model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.AccountCode))
                    return (false, "Account Code is required.");
                if (string.IsNullOrWhiteSpace(model.Description))
                    return (false, "Description is required.");
                if (string.IsNullOrWhiteSpace(model.AccountType))
                    return (false, "Account Type is required.");
                if (string.IsNullOrWhiteSpace(model.NatureOfAccount))
                    return (false, "Nature of Account is required.");

                var code = model.AccountCode.Trim();
                var status = (model.AccountStatus ?? "ACTVE").Trim().ToUpper();
                var loggedIn = Trunc(model.Username, 30) ?? code;
                var loc = Trunc(model.Location ?? "BILZ", 5) ?? "BILZ";
                var now = DateTime.Now;

                var existing = await _context.CfnAccounts.FirstOrDefaultAsync(x => x.Accountcode == code);

                if (existing != null)
                {
                    existing.Description = Trunc(model.Description?.Trim(), 100);
                    existing.Accounttype = model.AccountType;
                    existing.Natureofaccount = model.NatureOfAccount;
                    existing.Accountstatus = status;
                    existing.Controlaccount = YN(model.ControlAccount);
                    existing.Bankcode = model.Banker;
                    existing.Billwiseappl = YN(model.BillwiseAppl);
                    existing.Budgetappl = YN(model.BudgetAppl);
                    existing.Costappl = YN(model.CostAppl);
                    existing.Subledgerappl = YN(model.SubledgerAppl);
                    existing.Employeeappl = YN(model.EmployeeAppl);
                    existing.Costtypeappl = YN(model.CostTypeAppl);
                    existing.Expenseappl = YN(model.ExpenseAppl);
                    existing.CtrlLastupdate = now;
                    existing.CtrlUsername = loggedIn;
                    existing.CtrlLocationcode = loc;
                    existing.CtrlNextrefrflag = "N";

                    await _context.SaveChangesAsync();
                    return (true, "Account updated successfully.");
                }

                _context.CfnAccounts.Add(new CfnAccount
                {
                    Accountcode = Trunc(code, 20)!,
                    Description = Trunc(model.Description?.Trim(), 100),
                    Natureofaccount = model.NatureOfAccount,
                    Createdon = now,
                    Accountstatus = status,
                    Controlaccount = YN(model.ControlAccount),
                    Accounttype = model.AccountType,
                    Billwiseappl = YN(model.BillwiseAppl),
                    Costappl = YN(model.CostAppl),
                    Stockappl = "N",
                    Budgetappl = YN(model.BudgetAppl),
                    Subledgerappl = YN(model.SubledgerAppl),
                    Employeeappl = YN(model.EmployeeAppl),
                    Productappl = "N",
                    Expenseappl = YN(model.ExpenseAppl),
                    Costtypeappl = YN(model.CostTypeAppl),
                    Bankcode = model.Banker,

                    CtrlOnholdno = $"{loc}ACC{now:yyyyMMddHHmmssfff}",
                    CtrlStatus = "Post",
                    CtrlCancelflag = "N",
                    CtrlLocationcode = loc,
                    CtrlAccperiod = model.ActivatePeriod,
                    CtrlUsername = loggedIn,
                    CtrlCreatedon = now,
                    CtrlLastupdate = now,
                    CtrlLogextract = "N",
                    CtrlTrglocationcode = loc,
                    CtrlLogextracttype = "N",
                    CtrlNextrefrflag = "N"
                });

                await _context.SaveChangesAsync();
                return (true, "Account created successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                var inner = dbEx.InnerException?.Message ?? dbEx.Message;
                return (false, "DB error while saving account: " + inner);
            }
            catch (Exception ex)
            {
                return (false, "Error while saving account: " + ex.Message);
            }
        }

        private static string YN(string? v) =>
            string.Equals(v, "Y", StringComparison.OrdinalIgnoreCase) ? "Y" : "N";

        private static string? Trunc(string? value, int maxLen) =>
            value == null ? null : (value.Length > maxLen ? value[..maxLen] : value);
    }
}
