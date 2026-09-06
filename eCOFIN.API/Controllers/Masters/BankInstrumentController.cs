using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using eCOFIN.Application.Interfaces.Vouchers;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankInstrumentController : ControllerBase
    {
        private readonly IBankInstrumentService _svc;
        private readonly IBanksAndAccountsService _bas;

        public BankInstrumentController(IBankInstrumentService svc, IBanksAndAccountsService bas)
        {
            _svc = svc;
            _bas = bas;
        }

        [HttpGet("GetAllBanksWithAccounts")]
        public async Task<IActionResult> GetAllBanksWithAccounts()
        {
            try
            {
                var data = await _bas.GetAllBanksWithAccountsAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No banks found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Banks retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetAllInstruments")]
        public async Task<IActionResult> GetAllInstruments()
        {
            try
            {
                var data = await _svc.GetAllInstrumentsAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No instrument books found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Instrument books retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("SaveOrUpdateInstrument")]
        public async Task<IActionResult> SaveOrUpdateInstrument([FromBody] BankInstrumentCreateModel model)
        {
            try
            {
                var result = await _svc.SaveOrUpdateInstrumentAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }
    }
}
