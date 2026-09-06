using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class CustomerService : ICustomerService
    {
        private readonly BilzFinDbContext _context;

        public CustomerService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomerAsync()
        {
            try
            {
                var customers = await _context.CfnCustomers
                    .AsNoTracking()
                    .OrderBy(x => x.Customercode)
                    .ToListAsync();

                var allLinks = await _context.CfnAcccustomers
                    .AsNoTracking()
                    .ToListAsync();

                var linkMap = allLinks
                    .GroupBy(x => x.Customercode.ToUpper())
                    .ToDictionary(g => g.Key, g => g.First().Accountcode);

                return customers.Select(x => new CustomerDto
                {
                    CustomerCode = x.Customercode,
                    CustomerName = x.Customername ?? "",
                    CustomerType = x.Customertype ?? "",
                    BusinessNature = x.Businessnature ?? "",
                    GeographyCode = x.Geographycode ?? "",
                    LstNoDate = x.Lstnodate ?? "",
                    CstNoDate = x.Cstnodate ?? "",
                    ApplCustomerCode = x.Applcustomercode ?? "",
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
                    AccountCode = linkMap.TryGetValue(x.Customercode.ToUpper(), out var ac) ? ac : null
                });
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving customers: " + ex.Message);
            }
        }

        public async Task<IEnumerable<CustomerDto>> GetAllActiveCustomersAsync()
        {
            try
            {
                return await _context.CfnCustomers
                    .AsNoTracking()
                    .Where(x => x.Objectstatus == "ACTVE")
                    .OrderBy(x => x.Customercode)
                    .Select(x => new CustomerDto
                    {
                        CustomerCode = x.Customercode,
                        CustomerName = x.Customername,
                        CustomerType = x.Customertype,
                        CommTelephone1 = x.CommTelephone1,
                        CommEmail = x.CommEmail,
                        AddrCity = x.AddrCity,
                        ObjectStatus = x.Objectstatus
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving active customers: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> SaveOrUpdateCustomerAsync(CustomerCreateModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.CustomerCode)) return (false, "Customer Code is required.");
                if (string.IsNullOrWhiteSpace(model.CustomerName)) return (false, "Customer Name is required.");
                if (string.IsNullOrWhiteSpace(model.CustomerType)) return (false, "Customer Type is required.");
                if (string.IsNullOrWhiteSpace(model.AccountCode)) return (false, "Debtors Account is required.");

                var status = (model.ObjectStatus ?? "ACTVE").Trim().ToUpper();
                if (status != "ACTVE" && status != "INACTV")
                    return (false, "Object Status must be 'ACTVE' or 'INACTV'.");

                var code = model.CustomerCode.Trim().ToUpper();

                var existing = await _context.CfnCustomers
                    .FirstOrDefaultAsync(x => x.Customercode.ToUpper() == code);

                string message;

                if (existing != null)
                {
                    existing.Customername = model.CustomerName?.Trim();
                    existing.Customertype = model.CustomerType?.Trim();
                    existing.Businessnature = model.BusinessNature?.Trim();
                    existing.Geographycode = model.GeographyCode?.Trim();
                    existing.Lstnodate = model.LstNoDate?.Trim();
                    existing.Cstnodate = model.CstNoDate?.Trim();
                    existing.Applcustomercode = model.ApplCustomerCode?.Trim();
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

                    message = "Customer updated successfully.";
                }
                else
                {
                    var entity = new CfnCustomer
                    {
                        Customercode = code,
                        Customername = model.CustomerName?.Trim(),
                        Customertype = model.CustomerType?.Trim(),
                        Businessnature = model.BusinessNature?.Trim(),
                        Geographycode = model.GeographyCode?.Trim(),
                        Lstnodate = model.LstNoDate?.Trim(),
                        Cstnodate = model.CstNoDate?.Trim(),
                        Applcustomercode = model.ApplCustomerCode?.Trim(),
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
                    };
                    _context.CfnCustomers.Add(entity);
                    message = "Customer created successfully.";
                }

                await _context.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(model.AccountCode))
                {
                    await UpsertAccountLinkAsync(code, model.AccountCode.Trim(), "ACTVE");
                }

                return (true, message);
            }
            catch (Exception ex)
            {
                return (false, "Error while saving customer: " + ex.Message);
            }
        }

        public async Task<IEnumerable<ImportableCustomerDto>> GetImportableCustomersAsync()
        {
            try
            {

                var sql = @"
                    SELECT
                        c.custcode,
                        c.coname,
                        c.add1,
                        c.add2,
                        c.City,
                        c.pincode,
                        c.State,
                        c.type,
                        c.country,
                        c.phoneno0,
                        c.faxno0,
                        c.mail0,
                        CONVERT(varchar(30), c.cstdate, 100) AS cstdate
                    FROM customer c
                    WHERE c.custcode NOT IN (
                        SELECT customercode FROM cfn_customer
                    )
                    AND c.permanent_customer = 'Y'
                    ORDER BY c.custcode";

                var result = await _context.Database
                    .SqlQueryRaw<ImportableCustomerDto>(sql)
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving importable customers: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> ImportCustomerAsync(ImportCustomerModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.CustomerCode)) return (false, "Customer Code is required.");
                if (string.IsNullOrWhiteSpace(model.AccountCode)) return (false, "Account Code is required.");

                var code = model.CustomerCode.Trim().ToUpper();
                var name = model.CustomerName?.Trim().ToUpper();
                var exists = await _context.CfnCustomers.AnyAsync(x => x.Customercode.ToUpper() == code);
                if (exists) return (false, $"Customer {code} - {name} has already been imported.");

                var entity = BuildImportEntity(code, model);
                _context.CfnCustomers.Add(entity);
                await _context.SaveChangesAsync();

                await UpsertAccountLinkAsync(code, model.AccountCode.Trim(), "ACTVE");

                return (true, $"Customer {code} - {name} imported successfully.");
            }
            catch (Exception ex)
            {
                return (false, "Error importing customer: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> ImportCustomersAsync(
            IEnumerable<ImportCustomerModel> models)
        {
            var list = models?.ToList() ?? new List<ImportCustomerModel>();
            if (list.Count == 0) return (false, "No customers provided.");

            int imported = 0;
            var errors = new List<string>();

            foreach (var model in list)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(model.CustomerCode)) { errors.Add("Empty code skipped."); continue; }
                    if (string.IsNullOrWhiteSpace(model.AccountCode)) { errors.Add($"{model.CustomerCode}: no account."); continue; }

                    var code = model.CustomerCode.Trim().ToUpper();

                    var exists = await _context.CfnCustomers
                        .AnyAsync(x => x.Customercode.ToUpper() == code);
                    if (exists) { errors.Add($"{code}: already imported."); continue; }

                    var entity = BuildImportEntity(code, model);
                    _context.CfnCustomers.Add(entity);
                    await _context.SaveChangesAsync();

                    await UpsertAccountLinkAsync(code, model.AccountCode.Trim(), "ACTVE");
                    imported++;
                }
                catch (Exception ex)
                {
                    errors.Add($"{model.CustomerCode}: {ex.Message}");
                }
            }

            if (imported == 0)
                return (false, "No customers imported. " + string.Join(" | ", errors));

            var msg = $"{imported} customer(s) imported successfully.";
            if (errors.Count > 0) msg += " Skipped: " + string.Join(" | ", errors);
            return (true, msg);
        }

        public async Task<IEnumerable<AccountDto>> GetDebtorAccountsAsync()
        {
            try
            {
                return await _context.CfnAccounts
                    .AsNoTracking()
                    .Where(x => x.Accounttype == "DEBT" && x.Accountstatus != "OBSLT")
                    .OrderBy(x => x.Accountcode)
                    .Select(x => new AccountDto
                    {
                        AccountCode = x.Accountcode,
                        Description = x.Description ?? ""
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving accounts: " + ex.Message);
            }
        }

        public async Task<IEnumerable<AccCustomerDto>> GetLinkedAccountsAsync(string customerCode)
        {
            try
            {
                var code = (customerCode ?? "").Trim().ToUpper();
                return await _context.CfnAcccustomers
                    .AsNoTracking()
                    .Where(x => x.Customercode.ToUpper() == code)
                    .Select(x => new AccCustomerDto
                    {
                        AccountCode = x.Accountcode,
                        CustomerCode = x.Customercode,
                        CustomerStatus = x.Customerstatus
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving linked accounts: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> SaveAccountLinkAsync(AccountLinkModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.CustomerCode)) return (false, "Customer Code is required.");
                if (string.IsNullOrWhiteSpace(model.AccountCode)) return (false, "Account Code is required.");

                await UpsertAccountLinkAsync(
                    model.CustomerCode.Trim().ToUpper(),
                    model.AccountCode.Trim(),
                    model.CustomerStatus ?? "ACTVE");

                return (true, "Account link saved.");
            }
            catch (Exception ex)
            {
                return (false, "Error saving account link: " + ex.Message);
            }
        }

        private CfnCustomer BuildImportEntity(string code, ImportCustomerModel model) => new()
        {
            Customercode = code,
            Customername = model.CustomerName?.Trim(),
            Customertype = (model.CustomerType ?? "DOMES").Trim(),
            Objectstatus = "ACTVE",
            AddrLine1 = model.AddrLine1?.Trim(),
            AddrLine2 = model.AddrLine2?.Trim(),
            AddrCity = model.AddrCity?.Trim(),
            AddrPin = model.AddrPin?.Trim(),
            AddrState = model.AddrState?.Trim(),
            AddrCountry = model.AddrCountry?.Trim(),
            CommTelephone1 = model.CommTelephone1?.Trim(),
            CommFaxno = model.CommFaxno?.Trim(),
            CommEmail = model.CommEmail?.Trim(),
            Cstnodate = model.CstNoDate?.Trim(),
            CtrlStatus = "Post",
            CtrlCancelflag = "N",
            CtrlCreatedon = DateTime.Now,
            CtrlLastupdate = DateTime.Now,
            CtrlUsername = model.Username,
            CtrlLocationcode = model.Location ?? "BILZ",
            CtrlLogextract = "N",
            CtrlNextrefrflag = "N"
        };

        private async Task UpsertAccountLinkAsync(string customerCode, string accountCode, string status)
        {
            var link = await _context.CfnAcccustomers.FirstOrDefaultAsync(x => x.Customercode == customerCode && x.Accountcode == accountCode);
            if (link != null)
            {
                link.Customerstatus = status;
            }
            else
            {
                _context.CfnAcccustomers.Add(new CfnAcccustomer
                {
                    Accountcode = accountCode,
                    Customercode = customerCode,
                    Customerstatus = status
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}