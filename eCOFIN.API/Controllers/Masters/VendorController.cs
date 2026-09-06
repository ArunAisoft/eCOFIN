using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet("GetAllVendor")]
        public async Task<IActionResult> GetAllVendor()
        {
            try
            {
                var data = await _vendorService.GetAllVendorAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No vendors found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Vendors retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetAllActiveVendors")]
        public async Task<IActionResult> GetAllActiveVendors()
        {
            try
            {
                var data = await _vendorService.GetAllActiveVendorsAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No active vendors found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Vendors retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("SaveOrUpdateVendor")]
        public async Task<IActionResult> SaveOrUpdateVendor([FromBody] VendorCreateModel model)
        {
            try
            {
                var result = await _vendorService.SaveOrUpdateVendorAsync(model);
                if (!result.Success) return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetByVendor")]
        public async Task<IActionResult> GetByVendor([FromQuery] string vendorCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(vendorCode))
                    return Ok(new { success = false, status = 201, message = "vendorCode is required." });
                var data = await _vendorService.GetByVendorAsync(vendorCode.Trim());
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No account links found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Account links retrieved.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetVendorAccounts")]
        public async Task<IActionResult> GetVendorAccounts()
        {
            try
            {
                var data = await _vendorService.GetVendorAccountsAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No vendor accounts found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Credit accounts retrieved.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("SaveAccVendor")]
        public async Task<IActionResult> SaveAccVendor([FromBody] AccVendorCreateModel model)
        {
            try
            {
                var result = await _vendorService.SaveAccVendorAsync(model);
                if (!result.Success) return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetImportableSuppliers")]
        public async Task<IActionResult> GetImportableSuppliers()
        {
            try
            {
                var data = await _vendorService.GetImportableSuppliersAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No importable suppliers found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Importable suppliers retrieved.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetImportableVendors")]
        public async Task<IActionResult> GetImportableVendors()
        {
            try
            {
                var data = await _vendorService.GetImportableVendorsAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No importable vendors found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Importable vendors retrieved.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("ImportVendor")]
        public async Task<IActionResult> ImportVendor([FromBody] ImportVendorModel model)
        {
            try
            {
                var result = await _vendorService.ImportVendorAsync(model);
                if (!result.Success) return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("ImportVendors")]
        public async Task<IActionResult> ImportVendors([FromBody] List<ImportVendorModel> models)
        {
            try
            {
                var result = await _vendorService.ImportVendorsAsync(models);
                if (!result.Success) return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }
    }
}