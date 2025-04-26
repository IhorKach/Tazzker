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
using System.Net.Http.Json;
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

