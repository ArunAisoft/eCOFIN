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

    public class CashPaymentsController : ControllerBase
    {
        private readonly ICashPaymentsService _CashPaymentsService;
        private readonly ILogger<CashPaymentsController> _logger;
        public CashPaymentsController(ICashPaymentsService CashPaymentsService, ILogger<CashPaymentsController> logger)
        {
            _CashPaymentsService = CashPaymentsService;
            _logger = logger;
        }

        [HttpGet("GetAllCashPayments")]
        public async Task<IActionResult> GetAllCashPayments(string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "Financial Year & Month is required." });

                var normalized = System.Text.RegularExpressions.Regex.Replace(accPeriod.Trim(), @"\s*-\s*", " - ");
                var vouchers = await _CashPaymentsService.GetAllCashPaymentsAsync(normalized);

                return Ok(new { success = true, status = 200, message = "All Cash Payments retrieved successfully.", data = vouchers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All Cash Payments details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving All Cash Payments.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetCashPaymentWithDetails")]
        public async Task<IActionResult> GetCashPaymentWithDetails(string onHoldNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(onHoldNo))
                    return Ok(new { success = false, status = 201, message = "Onhold No is required." });

                var voucher = await _CashPaymentsService.GetCashPaymentWithDetailsAsync(onHoldNo);
                if (voucher == null)
                    return Ok(new { success = false, status = 201, message = $"No Cash Payment found for Onhold No: {onHoldNo}." });

                return Ok(new { success = true, status = 200, message = "Cash Payment details retrieved successfully.", data = voucher });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Cash Payment details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving the Cash Payment details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpPost("OnHoldCashPayment")]
        public async Task<IActionResult> OnHoldCashPayment([FromBody] CashPaymentsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Payment is required." });

                var onHoldNo = await _CashPaymentsService.OnHoldCashPaymentAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Cash Payment saved successfully.", data = onHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving OnHold Cash Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Cash Payment (possible duplicate Onhold No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving OnHold Cash Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Cash Payment details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving OnHold Cash Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Cash Payment.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving OnHold Cash Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Cash Payment.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("PostCashPayment")]
        public async Task<IActionResult> PostCashPayment([FromBody] CashPaymentsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Payment is required." });

                var voucherNumber = await _CashPaymentsService.PostCashPaymentAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Cash Payment posted successfully.", data = request.VoucherData.CtrlOnHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving Post Cash Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Cash Payment (possible duplicate Post No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving Post Cash Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Cash Payment details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving Post Cash Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Cash Payment.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Post Cash Payment for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Cash Payment.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
