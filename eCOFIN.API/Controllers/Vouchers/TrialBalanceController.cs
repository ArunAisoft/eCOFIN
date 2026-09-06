using eCOFIN.Application.Interfaces.Vouchers;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace eCOFIN.API.Controllers.Vouchers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrialBalanceController : ControllerBase
    {
        private readonly ITrialBalanceService _trialBalanceService;

        public TrialBalanceController(ITrialBalanceService trialBalanceService)
        {
            _trialBalanceService = trialBalanceService;
        }

        // ── existing endpoints (unchanged) ────────────────────────────────────

        [HttpGet("GetTrialBalance")]
        public async Task<IActionResult> GetTrialBalance([FromQuery] string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "AccPeriod is required." });

                var tb = await _trialBalanceService.GetTrialBalanceAsync(accPeriod.Trim());
                if (tb == null || !tb.Any())
                    return Ok(new { success = false, status = 201, message = "No trial balance data found for the specified period." });

                return Ok(new { success = true, status = 200, message = "Trial balance retrieved successfully.", data = tb });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving trial balance.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetGLDetails")]
        public async Task<IActionResult> GetGLDetails([FromQuery] string accPeriod, [FromQuery] string accCode, [FromQuery] string fromDate, [FromQuery] string toDate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accCode))
                    return Ok(new { success = false, status = 201, message = "Account code (accCode) is required." });

                if (!DateTime.TryParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fDate))
                    return Ok(new { success = false, status = 201, message = "fromDate must be in yyyy-MM-dd format." });

                if (!DateTime.TryParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tDate))
                    return Ok(new { success = false, status = 201, message = "toDate must be in yyyy-MM-dd format." });

                var details = await _trialBalanceService.GetGLDetailsAsync(accPeriod.Trim(), accCode.Trim(), fDate, tDate);
                if (details == null || !details.Any())
                    return Ok(new { success = false, status = 201, message = "No GL details found for the specified account and date range." });

                return Ok(new { success = true, status = 200, message = "GL details retrieved successfully.", data = details });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving GL details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetSubledgerSchedule")]
        public async Task<IActionResult> GetSubledgerSchedule([FromQuery] string accPeriod, [FromQuery] string accCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod) || string.IsNullOrWhiteSpace(accCode))
                    return Ok(new { success = false, status = 201, message = "Both accPeriod and accCode are required." });

                var rows = await _trialBalanceService.GetSubledgerScheduleAsync(accPeriod.Trim(), accCode.Trim());
                if (rows == null || !rows.Any())
                    return Ok(new { success = false, status = 201, message = "No subledger schedule found for the specified account and period." });

                return Ok(new { success = true, status = 200, message = "Subledger schedule retrieved successfully.", data = rows });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving subledger schedule.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetSubledgerAccountDetails")]
        public async Task<IActionResult> GetSubledgerAccountDetails(
            [FromQuery] string accPeriod, [FromQuery] string accCode,
            [FromQuery] string subCode, [FromQuery] string fromDate, [FromQuery] string toDate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod) || string.IsNullOrWhiteSpace(accCode) || string.IsNullOrWhiteSpace(subCode))
                    return Ok(new { success = false, status = 201, message = "accPeriod, accCode and subCode are required." });

                if (!DateTime.TryParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fDate))
                    return Ok(new { success = false, status = 201, message = "fromDate must be in yyyy-MM-dd format." });

                if (!DateTime.TryParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var tDate))
                    return Ok(new { success = false, status = 201, message = "toDate must be in yyyy-MM-dd format." });

                var rows = await _trialBalanceService.GetSubledgerAccountDetailsAsync(accPeriod.Trim(), accCode.Trim(), subCode.Trim(), fDate, tDate);
                if (rows == null || !rows.Any())
                    return Ok(new { success = false, status = 201, message = "No subledger account details found for the specified inputs." });

                return Ok(new { success = true, status = 200, message = "Subledger account details retrieved successfully.", data = rows });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving subledger account details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetBillsAndPayments")]
        public async Task<IActionResult> GetBillsAndPayments([FromQuery] string accCode, [FromQuery] string subCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accCode) || string.IsNullOrWhiteSpace(subCode))
                    return Ok(new { success = false, status = 201, message = "Both accCode and subCode are required." });

                var items = await _trialBalanceService.GetBillsAndPaymentsAsync(accCode.Trim(), subCode.Trim());
                if (items == null || !items.Any())
                    return Ok(new { success = false, status = 201, message = "No bills or payments found for the specified inputs." });

                return Ok(new { success = true, status = 200, message = "Bills and payments retrieved successfully.", data = items });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving bills and payments.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetVoucherEntries")]
        public async Task<IActionResult> GetVoucherEntries([FromQuery] string voucherNumber, [FromQuery] string voucherDate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(voucherNumber) || string.IsNullOrWhiteSpace(voucherDate))
                    return Ok(new { success = false, status = 201, message = "Both voucherNumber and voucherDate are required." });

                if (!DateTime.TryParseExact(voucherDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var vDate))
                    return Ok(new { success = false, status = 201, message = "Voucher date must be in yyyy-MM-dd format." });

                var items = await _trialBalanceService.GetVoucherEntriesAsync(voucherNumber.Trim(), vDate);
                if (items == null || !items.Any())
                    return Ok(new { success = false, status = 201, message = "No voucher entries found for the given voucher number and date." });

                return Ok(new { success = true, status = 200, message = "Voucher entries retrieved successfully.", data = items });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while retrieving voucher entries.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        // ── NEW: GetCostProductEntries ────────────────────────────────────────
        // GET api/TrialBalance/GetCostProductEntries?voucherNumber=2601YBI0004
        [HttpGet("GetCostProductEntries")]
        public async Task<IActionResult> GetCostProductEntries([FromQuery] string voucherNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(voucherNumber))
                    return Ok(new { success = false, status = 201, message = "voucherNumber is required." });

                var data = await _trialBalanceService.GetCostProductEntriesAsync(voucherNumber.Trim());

                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No cost/product entries found for this voucher.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Cost/product entries retrieved successfully.", data });
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

        // ── NEW: GetBillsPaymentsAdjusted ────────────────────────────────────
        // GET api/TrialBalance/GetBillsPaymentsAdjusted?voucherNumber=2601SBR0100
        [HttpGet("GetBillsPaymentsAdjusted")]
        public async Task<IActionResult> GetBillsPaymentsAdjusted([FromQuery] string voucherNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(voucherNumber))
                    return Ok(new { success = false, status = 201, message = "voucherNumber is required." });

                var data = await _trialBalanceService.GetBillsPaymentsAdjustedAsync(voucherNumber.Trim());

                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No bills/payments adjusted entries found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Bills/payments adjusted retrieved successfully.", data });
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