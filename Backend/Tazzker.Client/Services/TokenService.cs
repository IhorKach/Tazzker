/*
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Tazzker.Client.Data.Models;
namespace Tazzker.Client.Services
{
    public class TokenService
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _http;
        private const string AccessKey = "authToken";
        private const string RefreshKey = "refreshToken";

        private readonly NavigationManager _navigation;

        public TokenService(IJSRuntime js, HttpClient http, NavigationManager navigation)
        {
            _js = js;
            _http = http;
            _navigation = navigation;
        }

        public async Task<string?> GetAccessTokenAsync()
            => await _js.InvokeAsync<string>("localStorage.getItem", AccessKey);

        public async Task<string?> GetRefreshTokenAsync()
            => await _js.InvokeAsync<string>("localStorage.getItem", RefreshKey);

        public async Task SetTokensAsync(string accessToken, string refreshToken)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", AccessKey, accessToken);
            await _js.InvokeVoidAsync("localStorage.setItem", RefreshKey, refreshToken);

            // Автоматически обновляем токен в http заголовках
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task ClearTokensAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", AccessKey);
            await _js.InvokeVoidAsync("localStorage.removeItem", RefreshKey);

            _http.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> AttachTokenToHttpClientAsync()
        {
            var token = await GetAccessTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return true;
            }
            return false;
        }

        public async Task<bool> TryRefreshTokensAsync()
        {
            var refreshToken = await GetRefreshTokenAsync();
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                await ForceLogoutAsync();
                return false;
            }

            var response = await _http.PostAsJsonAsync("/api/auth/refresh", new { RefreshToken = refreshToken });
            if (!response.IsSuccessStatusCode)
            {
                await ForceLogoutAsync();
                return false;
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (authResponse == null)
            {
                await ForceLogoutAsync();
                return false;
            }

            await SetTokensAsync(authResponse.AccessToken, authResponse.RefreshToken);
            return true;
        }


        public async Task ForceLogoutAsync()
        {
            await ClearTokensAsync();
            _navigation.NavigateTo("/login");
        }
    }
}
*/