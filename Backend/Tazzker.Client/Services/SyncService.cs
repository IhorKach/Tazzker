/*using System.Net.Http.Json;
using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;

namespace Tazzker.Client.Services
{
    public class SyncService
    {
        private readonly HttpClient _http;
        private readonly IndexedDbBridge _db;

        private readonly TokenService _tokenService;

        public SyncService(HttpClient http, IndexedDbBridge db, TokenService tokenService)
        {
            _http = http;
            _db = db;
            _tokenService = tokenService;
        }


        public async Task SyncAllAsync()
        {
            await SyncEntity<ListModel>("Lists", "/api/lists/syncLists");
            await SyncEntity<SublistModel>("Sublists", "/api/sublists/syncSublists");
            await SyncEntity<TaskModel>("Tasks", "/api/tasks/syncTasks");
            await SyncEntity<NoteModel>("Notes", "/api/notes/syncNotes");
        }

        public async Task PullAllAsync()
        {
            await PullEntity<ListModel>("Lists", "/api/lists/getLists");
            await PullEntity<SublistModel>("Sublists", "/api/sublists/getSublists");
            await PullEntity<TaskModel>("Tasks", "/api/tasks/getTasks");
            await PullEntity<NoteModel>("Notes", "/api/notes/getNotes");
        }



        private async Task SyncEntity<T>(string store, string endpoint) where T : class, ISyncable
        {
            var items = await _db.GetAllAsync<T>(store);
            var unsynced = items.Where(x => !x.IsSynced && !x.IsDeleted).ToList();

            if (unsynced.Count == 0) return;

            var token = await _tokenService.GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.PostAsJsonAsync(endpoint, unsynced);

            if (response.IsSuccessStatusCode)
            {
                foreach (var item in unsynced)
                {
                    item.IsSynced = true;
                    await _db.AddAsync(store, item);
                }
            }
        }


        public async Task PullEntity<T>(string store, string endpoint) where T : class, ISyncable
        {
            var token = await _tokenService.GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode) return;

            var items = await response.Content.ReadFromJsonAsync<List<T>>();

            if (items != null)
            {
                await _db.ClearAsync(store);

                foreach (var item in items)
                {
                    item.IsSynced = true;
                    await _db.AddAsync(store, item);
                }
            }
        }

    }

}
*/


//before token refresh

