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
    }
}
