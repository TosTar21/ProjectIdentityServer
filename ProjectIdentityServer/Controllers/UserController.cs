using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIdentityServer.DTOs;
using Services;


namespace ProjectIdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISvUser _userService;

        public UserController(ISvUser userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);  
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] DTORegisterUser request)
        {
            var result = await _userService.RegisterUserAsync(request.UserName, request.Password, request.Role);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Usuario registrado exitosamente con el rol asignado.");
        }
    }
}