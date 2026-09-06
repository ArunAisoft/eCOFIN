using eCOFIN.Application.DTOs.Login;
using eCOFIN.Application.Interfaces.Login;
using Microsoft.AspNetCore.Mvc;

namespace eCOFIN.API.Controllers.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) => _userService = userService;

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginDto)
        {
            try
            {
                if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
                    return Ok(new { success = false, status = 201, message = "Username and Password are required." });

                var user = await _userService.ValidateUserAsync(loginDto.Username, loginDto.Password);
                if (user == null)
                    return Ok(new { success = false, status = 201, message = "Invalid Username or Password." });

                return Ok(new { success = true, status = 200, message = "Login successful.", data = user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, status = 500, message = "An error occurred while validating login.", error = ex });
            }
        }
    }

}
