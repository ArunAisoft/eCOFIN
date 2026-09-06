using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class BankService : IBankService
    {
        private readonly BilzFinDbContext _context;

        public BankService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<List<BankDto>> GetAllBanksAsync()
        {
            try
            {
                return await _context.CfnBanks
                    .AsNoTracking()
                    .OrderBy(x => x.Bankcode)
                    .Select(x => new BankDto
                    {
                        BankCode     = x.Bankcode,
                        Name         = x.Name ?? "",
                        ObjectStatus = x.Objectstatus ?? "ACTVE",
                        AddrLine1    = x.AddrLine1 ?? "",
                        AddrLine2    = x.AddrLine2 ?? "",
                        AddrLine3    = x.AddrLine3 ?? "",
                        AddrLine4    = x.AddrLine4 ?? "",
                        AddrCity     = x.AddrCity ?? "",
                        AddrPin      = x.AddrPin ?? "",
                        AddrState    = x.AddrState ?? "",
                        AddrCountry  = x.AddrCountry ?? ""
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving banks: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> SaveOrUpdateBankAsync(BankCreateModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.BankCode))
                    return (false, "Bank Code is required.");

                if (string.IsNullOrWhiteSpace(model.Name))
                    return (false, "Bank Name is required.");

                var status = (model.ObjectStatus ?? "ACTVE").Trim().ToUpper();
                if (status != "ACTVE" && status != "INACT")
                    return (false, "Object Status must be 'ACTVE' or 'INACT'.");

                var code = model.BankCode.Trim().ToUpper();

                var existing = await _context.CfnBanks
                    .FirstOrDefaultAsync(x => x.Bankcode.ToUpper() == code);

                if (existing != null)
                {
                    existing.Name              = Trunc(model.Name?.Trim(), 100);
                    existing.Objectstatus      = status;
                    existing.AddrLine1         = Trunc(model.AddrLine1?.Trim(), 100);
                    existing.AddrLine2         = Trunc(model.AddrLine2?.Trim(), 100);
                    existing.AddrLine3         = Trunc(model.AddrLine3?.Trim(), 100);
                    existing.AddrLine4         = Trunc(model.AddrLine4?.Trim(), 100);
                    existing.AddrCity          = Trunc(model.AddrCity?.Trim(), 50);
                    existing.AddrPin           = Trunc(model.AddrPin?.Trim(), 10);
                    existing.AddrState         = Trunc(model.AddrState?.Trim(), 50);
                    existing.AddrCountry       = Trunc(model.AddrCountry?.Trim(), 50);
                    existing.CtrlLastupdate    = DateTime.Now;
                    existing.CtrlUsername      = Trunc(model.Username, 30);
                    existing.CtrlLocationcode  = Trunc(model.Location, 5);
                    existing.CtrlAccperiod     = model.AccPeriod;
                    existing.CtrlNextrefrflag  = "N";

                    await _context.SaveChangesAsync();
                    return (true, "Bank updated successfully.");
                }

                _context.CfnBanks.Add(new CfnBank
                {
                    Bankcode             = Trunc(code, 20)!,
                    Name                 = Trunc(model.Name?.Trim(), 100)!,
                    Objectstatus         = status,
                    AddrLine1            = Trunc(model.AddrLine1?.Trim(), 100),
                    AddrLine2            = Trunc(model.AddrLine2?.Trim(), 100),
                    AddrLine3            = Trunc(model.AddrLine3?.Trim(), 100),
                    AddrLine4            = Trunc(model.AddrLine4?.Trim(), 100),
                    AddrCity             = Trunc(model.AddrCity?.Trim(), 50),
                    AddrPin              = Trunc(model.AddrPin?.Trim(), 10),
                    AddrState            = Trunc(model.AddrState?.Trim(), 50),
                    AddrCountry          = Trunc(model.AddrCountry?.Trim(), 50),
                    CtrlStatus           = "Post",
                    CtrlCancelflag       = "N",
                    CtrlCreatedon        = DateTime.Now,
                    CtrlLastupdate       = DateTime.Now,
                    CtrlUsername         = Trunc(model.Username, 30),
                    CtrlLocationcode     = Trunc(model.Location ?? "BILZ", 5),
                    CtrlTrglocationcode  = Trunc(model.Location ?? "BILZ", 5),
                    CtrlLogextract       = "N",
                    CtrlLogextracttype   = "N",
                    CtrlNextrefrflag     = "N"
                });

                await _context.SaveChangesAsync();
                return (true, "Bank created successfully.");
            }
            catch (Exception ex)
            {
                return (false, "Error while saving bank: " + ex.Message);
            }
        }

        private static string? Trunc(string? value, int maxLen) =>
            value == null ? null : (value.Length > maxLen ? value[..maxLen] : value);
    }
}
