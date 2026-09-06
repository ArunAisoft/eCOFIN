using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("GetCompany")]
        public async Task<IActionResult> GetCompany()
        {
            try
            {
                var data = await _companyService.GetCompanyAsync();
                if (data == null)
                    return Ok(new { success = false, status = 201, message = "No company record found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Company retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("SaveOrUpdateCompany")]
        public async Task<IActionResult> SaveOrUpdateCompany([FromBody] CompanyCreateModel model)
        {
            try
            {
                var result = await _companyService.SaveOrUpdateCompanyAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }
    }
}
