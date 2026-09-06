using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class VendorService : IVendorService
    {
        private readonly BilzFinDbContext _context;

        public VendorService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VendorDto>> GetAllVendorAsync()
        {
            try
            {
                var data = await _context.CfnVendors.AsNoTracking().OrderBy(x => x.Vendorcode).ToListAsync();
                return data.Select(x => new VendorDto
                {
                    VendorCode = x.Vendorcode,
                    VendorName = x.Vendorname ?? "",
                    VendorType = x.Vendortype ?? "",
                    VendorCategory = x.Vendorcategory ?? "",
                    PanNumber = x.Pannumber ?? "",
                    LstNumber = x.Lstnumber ?? "",
                    CstNumber = x.Cstnumber ?? "",
                    TinNumber = x.Tinnumber ?? "",
                    ServiceTax = x.Servicetax ?? "",
                    EccNumber = x.Eccnumber ?? "",
                    VendorStatus = x.Vendorstatus ?? "",
                    ObjectStatus = x.Objectstatus ?? "ACTVE",
                    AddrLine1 = x.AddrLine1 ?? "",
                    AddrLine2 = x.AddrLine2 ?? "",
                    AddrLine3 = x.AddrLine3 ?? "",
                    AddrLine4 = x.AddrLine4 ?? "",
                    AddrCity = x.AddrCity ?? "",
                    AddrPin = x.AddrPin ?? "",
                    AddrState = x.AddrState ?? "",
                    AddrCountry = x.AddrCountry ?? ""
                });
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving vendors: " + ex.Message); }
        }

        public async Task<IEnumerable<VendorDto>> GetAllActiveVendorsAsync()
        {
            try
            {
                return await _context.CfnVendors.AsNoTracking()
                    .Where(x => x.Objectstatus == "ACTVE").OrderBy(x => x.Vendorcode)
                    .Select(x => new VendorDto
                    {
                        VendorCode = x.Vendorcode,
                        VendorName = x.Vendorname,
                        VendorType = x.Vendortype,
                        AddrCity = x.AddrCity,
                        ObjectStatus = x.Objectstatus
                    }).ToListAsync();
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving active vendors: " + ex.Message); }
        }

        public async Task<(bool Success, string Message)> SaveOrUpdateVendorAsync(VendorCreateModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.VendorCode)) return (false, "Vendor Code is required.");
                if (string.IsNullOrWhiteSpace(model.VendorName)) return (false, "Vendor Name is required.");

                var status = (model.ObjectStatus ?? "ACTVE").Trim().ToUpper();
                if (status != "ACTVE" && status != "INACTV")
                    return (false, "Object Status must be 'ACTVE' or 'INACTV'.");

                var code = model.VendorCode.Trim().ToUpper();
                var existing = await _context.CfnVendors.FirstOrDefaultAsync(x => x.Vendorcode.ToUpper() == code);

                if (existing != null)
                {
                    existing.Vendorname = model.VendorName?.Trim();
                    existing.Vendortype = model.VendorType?.Trim();
                    existing.Vendorcategory = model.VendorCategory?.Trim();
                    existing.Pannumber = model.PanNumber?.Trim();
                    existing.Lstnumber = model.LstNumber?.Trim();
                    existing.Cstnumber = model.CstNumber?.Trim();
                    existing.Tinnumber = model.TinNumber?.Trim();
                    existing.Servicetax = model.ServiceTax?.Trim();
                    existing.Eccnumber = model.EccNumber?.Trim();
                    existing.Vendorstatus = model.VendorStatus?.Trim();
                    existing.Objectstatus = status;
                    existing.AddrLine1 = model.AddrLine1?.Trim();
                    existing.AddrLine2 = model.AddrLine2?.Trim();
                    existing.AddrLine3 = model.AddrLine3?.Trim();
                    existing.AddrLine4 = model.AddrLine4?.Trim();
                    existing.AddrCity = model.AddrCity?.Trim();
                    existing.AddrPin = model.AddrPin?.Trim();
                    existing.AddrState = model.AddrState?.Trim();
                    existing.AddrCountry = model.AddrCountry?.Trim();
                    existing.CtrlLastupdate = DateTime.Now;
                    existing.CtrlUsername = model.Username;
                    existing.CtrlLocationcode = model.Location;
                    existing.CtrlAccperiod = model.AccPeriod;
                    existing.CtrlNextrefrflag = "N";
                    await _context.SaveChangesAsync();
                    return (true, "Vendor updated successfully.");
                }

                _context.CfnVendors.Add(new CfnVendor
                {
                    Vendorcode = code,
                    Vendorname = model.VendorName?.Trim(),
                    Vendortype = model.VendorType?.Trim(),
                    Vendorcategory = model.VendorCategory?.Trim(),
                    Pannumber = model.PanNumber?.Trim(),
                    Lstnumber = model.LstNumber?.Trim(),
                    Cstnumber = model.CstNumber?.Trim(),
                    Tinnumber = model.TinNumber?.Trim(),
                    Servicetax = model.ServiceTax?.Trim(),
                    Eccnumber = model.EccNumber?.Trim(),
                    Vendorstatus = model.VendorStatus?.Trim(),
                    Objectstatus = status,
                    AddrLine1 = model.AddrLine1?.Trim(),
                    AddrLine2 = model.AddrLine2?.Trim(),
                    AddrLine3 = model.AddrLine3?.Trim(),
                    AddrLine4 = model.AddrLine4?.Trim(),
                    AddrCity = model.AddrCity?.Trim(),
                    AddrPin = model.AddrPin?.Trim(),
                    AddrState = model.AddrState?.Trim(),
                    AddrCountry = model.AddrCountry?.Trim(),
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
                return (true, "Vendor created successfully.");
            }
            catch (Exception ex) { return (false, "Error while saving vendor: " + ex.Message); }
        }

        public async Task<IEnumerable<AccVendorDto>> GetByVendorAsync(string vendorCode)
        {
            try
            {
                return await _context.CfnAccvendors.AsNoTracking()
                    .Where(x => x.Vendorcode == vendorCode).OrderBy(x => x.Accountcode)
                    .Select(x => new AccVendorDto
                    {
                        AccountCode = x.Accountcode,
                        VendorCode = x.Vendorcode,
                        VendorStatus = x.Vendorstatus ?? "ACTVE"
                    }).ToListAsync();
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving account-vendor links: " + ex.Message); }
        }

        public async Task<IEnumerable<AccVendorAccountDto>> GetVendorAccountsAsync()
        {
            try
            {
                return await _context.CfnAccounts.AsNoTracking()
                    .Where(x => x.Accountstatus != "OBSLT" && x.Accounttype == "CRDT")
                    .OrderBy(x => x.Accountcode)
                    .Select(x => new AccVendorAccountDto
                    {
                        AccountCode = x.Accountcode,
                        Description = x.Description
                    }).ToListAsync();
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving credit accounts: " + ex.Message); }
        }

        public async Task<(bool Success, string Message)> SaveAccVendorAsync(AccVendorCreateModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.VendorCode)) return (false, "Vendor Code is required.");
                if (model.Rows == null || model.Rows.Count == 0) return (false, "At least one account row is required.");
                foreach (var row in model.Rows)
                {
                    if (string.IsNullOrWhiteSpace(row.AccountCode)) return (false, "Account Code is required for all rows.");
                    var s = (row.VendorStatus ?? "ACTVE").Trim().ToUpper();
                    if (s != "ACTVE" && s != "INACT")
                        return (false, $"Invalid status '{row.VendorStatus}' for account '{row.AccountCode}'.");
                }
                var existing = await _context.CfnAccvendors.Where(x => x.Vendorcode == model.VendorCode.Trim()).ToListAsync();
                _context.CfnAccvendors.RemoveRange(existing);
                foreach (var row in model.Rows)
                    _context.CfnAccvendors.Add(new CfnAccvendor
                    {
                        Accountcode = row.AccountCode.Trim(),
                        Vendorcode = model.VendorCode.Trim(),
                        Vendorstatus = (row.VendorStatus ?? "ACTVE").Trim().ToUpper()
                    });
                await _context.SaveChangesAsync();
                return (true, "Account-vendor links saved successfully.");
            }
            catch (Exception ex) { return (false, "Error while saving: " + ex.Message); }
        }

        public async Task<IEnumerable<ImportableSupplierDto>> GetImportableSuppliersAsync()
        {
            try
            {
                var sql = @"
                    SELECT
                        sd.SuppCode AS Code,
                        sd.SuppName AS Name,
                        sd.add1 AS Add1,
                        sd.add2 AS Add2,
                        sd.City AS City,
                        sd.State AS State,
                        sd.Pincode AS PinCode,
                        sd.Country AS Country,
                        sd.phoneno AS PhoneNo,
                        sd.faxno AS FaxNo,
                        sd.EMail AS EmailId,
                        sd.PAN_No AS PanNo,
                        sd.TINNo AS TinNo,
                        sd.EccNo AS EccNo
                    FROM SupplierDetail sd
                    WHERE sd.SuppCode NOT IN (SELECT vendorcode FROM cfn_vendor)
                    ORDER BY sd.SuppCode";

                return await _context.Database
                    .SqlQueryRaw<ImportableSupplierDto>(sql)
                    .ToListAsync();
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving importable suppliers: " + ex.Message); }
        }

        public async Task<IEnumerable<ImportableVendorDto>> GetImportableVendorsAsync()
        {
            try
            {
                var sql = @"
                    SELECT
                        v.code AS Code,
                        v.Name AS Name,
                        v.Add1 AS Add1,
                        v.Add2 AS Add2,
                        v.City AS City,
                        v.State AS State,
                        v.PinCode AS PinCode,
                        v.Country AS Country,
                        v.Phone_No AS PhoneNo,
                        v.Fax_No  AS FaxNo,
                        v.Email_ID AS EmailId,
                        v.PAN_No  AS PanNo,
                        v.TIN_No  AS TinNo,
                        v.ECC_No  AS EccNo
                    FROM vendcode v
                    WHERE v.code NOT IN (SELECT vendorcode FROM cfn_vendor)
                    ORDER BY v.code";

                return await _context.Database
                    .SqlQueryRaw<ImportableVendorDto>(sql)
                    .ToListAsync();
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving importable vendors: " + ex.Message); }
        }

        public async Task<(bool Success, string Message)> ImportVendorAsync(ImportVendorModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.VendorCode)) return (false, "Vendor Code is required.");
                if (string.IsNullOrWhiteSpace(model.AccountCode)) return (false, "Account Code is required.");

                var code = model.VendorCode.Trim().ToUpper();
                var name = model.VendorName?.Trim().ToUpper();
                if (await _context.CfnVendors.AnyAsync(x => x.Vendorcode.ToUpper() == code))
                    return (false, $"Vendor {code} - {name} has already been imported.");

                _context.CfnVendors.Add(BuildImportEntity(code, model));
                await _context.SaveChangesAsync();
                await UpsertAccVendorAsync(code, model.AccountCode.Trim(), "ACTVE");
                return (true, $"Vendor {code} - {name} imported successfully.");
            }
            catch (Exception ex) { return (false, "Error importing vendor: " + ex.Message); }
        }

        public async Task<(bool Success, string Message)> ImportVendorsAsync(IEnumerable<ImportVendorModel> models)
        {
            var list = models?.ToList() ?? new List<ImportVendorModel>();
            if (list.Count == 0) return (false, "No vendors provided.");
            int imported = 0;
            var errors = new List<string>();
            foreach (var model in list)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(model.VendorCode)) { errors.Add("Empty code skipped."); continue; }
                    if (string.IsNullOrWhiteSpace(model.AccountCode)) { errors.Add($"{model.VendorCode}: no account."); continue; }
                    var code = model.VendorCode.Trim().ToUpper();
                    if (await _context.CfnVendors.AnyAsync(x => x.Vendorcode.ToUpper() == code)) { errors.Add($"{code}: already imported."); continue; }
                    _context.CfnVendors.Add(BuildImportEntity(code, model));
                    await _context.SaveChangesAsync();
                    await UpsertAccVendorAsync(code, model.AccountCode.Trim(), "ACTVE");
                    imported++;
                }
                catch (Exception ex) { errors.Add($"{model.VendorCode}: {ex.Message}"); }
            }
            if (imported == 0) return (false, "No vendors imported. " + string.Join(" | ", errors));
            var msg = $"{imported} vendor(s) imported successfully.";
            if (errors.Count > 0) msg += " Skipped: " + string.Join(" | ", errors);
            return (true, msg);
        }

        private static string? Trunc(string? value, int maxLen)
            => value == null ? null : (value.Length > maxLen ? value[..maxLen] : value);

        private CfnVendor BuildImportEntity(string code, ImportVendorModel model) => new()
        {
            Vendorcode = Trunc(code, 10)!,
            Vendorname = Trunc(model.VendorName?.Trim(), 100),
            Vendortype = Trunc((model.VendorType ?? "INLND").Trim(), 5),
            Objectstatus = "ACTVE",
            Pannumber = Trunc(model.PanNumber?.Trim(), 50),
            Tinnumber = Trunc(model.TinNumber?.Trim(), 100),
            Eccnumber = Trunc(model.EccNumber?.Trim(), 100),
            AddrLine1 = Trunc(model.AddrLine1?.Trim(), 100),
            AddrLine2 = Trunc(model.AddrLine2?.Trim(), 100),
            AddrCity = Trunc(model.AddrCity?.Trim(), 50),
            AddrPin = Trunc(model.AddrPin?.Trim(), 10),
            AddrState = Trunc(model.AddrState?.Trim(), 50),
            AddrCountry = Trunc(model.AddrCountry?.Trim(), 50),
            CtrlStatus = "Post",
            CtrlCancelflag = "N",
            CtrlCreatedon = DateTime.Now,
            CtrlLastupdate = DateTime.Now,
            CtrlUsername = Trunc(model.Username, 30),
            CtrlLocationcode = Trunc(model.Location ?? "BILZ", 5),
            CtrlTrglocationcode = Trunc(model.Location ?? "BILZ", 5),
            CtrlLogextract = "N",
            CtrlLogextracttype = "N",
            CtrlNextrefrflag = "N",
            Vendorstatus= "VREG"
        };

        private async Task UpsertAccVendorAsync(string vendorCode, string accountCode, string status)
        {
            var link = await _context.CfnAccvendors
                .FirstOrDefaultAsync(x => x.Vendorcode == vendorCode && x.Accountcode == accountCode);
            if (link != null)
                link.Vendorstatus = status;
            else
                _context.CfnAccvendors.Add(new CfnAccvendor
                {
                    Accountcode = accountCode,
                    Vendorcode = vendorCode,
                    Vendorstatus = status
                });
            await _context.SaveChangesAsync();
        }
    }
}