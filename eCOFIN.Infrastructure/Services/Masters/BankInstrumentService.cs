using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.DTOs.Vouchers;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class BankInstrumentService : IBankInstrumentService
    {
        private readonly BilzFinDbContext _context;

        public BankInstrumentService(BilzFinDbContext context)
        {
            _context = context;
        }

        // ── Banks + accounts (reuses existing logic) ──────────────────────────
        public async Task<IEnumerable<Application.DTOs.Vouchers.BanksAndAccountsDto>> GetBanksWithAccountsAsync()
        {
            try
            {
                var banks = await _context.CfnBanks.AsNoTracking()
                    .OrderBy(b => b.Name)
                    .Select(b => new BanksAndAccountsDto
                    {
                        BankCode     = b.Bankcode,
                        BankName     = b.Name,
                        ObjectStatus = b.Objectstatus,
                        BankAccounts = _context.CfnAccounts.AsNoTracking()
                            .Where(a => a.Bankcode == b.Bankcode && a.Accountstatus == "ACTVE")
                            .OrderBy(a => a.Description)
                            .Select(a => new BankAccountDto
                            {
                                AccountCode   = a.Accountcode,
                                AccountName   = a.Description,
                                AccountStatus = a.Accountstatus,
                                BankCode      = a.Bankcode,
                                AccountType   = a.Accounttype
                            })
                            .ToList()
                    })
                    .ToListAsync();

                return banks;
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving banks: " + ex.Message); }
        }

        // ── All instrument books joined with bank + account names ──────────────
        public async Task<IEnumerable<BankInstrumentDto>> GetAllInstrumentsAsync()
        {
            try
            {
                var list = await (
                    from bi in _context.CfnBankinstruments.AsNoTracking()
                    join bk in _context.CfnBanks.AsNoTracking()
                        on bi.Bankcode equals bk.Bankcode
                    join ac in _context.CfnAccounts.AsNoTracking()
                        on bi.Accountcode equals ac.Accountcode
                    where bi.Bankcode    == bk.Bankcode
                       && bi.Accountcode == ac.Accountcode
                       && ac.Bankcode    == bk.Bankcode
                    orderby bk.Name, ac.Description, bi.Instrumentbookno
                    select new BankInstrumentDto
                    {
                        BankCode           = bi.Bankcode,
                        BankName           = bk.Name,
                        AccountCode        = bi.Accountcode,
                        AccountDescription = ac.Description,
                        InstrumentCategory = bi.Instrumentcategory,
                        InstrumentType     = bi.Instrumenttype,
                        InstrumentBookNo   = bi.Instrumentbookno,
                        BookDescription    = bi.Bookdescription,
                        StartingSerialNo   = bi.Startingserialno,
                        EndingSerialNo     = bi.Endingserialno,
                        RunningSerialNo    = bi.Runningserialno,
                        InstrumentLeaves   = bi.Instrumentleaves,
                        ActiveStatus       = bi.ActiveStatus
                    }
                ).ToListAsync();

                return list;
            }
            catch (Exception ex) { throw new ApplicationException("Error retrieving instrument books: " + ex.Message); }
        }

        // ── Save or Update ────────────────────────────────────────────────────
        public async Task<(bool Success, string Message)> SaveOrUpdateInstrumentAsync(BankInstrumentCreateModel model)
        {
            try
            {
                // Basic validation
                if (string.IsNullOrWhiteSpace(model.BankCode))    return (false, "Bank Code is required.");
                if (string.IsNullOrWhiteSpace(model.AccountCode)) return (false, "Account Code is required.");
                if (model.EndingSerialNo < model.StartingSerialNo)
                    return (false, "Ending serial must be >= starting serial.");

                var status = (model.ActiveStatus ?? "ACTVE").Trim().ToUpper();

                // ── EDIT path: InstrumentBookNo is provided ───────────────────
                if (model.InstrumentBookNo.HasValue && model.InstrumentBookNo > 0)
                {
                    var existing = await _context.CfnBankinstruments
                        .FirstOrDefaultAsync(x => x.Bankcode    == model.BankCode
                                               && x.Accountcode == model.AccountCode
                                               && x.Instrumentbookno == model.InstrumentBookNo.Value);

                    if (existing == null)
                        return (false, $"Instrument book {model.InstrumentBookNo} not found.");

                    // Ending must be >= running
                    if (existing.Runningserialno > 0 &&    model.EndingSerialNo < existing.Runningserialno)
                        return (false, $"Ending serial ({model.EndingSerialNo}) must be >= running serial ({existing.Runningserialno}).");

                    // Overlap check (exclude self)
                    var overlap = await _context.CfnBankinstruments
                        .Where(x => x.Bankcode    == model.BankCode
                                 && x.Accountcode == model.AccountCode
                                 && x.Instrumentbookno != model.InstrumentBookNo.Value
                                 && x.Startingserialno <= model.EndingSerialNo
                                 && x.Endingserialno   >= model.StartingSerialNo)
                        .AnyAsync();

                    if (overlap) return (false, "Serial range overlaps an existing instrument book.");

                    // Update editable fields only
                    existing.Bookdescription  = Trunc(model.BookDescription?.Trim(), 100);
                    existing.Endingserialno   = model.EndingSerialNo;
                    existing.Instrumentleaves = model.EndingSerialNo - model.StartingSerialNo + 1;
                    existing.ActiveStatus     = status;
                    existing.CtrlLastupdate   = DateTime.Now;
                    existing.CtrlUsername     = Trunc(model.Username, 30);
                    existing.CtrlLocationcode = Trunc(model.Location ?? "BILZ", 5);
                    existing.CtrlNextrefrflag = "N";

                    await _context.SaveChangesAsync();
                    return (true, "Instrument book updated successfully.");
                }

                // ── INSERT path ────────────────────────────────────────────────

                // Overlap check for new book
                var overlapNew = await _context.CfnBankinstruments
                    .Where(x => x.Bankcode    == model.BankCode
                             && x.Accountcode == model.AccountCode
                             && x.Startingserialno <= model.EndingSerialNo
                             && x.Endingserialno   >= model.StartingSerialNo)
                    .AnyAsync();

                if (overlapNew) return (false, "Serial range overlaps an existing instrument book.");

                // Auto-assign next InstrumentBookNo (max + 1 for this bank+account)
                var maxBookNo = await _context.CfnBankinstruments
                    .Where(x => x.Bankcode    == model.BankCode
                             && x.Accountcode == model.AccountCode)
                    .MaxAsync(x => (int?)x.Instrumentbookno) ?? 0;

                var newBookNo = maxBookNo + 1;

                _context.CfnBankinstruments.Add(new CfnBankinstrument
                {
                    Bankcode           = model.BankCode.Trim(),
                    Accountcode        = model.AccountCode.Trim(),
                    Instrumentcategory = Trunc(model.InstrumentCategory?.Trim(), 5),
                    Instrumenttype     = Trunc(model.InstrumentType?.Trim(), 5),
                    Instrumentbookno   = newBookNo,
                    Bookdescription    = Trunc(model.BookDescription?.Trim(), 100),
                    Startingserialno   = model.StartingSerialNo,
                    Endingserialno     = model.EndingSerialNo,
                    Runningserialno    = model.StartingSerialNo,   // starts at first serial
                    Instrumentleaves   = model.EndingSerialNo - model.StartingSerialNo + 1,
                    ActiveStatus       = status,
                    CtrlStatus         = "Post",
                    CtrlCancelflag     = "N",
                    CtrlCreatedon      = DateTime.Now,
                    CtrlLastupdate     = DateTime.Now,
                    CtrlUsername       = Trunc(model.Username, 30),
                    CtrlLocationcode   = Trunc(model.Location ?? "BILZ", 5),
                    CtrlTrglocationcode = Trunc(model.Location ?? "BILZ", 5),
                    CtrlLogextract     = "N",
                    CtrlLogextracttype = "N",
                    CtrlNextrefrflag   = "N"
                });

                await _context.SaveChangesAsync();
                return (true, $"Instrument book {newBookNo} created successfully.");
            }
            catch (Exception ex) { return (false, "Error saving instrument book: " + ex.Message); }
        }

        private static string? Trunc(string? value, int maxLen)
            => value == null ? null : (value.Length > maxLen ? value[..maxLen] : value);
    }
}
