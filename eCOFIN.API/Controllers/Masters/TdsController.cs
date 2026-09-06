using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class TdsController : ControllerBase
    {
        private readonly ITdsService _tdsService;

        public TdsController(ITdsService tdsService)
        {
            _tdsService = tdsService;
        }

        [HttpGet("GetAllTDS")]
        public async Task<IActionResult> GetAllTDS()
        {
            try
            {
                var data = await _tdsService.GetAllTDSAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No TDS found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "TDS retrieved successfully.", data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while retrieving TDS.", error = ex });
            }
        }

        [HttpGet("GetAllActiveTDS")]
        public async Task<IActionResult> GetAllActiveTDS()
        {
            try
            {
                var data = await _tdsService.GetAllActiveTDSAsync();

                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No TDS records found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "TDS records retrieved successfully.", data });
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

        [HttpPost("SaveOrUpdateTds")]
        public async Task<IActionResult> SaveOrUpdateTds([FromBody] TdsCreateModel model)
        {
            try
            {
                var result = await _tdsService.SaveOrUpdateTdsAsync(model);

                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });

                return Ok(new { success = true, status = 200, message = result.Message });
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
    }
}
