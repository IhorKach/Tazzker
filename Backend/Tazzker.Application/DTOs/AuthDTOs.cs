using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tazzker.Application.DTOs
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }

    public class RefreshRequestDto
    {
        public string RefreshToken { get; set; } = null!;
    }

}
