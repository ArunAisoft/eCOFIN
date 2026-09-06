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

    public class JournalsController : ControllerBase
    {
        private readonly IJournalsService _journalsService;
        private readonly ILogger<JournalsController> _logger;
        public JournalsController(IJournalsService journalsService, ILogger<JournalsController> logger)
        {
            _journalsService = journalsService;
            _logger = logger;
        }

        [HttpGet("GetAllJournals")]
        public async Task<IActionResult> GetAllJournals(string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "Financial Year & Month is required." });

                var normalized = System.Text.RegularExpressions.Regex.Replace(accPeriod.Trim(), @"\s*-\s*", " - ");
                var vouchers = await _journalsService.GetAllJournalsAsync(normalized);

                return Ok(new { success = true, status = 200, message = "All Journals retrieved successfully.", data = vouchers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All Journals details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving All Journals.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetJournalWithDetails")]
        public async Task<IActionResult> GetJournalWithDetails(string onHoldNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(onHoldNo))
                    return Ok(new { success = false, status = 201, message = "Onhold No is required." });

                var voucher = await _journalsService.GetJournalWithDetailsAsync(onHoldNo);
                if (voucher == null)
                    return Ok(new { success = false, status = 201, message = $"No Journal found for Onhold No: {onHoldNo}." });

                return Ok(new { success = true, status = 200, message = "Journal details retrieved successfully.", data = voucher });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Journal details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving the Journal details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpPost("OnHoldJournal")]
        public async Task<IActionResult> OnHoldJournal([FromBody] JournalsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var onHoldNo = await _journalsService.OnHoldJournalAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Journal saved successfully.", data = onHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving OnHold Journal for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Journal (possible duplicate Onhold No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving OnHold Journal for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Journal details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving OnHold Journal for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Journal.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving OnHold Journal for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Journal.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("PostJournal")]
        public async Task<IActionResult> PostJournal([FromBody] JournalsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var voucherNumber = await _journalsService.PostJournalAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Journal posted successfully.", data = request.VoucherData.CtrlOnHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving Post Journal for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Journal (possible duplicate Post No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving Post Journal for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Journal details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving Post Journal for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Journal.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Post Journal for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Journal.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
