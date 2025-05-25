using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;
using Tazzker.Domain;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;


namespace Tazzker.Application.Services
{
    /* public class AuthService : IAuthService
	 {
		 private readonly IUserRepository _userRepository;
		 private readonly IConfiguration _config;

		 public AuthService(IUserRepository userRepository, IConfiguration config)
		 {
			 _userRepository = userRepository;
			 _config = config;
		 }

		 public async Task<string?> UserLoginAsync(UserLoginDto dto)
		 {
			 if (dto == null)
				 return null;

			 var user = await _userRepository.GetByUsernameAsync(dto.Username);


			 if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
				 return null;

			 return GenerateToken(user, TimeSpan.FromMinutes(15));
		 }

		 public async Task<string?> UserRegisterAsync(UserCreateDto dto)
		 {
			 if (dto == null) return null;

			 if (!(await _userRepository.GetByUsernameAsync(dto.Username) is null) || !(await _userRepository.GetByEmailAsync(dto.Email) is null)) return null;

			 var newUser = new User
			 {
				 Username = dto.Username,
				 Email = dto.Email,
				 PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
			 };

			 await _userRepository.AddAsync(newUser);

			 return GenerateToken(newUser, TimeSpan.FromMinutes(15));
		 }

		 //new method
		 public async Task<AuthResponse?> UserRegisterAsyncNew(UserCreateDto dto)
		 {
			 if (dto == null) return null;

			 if (!(await _userRepository.GetByUsernameAsync(dto.Username) is null) || !(await _userRepository.GetByEmailAsync(dto.Email) is null)) return null;

			 var newUser = new User
			 {
				 Username = dto.Username,
				 Email = dto.Email,
				 PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
			 };

			 var newAccessToken = GenerateToken(newUser, TimeSpan.FromMinutes(15));
			 var newRefreshToken = GenerateToken(newUser, TimeSpan.FromDays(7));

			 newUser.RefreshToken = newRefreshToken;

			 await _userRepository.AddAsync(newUser);





			 return new AuthResponse
			 {
				 AccessToken = newAccessToken,
				 RefreshToken = newRefreshToken
			 };
		 }

		 //new method 
		 public async Task<AuthResponse?> UserLoginAsyncNew(UserLoginDto dto)
		 {
			 if (dto == null)
				 return null;

			 var user = await _userRepository.GetByUsernameAsync(dto.Username);


			 if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
				 return null;


			 var newAccessToken = GenerateToken(user, TimeSpan.FromMinutes(15));
			 var newRefreshToken = GenerateToken(user, TimeSpan.FromDays(7));

			 user.RefreshToken = newRefreshToken;

			 await _userRepository.UpdateAsync(user);

			 return new AuthResponse
			 {
				 AccessToken = newAccessToken,
				 RefreshToken = newRefreshToken
			 };

		 }



		 public async Task<AuthResponse?> RefreshTokenAsync(string clientRefreshToken)
		 {
			 var handler = new JwtSecurityTokenHandler();
			 var token = handler.ReadJwtToken(clientRefreshToken);

			 var userId = token.Claims.FirstOrDefault(c=> c.Type == ClaimTypes.NameIdentifier)?.Value;
			 if (userId == null) 
				 return null;

			 var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
			 if (user == null || user.RefreshToken != clientRefreshToken) 
				 return null;

			 if(token.ValidTo < DateTime.UtcNow)
				 return null;

			 var newAccessToken = GenerateToken(user, TimeSpan.FromMinutes(15));
			 var newRefreshToken = GenerateToken(user, TimeSpan.FromDays(7));
			 user.RefreshToken = newRefreshToken;
			 await _userRepository.UpdateAsync(user);

			 return new AuthResponse
			 {
				 AccessToken = newAccessToken,
				 RefreshToken = newRefreshToken
			 };
		 }


		 private string GenerateToken(User user, TimeSpan validFor)
		 {
			 var claims = new[]
			 {
				 new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
				 new Claim(ClaimTypes.Name, user.Username)
			 };

			 var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

			 var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			 var token = new JwtSecurityToken(
				 issuer: _config["Jwt:Issuer"],
				 audience: _config["Jwt:Audience"],
				 claims: claims,
				 expires: DateTime.UtcNow.Add(validFor),
				 signingCredentials: creds
				 );

			 return new JwtSecurityTokenHandler().WriteToken(token);
		 }
	 }*/

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        private readonly IConfiguration _config;


        public AuthService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }


        /*      public AuthService(IUserRepository userRepository, IConfiguration config)
              {
                  _userRepository = userRepository;
                  _config = config;
              }*/

        public async Task<bool> UserLoginAsync(HttpContext context, UserLoginDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return false;


            var accessToken = GenerateToken(user, TimeSpan.FromMinutes(15));
            var refreshToken = GenerateToken(user, TimeSpan.FromDays(7));

            user.RefreshToken = refreshToken;
            await _userRepository.UpdateAsync(user);

            SetCookies(context, accessToken, refreshToken);

            return true;
        }

        /*        public async Task<bool> UserRegisterAsync(HttpContext context, UserCreateDto dto)
                {
                    if (await _userRepository.GetByUsernameAsync(dto.Username) != null ||
                        await _userRepository.GetByEmailAsync(dto.Email) != null)
                        return false;

                    var user = new User
                    {
                        Username = dto.Username,
                        Email = dto.Email,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                    };

                    var accessToken = GenerateToken(user, TimeSpan.FromMinutes(15));
                    var refreshToken = GenerateToken(user, TimeSpan.FromDays(7));
                    user.RefreshToken = refreshToken;

                    await _userRepository.AddAsync(user);

                    SetCookies(context, accessToken, refreshToken);
                    return true;
                }*/


        public async Task<bool> UserRegisterAsync(HttpContext context, UserCreateDto dto)
        {
            if (await _userRepository.GetByUsernameAsync(dto.Username) != null /*|| await _userRepository.GetByEmailAsync(dto.Email) != null*/)
                return false;

            var user = new User
            {
                Username = dto.Username,
                //Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            var accessToken = GenerateToken(user, TimeSpan.FromMinutes(15));
            var refreshToken = GenerateToken(user, TimeSpan.FromDays(7));
            user.RefreshToken = refreshToken;

            await _userRepository.AddAsync(user);

            SetCookies(context, accessToken, refreshToken);
            return true;
        }



        public async Task<bool> RefreshTokenAsync(HttpContext context)
        {
            var refreshToken = context.Request.Cookies["refresh_token"];
            if (refreshToken == null) return false;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(refreshToken);

            var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return false;

            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
            if (user == null || user.RefreshToken != refreshToken) return false;
            if (token.ValidTo < DateTime.UtcNow) return false;

            var newAccessToken = GenerateToken(user, TimeSpan.FromMinutes(15));
            var newRefreshToken = GenerateToken(user, TimeSpan.FromDays(7));
            user.RefreshToken = newRefreshToken;

            await _userRepository.UpdateAsync(user);
            SetCookies(context, newAccessToken, newRefreshToken);
            return true;
        }

        private void SetCookies(HttpContext context, string accessToken, string refreshToken)
        {
            context.Response.Cookies.Append("access_token", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            context.Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

        private string GenerateToken(User user, TimeSpan validFor)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.Add(validFor),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }


}