/*using System.Net.Http.Json;
using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;
namespace Tazzker.Client.Services
{
    public class SyncService
    {
        private readonly HttpClient _http;
        private readonly IndexedDbBridge _db;
        private readonly TokenService _tokenService;

        public SyncService(HttpClient http, IndexedDbBridge db, TokenService tokenService)
        {
            _http = http;
            _db = db;
            _tokenService = tokenService;
        }

        public async Task SyncAllAsync()
        {
            await SyncEntity<ListModel>("Lists", "/api/lists/syncLists");
            await SyncEntity<SublistModel>("Sublists", "/api/sublists/syncSublists");
            await SyncEntity<TaskModel>("Tasks", "/api/tasks/syncTasks");
            await SyncEntity<NoteModel>("Notes", "/api/notes/syncNotes");
        }

        public async Task PullAllAsync()
        {
            await PullEntity<ListModel>("Lists", "/api/lists/getLists");
            await PullEntity<SublistModel>("Sublists", "/api/sublists/getSublists");
            await PullEntity<TaskModel>("Tasks", "/api/tasks/getTasks");
            await PullEntity<NoteModel>("Notes", "/api/notes/getNotes");
        }

        public async Task SyncHardDeletedAsync()
        {
            await SyncPermDeleted<ListModel>("Lists", "/api/lists/clearTrashedLists");
            await SyncPermDeleted<SublistModel>("Sublists", "/api/sublists/clearTrashedSublists");
            await SyncPermDeleted<TaskModel>("Tasks", "/api/tasks/clearTrashedTasks");
            await SyncPermDeleted<NoteModel>("Notes", "/api/notes/clearTrashedNotes");
        }

        private async Task SyncEntity<T>(string store, string endpoint) where T : class, ISyncable
        {
            var items = await _db.GetAllAsync<T>(store);
            var unsynced = items.Where(x => !x.IsSynced && !x.IsDeleted).ToList();
            if (unsynced.Count == 0) return;

            if (!await _tokenService.AttachTokenToHttpClientAsync(_http)) return;

            var response = await _http.PostAsJsonAsync(endpoint, unsynced);
            if (response.IsSuccessStatusCode)
            {
                foreach (var item in unsynced)
                {
                    item.IsSynced = true;
                    await _db.AddAsync(store, item);
                }
            }
        }

        private async Task PullEntity<T>(string store, string endpoint) where T : class, ISyncable
        {
            if (!await _tokenService.AttachTokenToHttpClientAsync(_http)) return;

            var response = await _http.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode) return;

            var items = await response.Content.ReadFromJsonAsync<List<T>>();
            if (items != null)
            {
                await _db.ClearAsync(store);
                foreach (var item in items)
                {
                    item.IsSynced = true;
                    await _db.AddAsync(store, item);
                }
            }
        }

        private async Task SyncPermDeleted<T>(string store, string endpoint) where T : class, ISyncable
        {
            var items = await _db.GetAllAsync<T>(store);
            var toDelete = items.Where(x => x.IsDeleted && x.PermDeleted).Select(x => x.Id.ToString()).ToList();
            if (toDelete.Count == 0) return;

            if (!await _tokenService.AttachTokenToHttpClientAsync(_http)) return;

            var response = await _http.PostAsJsonAsync(endpoint, toDelete);
            if (response.IsSuccessStatusCode)
            {
                foreach (var id in toDelete)
                {
                    await _db.DeleteAsync(store, id);
                }
            }
        }
    }
}
*/


//token refresh applied



/*using System.Net.Http.Json;
using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;

namespace Tazzker.Client.Services
{
    public class SyncService
    {
        private readonly HttpClient _http;
        private readonly IndexedDbBridge _db;
        private readonly TokenService _tokenService;

        public SyncService(HttpClient http, IndexedDbBridge db, TokenService tokenService)
        {
            _http = http;
            _db = db;
            _tokenService = tokenService;
        }

        public async Task SyncAllAsync()
        {
            await SyncEntity<ListModel>("Lists", "/api/lists/syncLists");
            await SyncEntity<SublistModel>("Sublists", "/api/sublists/syncSublists");
            await SyncEntity<TaskModel>("Tasks", "/api/tasks/syncTasks");
            await SyncEntity<NoteModel>("Notes", "/api/notes/syncNotes");
        }

        public async Task PullAllAsync()
        {
            await PullEntity<ListModel>("Lists", "/api/lists/getLists");
            await PullEntity<SublistModel>("Sublists", "/api/sublists/getSublists");
            await PullEntity<TaskModel>("Tasks", "/api/tasks/getTasks");
            await PullEntity<NoteModel>("Notes", "/api/notes/getNotes");
        }

        public async Task SyncHardDeletedAsync()
        {
            await SyncPermDeleted<ListModel>("Lists", "/api/lists/clearTrashedLists");
            await SyncPermDeleted<SublistModel>("Sublists", "/api/sublists/clearTrashedSublists");
            await SyncPermDeleted<TaskModel>("Tasks", "/api/tasks/clearTrashedTasks");
            await SyncPermDeleted<NoteModel>("Notes", "/api/notes/clearTrashedNotes");
        }

        private async Task SyncEntity<T>(string store, string endpoint) where T : class, ISyncable
        {
            var items = await _db.GetAllAsync<T>(store);
            var unsynced = items.Where(x => !x.IsSynced && !x.IsDeleted).ToList();
            if (unsynced.Count == 0) return;

            if (!await _tokenService.AttachTokenToHttpClientAsync()) return;

            var response = await _http.PostAsJsonAsync(endpoint, unsynced);
            response = await HandleUnauthorizedAsync(() => _http.PostAsJsonAsync(endpoint, unsynced), response);
            if (response == null) return;

            if (response.IsSuccessStatusCode)
            {
                foreach (var item in unsynced)
                {
                    item.IsSynced = true;
                    await _db.AddAsync(store, item);
                }
            }
        }

        private async Task PullEntity<T>(string store, string endpoint) where T : class, ISyncable
        {
            if (!await _tokenService.AttachTokenToHttpClientAsync()) return;

            var response = await _http.GetAsync(endpoint);
            response = await HandleUnauthorizedAsync(() => _http.GetAsync(endpoint), response);
            if (response == null) return;

            if (response.IsSuccessStatusCode)
            {
                var items = await response.Content.ReadFromJsonAsync<List<T>>();
                if (items != null)
                {
                    await _db.ClearAsync(store);
                    foreach (var item in items)
                    {
                        item.IsSynced = true;
                        await _db.AddAsync(store, item);
                    }
                }
            }
        }

        private async Task SyncPermDeleted<T>(string store, string endpoint) where T : class, ISyncable
        {
            var items = await _db.GetAllAsync<T>(store);
            var toDelete = items.Where(x => x.IsDeleted && x.PermDeleted).Select(x => x.Id.ToString()).ToList();
            if (toDelete.Count == 0) return;

            if (!await _tokenService.AttachTokenToHttpClientAsync()) return;

            var response = await _http.PostAsJsonAsync(endpoint, toDelete);
            response = await HandleUnauthorizedAsync(() => _http.PostAsJsonAsync(endpoint, toDelete), response);
            if (response == null) return;

            if (response.IsSuccessStatusCode)
            {
                foreach (var id in toDelete)
                {
                    await _db.DeleteAsync(store, id);
                }
            }
        }

        private async Task<HttpResponseMessage?> HandleUnauthorizedAsync(Func<Task<HttpResponseMessage>> retryRequest, HttpResponseMessage response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
                return response;

            var refreshSuccess = await _tokenService.TryRefreshTokensAsync();
            if (!refreshSuccess)
                return null;

            return await retryRequest();
        }

        public async Task ClearAllAsync()
        {
            await _db.ClearAsync("Lists");
            await _db.ClearAsync("Sublists");
            await _db.ClearAsync("Tasks");
            await _db.ClearAsync("Notes");
        }

    }
}

*/



