using Microsoft.JSInterop;

namespace Tazzker.Client.Services
{

    public class IndexedDbBridge
    {
        private readonly IJSRuntime _js;

        private readonly string _dbName = "TazzkerDb";
        private readonly int _version = 1;
        private readonly string[] _stores = new[] { "Tasks", "Notes", "Lists", "Sublists" };

        public IndexedDbBridge(IJSRuntime js)
        {
            _js = js;
        }

        public async Task InitAsync()
        {
            await _js.InvokeVoidAsync("indexedDbBridge.openDb", _dbName, _version, _stores);
        }

        public async Task AddAsync<T>(string store, T data)
        {
            await _js.InvokeVoidAsync("indexedDbBridge.addRecord", _dbName, store, data);
        }

        public async Task<List<T>> GetAllAsync<T>(string store)
        {
            return await _js.InvokeAsync<List<T>>("indexedDbBridge.getAll", _dbName, store);
        }

        public async Task DeleteAsync(string store, string id)
        {
            await _js.InvokeVoidAsync("indexedDbBridge.deleteRecord", _dbName, store, id);
        }

        public async Task ClearAsync(string store)
        {
            await _js.InvokeVoidAsync("indexedDbBridge.clearStore", _dbName, store);
        }

        public async Task<bool> HasAnyStoresAsync()
        {
            return await _js.InvokeAsync<bool>("checkIndexedDbStores", _dbName);
        }
    }

}
