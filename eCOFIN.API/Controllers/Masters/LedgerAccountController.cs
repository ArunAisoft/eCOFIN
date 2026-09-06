using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class LedgerAccountController : ControllerBase
    {
        private readonly ILedgerAccountService _service;

        public LedgerAccountController(ILedgerAccountService service)
        {
            _service = service;
        }

        [HttpGet("GetAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var data = await _service.GetAllAccountsAsync();
                return Ok(new { success = true, status = 200, message = "Accounts retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetAccountTypes")]
        public async Task<IActionResult> GetAccountTypes()
        {
            try
            {
                var data = await _service.GetAccountTypesAsync();
                return Ok(new { success = true, status = 200, message = "Account types retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetAccountNatures")]
        public async Task<IActionResult> GetAccountNatures()
        {
            try
            {
                var data = await _service.GetAccountNaturesAsync();
                return Ok(new { success = true, status = 200, message = "Account natures retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetBanks")]
        public async Task<IActionResult> GetBanks()
        {
            try
            {
                var data = await _service.GetBanksAsync();
                return Ok(new { success = true, status = 200, message = "Banks retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetEfcAccounts")]
        public async Task<IActionResult> GetEfcAccounts()
        {
            try
            {
                var data = await _service.GetEfcAccountsAsync();
                return Ok(new { success = true, status = 200, message = "EFC accounts retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("SaveOrUpdateAccount")]
        public async Task<IActionResult> SaveOrUpdateAccount([FromBody] SaveAccountRequest model)
        {
            try
            {
                var result = await _service.SaveOrUpdateAccountAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }
    }
}
