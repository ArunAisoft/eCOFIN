using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubLedgerReportController : ControllerBase
    {
        private readonly ISubLedgerReportService _svc;
        public SubLedgerReportController(ISubLedgerReportService svc) => _svc = svc;

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

        // ── Debtor Ledger ─────────────────────────────────────────────────────
        [HttpGet("GetDebtorLedger")]
        public async Task<IActionResult> GetDebtorLedger(
            [FromQuery] string accPeriod,
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate)
        {
            try
            {
                var data = await _svc.GetDebtorLedgerAsync(new SubLedgerFilter
                {
                    AccPeriod = accPeriod,
                    FromDate = fromDate,
                    ToDate = toDate
                });
                if (!data.Any()) return Ok(new { success = false, status = 201, message = "No Debtor records found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Debtor Ledger retrieved.", data });
            }
            catch (ApplicationException ex) { return StatusCode(500, new { success = false, status = 500, message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        // ── Credit Ledger ─────────────────────────────────────────────────────
        [HttpGet("GetCreditLedger")]
        public async Task<IActionResult> GetCreditLedger(
            [FromQuery] string accPeriod,
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] string? userName)
        {
            try
            {
                var data = await _svc.GetCreditLedgerAsync(new SubLedgerFilter
                {
                    AccPeriod = accPeriod,
                    FromDate = fromDate,
                    ToDate = toDate,
                    UserName = userName
                });
                if (!data.Any()) return Ok(new { success = false, status = 201, message = "No Creditor records found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Credit Ledger retrieved.", data });
            }
            catch (ApplicationException ex) { return StatusCode(500, new { success = false, status = 500, message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        // ── Staff Loan Ledger ─────────────────────────────────────────────────
        [HttpGet("GetStaffLoanLedger")]
        public async Task<IActionResult> GetStaffLoanLedger(
            [FromQuery] string accPeriod,
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate)
        {
            try
            {
                var data = await _svc.GetStaffLoanLedgerAsync(new SubLedgerFilter
                {
                    AccPeriod = accPeriod,
                    FromDate = fromDate,
                    ToDate = toDate
                });
                if (!data.Any()) return Ok(new { success = false, status = 201, message = "No Staff Loan records found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Staff Loan Ledger retrieved.", data });
            }
            catch (ApplicationException ex) { return StatusCode(500, new { success = false, status = 500, message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        // ── Staff Advance Ledger ──────────────────────────────────────────────
        [HttpGet("GetStaffAdvanceLedger")]
        public async Task<IActionResult> GetStaffAdvanceLedger(
            [FromQuery] string accPeriod,
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate)
        {
            try
            {
                var data = await _svc.GetStaffAdvanceLedgerAsync(new SubLedgerFilter
                {
                    AccPeriod = accPeriod,
                    FromDate = fromDate,
                    ToDate = toDate
                });
                if (!data.Any()) return Ok(new { success = false, status = 201, message = "No Staff Advance records found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Staff Advance Ledger retrieved.", data });
            }
            catch (ApplicationException ex) { return StatusCode(500, new { success = false, status = 500, message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }
    }
}
