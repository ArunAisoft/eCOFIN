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

    public class BankReceiptsController : ControllerBase
    {
        private readonly IBankReceiptsService _bankReceiptsService;
        private readonly ILogger<BankReceiptsController> _logger;
        public BankReceiptsController(IBankReceiptsService bankReceiptsService, ILogger<BankReceiptsController> logger)
        {
            _bankReceiptsService = bankReceiptsService;
            _logger = logger;
        }

        [HttpGet("GetAllBankReceipts")]
        public async Task<IActionResult> GetAllBankReceipts(string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "Financial Year & Month is required." });

                var normalized = System.Text.RegularExpressions.Regex.Replace(accPeriod.Trim(), @"\s*-\s*", " - ");
                var vouchers = await _bankReceiptsService.GetAllBankReceiptsAsync(normalized);

                return Ok(new { success = true, status = 200, message = "All Bank Receipts retrieved successfully.", data = vouchers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All Bank Receipts details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving All Bank Receipts.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetBankReceiptWithDetails")]
        public async Task<IActionResult> GetBankReceiptWithDetails(string onHoldNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(onHoldNo))
                    return Ok(new { success = false, status = 201, message = "Onhold No is required." });

                var voucher = await _bankReceiptsService.GetBankReceiptWithDetailsAsync(onHoldNo);
                if (voucher == null)
                    return Ok(new { success = false, status = 201, message = $"No Bank Receipt found for Onhold No: {onHoldNo}." });

                return Ok(new { success = true, status = 200, message = "Bank Receipt details retrieved successfully.", data = voucher });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Bank Receipt details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving the Bank Receipt details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpPost("OnHoldBankReceipt")]
        public async Task<IActionResult> OnHoldBankReceipt([FromBody] BankReceiptsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var onHoldNo = await _bankReceiptsService.OnHoldBankReceiptAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Bank Receipt saved successfully.", data = onHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving OnHold Bank Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Bank Receipt (possible duplicate Onhold No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving OnHold Bank Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Bank Receipt details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving OnHold Bank Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Bank Receipt.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving OnHold Bank Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Bank Receipt.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("PostBankReceipt")]
        public async Task<IActionResult> PostBankReceipt([FromBody] BankReceiptsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var voucherNumber = await _bankReceiptsService.PostBankReceiptAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Bank Receipt posted successfully.", data = request.VoucherData.CtrlOnHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving Post Bank Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Bank Receipt (possible duplicate Post No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving Post Bank Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Bank Receipt details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving Post Bank Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Bank Receipt.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Post Bank Receipt for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Bank Receipt.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
