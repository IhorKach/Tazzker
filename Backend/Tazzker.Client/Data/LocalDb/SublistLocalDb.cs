using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;
using Tazzker.Client.Services;

namespace Tazzker.Client.Data.LocalDb
{
	public class SublistLocalDb : ILocalDb<SublistModel>
	{
		private readonly IndexedDbBridge _db;
		private const string StoreName = "Sublists";

		public SublistLocalDb(IndexedDbBridge db)
		{
			_db = db;
		}

		public async Task<List<SublistModel>> GetAllAsync()
		{
			var all = await _db.GetAllAsync<SublistModel>(StoreName);
			return all.Where(x => !x.PermDeleted).OrderBy(x => x.Order).ToList();
		}

		public async Task<SublistModel?> GetByIdAsync(Guid id)
		{
			var all = await GetAllAsync();
			return all.FirstOrDefault(x => x.Id == id);
		}

		public async Task AddOrUpdateAsync(SublistModel item)
		{
			item.UpdatedAt = DateTime.UtcNow;
			item.IsSynced = false;
			await _db.AddAsync(StoreName, item);
		}

		public async Task SoftDeleteAsync(Guid id)
		{
			var sublist = await GetByIdAsync(id);
			if (sublist is null) return;

			sublist.PermDeleted = true;

			await AddOrUpdateAsync(sublist);
		}

        public async Task<List<SublistModel>> GetAllRawAsync()
        {
            return await _db.GetAllAsync<SublistModel>(StoreName);
        }

    }
}
