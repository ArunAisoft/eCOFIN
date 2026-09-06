using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseRegisterController : ControllerBase
    {
        private readonly IPurchaseReportService _svc;

        public PurchaseRegisterController(IPurchaseReportService svc)
        {
            _svc = svc;
        }

        [HttpGet("GetPurchaseAccounts")]
        public async Task<IActionResult> GetPurchaseAccounts()
        {
            try
            {
                var data = await _svc.GetPurchaseAccountsAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No purchase accounts found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Purchase accounts retrieved successfully.", data });
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

        [HttpPost("GetPurchaseReport")]
        public async Task<IActionResult> GetPurchaseReport([FromBody] PurchaseReportFilterModel filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter.AccountCode))
                    return Ok(new { success = false, status = 201, message = "Please select an Account Code.", data = (object?)null });

                if (string.IsNullOrWhiteSpace(filter.FromDate) || string.IsNullOrWhiteSpace(filter.ToDate))
                    return Ok(new { success = false, status = 201, message = "From Date and To Date are required.", data = (object?)null });

                var data = await _svc.GetPurchaseReportAsync(filter);
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No records found for the selected criteria.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Purchase Register retrieved successfully.", data });
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
