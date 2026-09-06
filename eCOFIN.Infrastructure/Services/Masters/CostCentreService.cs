using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure.Services.Masters
{
    public class CostCentreService : ICostCentreService
    {
        private readonly BilzFinDbContext _context;

        public CostCentreService(BilzFinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CostCentreDto>> GetAllCostCentreAsync()
        {
            try
            {
                var data = await _context.CfnCostcentres
                    .AsNoTracking()
                    .OrderBy(x => x.Costcentrecode)
                    .ToListAsync();

                return data.Select(x => new CostCentreDto
                {
                    CostCentreCode = x.Costcentrecode,
                    Description = x.Description ?? "",
                    CentreType = x.Centretype ?? "",
                    CostCentreStatus = x.Costcentrestatus ?? "",
                    ObjectStatus = x.Objectstatus ?? "ACTVE"
                });
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving cost centres: " + ex.Message);
            }
        }

        public async Task<IEnumerable<CostCentreDto>> GetAllActiveCostCentresAsync()
        {
            try
            {
                return await _context.CfnCostcentres
                    .AsNoTracking()
                    .Where(x => x.Objectstatus == "ACTVE")
                    .OrderBy(x => x.Costcentrecode)
                    .Select(x => new CostCentreDto
                    {
                        CostCentreCode = x.Costcentrecode,
                        Description = x.Description,
                        CentreType = x.Centretype,
                        CostCentreStatus = x.Costcentrestatus,
                        ObjectStatus = x.Objectstatus
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving active cost centres: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> SaveOrUpdateCostCentreAsync(CostCentreCreateModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.CostCentreCode))
                    return (false, "Cost Centre Code is required.");

                if (string.IsNullOrWhiteSpace(model.Description))
                    return (false, "Description is required.");

                var status = (model.ObjectStatus ?? "ACTVE").Trim().ToUpper();
                if (status != "ACTVE" && status != "INACTV")
                    return (false, "Object Status must be 'ACTVE' (Active) or 'INACTV' (Inactive).");

                var code = model.CostCentreCode.Trim().ToUpper();

                var existing = await _context.CfnCostcentres
                    .FirstOrDefaultAsync(x => x.Costcentrecode == code);

                if (existing != null)
                {

                    existing.Description = model.Description?.Trim();
                    existing.Centretype = model.CentreType?.Trim();
                    existing.Costcentrestatus = model.CostCentreStatus?.Trim();
                    existing.Objectstatus = status;
                    existing.CtrlLastupdate = DateTime.Now;
                    existing.CtrlUsername = model.Username;
                    existing.CtrlLocationcode = model.Location;
                    existing.CtrlNextrefrflag = "N";

                    await _context.SaveChangesAsync();
                    return (true, "Cost Centre updated successfully.");
                }

                var entity = new CfnCostcentre
                {
                    Costcentrecode = code,
                    Description = model.Description?.Trim(),
                    Centretype = model.CentreType?.Trim(),
                    Costcentrestatus = model.CostCentreStatus?.Trim(),
                    Objectstatus = status,
                    CtrlStatus = "Post",
                    CtrlCreatedon = DateTime.Now,
                    CtrlLastupdate = DateTime.Now,
                    CtrlUsername = model.Username,
                    CtrlLocationcode = model.Location,
                    CtrlNextrefrflag = "N"
                };

                _context.CfnCostcentres.Add(entity);
                await _context.SaveChangesAsync();
                return (true, "Cost Centre created successfully.");
            }
            catch (Exception ex)
            {
                return (false, "Error while saving cost centre: " + ex.Message);
            }
        }
    }
}
