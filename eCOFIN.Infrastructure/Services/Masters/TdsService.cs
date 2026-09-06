using AutoMapper;
using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class TdsService : ITdsService
    {
        private readonly BilzFinDbContext _context;
        public TdsService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TdsReportDto>> GetAllTDSAsync()
        {
            try
            {
                return await _context.CfnTds
                    .AsNoTracking()
                    .OrderBy(x => x.Tdscode)
                    .Select(x => new TdsReportDto
                    {
                        TdsAccount = x.Accountcode,
                        Tdscode = x.Tdscode,
                        Tdsdescription = x.Tdsdescription,
                        Tdsperc = x.Tdsperc,
                        ObjectStatus = x.Objectstatus
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving active TDS records: " + ex.Message);
            }
        }

        public async Task<IEnumerable<TdsReportDto>> GetAllActiveTDSAsync()
        {
            try
            {
                var currencies = await _context.CfnTds.AsNoTracking()
                    .Where(a => a.Objectstatus == "ACTVE" && !string.IsNullOrWhiteSpace(a.Tdscode))
                    .OrderBy(c => c.Tdsdescription)
                    .Select(c => new TdsReportDto
                    {
                        Tdscode = c.Tdscode,
                        Tdsdescription = c.Tdsdescription,
                        Tdsperc = c.Tdsperc,
                        ObjectStatus = c.Objectstatus,
                        TdsAccount = c.Accountcode
                    })
                    .ToListAsync();

                return currencies;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving TDS :" + ex);
            }
        }

        public async Task<(bool Success, string Message)> SaveOrUpdateTdsAsync(TdsCreateModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Tdscode))
                    return (false, "TDS Code is required.");

                if (string.IsNullOrWhiteSpace(model.Tdsdescription))
                    return (false, "TDS Description is required.");

                if (model.Tdsperc.HasValue && (model.Tdsperc < 0 || model.Tdsperc > 100))
                    return (false, "TDS Percentage must be between 0 and 100.");

                var status = (model.ObjectStatus ?? "ACTVE").Trim().ToUpper();
                if (status != "ACTVE" && status != "INACTV")
                    return (false, "Object Status must be 'ACTVE' (Active) or 'INACTV' (Inactive).");

                var code = model.Tdscode.Trim().ToUpper();

                var existing = await _context.CfnTds
                    .FirstOrDefaultAsync(x => x.Tdscode == code);

                if (existing != null)
                {
                    existing.Tdsdescription = model.Tdsdescription?.Trim();
                    existing.Tdsperc = model.Tdsperc;
                    existing.Objectstatus = status;
                    existing.CtrlLastupdate = DateTime.Now;
                    existing.CtrlUsername = model.Username;
                    existing.CtrlLocationcode = model.Location;

                    await _context.SaveChangesAsync();
                    return (true, "TDS updated successfully.");
                }

                var entity = new CfnTd
                {
                    Tdscode = code,
                    Tdsdescription = model.Tdsdescription?.Trim(),
                    Tdsperc = model.Tdsperc,
                    Objectstatus = status,
                    CtrlStatus = "Post",
                    CtrlCreatedon = DateTime.Now,
                    CtrlLastupdate = DateTime.Now,
                    CtrlUsername = model.Username,
                    CtrlLocationcode = model.Location
                };

                _context.CfnTds.Add(entity);
                await _context.SaveChangesAsync();
                return (true, "TDS created successfully.");
            }
            catch (Exception ex)
            {
                return (false, "Error while saving TDS: " + ex.Message);
            }
        }

    }
}