//cookies only
//last stable version of entire class
/*
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;
using Microsoft.JSInterop;

namespace Tazzker.Client.Services
{
    public class SyncService
    {
        private readonly HttpClient _http;
        private readonly IndexedDbBridge _db;
        private readonly NavigationManager _nav;
        private readonly IJSRuntime JS;

        public SyncService(HttpClient http, IndexedDbBridge db, NavigationManager nav, IJSRuntime js)
        {
            _http = http;
            _db = db;
            _nav = nav;
            JS = js;
        }

        // в SyncStatusService.cs
        public event Func<Task>? OnSynced;

        public async Task NotifySynced()
        {
            if (OnSynced != null) await OnSynced.Invoke();
        }


		public async Task TrySyncEverythingAsync()
		{
			await ExecuteWithRefresh(async () =>
			{
				await SyncAllAsync();
				await SyncHardDeletedAsync();
				await PullAllAsync();
			});
		}


		private async Task ExecuteWithRefresh(Func<Task> operation)
		{
			try
			{
				await operation();
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine($"❌ Ошибка запроса: {ex.Message}");
			}
			catch (UnauthorizedAccessException)
			{
				Console.WriteLine("🔒 Неавторизован. Пробуем обновить токен...");

				bool refreshed = await TryRefreshToken();
				if (refreshed)
				{
					Console.WriteLine("✅ Токен обновлён. Повторяем операцию...");
					await operation();
				}
				else
				{
					Console.WriteLine("⛔ Не удалось обновить токен. Перенаправление на /auth");
					await JS.InvokeVoidAsync("localStorage.setItem", "isLoggedIn", "false");
					_nav.NavigateTo("/auth", forceLoad: true);
				}
			}
		}

		private async Task<bool> TryRefreshToken()
		{
			try
			{
				var refreshRequest = new HttpRequestMessage(HttpMethod.Post, "/api/auth/refresh");
				refreshRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

				var response = await _http.SendAsync(refreshRequest);
				return response.IsSuccessStatusCode;
			}
			catch
			{
				return false;
			}
		}




		public async Task SyncAllAsync()
        {
            await SyncEntity<ListModel>("Lists", "/api/lists/syncLists");
            await SyncEntity<SublistModel>("Sublists", "/api/sublists/syncSublists");
            await SyncEntity<TaskModel>("Tasks", "/api/tasks/syncTasks");
            await SyncEntity<NoteModel>("Notes", "/api/notes/syncNotes");
            Console.WriteLine("Call sync");
            await NotifySynced();
        }

        public async Task PullAllAsync()
        {
            await PullEntity<ListModel>("Lists", "/api/lists/getLists");
            await PullEntity<SublistModel>("Sublists", "/api/sublists/getSublists");
            await PullEntity<TaskModel>("Tasks", "/api/tasks/getTasks");
            await PullEntity<NoteModel>("Notes", "/api/notes/getNotes");
            Console.WriteLine("Call pull");
			await NotifySynced();
		}

        public async Task SyncHardDeletedAsync()
        {
            await SyncPermDeleted<ListModel>("Lists", "/api/lists/clearTrashedLists");
            await SyncPermDeleted<SublistModel>("Sublists", "/api/sublists/clearTrashedSublists");
            await SyncPermDeleted<TaskModel>("Tasks", "/api/tasks/clearTrashedTasks");
            await SyncPermDeleted<NoteModel>("Notes", "/api/notes/clearTrashedNotes");
            Console.WriteLine("Call delete");
			await NotifySynced();
		}

        private async Task SyncEntity<T>(string store, string endpoint) where T : class, ISyncable
        {
            var items = await _db.GetAllAsync<T>(store);
            var unsynced = items.Where(x => !x.IsSynced && !x.IsDeleted).ToList();
            if (unsynced.Count == 0) return;

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = JsonContent.Create(unsynced)
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _http.SendAsync(request);
            response = await HandleUnauthorizedAsync(() => _http.SendAsync(request), response);
            if (response == null || !response.IsSuccessStatusCode) return;

            foreach (var item in unsynced)
            {
                item.IsSynced = true;
                await _db.AddAsync(store, item);
            }
        }

        private async Task PullEntity<T>(string store, string endpoint) where T : class, ISyncable
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _http.SendAsync(request);
            response = await HandleUnauthorizedAsync(() => _http.SendAsync(request), response);
            if (response == null || !response.IsSuccessStatusCode) return;

            var items = await response.Content.ReadFromJsonAsync<List<T>>();
            if (items == null) return;

            await _db.ClearAsync(store);
            foreach (var item in items)
            {
                item.IsSynced = true;
                await _db.AddAsync(store, item);
            }
        }

        private async Task SyncPermDeleted<T>(string store, string endpoint) where T : class, ISyncable
        {
            var items = await _db.GetAllAsync<T>(store);
            var toDelete = items.Where(x => x.IsDeleted && x.PermDeleted).Select(x => x.Id.ToString()).ToList();
            if (toDelete.Count == 0) return;

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = JsonContent.Create(toDelete)
            };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _http.SendAsync(request);
            response = await HandleUnauthorizedAsync(() => _http.SendAsync(request), response);
            if (response == null || !response.IsSuccessStatusCode) return;

            foreach (var id in toDelete)
            {
                await _db.DeleteAsync(store, id);
            }
        }

        private async Task<HttpResponseMessage?> HandleUnauthorizedAsync(Func<Task<HttpResponseMessage>> retryRequest, HttpResponseMessage response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
                return response;

            // Пробуем refresh
            var refreshRequest = new HttpRequestMessage(HttpMethod.Post, "/api/auth/refresh");
            refreshRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var refreshResponse = await _http.SendAsync(refreshRequest);

            if (!refreshResponse.IsSuccessStatusCode)
            {
                // Refresh не сработал — логаут
                await JS.InvokeVoidAsync("localStorage.setItem", "isLoggedIn", "false");
                await JS.InvokeVoidAsync("localStorage.removeItem", "currentUserId");

                var path = _nav.ToBaseRelativePath(_nav.Uri).ToLowerInvariant();
                if (!path.StartsWith("auth") && !path.StartsWith("login"))
                {
                    _nav.NavigateTo("/auth", forceLoad: true);
                }

                return null;
            }

            // Повторяем запрос после успешного refresh
            var retry = await retryRequest();

            // Если он всё ещё неуспешный — снова выходим
            if (!retry.IsSuccessStatusCode)
            {
                await JS.InvokeVoidAsync("localStorage.setItem", "isLoggedIn", "false");
                await JS.InvokeVoidAsync("localStorage.removeItem", "currentUserId");

                var path = _nav.ToBaseRelativePath(_nav.Uri).ToLowerInvariant();
                if (!path.StartsWith("auth") && !path.StartsWith("login"))
                {
                    _nav.NavigateTo("/auth", forceLoad: true);
                }

                return null;
            }

            return retry;
        }
        public async Task ClearAllAsync()
        {
            await _db.ClearAsync("Lists");
            await _db.ClearAsync("Sublists");
            await _db.ClearAsync("Tasks");
            await _db.ClearAsync("Notes");
        }
        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/auth/userid");
                request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
                var response = await _http.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
*/





















