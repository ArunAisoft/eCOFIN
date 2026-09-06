using eCOFIN.Application.DTOs.Reports;
using eCOFIN.Application.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankReconciliationReportController : ControllerBase
    {
        private readonly IBankReconciliationReportService _svc;
        public BankReconciliationReportController(IBankReconciliationReportService svc) => _svc = svc;

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

        // ── Cheque Issued Not Presented (BR_ISSUE) ────────────────────────────
        [HttpGet("GetChequeIssuedNotPresented")]
        public async Task<IActionResult> GetChequeIssuedNotPresented(
            [FromQuery] string accPeriod,
            [FromQuery] DateTime asAtDate)
        {
            try
            {
                var data = await _svc.GetChequeIssuedNotPresentedAsync(new BankReconFilter
                {
                    AccPeriod = accPeriod,
                    AsAtDate = asAtDate
                });
                if (!data.Any()) return Ok(new { success = false, status = 201, message = "No records found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Cheque Issued Not Presented retrieved.", data });
            }
            catch (ApplicationException ex) { return StatusCode(500, new { success = false, status = 500, message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        // ── Cheque Deposited Not Presented (BR_DEP) ───────────────────────────
        [HttpGet("GetChequeDepositedNotPresented")]
        public async Task<IActionResult> GetChequeDepositedNotPresented(
            [FromQuery] string accPeriod,
            [FromQuery] DateTime asAtDate)
        {
            try
            {
                var data = await _svc.GetChequeDepositedNotPresentedAsync(new BankReconFilter
                {
                    AccPeriod = accPeriod,
                    AsAtDate = asAtDate
                });
                if (!data.Any()) return Ok(new { success = false, status = 201, message = "No records found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Cheque Deposited Not Presented retrieved.", data });
            }
            catch (ApplicationException ex) { return StatusCode(500, new { success = false, status = 500, message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        // ── Debited By Bank Not Accounted (BR_DBT) ────────────────────────────
        [HttpGet("GetDebitedByBankNotAccounted")]
        public async Task<IActionResult> GetDebitedByBankNotAccounted(
            [FromQuery] string accPeriod,
            [FromQuery] DateTime asAtDate)
        {
            try
            {
                var data = await _svc.GetDebitedByBankNotAccountedAsync(new BankReconFilter
                {
                    AccPeriod = accPeriod,
                    AsAtDate = asAtDate
                });
                if (!data.Any()) return Ok(new { success = false, status = 201, message = "No records found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Debited By Bank Not Accounted retrieved.", data });
            }
            catch (ApplicationException ex) { return StatusCode(500, new { success = false, status = 500, message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        // ── Credited By Bank Not Accounted (BR_CRDT) ──────────────────────────
        [HttpGet("GetCreditedByBankNotAccounted")]
        public async Task<IActionResult> GetCreditedByBankNotAccounted(
            [FromQuery] string accPeriod,
            [FromQuery] DateTime asAtDate)
        {
            try
            {
                var data = await _svc.GetCreditedByBankNotAccountedAsync(new BankReconFilter
                {
                    AccPeriod = accPeriod,
                    AsAtDate = asAtDate
                });
                if (!data.Any()) return Ok(new { success = false, status = 201, message = "No records found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Credited By Bank Not Accounted retrieved.", data });
            }
            catch (ApplicationException ex) { return StatusCode(500, new { success = false, status = 500, message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }
    }
}
