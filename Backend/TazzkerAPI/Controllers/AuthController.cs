using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;
using Tazzker.Application.Services;

namespace TazzkerAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegistration([FromBody] UserCreateDto dto)
        {
            var result = await _service.UserRegisterAsync(dto);
            return result == null ? BadRequest("User Already exists!") : Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDto dto)
        {
            var result = await _service.UserLoginAsync(dto);
            return result == null ? BadRequest("Wrong Login or Password!") : Ok(result);
        }
    }
}