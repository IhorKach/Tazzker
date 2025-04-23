using Microsoft.JSInterop;

namespace Tazzker.Client.Services
{
    public class TokenService
    {
        private readonly IJSRuntime _js;
        private const string Key = "authToken";

        public TokenService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<string?> GetTokenAsync()
            => await _js.InvokeAsync<string>("localStorage.getItem", Key);

        public async Task SetTokenAsync(string token)
            => await _js.InvokeVoidAsync("localStorage.setItem", Key, token);

        public async Task ClearTokenAsync()
            => await _js.InvokeVoidAsync("localStorage.removeItem", Key);

        public async Task<bool> AttachTokenToHttpClientAsync(HttpClient http)
        {
            var token = await GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                return true;
            }
            return false;
        }
    }

}
