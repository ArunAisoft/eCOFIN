using eCOFIN.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class GstReportController : ControllerBase
    {
        private readonly IGstReportService _svc;

        public GstReportController(IGstReportService svc)
        {
            _svc = svc;
        }

        [HttpGet("GetGstReport")]
        public async Task<IActionResult> GetGstReport([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            try
            {
                if (fromDate > toDate)
                    return Ok(new { success = false, status = 201, message = "From Date must be ≤ To Date." });

                var data = await _svc.GetGstReportAsync(fromDate, toDate);
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No GST records found for the selected period.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "GST report retrieved successfully.", data });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(500, new { success = false, status = 500, message = appEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
