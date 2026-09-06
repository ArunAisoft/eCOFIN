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

    public class PurchaseBillsController : ControllerBase
    {
        private readonly IPurchaseBillsService _purchasebillsService;
        private readonly ILogger<PurchaseBillsController> _logger;
        public PurchaseBillsController(IPurchaseBillsService purchasebillsService, ILogger<PurchaseBillsController> logger)
        {
            _purchasebillsService = purchasebillsService;
            _logger = logger;
        }

        [HttpGet("GetAllPurchaseBills")]
        public async Task<IActionResult> GetAllPurchaseBills(string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "Financial Year & Month is required." });

                var normalized = System.Text.RegularExpressions.Regex.Replace(accPeriod.Trim(), @"\s*-\s*", " - ");
                var vouchers = await _purchasebillsService.GetAllPurchaseBillsAsync(normalized);

                return Ok(new { success = true, status = 200, message = "All Purchase Bills retrieved successfully.", data = vouchers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All Purchase Bills details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving All Purchase Bills.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetPurchaseBillWithDetails")]
        public async Task<IActionResult> GetPurchaseBillWithDetails(string onHoldNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(onHoldNo))
                    return Ok(new { success = false, status = 201, message = "Onhold No is required." });

                var voucher = await _purchasebillsService.GetPurchaseBillWithDetailsAsync(onHoldNo);
                if (voucher == null)
                    return Ok(new { success = false, status = 201, message = $"No Purchase Bill found for Onhold No: {onHoldNo}." });

                return Ok(new { success = true, status = 200, message = "Purchase Bill details retrieved successfully.", data = voucher });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Purchase Bill details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving the Purchase Bill details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpPost("OnHoldPurchaseBill")]
        public async Task<IActionResult> OnHoldPurchaseBill([FromBody] PurchaseBillsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var onHoldNo = await _purchasebillsService.OnHoldPurchaseBillAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Purchase Bill saved successfully.", data = onHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving OnHold Purchase Bill for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Purchase Bill (possible duplicate Onhold No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving OnHold Purchase Bill for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Purchase Bill details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving OnHold Purchase Bill for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Purchase Bill.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving OnHold Purchase Bill for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Purchase Bill.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("PostPurchaseBill")]
        public async Task<IActionResult> PostPurchaseBill([FromBody] PurchaseBillsRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var voucherNumber = await _purchasebillsService.PostPurchaseBillAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Purchase Bill posted successfully.", data = request.VoucherData.VoucherNumber });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving Post Purchase Bill for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Purchase Bill (possible duplicate Post No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving Post Purchase Bill for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Purchase Bill details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving Post Purchase Bill for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Purchase Bill.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Post Purchase Bill for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Purchase Bill.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetGINImportData")]
        public async Task<IActionResult> GetGINImportData([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try
            {
                if (!fromDate.HasValue || !toDate.HasValue)
                    return Ok(new { success = false, status = 201, message = "Both fromDate and toDate are required." });

                var data = await _purchasebillsService.GetGINImportDataAsync(fromDate.Value, toDate.Value);
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No GIN records found for the specified date range." });

                return Ok(new { success = true, status = 200, message = "GIN import data retrieved successfully.", data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All GIN Import details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving GIN import data.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetJINImportData")]
        public async Task<IActionResult> GetJINImportData([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try
            {
                if (!fromDate.HasValue || !toDate.HasValue)
                    return Ok(new { success = false, status = 201, message = "Both fromDate and toDate are required." });

                var data = await _purchasebillsService.GetJINImportDataAsync(fromDate.Value, toDate.Value);
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No JIN records found for the specified date range." });

                return Ok(new { success = true, status = 200, message = "JIN import data retrieved successfully.", data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All JIN Import details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving JIN import data.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("OnHoldGIN")]
        public async Task<IActionResult> OnHoldGIN([FromBody] PurchaseERPRequestDto request)
        {
            try
            {
                if (request == null || request.DocumentNumbers == null || !request.DocumentNumbers.Any())
                    return Ok(new { success = false, status = 201, message = "GIN numbers are required." });

                var voucherType = request.VoucherType ?? string.Empty;
                var accountingPeriod = request.AccountingPeriod ?? string.Empty;
                var username = request.Username ?? string.Empty;
                var locationCode = request.LocationCode ?? string.Empty;
                var result = await _purchasebillsService.OnHoldMultipleGINAsync(request.DocumentNumbers, voucherType, accountingPeriod, username, locationCode, request.VoucherDate).ConfigureAwait(false);

                return Ok(new { success = true, status = 200, message = "GIN OnHold vouchers created successfully.", data = result });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error creating GIN OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Database conflict occurred while creating GIN OnHold vouchers.", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error creating GIN OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Application error occurred while creating GIN OnHold vouchers.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating GIN OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error occurred while creating GIN OnHold vouchers.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("OnHoldJIN")]
        public async Task<IActionResult> OnHoldJIN([FromBody] PurchaseERPRequestDto request)
        {
            try
            {
                if (request == null || request.DocumentNumbers == null || !request.DocumentNumbers.Any())
                    return Ok(new { success = false, status = 201, message = "JIN numbers are required." });

                var voucherType = request.VoucherType ?? string.Empty;
                var accountingPeriod = request.AccountingPeriod ?? string.Empty;
                var username = request.Username ?? string.Empty;
                var locationCode = request.LocationCode ?? string.Empty;
                var result = await _purchasebillsService.OnHoldMultipleJINAsync(request.DocumentNumbers, voucherType, accountingPeriod, username, locationCode, request.VoucherDate).ConfigureAwait(false);

                return Ok(new { success = true, status = 200, message = "JIN OnHold vouchers created successfully.", data = result });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error creating JIN OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Database conflict occurred while creating JIN OnHold vouchers.", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error creating JIN OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Application error occurred while creating JIN OnHold vouchers.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating JIN OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error occurred while creating JIN OnHold vouchers.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("PostMultiplePurchaseBills")]
        public async Task<IActionResult> PostMultiplePurchaseBills([FromBody] PostMultipleRequest request)
        {
            try
            {
                if (request == null)
                    return Ok(new { success = false, status = 201, message = "Request body is required." });

                if (request.OnHoldNumbers == null || !request.OnHoldNumbers.Any())
                    return Ok(new { success = false, status = 201, message = "At least one OnHold number is required." });

                var result = await _purchasebillsService.PostMultiplePurchaseBillsAsync(request.OnHoldNumbers, request.AccountingPeriod, request.Username, request.LocationCode).ConfigureAwait(false);

                if (result.HasFailures && result.Posted.Any())
                    return Ok(new
                    {
                        success = true,
                        status = 200,
                        message = $"{result.Posted.Count} voucher(s) posted. {result.Failed.Count} failed — see 'failed' for details.",
                        total = request.OnHoldNumbers.Count,
                        posted = result.Posted.Count,
                        failed = result.Failed.Count,
                        data = result.Posted,
                        failures = result.Failed
                    });

                if (!result.Posted.Any())
                    return Ok(new
                    {
                        success = false,
                        status = 201,
                        message = "No vouchers were posted. All failed — see 'failed' for details.",
                        total = request.OnHoldNumbers.Count,
                        posted = 0,
                        failed = result.Failed.Count,
                        failures = result.Failed
                    });

                return Ok(new { success = true, status = 200, message = $"All {result.Posted.Count} voucher(s) posted successfully.", total = request.OnHoldNumbers.Count, posted = result.Posted.Count, failed = 0, data = result.Posted });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "DB error posting multiple Purchase Bills.");
                return StatusCode(500, new { success = false, status = 500, message = "Database conflict occurred while posting Purchase Bills.", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error posting multiple Purchase Bills.");
                return StatusCode(500, new { success = false, status = 500, message = "Application error occurred while posting Purchase Bills.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error posting multiple Purchase Bills.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while posting Purchase Bills.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
