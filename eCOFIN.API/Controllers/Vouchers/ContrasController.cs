using AutoMapper;
using Azure.Core;
using eCOFIN.Application.DTOs.Vouchers;
using eCOFIN.Application.Interfaces.Vouchers;
using eCOFIN.Infrastructure.Services.Vouchers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCOFIN.API.Controllers.Vouchers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ContrasController : ControllerBase
    {
        private readonly IContrasService _contraService;
        private readonly ILogger<ContrasController> _logger;
        public ContrasController(IContrasService contraService, ILogger<ContrasController> logger)
        {
            _contraService = contraService;
            _logger = logger;
        }

        [HttpGet("GetAllContras")]
        public async Task<IActionResult> GetAllContras(string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "Financial Year & Month is required." });

                var normalized = System.Text.RegularExpressions.Regex.Replace(accPeriod.Trim(), @"\s*-\s*", " - ");
                var vouchers = await _contraService.GetAllContrasAsync(normalized);

                return Ok(new { success = true, status = 200, message = "All Contras retrieved successfully.", data = vouchers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All Contras details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving All Contras.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetContraWithDetails")]
        public async Task<IActionResult> GetContraWithDetails(string onHoldNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(onHoldNo))
                    return Ok(new { success = false, status = 201, message = "Onhold No is required." });

                var voucher = await _contraService.GetContraWithDetailsAsync(onHoldNo);
                if (voucher == null)
                    return Ok(new { success = false, status = 201, message = $"No Contra found for Onhold No: {onHoldNo}." });

                return Ok(new { success = true, status = 200, message = "Contra details retrieved successfully.", data = voucher });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Contra details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving the Contra details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpPost("OnHoldContra")]
        public async Task<IActionResult> OnHoldContra([FromBody] ContrasRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var onHoldNo = await _contraService.OnHoldContraAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Contra saved successfully.", data = onHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving OnHold Contra for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Contra (possible duplicate Onhold No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving OnHold Contra for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Contra details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving OnHold Contra for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Contra.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving OnHold Contra for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Contra.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("PostContra")]
        public async Task<IActionResult> PostContra([FromBody] ContrasRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var voucherNumber = await _contraService.PostContraAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Contra posted successfully.", data = request.VoucherData.CtrlOnHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving Post Contra for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Contra (possible duplicate Post No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving Post Contra for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Contra details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving Post Contra for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Contra.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Post Contra for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Contra.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
