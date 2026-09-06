using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var data = await _employeeService.GetAllEmployeeAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No employees found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Employees retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetAllActiveEmployees")]
        public async Task<IActionResult> GetAllActiveEmployees()
        {
            try
            {
                var data = await _employeeService.GetAllActiveEmployeesAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No active employees found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Employees retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("SaveOrUpdateEmployee")]
        public async Task<IActionResult> SaveOrUpdateEmployee([FromBody] EmployeeCreateModel model)
        {
            try
            {
                var result = await _employeeService.SaveOrUpdateEmployeeAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetByEmployee")]
        public async Task<IActionResult> GetByEmployee([FromQuery] string employeeCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(employeeCode))
                    return Ok(new { success = false, status = 201, message = "employeeCode is required." });
                var data = await _employeeService.GetByEmployeeAsync(employeeCode.Trim());
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No account links found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Account links retrieved.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetEmployeeAccounts")]
        public async Task<IActionResult> GetEmployeeAccounts()
        {
            try
            {
                var data = await _employeeService.GetEmployeeAccountsAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No accounts found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Accounts retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("SaveAccEmployee")]
        public async Task<IActionResult> SaveAccEmployee([FromBody] AccEmployeeCreateModel model)
        {
            try
            {
                var result = await _employeeService.SaveAccEmployeeAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetPendingPersonnel")]
        public async Task<IActionResult> GetPendingPersonnel()
        {
            try
            {
                var data = await _employeeService.GetPendingPersonnelAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No pending personnel found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Pending personnel retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetEmpAccounts")]
        public async Task<IActionResult> GetEmpAccounts()
        {
            try
            {
                var data = await _employeeService.GetEmpAccountsAsync();
                if (data == null || !data.Any())
                    return Ok(new { success = false, status = 201, message = "No accounts found.", data = (object?)null });
                return Ok(new { success = true, status = 200, message = "Employee Accounts retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("ImportEmployee")]
        public async Task<IActionResult> ImportEmployee([FromBody] ImportEmployeeModel model)
        {
            try
            {
                var result = await _employeeService.ImportEmployeeAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("ImportEmployees")]
        public async Task<IActionResult> ImportEmployees([FromBody] List<ImportEmployeeModel> models)
        {
            try
            {
                var result = await _employeeService.ImportEmployeesAsync(models);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }
    }
}