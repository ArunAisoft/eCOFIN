using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesRegisterController : ControllerBase
    {
        private readonly ISalesReportService _svc;

        public SalesRegisterController(ISalesReportService svc)
        {
            _svc = svc;
        }

        [HttpGet("GetSalesAccounts")]
        public async Task<IActionResult> GetSalesAccounts()
        {
            try
            {
                var data = await _svc.GetSalesAccountsAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No sales accounts found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Sales accounts retrieved successfully.", data });
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

        [HttpPost("GetSalesReport")]
        public async Task<IActionResult> GetSalesReport([FromBody] SalesReportFilterModel filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter.AccountCode))
                    return Ok(new { success = false, status = 201, message = "Please select an Account Code.", data = (object?)null });

                if (string.IsNullOrWhiteSpace(filter.FromDate) || string.IsNullOrWhiteSpace(filter.ToDate))
                    return Ok(new { success = false, status = 201, message = "From Date and To Date are required.", data = (object?)null });

                var data = await _svc.GetSalesReportAsync(filter);
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No records found for the selected criteria.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Sales Register retrieved successfully.", data });
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
