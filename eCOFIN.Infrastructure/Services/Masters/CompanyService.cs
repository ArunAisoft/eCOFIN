using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class CompanyService : ICompanyService
    {
        private readonly BilzFinDbContext _context;

        public CompanyService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<CompanyDto?> GetCompanyAsync()
        {
            try
            {
                var x = await _context.CfnCompanies
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (x == null) return null;

                return new CompanyDto
                {
                    CompanyCode = x.Companycode,
                    CompanyName = x.Companyname ?? "",
                    ObjectStatus = x.Objectstatus ?? "ACTVE",
                    AddrLine1 = x.AddrLine1 ?? "",
                    AddrLine2 = x.AddrLine2 ?? "",
                    AddrLine3 = x.AddrLine3 ?? "",
                    AddrLine4 = x.AddrLine4 ?? "",
                    AddrCity = x.AddrCity ?? "",
                    AddrPin = x.AddrPin ?? "",
                    AddrState = x.AddrState ?? "",
                    AddrCountry = x.AddrCountry ?? ""
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving company: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> SaveOrUpdateCompanyAsync(CompanyCreateModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.CompanyCode))
                    return (false, "Company Code is required.");

                if (string.IsNullOrWhiteSpace(model.CompanyName))
                    return (false, "Company Name is required.");

                var status = (model.ObjectStatus ?? "ACTVE").Trim().ToUpper();
                if (status != "ACTVE" && status != "INACTV")
                    return (false, "Object Status must be 'ACTVE' or 'INACTV'.");

                var code = model.CompanyCode.Trim().ToUpper();

                var existing = await _context.CfnCompanies
                    .FirstOrDefaultAsync(x => x.Companycode.ToUpper() == code);

                if (existing != null)
                {
                    existing.Companyname = Trunc(model.CompanyName?.Trim(), 50);
                    existing.Objectstatus = status;
                    existing.AddrLine1 = Trunc(model.AddrLine1?.Trim(), 100);
                    existing.AddrLine2 = Trunc(model.AddrLine2?.Trim(), 100);
                    existing.AddrLine3 = Trunc(model.AddrLine3?.Trim(), 100);
                    existing.AddrLine4 = Trunc(model.AddrLine4?.Trim(), 100);
                    existing.AddrCity = Trunc(model.AddrCity?.Trim(), 50);
                    existing.AddrPin = Trunc(model.AddrPin?.Trim(), 10);
                    existing.AddrState = Trunc(model.AddrState?.Trim(), 50);
                    existing.AddrCountry = Trunc(model.AddrCountry?.Trim(), 50);
                    existing.CtrlLastupdate = DateTime.Now;
                    existing.CtrlUsername = Trunc(model.Username, 30);
                    existing.CtrlLocationcode = Trunc(model.Location, 5);
                    existing.CtrlAccperiod = model.AccPeriod;
                    existing.CtrlNextrefrflag = "N";

                    await _context.SaveChangesAsync();
                    return (true, "Company updated successfully.");
                }

                _context.CfnCompanies.Add(new CfnCompany
                {
                    Companycode = Trunc(code, 5)!,
                    Companyname = Trunc(model.CompanyName?.Trim(), 50)!,
                    Objectstatus = status,
                    AddrLine1 = Trunc(model.AddrLine1?.Trim(), 100),
                    AddrLine2 = Trunc(model.AddrLine2?.Trim(), 100),
                    AddrLine3 = Trunc(model.AddrLine3?.Trim(), 100),
                    AddrLine4 = Trunc(model.AddrLine4?.Trim(), 100),
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
                    CtrlNextrefrflag = "N"
                });

                await _context.SaveChangesAsync();
                return (true, "Company created successfully.");
            }
            catch (Exception ex)
            {
                return (false, "Error while saving company: " + ex.Message);
            }
        }

        private static string? Trunc(string? value, int maxLen) => value == null ? null : (value.Length > maxLen ? value[..maxLen] : value);
    }
}
