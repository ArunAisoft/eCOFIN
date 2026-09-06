using eCOFIN.Application.Interfaces.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.API.Controllers.Dashboard
{
    [ApiController]
    [Route("api/[controller]")]

    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger)
        {
            _dashboardService = dashboardService;
            _logger = logger;
        }

        [HttpGet("Summary")]
        public async Task<IActionResult> GetSummary([FromQuery] string userName, [FromQuery] string? bankCode = null, [FromQuery] string? accPeriod = null, [FromQuery] string? finYear = null)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest(new { success = false, status = 400, message = "Username is required." });

            try
            {
                var result = await _dashboardService.GetDashboardSummaryAsync(userName.Trim(), bankCode?.Trim(), accPeriod?.Trim(), finYear?.Trim(), HttpContext.RequestAborted);
                return Ok(new { success = true, status = 200, message = "Dashboard summary retrieved successfully.", data = result });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error building dashboard summary.");
                return StatusCode(500, new { success = false, status = 500, message = "Application error building dashboard summary.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error building dashboard summary.");
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error building dashboard summary.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("DrillDown")]
        public async Task<IActionResult> GetDrillDown([FromQuery] string voucherSysCategory, [FromQuery] string userName, [FromQuery] string? bankCode = null, [FromQuery] string? accPeriod = null)
        {
            if (string.IsNullOrWhiteSpace(voucherSysCategory))
                return BadRequest(new { success = false, status = 400, message = "Voucher sys-category is required." });

            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest(new { success = false, status = 400, message = "Username is required." });

            try
            {
                var result = await _dashboardService.GetVoucherTypeDrillAsync(voucherSysCategory.Trim().ToUpperInvariant(), userName.Trim(), bankCode?.Trim(), accPeriod?.Trim(), HttpContext.RequestAborted);
                return Ok(new { success = true, status = 200, message = $"Drill-down for {voucherSysCategory} retrieved successfully.", data = result });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error in drill-down for {SysCat}.", voucherSysCategory);
                return StatusCode(500, new { success = false, status = 500, message = $"Application error in drill-down for {voucherSysCategory}.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in drill-down for {SysCat}.", voucherSysCategory);
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error in drill-down.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("MonthlyTrend")]
        public async Task<IActionResult> GetMonthlyTrend([FromQuery] string? voucherGroup = null, [FromQuery] string? accPeriod = null, [FromQuery] string? bankCode = null, [FromQuery] string? finYear = null)
        {
            try
            {
                var result = await _dashboardService.GetMonthlyTrendAsync(voucherGroup?.Trim(), accPeriod?.Trim(), bankCode?.Trim(), finYear?.Trim(), HttpContext.RequestAborted);
                if (result == null || !result.Any())
                    return Ok(new { success = false, status = 404, message = "No trend data found for the specified period." });

                return Ok(new { success = true, status = 200, message = "Monthly trend retrieved successfully.", data = result });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error retrieving monthly trend.");
                return StatusCode(500, new { success = false, status = 500, message = "Application error retrieving monthly trend.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error retrieving monthly trend.");
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error retrieving monthly trend.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("BankSummary")]
        public async Task<IActionResult> GetBankSummary([FromQuery] string userName, [FromQuery] string? bankCode = null, [FromQuery] string? accPeriod = null)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest(new { success = false, status = 400, message = "Username is required." });

            try
            {
                var result = await _dashboardService.GetBankSummaryAsync(userName.Trim(), bankCode?.Trim(), accPeriod?.Trim(), HttpContext.RequestAborted);
                if (result == null || !result.Any())
                    return Ok(new { success = false, status = 404, message = "No bank data found." });

                return Ok(new { success = true, status = 200, message = "Bank summary retrieved successfully.", data = result });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error retrieving bank summary.");
                return StatusCode(500, new { success = false, status = 500, message = "Application error retrieving bank summary.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error retrieving bank summary.");
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error retrieving bank summary.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("RecentVouchers")]
        public async Task<IActionResult> GetRecentVouchers([FromQuery] string? voucherSysCategory = null, [FromQuery] string? bankCode = null, [FromQuery] string? ctrlStatus = null, [FromQuery] int top = 20, [FromQuery] string? accPeriod = null)
        {
            top = Math.Clamp(top, 1, 100);
            try
            {
                var result = await _dashboardService.GetRecentVouchersAsync(voucherSysCategory?.Trim().ToUpperInvariant(), bankCode?.Trim(), ctrlStatus?.Trim(), top, accPeriod?.Trim(), HttpContext.RequestAborted);
                if (result == null || !result.Any())
                    return Ok(new { success = false, status = 404, message = "No vouchers found for the specified filters." });

                return Ok(new { success = true, status = 200, message = "Recent vouchers retrieved successfully.", data = result });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error retrieving recent vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Application error retrieving recent vouchers.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error retrieving recent vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error retrieving recent vouchers.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}