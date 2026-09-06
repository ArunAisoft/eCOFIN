using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class DefinePrefixController : ControllerBase
    {
        private readonly IDefinePrefixService _service;

        public DefinePrefixController(IDefinePrefixService service)
        {
            _service = service;
        }

        /// <summary>
        /// Paged grid data.
        /// GET api/DefinePrefix/GetLinks/P/D?search=8100&amp;page=1&amp;pageSize=50
        /// vchrType: P = Purchase, I = Sales
        /// prefixType: Purchase D/I, Sales D/E
        /// </summary>
        [HttpGet("GetLinks/{vchrType}/{prefixType}")]
        public async Task<IActionResult> GetLinks(
            string vchrType,
            string prefixType,
            [FromQuery] string? search = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50)
        {
            try
            {
                var data = await _service.GetLinksAsync(vchrType, prefixType, search, page, pageSize);
                return Ok(new
                {
                    success = true,
                    status = 200,
                    message = "Prefix links retrieved successfully.",
                    data = data.Items,
                    page = data.Page,
                    pageSize = data.PageSize,
                    totalCount = data.TotalCount,
                    totalPages = data.TotalPages
                });
            }
            catch (ApplicationException appEx) { return Problem500(appEx.Message); }
            catch (Exception ex) { return Problem500("Unexpected error.", ex); }
        }

        /// <summary>
        /// Permission, help metadata and lookups for the Add button.
        /// Replaces roughly twelve legacy round trips with one.
        /// Not cached - it is per-user and permission-bearing.
        /// </summary>
        [HttpGet("GetAddContext")]
        public async Task<IActionResult> GetAddContext([FromQuery] string username)
        {
            try
            {
                var data = await _service.GetAddContextAsync(username);
                return Ok(new { success = true, status = 200, message = "Add context retrieved.", data });
            }
            catch (ApplicationException appEx) { return Problem500(appEx.Message); }
            catch (Exception ex) { return Problem500("Unexpected error.", ex); }
        }

        /// <summary>Accounts and customers in one round trip. Cached server-side.</summary>
        [HttpGet("GetLookups")]
        public async Task<IActionResult> GetLookups()
        {
            try
            {
                var data = await _service.GetLookupsAsync();
                return Ok(new { success = true, status = 200, message = "Lookups retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return Problem500(appEx.Message); }
            catch (Exception ex) { return Problem500("Unexpected error.", ex); }
        }

        [HttpGet("GetAccounts")]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                var data = await _service.GetAccountsAsync();
                return Ok(new { success = true, status = 200, message = "Accounts retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return Problem500(appEx.Message); }
            catch (Exception ex) { return Problem500("Unexpected error.", ex); }
        }

        [HttpGet("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var data = await _service.GetCustomersAsync();
                return Ok(new { success = true, status = 200, message = "Customers retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return Problem500(appEx.Message); }
            catch (Exception ex) { return Problem500("Unexpected error.", ex); }
        }

        /// <summary>ctrlStatus: "Post" (default) or "ONHOLD".</summary>
        [HttpPost("SaveLinks")]
        public async Task<IActionResult> SaveLinks(
            [FromBody] SavePrefixLinksRequest model,
            [FromQuery] string ctrlStatus = "Post")
        {
            try
            {
                var result = await _service.SaveLinksAsync(model, ctrlStatus);
                return Ok(new
                {
                    success = result.Success,
                    status = result.Success ? 200 : 201,
                    message = result.Message
                });
            }
            catch (ApplicationException appEx) { return Problem500(appEx.Message); }
            catch (Exception ex) { return Problem500("Unexpected error.", ex); }
        }

        [HttpPost("DeleteLink")]
        public async Task<IActionResult> DeleteLink([FromBody] DeletePrefixLinkRequest model)
        {
            try
            {
                var result = await _service.DeleteLinkAsync(model);
                return Ok(new
                {
                    success = result.Success,
                    status = result.Success ? 200 : 201,
                    message = result.Message
                });
            }
            catch (ApplicationException appEx) { return Problem500(appEx.Message); }
            catch (Exception ex) { return Problem500("Unexpected error.", ex); }
        }

        private IActionResult Problem500(string message, Exception? ex = null) =>
            StatusCode(500, new
            {
                success = false,
                status = 500,
                message,
                error = ex?.InnerException?.Message ?? ex?.Message
            });
    }
}