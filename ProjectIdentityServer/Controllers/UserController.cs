using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIdentityServer.DTOs;
using Services.User;

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

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var result = await _userService.CreateUserAsync(request.UserName, request.Email, request.Password, request.RoleId);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Usuario creado exitosamente");
        }

    }
}