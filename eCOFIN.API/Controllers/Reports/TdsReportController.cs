using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class TdsReportController : ControllerBase
    {
        private readonly ITdsReportService _svc;

        public TdsReportController(ITdsReportService svc)
        {
            _svc = svc;
        }

        [HttpGet("GetTdsAccounts")]
        public async Task<IActionResult> GetTdsAccounts()
        {
            try
            {
                var data = await _svc.GetTdsAccountsAsync();

                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No TDS accounts found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "TDS accounts retrieved successfully.", data });
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

        [HttpPost("GetTdsReport")]
        public async Task<IActionResult> GetTdsReport([FromBody] TdsReportFilterModel filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter.AccountCode))
                    return Ok(new { success = false, status = 201, message = "Please select an Account Code.", data = (object?)null });

                if (string.IsNullOrWhiteSpace(filter.FromDate) || string.IsNullOrWhiteSpace(filter.ToDate))
                    return Ok(new { success = false, status = 201, message = "From Date and To Date are required.", data = (object?)null });

                var data = await _svc.GetTdsReportAsync(filter);

                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No records found for the selected criteria.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "TDS report retrieved successfully.", data });
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

        [HttpGet("GetVoucherDetails/{vchrNumber}")]
        public async Task<IActionResult> GetVoucherDetails(string vchrNumber)
        {
            try
            {
                var data = await _svc.GetVoucherDetailsAsync(vchrNumber);

                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No voucher details found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Voucher details retrieved successfully.", data });
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