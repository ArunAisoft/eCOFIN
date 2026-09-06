using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class EmployeeService : IEmployeeService
    {
        private readonly BilzFinDbContext _context;

        public EmployeeService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeeAsync()
        {
            try
            {
                var data = await _context.CfnEmployees
                    .AsNoTracking().OrderBy(x => x.Employeecode).ToListAsync();

                var allLinks = await _context.CfnAccemployees.AsNoTracking().ToListAsync();
                var linkMap = allLinks
                    .GroupBy(x => x.Employeecode.ToUpper())
                    .ToDictionary(g => g.Key, g => g.First().Accountcode);

                return data.Select(x => new EmployeeDto
                {
                    EmployeeCode = x.Employeecode,
                    EmployeeName = x.Employeename ?? "",
                    EmployeeType = x.Employeetype ?? "",
                    BankAccount = x.Bankaccount ?? "",
                    //ReportTo = x.ReportTo ?? "",
                    ObjectStatus = x.Objectstatus ?? "ACTVE",
                    AddrLine1 = x.AddrLine1 ?? "",
                    AddrLine2 = x.AddrLine2 ?? "",
                    AddrLine3 = x.AddrLine3 ?? "",
                    AddrLine4 = x.AddrLine4 ?? "",
                    AddrCity = x.AddrCity ?? "",
                    AddrPin = x.AddrPin ?? "",
                    AddrState = x.AddrState ?? "",
                    AddrCountry = x.AddrCountry ?? "",
                    CommTelephone1 = x.CommTelephone1 ?? "",
                    CommTelephone2 = x.CommTelephone2 ?? "",
                    CommFaxno = x.CommFaxno ?? "",
                    CommTelexno = x.CommTelexno ?? "",
                    CommEmail = x.CommEmail ?? "",
                    CommGrams = x.CommGrams ?? "",
                    CommContactperson = x.CommContactperson ?? "",
                    AccountCode = linkMap.TryGetValue(x.Employeecode.ToUpper(), out var ac) ? ac : null
                });
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving employees: " + ex.Message); }
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllActiveEmployeesAsync()
        {
            try
            {
                return await _context.CfnEmployees.AsNoTracking()
                    .Where(x => x.Objectstatus == "ACTVE").OrderBy(x => x.Employeecode)
                    .Select(x => new EmployeeDto
                    {
                        EmployeeCode = x.Employeecode,
                        EmployeeName = x.Employeename,
                        EmployeeType = x.Employeetype,
                        BankAccount = x.Bankaccount,
                        CommTelephone1 = x.CommTelephone1,
                        CommEmail = x.CommEmail,
                        AddrCity = x.AddrCity,
                        ObjectStatus = x.Objectstatus
                    }).ToListAsync();
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving active employees: " + ex.Message); }
        }

        public async Task<(bool Success, string Message)> SaveOrUpdateEmployeeAsync(EmployeeCreateModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.EmployeeCode)) return (false, "Employee Code is required.");
                if (string.IsNullOrWhiteSpace(model.EmployeeName)) return (false, "Employee Name is required.");

                var status = (model.ObjectStatus ?? "ACTVE").Trim().ToUpper();
                if (status != "ACTVE" && status != "INACTV")
                    return (false, "Object Status must be 'ACTVE' or 'INACTV'.");

                var code = model.EmployeeCode.Trim().ToUpper();
                var existing = await _context.CfnEmployees.FirstOrDefaultAsync(x => x.Employeecode.ToUpper() == code);

                if (existing != null)
                {
                    existing.Employeename = model.EmployeeName?.Trim();
                    existing.Employeetype = model.EmployeeType?.Trim();
                    existing.Bankaccount = model.BankAccount?.Trim();
                    //existing.ReportTo = model.ReportTo?.Trim();
                    existing.Objectstatus = status;
                    existing.AddrLine1 = model.AddrLine1?.Trim();
                    existing.AddrLine2 = model.AddrLine2?.Trim();
                    existing.AddrLine3 = model.AddrLine3?.Trim();
                    existing.AddrLine4 = model.AddrLine4?.Trim();
                    existing.AddrCity = model.AddrCity?.Trim();
                    existing.AddrPin = model.AddrPin?.Trim();
                    existing.AddrState = model.AddrState?.Trim();
                    existing.AddrCountry = model.AddrCountry?.Trim();
                    existing.CommTelephone1 = model.CommTelephone1?.Trim();
                    existing.CommTelephone2 = model.CommTelephone2?.Trim();
                    existing.CommFaxno = model.CommFaxno?.Trim();
                    existing.CommTelexno = model.CommTelexno?.Trim();
                    existing.CommEmail = model.CommEmail?.Trim();
                    existing.CommGrams = model.CommGrams?.Trim();
                    existing.CommContactperson = model.CommContactperson?.Trim();
                    existing.CtrlLastupdate = DateTime.Now;
                    existing.CtrlUsername = model.Username;
                    existing.CtrlLocationcode = model.Location;
                    existing.CtrlAccperiod = model.AccPeriod;
                    existing.CtrlNextrefrflag = "N";
                    await _context.SaveChangesAsync();
                    return (true, "Employee updated successfully.");
                }

                _context.CfnEmployees.Add(new CfnEmployee
                {
                    Employeecode = code,
                    Employeename = model.EmployeeName?.Trim(),
                    Employeetype = model.EmployeeType?.Trim(),
                    Bankaccount = model.BankAccount?.Trim(),
                    //ReportTo = model.ReportTo?.Trim(),
                    Objectstatus = status,
                    AddrLine1 = model.AddrLine1?.Trim(),
                    AddrLine2 = model.AddrLine2?.Trim(),
                    AddrLine3 = model.AddrLine3?.Trim(),
                    AddrLine4 = model.AddrLine4?.Trim(),
                    AddrCity = model.AddrCity?.Trim(),
                    AddrPin = model.AddrPin?.Trim(),
                    AddrState = model.AddrState?.Trim(),
                    AddrCountry = model.AddrCountry?.Trim(),
                    CommTelephone1 = model.CommTelephone1?.Trim(),
                    CommTelephone2 = model.CommTelephone2?.Trim(),
                    CommFaxno = model.CommFaxno?.Trim(),
                    CommTelexno = model.CommTelexno?.Trim(),
                    CommEmail = model.CommEmail?.Trim(),
                    CommGrams = model.CommGrams?.Trim(),
                    CommContactperson = model.CommContactperson?.Trim(),
                    CtrlStatus = "Post",
                    CtrlCancelflag = "N",
                    CtrlCreatedon = DateTime.Now,
                    CtrlLastupdate = DateTime.Now,
                    CtrlUsername = model.Username,
                    CtrlLocationcode = model.Location,
                    CtrlAccperiod = model.AccPeriod,
                    CtrlLogextract = "N",
                    CtrlNextrefrflag = "N"
                });
                await _context.SaveChangesAsync();
                return (true, "Employee created successfully.");
            }
            catch (Exception ex) { return (false, "Error while saving employee: " + ex.Message); }
        }

        public async Task<IEnumerable<AccEmployeeDto>> GetByEmployeeAsync(string employeeCode)
        {
            try
            {
                return await _context.CfnAccemployees.AsNoTracking()
                    .Where(x => x.Employeecode == employeeCode)
                    .OrderBy(x => x.Accountcode)
                    .Select(x => new AccEmployeeDto
                    {
                        AccountCode = x.Accountcode,
                        EmployeeCode = x.Employeecode,
                        EmployeeStatus = x.Employeestatus ?? "ACTVE"
                    }).ToListAsync();
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving account-employee links: " + ex.Message); }
        }

        public async Task<IEnumerable<AccountDropdownDto>> GetEmployeeAccountsAsync()
        {
            try
            {
                return await _context.CfnAccounts.AsNoTracking()
                    .Where(x => x.Accountstatus != "OBSLT"
                             && x.Accounttype == "STADV"
                             && x.CtrlNextrefrflag == "N")
                    .OrderBy(x => x.Accountcode)
                    .Select(x => new AccountDropdownDto
                    {
                        AccountCode = x.Accountcode,
                        Description = x.Description
                    }).ToListAsync();
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving employee accounts: " + ex.Message); }
        }

        public async Task<(bool Success, string Message)> SaveAccEmployeeAsync(AccEmployeeCreateModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.EmployeeCode)) return (false, "Employee Code is required.");
                if (model.Rows == null || model.Rows.Count == 0) return (false, "At least one account row is required.");

                foreach (var row in model.Rows)
                {
                    if (string.IsNullOrWhiteSpace(row.AccountCode)) return (false, "Account Code is required for all rows.");
                    var s = (row.EmployeeStatus ?? "ACTVE").Trim().ToUpper();
                    if (s != "ACTVE" && s != "INACTV")
                        return (false, $"Invalid status '{row.EmployeeStatus}' for account '{row.AccountCode}'.");
                }

                var existing = await _context.CfnAccemployees
                    .Where(x => x.Employeecode == model.EmployeeCode.Trim()).ToListAsync();
                _context.CfnAccemployees.RemoveRange(existing);

                foreach (var row in model.Rows)
                    _context.CfnAccemployees.Add(new CfnAccemployee
                    {
                        Accountcode = row.AccountCode.Trim(),
                        Employeecode = model.EmployeeCode.Trim(),
                        Employeestatus = (row.EmployeeStatus ?? "ACTVE").Trim().ToUpper()
                    });

                await _context.SaveChangesAsync();
                return (true, "Account-employee links saved successfully.");
            }
            catch (Exception ex) { return (false, "Error while saving: " + ex.Message); }
        }

        public async Task<IEnumerable<PendingPersonnelDto>> GetPendingPersonnelAsync()
        {
            try
            {
                var result = await (
                    from p in _context.Personels.AsNoTracking()
                    join e in _context.CfnEmployees.AsNoTracking()
                        on p.EmpNo.ToUpper() equals e.Employeecode.ToUpper()
                        into empJoin
                    from e in empJoin.DefaultIfEmpty()
                    where e == null
                    orderby p.EmpNo
                    select new PendingPersonnelDto
                    {
                        EmpNo = p.EmpNo,
                        Name = p.Name,
                        Padd1 = p.Padd1,
                        Padd2 = p.Padd2,
                        PCity = p.Pcity,
                        PState = p.Pstate,
                        Pincode = p.Pincode.ToString(),
                        Pcountry = p.Pcountry,
                        Tel = p.Tel,
                        Mobile = p.Mobile,
                        Bankaccno = p.Bankaccno
                    }
                ).ToListAsync();

                return result;
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving pending personnel: " + ex.Message); }
        }

        public async Task<IEnumerable<AccountDropdownDto>> GetEmpAccountsAsync()
        {
            try
            {
                return await _context.CfnAccounts.AsNoTracking()
                    .Where(x => x.Accountstatus != "OBSLT" && x.Accounttype == "STADV")
                    .OrderBy(x => x.Accountcode)
                    .Select(x => new AccountDropdownDto
                    {
                        AccountCode = x.Accountcode,
                        Description = x.Description
                    }).ToListAsync();
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving accounts: " + ex.Message); }
        }

        public async Task<(bool Success, string Message)> ImportEmployeeAsync(ImportEmployeeModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.EmployeeCode)) return (false, "Employee Code is required.");
                if (string.IsNullOrWhiteSpace(model.EmployeeName)) return (false, "Employee Name is required.");
                if (string.IsNullOrWhiteSpace(model.AccountCode)) return (false, "Account is required.");

                var code = model.EmployeeCode.Trim().ToUpper();
                var name = model.EmployeeName.Trim().ToUpper();
                if (await _context.CfnEmployees.AnyAsync(x => x.Employeecode.ToUpper() == code))
                    return (false, $"Employee '{code} - {name}' is already imported.");

                _context.CfnEmployees.Add(BuildImportEntity(code, model));
                await _context.SaveChangesAsync();
                await UpsertAccEmployeeAsync(code, model.AccountCode.Trim(), "ACTVE");
                return (true, $"Employee '{code} - {name}' imported successfully.");
            }
            catch (Exception ex) { return (false, "Error while importing employee: " + ex.Message); }
        }

        public async Task<(bool Success, string Message)> ImportEmployeesAsync(IEnumerable<ImportEmployeeModel> models)
        {
            var list = models?.ToList() ?? new List<ImportEmployeeModel>();
            if (list.Count == 0) return (false, "No employees provided.");

            int imported = 0;
            var errors = new List<string>();

            foreach (var model in list)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(model.EmployeeCode)) { errors.Add("Empty code skipped."); continue; }
                    if (string.IsNullOrWhiteSpace(model.AccountCode)) { errors.Add($"{model.EmployeeCode}: no account."); continue; }

                    var code = model.EmployeeCode.Trim().ToUpper();
                    if (await _context.CfnEmployees.AnyAsync(x => x.Employeecode.ToUpper() == code))
                    { errors.Add($"{code}: already imported."); continue; }

                    _context.CfnEmployees.Add(BuildImportEntity(code, model));
                    await _context.SaveChangesAsync();
                    await UpsertAccEmployeeAsync(code, model.AccountCode.Trim(), "ACTVE");
                    imported++;
                }
                catch (Exception ex) { errors.Add($"{model.EmployeeCode}: {ex.Message}"); }
            }

            if (imported == 0) return (false, "No employees imported. " + string.Join(" | ", errors));
            var msg = $"{imported} employee(s) imported successfully.";
            if (errors.Count > 0) msg += " Skipped: " + string.Join(" | ", errors);
            return (true, msg);
        }

        private static string? Trunc(string? value, int maxLen)
            => value == null ? null : (value.Length > maxLen ? value[..maxLen] : value);

        private CfnEmployee BuildImportEntity(string code, ImportEmployeeModel model) => new()
        {
            Employeecode = Trunc(code, 10)!,
            Employeename = Trunc(model.EmployeeName?.Trim(), 100),
            Bankaccount = Trunc(model.BankAccount?.Trim(), 15),
            //ReportTo = Trunc(model.ReportTo?.Trim(), 10),
            Objectstatus = (model.ObjectStatus ?? "ACTVE").Trim().ToUpper(),
            AddrLine1 = Trunc(model.AddrLine1?.Trim(), 100),
            AddrLine2 = Trunc(model.AddrLine2?.Trim(), 100),
            AddrCity = Trunc(model.AddrCity?.Trim(), 50),
            AddrPin = Trunc(model.AddrPin?.Trim(), 10),
            AddrState = Trunc(model.AddrState?.Trim(), 50),
            AddrCountry = Trunc(model.AddrCountry?.Trim(), 50),
            CommTelephone1 = Trunc(model.CommTelephone1?.Trim(), 15),
            CommTelephone2 = Trunc(model.CommTelephone2?.Trim(), 15),
            CtrlStatus = "Post",
            CtrlCancelflag = "N",
            CtrlCreatedon = DateTime.Now,
            CtrlLastupdate = DateTime.Now,
            CtrlUsername = Trunc(model.Username, 30),
            CtrlLocationcode = Trunc(model.Location ?? "BILZ", 5),
            CtrlTrglocationcode = Trunc(model.Location ?? "BILZ", 5),
            CtrlLogextract = "N",
            CtrlLogextracttype = "N",
            CtrlNextrefrflag = "N"
        };

        private async Task UpsertAccEmployeeAsync(string employeeCode, string accountCode, string status)
        {
            var link = await _context.CfnAccemployees
                .FirstOrDefaultAsync(x => x.Employeecode == employeeCode && x.Accountcode == accountCode);
            if (link != null)
                link.Employeestatus = status;
            else
                _context.CfnAccemployees.Add(new CfnAccemployee
                {
                    Accountcode = accountCode,
                    Employeecode = employeeCode,
                    Employeestatus = status
                });
            await _context.SaveChangesAsync();
        }
    }
}