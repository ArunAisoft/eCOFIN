using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class CostCentreController : ControllerBase
    {
        private readonly ICostCentreService _costCentreService;

        public CostCentreController(ICostCentreService costCentreService)
        {
            _costCentreService = costCentreService;
        }

        [HttpGet("GetAllCostCentre")]
        public async Task<IActionResult> GetAllCostCentreAsync()
        {
            try
            {
                var data = await _costCentreService.GetAllCostCentreAsync();

                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No Cost Centres found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Cost Centres retrieved successfully.", data });
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

        [HttpGet("GetAllActiveCostCentres")]
        public async Task<IActionResult> GetAllActiveCostCentres()
        {
            try
            {
                var data = await _costCentreService.GetAllActiveCostCentresAsync();

                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No active Cost Centres found.", data = (object?)null });

                return Ok(new { success = true, status = 200, message = "Cost Centres retrieved successfully.", data });
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

        [HttpPost("SaveOrUpdateCostCentre")]
        public async Task<IActionResult> SaveOrUpdateCostCentre([FromBody] CostCentreCreateModel model)
        {
            try
            {
                var result = await _costCentreService.SaveOrUpdateCostCentreAsync(model);

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
