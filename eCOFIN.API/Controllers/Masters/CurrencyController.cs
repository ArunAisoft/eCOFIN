using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("GetAllCurrencies")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            try
            {
                var data = await _currencyService.GetAllCurrenciesAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No Currencies found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Currencies retrieved successfully.", data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = true, status = 500, message = "An error occurred while retrieving Currencies.", error = ex });
            }
        }

        [HttpGet("GetAllActiveCurrencies")]
        public async Task<IActionResult> GetAllActiveCurrencies()
        {
            try
            {
                var data = await _currencyService.GetAllActiveCurrenciesAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No Currencies found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Currencies retrieved successfully.", data });
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

        [HttpPost("SaveOrUpdateCurrency")]
        public async Task<IActionResult> SaveOrUpdateCurrency([FromBody] CurrencyDto model)
        {
            try
            {
                var result = await _currencyService.SaveOrUpdateCurrencyAsync(model);
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