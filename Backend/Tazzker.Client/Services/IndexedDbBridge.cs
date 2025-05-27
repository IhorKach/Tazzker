using Microsoft.JSInterop;

namespace Tazzker.Client.Services
{

    public class IndexedDbBridge
    {
        private readonly IJSRuntime _js;

        private readonly string _dbName = "TazzkerDb";
        private readonly int _version = 2;
        private readonly string[] _stores = new[] { "Tasks", "Notes", "Lists", "Sublists" };

        public IndexedDbBridge(IJSRuntime js)
        {
            _js = js;
        }


        private bool _initialized = false;

        public async Task EnsureInitializedAsync()
        {
            if (_initialized) return;

            var hasStores = await HasAnyStoresAsync();
            if (!hasStores)
            {
                await InitAsync();
            }

            _initialized = true;
        }

        public async Task InitAsync()
        {
            await _js.InvokeVoidAsync("indexedDbBridge.openDb", _dbName, _version, _stores);
        }

        public async Task AddAsync<T>(string store, T data)
        {

            await EnsureInitializedAsync();
            await _js.InvokeVoidAsync("indexedDbBridge.addRecord", _dbName, store, data);
        }

        public async Task<List<T>> GetAllAsync<T>(string store)
        {
            await EnsureInitializedAsync();
            return await _js.InvokeAsync<List<T>>("indexedDbBridge.getAll", _dbName, store);
        }

        public async Task DeleteAsync(string store, string id)
        {
            await EnsureInitializedAsync();
            await _js.InvokeVoidAsync("indexedDbBridge.deleteRecord", _dbName, store, id);
        }

        public async Task ClearAsync(string store)
        {
            await EnsureInitializedAsync();
            await _js.InvokeVoidAsync("indexedDbBridge.clearStore", _dbName, store);
        }

        public async Task<bool> HasAnyStoresAsync()
        {
            return await _js.InvokeAsync<bool>("checkIndexedDbStores", _dbName);
        }
    }

}
