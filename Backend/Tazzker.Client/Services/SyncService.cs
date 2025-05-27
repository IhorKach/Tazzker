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
                Console.WriteLine("Not authorized. Trying refresh...");

                bool refreshed = await TryRefreshToken();
                if (refreshed && !isRetry)
                {
                    Console.WriteLine("Refresh Successful. repeating request...");
                    await SafeRequest(operation, isRetry: true); // recursionally repeating
                }
                else
                {
                    Console.WriteLine("Refresh failed. heading to /auth");
                    await JS.InvokeVoidAsync("localStorage.setItem", "isLoggedIn", "false");
                    await JS.InvokeVoidAsync("localStorage.removeItem", "currentUserId");
                    _nav.NavigateTo("/auth", forceLoad: true);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error request: {ex.Message}");
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
			var unsynced = items.Where(x => !x.IsSynced && !x.PermDeleted).ToList();
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
			var toDelete = items.Where(x => x.PermDeleted).Select(x => x.Id.ToString()).ToList();
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
