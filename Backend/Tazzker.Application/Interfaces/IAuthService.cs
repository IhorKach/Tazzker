using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Application.DTOs;

namespace Tazzker.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string?> UserRegisterAsync(UserCreateDto dto);
        Task<string?> UserLoginAsync(UserLoginDto dto);
        Task<AuthResponse?> UserRegisterAsyncNew(UserCreateDto dto);
        Task<AuthResponse?> UserLoginAsyncNew(UserLoginDto dto);
        Task<AuthResponse?> RefreshTokenAsync(string clientRefreshToken);

    }
}
