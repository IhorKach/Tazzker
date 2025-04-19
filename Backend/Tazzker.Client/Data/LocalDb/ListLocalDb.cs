using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;
using Tazzker.Client.Services;

namespace Tazzker.Client.Data.LocalDb
{
    public class ListLocalDb : ILocalDb<ListModel>
    {
        private readonly IndexedDbBridge _db;
        private const string StoreName = "Lists";

        public ListLocalDb(IndexedDbBridge db)
        {
            _db = db;
        }

        public async Task<List<ListModel>> GetAllAsync()
        {
            var all = await _db.GetAllAsync<ListModel>(StoreName);
            return all.Where(x=>!x.IsDeleted).OrderByDescending(x=>x.UpdatedAt).ToList();
        }
        
        
        public async Task<ListModel?> GetByIdAsync(string id)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(x=>x.Id == id);
        }



        public async Task AddOrUpdateAsync(ListModel item)
        {
            item.UpdatedAt = DateTime.UtcNow;
            await _db.AddAsync(StoreName, item);
        }


        public async Task SoftDeleteAsync(string id)
        {
            var list = await GetByIdAsync(id);
            if (list is null)
                return;
            list.IsDeleted = true;
            list.IsSynced = false;
            list.UpdatedAt = DateTime.UtcNow;

            await AddOrUpdateAsync(list);
        }
    }
}
