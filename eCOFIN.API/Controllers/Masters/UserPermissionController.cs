using eCOFIN.Application.DTOs.Masters;
using eCOFIN.Application.Interfaces.Masters;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Masters
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPermissionController : ControllerBase
    {
        private readonly IUserPermissionService _userPermissionService;

        public UserPermissionController(IUserPermissionService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var data = await _userPermissionService.GetAllUsersAsync();
                return Ok(new { success = true, status = 200, message = "Users retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetAllPermissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            try
            {
                var data = await _userPermissionService.GetAllPermissionsAsync();
                return Ok(new { success = true, status = 200, message = "Permissions retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetTasks")]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                var data = await _userPermissionService.GetTasksAsync();
                return Ok(new { success = true, status = 200, message = "Tasks retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetPanels/{taskId:int}")]
        public async Task<IActionResult> GetPanels(int taskId)
        {
            try
            {
                var data = await _userPermissionService.GetPanelsAsync(taskId);
                return Ok(new { success = true, status = 200, message = "Panels retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpGet("GetUserPermissions/{username}")]
        public async Task<IActionResult> GetUserPermissions(string username)
        {
            try
            {
                var data = await _userPermissionService.GetUserPermissionsAsync(username);
                return Ok(new { success = true, status = 200, message = "User permissions retrieved successfully.", data });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }

        [HttpPost("SaveOrUpdatePermissions")]
        public async Task<IActionResult> SaveOrUpdatePermissions([FromBody] SaveUserPermissionRequest model)
        {
            try
            {
                var result = await _userPermissionService.SaveOrUpdatePermissionsAsync(model);
                if (!result.Success)
                    return Ok(new { success = false, status = 201, message = result.Message });
                return Ok(new { success = true, status = 200, message = result.Message });
            }
            catch (ApplicationException appEx) { return StatusCode(500, new { success = false, status = 500, message = appEx.Message }); }
            catch (Exception ex) { return StatusCode(500, new { success = false, status = 500, message = "Unexpected error.", error = ex.InnerException?.Message ?? ex.Message }); }
        }
    }
}