//last stable verion
/*        private async Task<HttpResponseMessage?> HandleUnauthorizedAsync(Func<Task<HttpResponseMessage>> retryRequest, HttpResponseMessage response)
		{
			if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
				return response;

			var refreshRequest = new HttpRequestMessage(HttpMethod.Post, "/api/auth/refresh");
			refreshRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

			var refreshResponse = await _http.SendAsync(refreshRequest);

			if (!refreshResponse.IsSuccessStatusCode)
			{
				var path = _nav.ToBaseRelativePath(_nav.Uri).ToLowerInvariant();

				if (!path.StartsWith("auth") && !path.StartsWith("login"))
				{
					_nav.NavigateTo("/auth", forceLoad: true);
				}

				return null;
			}

			return await retryRequest();
		}*/

/*        private async Task<HttpResponseMessage?> HandleUnauthorizedAsync(Func<Task<HttpResponseMessage>> retryRequest, HttpResponseMessage response)
		{
			if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
				return response;

			var refreshRequest = new HttpRequestMessage(HttpMethod.Post, "/api/auth/refresh");
			refreshRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

			var refreshResponse = await _http.SendAsync(refreshRequest);

			if (!refreshResponse.IsSuccessStatusCode)
			{
				_nav.NavigateTo("/auth", forceLoad: true);
				return null;
			}

			return await retryRequest();
		}*/














