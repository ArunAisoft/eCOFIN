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

    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;
        private readonly ILogger<SalesController> _logger;
        public SalesController(ISalesService salesService, ILogger<SalesController> logger)
        {
            _salesService = salesService;
            _logger = logger;
        }

        [HttpGet("GetAllSales")]
        public async Task<IActionResult> GetAllSales(string accPeriod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accPeriod))
                    return Ok(new { success = false, status = 201, message = "Financial Year & Month is required." });

                var normalized = System.Text.RegularExpressions.Regex.Replace(accPeriod.Trim(), @"\s*-\s*", " - ");
                var vouchers = await _salesService.GetAllSalesAsync(normalized);

                return Ok(new { success = true, status = 200, message = "All Sales retrieved successfully.", data = vouchers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All Sales details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving All Sales.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetSaleWithDetails")]
        public async Task<IActionResult> GetSaleWithDetails(string onHoldNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(onHoldNo))
                    return Ok(new { success = false, status = 201, message = "Onhold No is required." });

                var voucher = await _salesService.GetSaleWithDetailsAsync(onHoldNo);
                if (voucher == null)
                    return Ok(new { success = false, status = 201, message = $"No Sale found for Onhold No: {onHoldNo}." });

                return Ok(new { success = true, status = 200, message = "Sale details retrieved successfully.", data = voucher });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Sale details.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving the Sale details.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }


        [HttpPost("OnHoldSale")]
        public async Task<IActionResult> OnHoldSale([FromBody] SalesRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var onHoldNo = await _salesService.OnHoldSaleAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Sale saved successfully.", data = onHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving OnHold Sale for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Sale (possible duplicate Onhold No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving OnHold Sale for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Sale details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving OnHold Sale for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Sale.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving OnHold Sale for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Sale.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("PostSale")]
        public async Task<IActionResult> PostSale([FromBody] SalesRequestDto request)
        {
            try
            {
                if (request == null || request.VoucherData == null)
                    return Ok(new { success = false, status = 201, message = "Request body or Receipt is required." });

                var voucherNumber = await _salesService.PostSaleAsync(request).ConfigureAwait(false);
                return Ok(new { success = true, status = 200, message = "Sale posted successfully.", data = request.VoucherData.CtrlOnHoldNo });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error saving Post Sale for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "Conflict while saving the Sale (possible duplicate Post No or FK issue).", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Error saving Post Sale for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An argument error occurred while retrieving the Sale details.", error = argEx.InnerException?.Message ?? argEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error saving Post Sale for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An application error occurred while saving the Sale.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Post Sale for {VoucherNo}", request?.VoucherData?.VoucherNumber);
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while saving the Sale.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetSaleDomesticData")]
        public async Task<IActionResult> GetSaleDomesticData([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try
            {
                if (!fromDate.HasValue || !toDate.HasValue)
                    return Ok(new { success = false, status = 201, message = "Both fromDate and toDate are required." });

                var data = await _salesService.GetSaleDomesticDataAsync(fromDate.Value, toDate.Value);
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No Domestic records found for the specified date range." });

                return Ok(new { success = true, status = 200, message = "Domestic import data retrieved successfully.", data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Domestics import data.");
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving Domestics import data.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetSaleExportData")]
        public async Task<IActionResult> GetSaleExportData([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try
            {
                if (!fromDate.HasValue || !toDate.HasValue)
                    return Ok(new { success = false, status = 201, message = "Both fromDate and toDate are required." });

                var data = await _salesService.GetSaleExportDataAsync(fromDate.Value, toDate.Value);
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No Export records found for the specified date range." });

                return Ok(new { success = true, status = 200, message = "Export import data retrieved successfully.", data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving Export import data.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("OnHoldDomesticSales")]
        public async Task<IActionResult> OnHoldDomesticSales([FromBody] SalesERPRequestDto request)
        {
            try
            {
                if (request == null || request.InvoiceNumbers == null || !request.InvoiceNumbers.Any())
                    return Ok(new { success = false, status = 201, message = "Invoice numbers are required." });

                var voucherType = request.VoucherType ?? string.Empty;
                var accountingPeriod = request.AccountingPeriod ?? string.Empty;
                var username = request.Username ?? string.Empty;
                var locationCode = request.LocationCode ?? string.Empty;
                var result = await _salesService.OnHoldMultipleDomesticSalesAsync(request.InvoiceNumbers, voucherType, accountingPeriod, username, locationCode, request.VoucherDate).ConfigureAwait(false);

                return Ok(new { success = true, status = 200, message = "Domestic Sales OnHold vouchers created successfully.", data = result });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error creating Domestic Sales OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Database conflict occurred while creating Domestic Sales OnHold vouchers.", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error creating Domestic Sales OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Application error occurred while creating Domestic Sales OnHold vouchers.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating Domestic Sales OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error occurred while creating Domestic Sales OnHold vouchers.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("OnHoldExportSales")]
        public async Task<IActionResult> OnHoldExportSales([FromBody] SalesERPRequestDto request)
        {
            try
            {
                if (request == null || request.InvoiceNumbers == null || !request.InvoiceNumbers.Any())
                    return Ok(new { success = false, status = 201, message = "Invoice numbers are required." });

                var voucherType = request.VoucherType ?? string.Empty;
                var accountingPeriod = request.AccountingPeriod ?? string.Empty;
                var username = request.Username ?? string.Empty;
                var locationCode = request.LocationCode ?? string.Empty;
                var result = await _salesService.OnHoldMultipleExportSalesAsync(request.InvoiceNumbers, voucherType, accountingPeriod, username, locationCode, request.VoucherDate).ConfigureAwait(false);

                return Ok(new { success = true, status = 200, message = "Export Sales OnHold vouchers created successfully.", data = result });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error creating Export Sales OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Database conflict occurred while creating Export Sales OnHold vouchers.", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error creating Export Sales OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Application error occurred while creating Export Sales OnHold vouchers.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating Export Sales OnHold vouchers.");
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error occurred while creating Export Sales OnHold vouchers.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("PostMultipleSales")]
        public async Task<IActionResult> PostMultipleSales([FromBody] PostMultipleRequest request)
        {
            try
            {
                if (request == null)
                    return Ok(new { success = false, status = 201, message = "Request body is required." });

                if (request.OnHoldNumbers == null || !request.OnHoldNumbers.Any())
                    return Ok(new { success = false, status = 201, message = "At least one OnHold number is required." });

                var result = await _salesService.PostMultipleSalesAsync(request.OnHoldNumbers, request.AccountingPeriod, request.Username, request.LocationCode).ConfigureAwait(false);

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
                _logger.LogError(dbEx, "DB error posting multiple Sales.");
                return StatusCode(500, new { success = false, status = 500, message = "Database conflict occurred while posting Sales.", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Application error posting multiple Sales.");
                return StatusCode(500, new { success = false, status = 500, message = "Application error occurred while posting Sales.", error = appEx.InnerException?.Message ?? appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error posting multiple Sales.");
                return StatusCode(500, new { success = false, status = 500, message = "An unexpected error occurred while posting Sales.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
