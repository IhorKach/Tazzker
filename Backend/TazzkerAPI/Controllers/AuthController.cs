/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;
using Tazzker.Application.Services;
using Tazzker.Infrastructure.Repositories;

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
            var result = await _service.UserRegisterAsyncNew(dto);
            return result == null ? BadRequest("User Already exists!") : Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDto dto)
        {
            var result = await _service.UserLoginAsyncNew(dto);
            return result == null ? BadRequest("Wrong Login or Password!") : Ok(result);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshRequestDto dto)
        {
            var result = await _service.RefreshTokenAsync(dto.RefreshToken);
            return result == null ? Unauthorized() : Ok(result);
        }

    }
}*/
using Microsoft.AspNetCore.Mvc;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;

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
		public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
		{
			var success = await _service.UserRegisterAsync(HttpContext, dto);
			return success ? Ok() : BadRequest("User already exists");
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
		{
			var success = await _service.UserLoginAsync(HttpContext, dto);
			return success ? Ok() : BadRequest("Wrong username or password");
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh()
		{
			var success = await _service.RefreshTokenAsync(HttpContext);
			return success ? Ok() : Unauthorized();
		}

		[HttpPost("logout")]
		public IActionResult Logout()
		{
			Response.Cookies.Delete("access_token");
			Response.Cookies.Delete("refresh_token");
			return Ok();
		}

		[HttpGet("userid")]
		public IActionResult GetUserId()
		{
			var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
			return userId != null ? Ok(userId) : Unauthorized();
		}

    }
}
