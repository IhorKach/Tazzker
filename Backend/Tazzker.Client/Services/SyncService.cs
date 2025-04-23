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