//experimental

/*using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;

namespace Tazzker.Client.Services;

public class SyncService
{
	private readonly HttpClient _http;
	private readonly IndexedDbBridge _db;
	private readonly NavigationManager _nav;
	private readonly IJSRuntime _js;

	public SyncService(HttpClient http, IndexedDbBridge db, NavigationManager nav, IJSRuntime js)
	{
		_http = http;
		_db = db;
		_nav = nav;
		_js = js;
	}

	public event Func<Task>? OnSynced;

	private async Task NotifySynced()
	{
		if (OnSynced != null) await OnSynced.Invoke();
	}

	public async Task SyncAllAsync()
	{
		await ExecuteWithRefresh(async () =>
		{
			await SyncEntity<ListModel>("Lists", "/api/lists/syncLists");
			await SyncEntity<SublistModel>("Sublists", "/api/sublists/syncSublists");
			await SyncEntity<TaskModel>("Tasks", "/api/tasks/syncTasks");
			await SyncEntity<NoteModel>("Notes", "/api/notes/syncNotes");
		});
		Console.WriteLine("✅ Sync completed");
		await NotifySynced();
	}

	public async Task PullAllAsync()
	{
		await ExecuteWithRefresh(async () =>
		{
			await PullEntity<ListModel>("Lists", "/api/lists/getLists");
			await PullEntity<SublistModel>("Sublists", "/api/sublists/getSublists");
			await PullEntity<TaskModel>("Tasks", "/api/tasks/getTasks");
			await PullEntity<NoteModel>("Notes", "/api/notes/getNotes");
		});
		Console.WriteLine("📥 Pull completed");
		await NotifySynced();
	}

	public async Task SyncHardDeletedAsync()
	{
		await ExecuteWithRefresh(async () =>
		{
			await SyncDeleted<ListModel>("Lists", "/api/lists/clearTrashedLists");
			await SyncDeleted<SublistModel>("Sublists", "/api/sublists/clearTrashedSublists");
			await SyncDeleted<TaskModel>("Tasks", "/api/tasks/clearTrashedTasks");
			await SyncDeleted<NoteModel>("Notes", "/api/notes/clearTrashedNotes");
		});
		Console.WriteLine("🗑 Hard delete sync complete");
		await NotifySynced();
	}

	private async Task ExecuteWithRefresh(Func<Task> operation)
	{
		try
		{
			await operation();
		}
		catch (UnauthorizedAccessException)
		{
			Console.WriteLine("🔒 Unauthorized. Trying token refresh...");

			if (await TryRefreshToken())
			{
				Console.WriteLine("🔁 Retrying after token refresh...");
				await operation();
			}
			else
			{
				Console.WriteLine("⛔ Token refresh failed. Logging out.");
				await ForceLogout();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"❌ Sync error: {ex.Message}");
		}
	}

	private async Task<HttpResponseMessage?> HandleUnauthorizedAsync(Func<Task<HttpResponseMessage>> retry, HttpResponseMessage response)
	{
		if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
			return response;

		if (!await TryRefreshToken())
		{
			await ForceLogout();
			return null;
		}

		var retryResponse = await retry();
		if (!retryResponse.IsSuccessStatusCode)
		{
			await ForceLogout();
			return null;
		}

		return retryResponse;
	}

	private async Task<bool> TryRefreshToken()
	{
		try
		{
			var request = new HttpRequestMessage(HttpMethod.Post, "/api/auth/refresh");
			request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

			var response = await _http.SendAsync(request);
			return response.IsSuccessStatusCode;
		}
		catch
		{
			return false;
		}
	}

	private async Task ForceLogout()
	{
		await _js.InvokeVoidAsync("localStorage.setItem", "isLoggedIn", "false");
		await _js.InvokeVoidAsync("localStorage.removeItem", "currentUserId");
		_nav.NavigateTo("/auth", forceLoad: true);
	}

	private async Task SyncEntity<T>(string store, string endpoint) where T : class, ISyncable
	{
		var items = await _db.GetAllAsync<T>(store);
		var unsynced = items.Where(x => !x.IsSynced && !x.IsDeleted).ToList();
		if (unsynced.Count == 0) return;

		var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
		{
			Content = JsonContent.Create(unsynced)
		};
		request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

		var response = await _http.SendAsync(request);
		response = await HandleUnauthorizedAsync(() => _http.SendAsync(request), response);
		if (response == null || !response.IsSuccessStatusCode) throw new UnauthorizedAccessException();

		foreach (var item in unsynced)
		{
			item.IsSynced = true;
			await _db.AddAsync(store, item);
		}
	}

	private async Task PullEntity<T>(string store, string endpoint) where T : class, ISyncable
	{
		var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
		request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

		var response = await _http.SendAsync(request);
		response = await HandleUnauthorizedAsync(() => _http.SendAsync(request), response);
		if (response == null || !response.IsSuccessStatusCode) throw new UnauthorizedAccessException();

		var items = await response.Content.ReadFromJsonAsync<List<T>>();
		if (items == null) return;

		await _db.ClearAsync(store);
		foreach (var item in items)
		{
			item.IsSynced = true;
			await _db.AddAsync(store, item);
		}
	}

	private async Task SyncDeleted<T>(string store, string endpoint) where T : class, ISyncable
	{
		var items = await _db.GetAllAsync<T>(store);
		var toDelete = items.Where(x => x.IsDeleted && x.PermDeleted).Select(x => x.Id.ToString()).ToList();
		if (toDelete.Count == 0) return;

		var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
		{
			Content = JsonContent.Create(toDelete)
		};
		request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

		var response = await _http.SendAsync(request);
		response = await HandleUnauthorizedAsync(() => _http.SendAsync(request), response);
		if (response == null || !response.IsSuccessStatusCode) throw new UnauthorizedAccessException();

		foreach (var id in toDelete)
		{
			await _db.DeleteAsync(store, id);
		}
	}

	public async Task ClearAllAsync()
	{
		await _db.ClearAsync("Lists");
		await _db.ClearAsync("Sublists");
		await _db.ClearAsync("Tasks");
		await _db.ClearAsync("Notes");
	}

	public async Task<bool> IsAuthenticatedAsync()
	{
		try
		{
			var request = new HttpRequestMessage(HttpMethod.Get, "/api/auth/userid");
			request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
			var response = await _http.SendAsync(request);
			return response.IsSuccessStatusCode;
		}
		catch
		{
			return false;
		}
	}
}
*/





