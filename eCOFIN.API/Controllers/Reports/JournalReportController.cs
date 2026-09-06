using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class JournalReportController : ControllerBase
    {
        private readonly IJournalReportService _svc;

        public JournalReportController(IJournalReportService svc)
        {
            _svc = svc;
        }

        // GET api/JournalReport/GetJournalReport?accPeriod=FEB%20-%202026
        [HttpGet("GetJournalReport")]
        public async Task<IActionResult> GetJournalReport([FromQuery] string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "accPeriod is required." });

                var data = await _svc.GetJournalReportAsync(new JournalReportFilterModel
                {
                    AccPeriod = accPeriod.Trim()
                });

                if (data == null || !data.Any())
                    return Ok(new
                    {
                        success = false,
                        status  = 201,
                        message = "No journal entries found for the specified period.",
                        data    = (object?)null
                    });

                return Ok(new
                {
                    success = true,
                    status  = 200,
                    message = "Journal report retrieved successfully.",
                    data
                });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(500, new { success = false, status = 500, message = appEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    status  = 500,
                    message = "Unexpected error occurred.",
                    error   = ex.Message
                });
            }
        }
    }
}
