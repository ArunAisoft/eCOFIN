using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralLedgerReportController : ControllerBase
    {
        private readonly IGeneralLedgerReportService _svc;
        public GeneralLedgerReportController(IGeneralLedgerReportService svc) => _svc = svc;

        // ── Acc Period dropdown ───────────────────────────────────────────────
        [HttpGet("GetAccPeriods")]
        public async Task<IActionResult> GetAccPeriods()
        {
            try
            {
                var data = await _svc.GetAccPeriodsAsync();
                if (!data.Any()) return Ok(new { success = false, status = 201, message = "No accounting periods found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Periods retrieved.", data });
            }
            catch (ApplicationException ex) { return StatusCode(500, new { success = false, status = 500, message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        // ── General Ledger ────────────────────────────────────────────────────
        [HttpGet("GetGeneralLedger")]
        public async Task<IActionResult> GetGeneralLedger(
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate)
        {
            try
            {
                var data = await _svc.GetGeneralLedgerAsync(new GeneralLedgerFilter
                {
                    FromDate = fromDate,
                    ToDate = toDate
                });

                if (!data.Any()) return Ok(new { success = false, status = 201, message = "No records found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "General Ledger retrieved.", data });
            }
            catch (ApplicationException ex) { return StatusCode(500, new { success = false, status = 500, message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }
    }
}
