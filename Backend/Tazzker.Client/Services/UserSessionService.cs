using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace Tazzker.Client.Services
{
    public class UserSessionService
    {
        private readonly IJSRuntime _js;

        private const string UserKey = "currentUserId";

        public UserSessionService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<string?> GetStoredUserIdAsync()
            => await _js.InvokeAsync<string>("localStorage.getItem", UserKey);

        public async Task SetStoredUserIdAsync(string userId)
            => await _js.InvokeVoidAsync("localStorage.setItem", UserKey, userId);

        public async Task ClearStoredUserIdAsync()
            => await _js.InvokeVoidAsync("localStorage.removeItem", UserKey);

        public string? ExtractUserIdFromAccessToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);

            var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }
}