using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;
using Microsoft.JSInterop;

namespace Tazzker.Client.Services
{
	public class SyncService
	{
		private readonly HttpClient _http;
		private readonly IndexedDbBridge _db;
		private readonly NavigationManager _nav;
		private readonly IJSRuntime JS;

		public SyncService(HttpClient http, IndexedDbBridge db, NavigationManager nav, IJSRuntime js)
		{
			_http = http;
			_db = db;
			_nav = nav;
			JS = js;
		}

		public event Func<Task>? OnSynced;

		public async Task NotifySynced()
		{
			if (OnSynced != null) await OnSynced.Invoke();
		}

		public async Task TrySyncEverythingAsync()
		{
			await SafeRequest(async () =>
			{
				await SyncAllAsync();
				await SyncHardDeletedAsync();
				await PullAllAsync();
			});
		}

        private async Task SafeRequest(Func<Task> operation, bool isRetry = false)
        {
            try
            {
                await operation();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("🔒 Неавторизован. Пробуем refresh...");

                bool refreshed = await TryRefreshToken();
                if (refreshed && !isRetry)
                {
                    Console.WriteLine("✅ Refresh успешен. Повтор запроса...");
                    await SafeRequest(operation, isRetry: true); // <-- рекурсивно повторяем
                }
                else
                {
                    Console.WriteLine("⛔ Refresh провалился. Уходим на /auth");
                    await JS.InvokeVoidAsync("localStorage.setItem", "isLoggedIn", "false");
                    await JS.InvokeVoidAsync("localStorage.removeItem", "currentUserId");
                    _nav.NavigateTo("/auth", forceLoad: true);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ Ошибка запроса: {ex.Message}");
            }
        }



        //last good safe request

        /*		private async Task SafeRequest(Func<Task> operation)
                {
                    try
                    {
                        await operation();
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"❌ Ошибка запроса: {ex.Message}");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("🔒 Неавторизован. Пробуем refresh...");

                        bool refreshed = await TryRefreshToken();
                        if (refreshed)
                        {
                            Console.WriteLine("✅ Refresh успешен. Повтор запроса...");
                            await operation(); // retry once
                        }
                        else
                        {
                            Console.WriteLine("⛔ Refresh провалился. Уходим на /auth");
                            await JS.InvokeVoidAsync("localStorage.setItem", "isLoggedIn", "false");
                            await JS.InvokeVoidAsync("localStorage.removeItem", "currentUserId");
                            _nav.NavigateTo("/auth", forceLoad: true);
                        }
                    }
                }*/

        private async Task<bool> TryRefreshToken()
		{
			try
			{
				var refreshRequest = new HttpRequestMessage(HttpMethod.Post, "/api/auth/refresh");
				refreshRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
				var response = await _http.SendAsync(refreshRequest);
				return response.IsSuccessStatusCode;
			}
			catch
			{
				return false;
			}
		}

		public async Task SyncAllAsync()
		{
			await SafeRequest(async () =>
			{
				await SyncEntity<ListModel>("Lists", "/api/lists/syncLists");
				await SyncEntity<SublistModel>("Sublists", "/api/sublists/syncSublists");
				await SyncEntity<TaskModel>("Tasks", "/api/tasks/syncTasks");
				await SyncEntity<NoteModel>("Notes", "/api/notes/syncNotes");
				Console.WriteLine("Call sync");
				await NotifySynced();
			});
		}

		public async Task PullAllAsync()
		{
			await SafeRequest(async () =>
			{
				await PullEntity<ListModel>("Lists", "/api/lists/getLists");
				await PullEntity<SublistModel>("Sublists", "/api/sublists/getSublists");
				await PullEntity<TaskModel>("Tasks", "/api/tasks/getTasks");
				await PullEntity<NoteModel>("Notes", "/api/notes/getNotes");
				Console.WriteLine("Call pull");
				await NotifySynced();
			});
		}

		public async Task SyncHardDeletedAsync()
		{
			await SafeRequest(async () =>
			{
				await SyncPermDeleted<ListModel>("Lists", "/api/lists/clearTrashedLists");
				await SyncPermDeleted<SublistModel>("Sublists", "/api/sublists/clearTrashedSublists");
				await SyncPermDeleted<TaskModel>("Tasks", "/api/tasks/clearTrashedTasks");
				await SyncPermDeleted<NoteModel>("Notes", "/api/notes/clearTrashedNotes");
				Console.WriteLine("Call delete");
				await NotifySynced();
			});
		}

		private async Task SyncEntity<T>(string store, string endpoint) where T : class, ISyncable
		{
			var items = await _db.GetAllAsync<T>(store);
			var unsynced = items.Where(x => !x.IsSynced /*&& !x.IsDeleted*/ && !x.PermDeleted).ToList();
			if (unsynced.Count == 0) return;

			var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
			{
				Content = JsonContent.Create(unsynced)
			};
			request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

			var response = await _http.SendAsync(request);
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				throw new UnauthorizedAccessException();

			if (!response.IsSuccessStatusCode) return;

			foreach (var item in unsynced)
			{
				item.IsSynced = true;
				await _db.AddAsync(store, item);
			}
		}

		private async Task PullEntity<T>(string store, string endpoint) where T : class, ISyncable
		{
			var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
			request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

			var response = await _http.SendAsync(request);
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				throw new UnauthorizedAccessException();

			if (!response.IsSuccessStatusCode) return;

			var items = await response.Content.ReadFromJsonAsync<List<T>>();
			if (items == null) return;

			await _db.ClearAsync(store);
			foreach (var item in items)
			{
				item.IsSynced = true;
				await _db.AddAsync(store, item);
			}
		}

		private async Task SyncPermDeleted<T>(string store, string endpoint) where T : class, ISyncable
		{
			var items = await _db.GetAllAsync<T>(store);
			var toDelete = items.Where(x => /*x.IsDeleted &&*/ x.PermDeleted).Select(x => x.Id.ToString()).ToList();
			if (toDelete.Count == 0) return;

			var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
			{
				Content = JsonContent.Create(toDelete)
			};
			request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

			var response = await _http.SendAsync(request);
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				throw new UnauthorizedAccessException();

			if (!response.IsSuccessStatusCode) return;

			foreach (var id in toDelete)
			{
				await _db.DeleteAsync(store, id);
			}
		}

		public async Task ClearAllAsync()
		{
			await _db.ClearAsync("Lists");
			await _db.ClearAsync("Sublists");
			await _db.ClearAsync("Tasks");
			await _db.ClearAsync("Notes");
		}

		public async Task<bool> IsAuthenticatedAsync()
		{
			try
			{
				var request = new HttpRequestMessage(HttpMethod.Get, "/api/auth/userid");
				request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
				var response = await _http.SendAsync(request);
				return response.IsSuccessStatusCode;
			}
			catch
			{
				return false;
			}
		}
	}
}
