using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                var data = await _customerService.GetAllCustomerAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No customers found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Customers retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetAllActiveCustomers")]
        public async Task<IActionResult> GetAllActiveCustomers()
        {
            try
            {
                var data = await _customerService.GetAllActiveCustomersAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No active customers found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Customers retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("SaveOrUpdateCustomer")]
        public async Task<IActionResult> SaveOrUpdateCustomer([FromBody] CustomerCreateModel model)
        {
            try
            {
                var result = await _customerService.SaveOrUpdateCustomerAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetImportableCustomers")]
        public async Task<IActionResult> GetImportableCustomers()
        {
            try
            {
                var data = await _customerService.GetImportableCustomersAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No importable customers found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Importable customers retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("ImportCustomer")]
        public async Task<IActionResult> ImportCustomer([FromBody] ImportCustomerModel model)
        {
            try
            {
                var result = await _customerService.ImportCustomerAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("ImportCustomers")]
        public async Task<IActionResult> ImportCustomers([FromBody] List<ImportCustomerModel> models)
        {
            try
            {
                var result = await _customerService.ImportCustomersAsync(models);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetDebtorAccounts")]
        public async Task<IActionResult> GetDebtorAccounts()
        {
            try
            {
                var data = await _customerService.GetDebtorAccountsAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No debtor accounts found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Accounts retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetLinkedAccounts")]
        public async Task<IActionResult> GetLinkedAccounts([FromQuery] string customerCode)
        {
            try
            {
                var data = await _customerService.GetLinkedAccountsAsync(customerCode);
                return Ok(new { success = true, status = 200, data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("SaveAccountLink")]
        public async Task<IActionResult> SaveAccountLink([FromBody] AccountLinkModel model)
        {
            try
            {
                var result = await _customerService.SaveAccountLinkAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }
    }
}