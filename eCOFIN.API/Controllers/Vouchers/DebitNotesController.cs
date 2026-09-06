using AutoMapper;
using Azure.Core;
using eCOFIN.Application.DTOs.Vouchers;
using eCOFIN.Application.Interfaces.Vouchers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCOFIN.API.Controllers.Vouchers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DebitNotesController : ControllerBase
    {
        private readonly IDebitNotesService _debitnotesService;
        private readonly ILogger<DebitNotesController> _logger;
        public DebitNotesController(IDebitNotesService debitnotesService, ILogger<DebitNotesController> logger)
        {
            _debitnotesService = debitnotesService;
            _logger = logger;
        }

        [HttpGet("GetAllDebitNotes")]
        public async Task<IActionResult> GetAllDebitNotes(string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "Financial Year & Month is required." });

                var normalized = System.Text.RegularExpressions.Regex.Replace(accPeriod.Trim(), @"\s*-\s*", " - ");
                var vouchers = await _debitnotesService.GetAllDebitNotesAsync(normalized);

                return Ok(new { success = true, status = 200, message = "All Debit Notes retrieved successfully.", data = vouchers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All Debit Notes details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving All Debit Notes.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetDebitNoteWithDetails")]
        public async Task<IActionResult> GetDebitNoteWithDetails(string onHoldNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(onHoldNo))
                    return Ok(new { success = false, status = 201, message = "Onhold No is required." });

                var voucher = await _debitnotesService.GetDebitNoteWithDetailsAsync(onHoldNo);
                if (voucher == null)
                    return Ok(new { success = false, status = 201, message = $"No Debit Note found for Onhold No: {onHoldNo}." });

                return Ok(new { success = true, status = 200, message = "Debit Note details retrieved successfully.", data = voucher });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Debit Note details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving the Debit Note details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpPost("OnHoldDebitNote")]
        public async Task<IActionResult> OnHoldDebitNote([FromBody] DebitNotesRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var onHoldNo = await _debitnotesService.OnHoldDebitNoteAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Debit Note saved successfully.", data = onHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving OnHold Debit Note for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Debit Note (possible duplicate Onhold No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving OnHold Debit Note for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Debit Note details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving OnHold Debit Note for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Debit Note.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving OnHold Debit Note for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Debit Note.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("PostDebitNote")]
        public async Task<IActionResult> PostDebitNote([FromBody] DebitNotesRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var voucherNumber = await _debitnotesService.PostDebitNoteAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Debit Note posted successfully.", data = request.VoucherData.CtrlOnHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving Post Debit Note for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Debit Note (possible duplicate Post No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving Post Debit Note for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Debit Note details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving Post Debit Note for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Debit Note.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Post Debit Note for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Debit Note.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
