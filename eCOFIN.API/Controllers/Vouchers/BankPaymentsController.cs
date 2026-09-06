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

    public class BankPaymentsController : ControllerBase
    {
        private readonly IBankPaymentsService _bankPaymentsService;
        private readonly ILogger<BankPaymentsController> _logger;
        public BankPaymentsController(IBankPaymentsService bankPaymentsService, ILogger<BankPaymentsController> logger)
        {
            _bankPaymentsService = bankPaymentsService;
            _logger = logger;
        }

        [HttpGet("GetAllBankPayments")]
        public async Task<IActionResult> GetAllBankPayments(string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "Financial Year & Month is required." });

                var normalized = System.Text.RegularExpressions.Regex.Replace(accPeriod.Trim(), @"\s*-\s*", " - ");
                var vouchers = await _bankPaymentsService.GetAllBankPaymentsAsync(normalized);

                return Ok(new { success = true, status = 200, message = "All Bank Payments retrieved successfully.", data = vouchers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All Bank Payments details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving All Bank Payments.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetBankPaymentWithDetails")]
        public async Task<IActionResult> GetBankPaymentWithDetails(string onHoldNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(onHoldNo))
                    return Ok(new { success = false, status = 201, message = "Onhold No is required." });

                var voucher = await _bankPaymentsService.GetBankPaymentWithDetailsAsync(onHoldNo);
                if (voucher == null)
                    return Ok(new { success = false, status = 201, message = $"No Bank Payment found for Onhold No: {onHoldNo}." });

                return Ok(new { success = true, status = 200, message = "Bank Payment details retrieved successfully.", data = voucher });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Bank Payment details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving the Bank Payment details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpPost("OnHoldBankPayment")]
        public async Task<IActionResult> OnHoldBankPayment([FromBody] BankPaymentsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Payment is required." });

                var onHoldNo = await _bankPaymentsService.OnHoldBankPaymentAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Bank Payment saved successfully.", data = onHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving OnHold Bank Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Bank Payment (possible duplicate Onhold No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving OnHold Bank Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Bank Payment details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving OnHold Bank Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Bank Payment.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving OnHold Bank Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Bank Payment.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("PostBankPayment")]
        public async Task<IActionResult> PostBankPayment([FromBody] BankPaymentsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Payment is required." });

                var voucherNumber = await _bankPaymentsService.PostBankPaymentAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Bank Payment posted successfully.", data = request.VoucherData.CtrlOnHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving Post Bank Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Bank Payment (possible duplicate Post No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving Post Bank Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Bank Payment details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving Post Bank Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Bank Payment.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Post Bank Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Bank Payment.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
