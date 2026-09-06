using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContraReportController : ControllerBase
    {
        private readonly IContraReportService _svc;

        public ContraReportController(IContraReportService svc)
        {
            _svc = svc;
        }

        // GET api/ContraReport/GetContraReport?accPeriod=FEB%20-%202026
        [HttpGet("GetContraReport")]
        public async Task<IActionResult> GetContraReport([FromQuery] string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "accPeriod is required." });

                var data = await _svc.GetContraReportAsync(new ContraReportFilterModel
                {
                    AccPeriod = accPeriod.Trim()
                });

                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No contra entries found for the specified period.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Contra report retrieved successfully.", data });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(500, new { success = false, status = 500, message = appEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error occurred.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
