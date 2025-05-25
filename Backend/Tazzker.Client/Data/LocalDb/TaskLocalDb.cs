using Tazzker.Client.Interfaces;
using Tazzker.Client.Services;
using Tazzker.Client.Data.Models;

namespace Tazzker.Client.Data.LocalDb
{
    public class TaskLocalDb : ILocalDb<TaskModel>
    {
        private readonly IndexedDbBridge _db;
        private const string StoreName = "Tasks";

        public TaskLocalDb(IndexedDbBridge db)
        {
            _db = db;
        }

        public async Task<List<TaskModel>> GetAllAsync()
        {
            var all = await _db.GetAllAsync<TaskModel>(StoreName);
            return all.Where(x => !x.PermDeleted/*IsDeleted*/).OrderBy(x => x.Order).ToList();
        }

        public async Task<TaskModel?> GetByIdAsync(Guid id)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(x => x.Id == id);
        }

        public async Task AddOrUpdateAsync(TaskModel item)
        {
            item.UpdatedAt = DateTime.UtcNow;
            item.IsSynced = false;
			await _db.AddAsync(StoreName, item);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var task = await GetByIdAsync(id);
            if (task is null) return;
            task.PermDeleted = true;
            //task.IsDeleted = true;

            await AddOrUpdateAsync(task);
        }

        public async Task<List<TaskModel>> GetAllRawAsync()
        {
            return await _db.GetAllAsync<TaskModel>(StoreName);
        }
    }
}
