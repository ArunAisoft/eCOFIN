using Azure.Core;
using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialYearsWithPeriodsController : ControllerBase
    {
        private readonly IFinancialYearsWithPeriodsService _financialYearService;

        public FinancialYearsWithPeriodsController(IFinancialYearsWithPeriodsService financialYearService)
        {
            _financialYearService = financialYearService;
        }

        [HttpGet("GetFinancialYearPeriods")]
        public async Task<IActionResult> GetFinancialYearPeriods()
        {
            try
            {
                var data = await _financialYearService.GetFinancialYearPeriodsAsync();

                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No Financial Year records found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Financial Years retrieved successfully.", data });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(500, new { success = false, status = 500, message = appEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "Unexpected error occurred.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetFinancialYears")]
        public async Task<IActionResult> GetFinancialYears()
        {
            try
            {
                var data = await _financialYearService.GetFinancialYearsAsync();
                return Ok(new { success = true, data });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(500, new { success = false, message = appEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Unexpected error occurred.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("CreateFinancialYear")]
        public async Task<IActionResult> CreateFinancialYear([FromBody] FinancialYearCreateModel model)
        {
            try
            {
                var result = await _financialYearService.CreateFinancialYearAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });

                return Ok(new { success = true, status = 200, message = "Financial Year created successfully." });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(500, new { success = false, message = appEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Unexpected error occurred.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetFinancialYears2")]
        public async Task<IActionResult> GetFinancialYears2()
        {
            try
            {
                var data = await _financialYearService.GetFinancialYears2Async();
                return Ok(new { success = true, data });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(500, new { success = false, message = appEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Unexpected error occurred.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("CreateFinancialYear2")]
        public async Task<IActionResult> CreateFinancialYear2([FromBody] FinancialYearCreateModel model)
        {
            try
            {
                var result = await _financialYearService.CreateFinancialYear2Async(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });

                return Ok(new { success = true, status = 200, message = "Financial Year created successfully." });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(500, new { success = false, message = appEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Unexpected error occurred.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("GetPeriodsByYear")]
        public async Task<IActionResult> GetPeriodsByYear(string financialYear)
        {
            try
            {
                var data = await _financialYearService.GetPeriodsByYearAsync(financialYear);
                return Ok(new { success = true, message = "Accounting periods retrieved successfully.", data });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(500, new { success = false, message = appEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Unexpected error occurred.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("SaveOrUpdatePeriod")]
        public async Task<IActionResult> SaveOrUpdatePeriodAsync([FromBody] AccountingPeriodCreateModel model)
        {
            try
            {
                var result = await _financialYearService.SaveOrUpdatePeriodAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });

                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx)
            {
                return StatusCode(500, new { success = false, message = appEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Unexpected error occurred.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}