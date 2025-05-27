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
		public async Task<IActionResult> Register([FromBody] UserDto dto)
		{
			var success = await _service.UserRegisterAsync(HttpContext, dto);
			return success ? Ok() : BadRequest("User already exists");
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserDto dto)
		{
			var success = await _service.UserLoginAsync(HttpContext, dto);
			return success ? Ok() : BadRequest("Wrong username or password");
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh()
		{
			var success = await _service.RefreshTokenAsync(HttpContext);
			return success ? Ok() : Unauthorized("Token is invalid or expired");
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
			return userId != null ? Ok(userId) : Unauthorized("User is not authorized.");
		}

    }
}
