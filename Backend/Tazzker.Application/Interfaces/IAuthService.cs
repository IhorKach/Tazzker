using Microsoft.AspNetCore.Http;
using Tazzker.Application.DTOs;

namespace Tazzker.Application.Interfaces
{
	public interface IAuthService
	{
        Task<bool> UserLoginAsync(HttpContext httpContext, UserDto dto);
		Task<bool> UserRegisterAsync(HttpContext httpContext, UserDto dto);
		Task<bool> RefreshTokenAsync(HttpContext httpContext);
	}
}
