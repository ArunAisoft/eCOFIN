using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelRegisterController : ControllerBase
    {
        private readonly ITravelReportService _svc;

        public TravelRegisterController(ITravelReportService svc)
        {
            _svc = svc;
        }

        [HttpGet("GetTravelAccounts")]
        public async Task<IActionResult> GetTravelAccounts()
        {
            try
            {
                var data = await _svc.GetTravelAccountsAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No travel accounts found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Travel accounts retrieved successfully.", data });
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

        [HttpPost("GetTravelReport")]
        public async Task<IActionResult> GetTravelReport([FromBody] TravelReportFilterModel filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter.AccountCode))
                    return Ok(new { success = false, status = 201, message = "Please select an Account Code.", data = (object?)null });

                if (string.IsNullOrWhiteSpace(filter.FromDate) || string.IsNullOrWhiteSpace(filter.ToDate))
                    return Ok(new { success = false, status = 201, message = "From Date and To Date are required.", data = (object?)null });

                var data = await _svc.GetTravelReportAsync(filter);
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No records found for the selected criteria.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Travel Register retrieved successfully.", data });
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
