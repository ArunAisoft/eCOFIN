using AutoMapper;
using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class CurrencyService : ICurrencyService
    {
        private readonly BilzFinDbContext _context;
        public CurrencyService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CurrencyDto>> GetAllCurrenciesAsync()
        {
            try
            {
                var data = await _context.CfnCurrencies
                    .AsNoTracking()
                    .OrderBy(x => x.Currencycode)
                    .ToListAsync();

                return data.Select(x => new CurrencyDto
                {
                    CurrencyCode = x.Currencycode,
                    CurrencyName = x.Currencyname,
                    Country = x.Country ?? "",
                    Symbol = x.Symbol ?? "",
                    ObjectStatus = x.Objectstatus
                });
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving currencies: " + ex.Message);
            }
        }

        public async Task<IEnumerable<CurrencyDto>> GetAllActiveCurrenciesAsync()
        {
            try
            {
                var currencies = await _context.CfnCurrencies.AsNoTracking()
                    .Where(a => a.Objectstatus == "ACTVE")
                    .OrderBy(c => c.Currencyname)
                    .Select(c => new CurrencyDto
                    {
                        CurrencyCode = c.Currencycode,
                        CurrencyName = c.Currencyname,
                        ObjectStatus = c.Objectstatus
                    })
                    .ToListAsync();

                return currencies;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving Currencies :" + ex);
            }
        }

        public async Task<(bool Success, string Message)> SaveOrUpdateCurrencyAsync(CurrencyDto model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.CurrencyCode))
                    return (false, "Currency Code is required.");

                if (string.IsNullOrWhiteSpace(model.CurrencyName))
                    return (false, "Currency Name is required.");

                var code = model.CurrencyCode.Trim().ToUpper();

                var existing = await _context.CfnCurrencies
                    .FirstOrDefaultAsync(x => x.Currencycode == code);

                if (existing != null)
                {
                    existing.Currencyname = model.CurrencyName?.Trim();
                    existing.Country = model.Country?.Trim();
                    existing.Symbol = model.Symbol?.Trim();
                    existing.CtrlLastupdate = DateTime.Now;
                    existing.CtrlUsername = model.Username;
                    existing.CtrlLocationcode = model.Location;
                    existing.CtrlNextrefrflag = "N";

                    await _context.SaveChangesAsync();
                    return (true, "Currency updated successfully.");
                }

                var entity = new CfnCurrency
                {
                    Currencycode = code,
                    Currencyname = model.CurrencyName?.Trim(),
                    Country = model.Country?.Trim(),
                    Symbol = model.Symbol?.Trim(),
                    Enabled = "N",
                    CtrlStatus = "Post",
                    CtrlCreatedon = DateTime.Now,
                    CtrlLastupdate = DateTime.Now,
                    CtrlUsername = model.Username,
                    CtrlLocationcode = model.Location,
                    CtrlNextrefrflag = "N",
                    Objectstatus = "ACTVE"
                };

                _context.CfnCurrencies.Add(entity);
                await _context.SaveChangesAsync();
                return (true, "Currency created successfully.");
            }
            catch (Exception ex)
            {
                return (false, "Error while saving currency: " + ex.Message);
            }
        }
    }
}
