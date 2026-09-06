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

    public class CashReceiptsController : ControllerBase
    {
        private readonly ICashReceiptsService _CashReceiptsService;
        private readonly ILogger<CashReceiptsController> _logger;
        public CashReceiptsController(ICashReceiptsService CashReceiptsService, ILogger<CashReceiptsController> logger)
        {
            _CashReceiptsService = CashReceiptsService;
            _logger = logger;
        }

        [HttpGet("GetAllCashReceipts")]
        public async Task<IActionResult> GetAllCashReceipts(string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "Financial Year & Month is required." });

                var normalized = System.Text.RegularExpressions.Regex.Replace(accPeriod.Trim(), @"\s*-\s*", " - ");
                var vouchers = await _CashReceiptsService.GetAllCashReceiptsAsync(normalized);

                return Ok(new { success = true, status = 200, message = "All Cash Receipts retrieved successfully.", data = vouchers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All Cash Receipts details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving All Cash Receipts.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetCashReceiptWithDetails")]
        public async Task<IActionResult> GetCashReceiptWithDetails(string onHoldNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(onHoldNo))
                    return Ok(new { success = false, status = 201, message = "Onhold No is required." });

                var voucher = await _CashReceiptsService.GetCashReceiptWithDetailsAsync(onHoldNo);
                if (voucher == null)
                    return Ok(new { success = false, status = 201, message = $"No Cash Receipt found for Onhold No: {onHoldNo}." });

                return Ok(new { success = true, status = 200, message = "Cash Receipt details retrieved successfully.", data = voucher });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Cash Receipt details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving the Cash Receipt details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpPost("OnHoldCashReceipt")]
        public async Task<IActionResult> OnHoldCashReceipt([FromBody] CashReceiptsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var onHoldNo = await _CashReceiptsService.OnHoldCashReceiptAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Cash Receipt saved successfully.", data = onHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving OnHold Cash Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Cash Receipt (possible duplicate Onhold No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving OnHold Cash Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Cash Receipt details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving OnHold Cash Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Cash Receipt.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving OnHold Cash Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Cash Receipt.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("PostCashReceipt")]
        public async Task<IActionResult> PostCashReceipt([FromBody] CashReceiptsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var voucherNumber = await _CashReceiptsService.PostCashReceiptAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Cash Receipt posted successfully.", data = request.VoucherData.CtrlOnHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving Post Cash Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Cash Receipt (possible duplicate Post No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving Post Cash Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Cash Receipt details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving Post Cash Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Cash Receipt.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Post Cash Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Cash Receipt.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
